using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public enum SoundType {
  NONE,
  POT_PLACED,
  PLANT_GROW,
  BUD_ATTACK,
  LEAF_ATTACK,
  FLOWER_ATTACK,
  PUMPKIN_HIT,
  ENEMY_HIT,
  ENEMY_DEATH,
  BUTTON_PRESS,
  PLANT_WATERED,
  PLANT_FROZEN,
  GAME_OVER,
  GAME_WON, 
  THEME
}

[System.Serializable]
public class Sound {
  public AudioClip sound;
  public SoundType type;
  
}
public static class Bus {
  public const string SOUND = "Sound";
  public const string MUSIC = "Music";
}
public class SoundPlayer {
  public AudioSource player;
  public bool inUse = false;
  public string bus;
  public float volume;
  public SoundPlayer(AudioSource player) {
    this.player = player;
  }
}
public class SfxManager : MonoBehaviour {
  public static SfxManager instance; 
  public float volumeMultiplier;
  public float currentVolumeMultiplier;
  public SfxData sfxData;
  private List<SoundPlayer> players = new List<SoundPlayer>();
  private List<SoundPlayer> musicPlayers = new List<SoundPlayer>();

  private List<int> currentlyLoopingSounds = new List<int>();
  private List<SoundType> currentlyPlayingSounds = new List<SoundType>();
  private bool muted = false;
  // Start is called before the first frame update
  void Awake() {
    instance = this;

    CreateAudioSources(100);

    this.currentVolumeMultiplier = volumeMultiplier;

    SceneManager.sceneLoaded += OnSceneLoad;
  }

  private void OnSceneLoad(Scene next, LoadSceneMode mode) {
    StopAllSounds();
  }

  public void Update() {
    // input manager used here
    /*if (InputManager.input.Mute.WasPressed) {
      this.muted = !muted;
    }*/
    foreach (SoundPlayer player in players) {
      if (player.inUse) {
        player.player.volume = player.volume * SaveDataManager.instance.GetData().busVolume[player.bus] * currentVolumeMultiplier;
      }
    }
  }

  public static int PlaySoundStatic(SoundType soundType, float volume = 1, bool looping = false) {
    return SfxManager.instance.PlaySound(soundType, volume, looping);
  }

  public int PlaySound(SoundType soundType, float volume = 1, bool looping = false, string bus = Bus.SOUND) {

    var computedVolume = this.currentVolumeMultiplier * volume * SaveDataManager.instance.GetData().busVolume[bus];
    if (soundType == SoundType.NONE) return -1;
    if (!looping) {
      var sameSoundsPlaying = 0;
      for (int i = 0; i < this.currentlyPlayingSounds.Count; i++) {
        if (this.currentlyPlayingSounds[i] == soundType) sameSoundsPlaying++;
      }
      if (sameSoundsPlaying > 3) return -1;
    }
    var playerIndex = GetAvailablePlayer();
    var player = this.players[playerIndex];
    player.bus = bus;
    if (player == null) return -1;
    if (looping) {
      player.player.clip = this.sfxData.GetClip(soundType);
      player.player.loop = true;
      player.volume = computedVolume;
      player.player.Play();
      currentlyLoopingSounds.Add(playerIndex);
    }
    else {

      this.currentlyPlayingSounds.Add(soundType);

      player.volume = 1;
      player.player.PlayOneShot(this.sfxData.GetClip(soundType), computedVolume);
      StartCoroutine(FreeUpSourceAfterSoundEnds(player, this.sfxData.GetClip(soundType), soundType));
    }

    return playerIndex;

  }

  /**
  If playerToken provided is -1, it will return the first player in the pool playing that sound.
   */
  public void StopLoopingSound(SoundType soundType, int playerToken) {
    if (playerToken > this.players.Count - 1) {
      return;
    }
    if (playerToken == -1) {
      for (int i = 0; i < players.Count; i++) {
        if (players[i].player.clip == sfxData.GetClip(soundType)) {
          playerToken = i;
        }
      }
      if (playerToken == -1) return;
    }
    if (this.players[playerToken] != null && currentlyLoopingSounds.IndexOf(playerToken) >= 0) {
      this.currentlyLoopingSounds.Remove(playerToken);
      this.players[playerToken].player.Stop();
      this.players[playerToken].inUse = false;
    }
    else {
      //Debug.LogWarning("StopLoopingSound failed; player not found for that playerIndex");
    }
  }

  public void StopAllSounds() {
    for (int i = 0; i < this.players.Count; i++) {
      if (players[i].player) {
        players[i].player.Stop();
        players[i].player.volume = 0;
      }
    }
  }

  private int GetAvailablePlayer() {
    for (int i = 0; i < this.players.Count; i++) {
      if (!this.players[i].inUse) {
        this.players[i].inUse = true;
        return i;
      }
    }
    Debug.Log("ran out of audio sources, creating more");
    CreateAudioSources(20);
    return GetAvailablePlayer();
  }

  private void CreateAudioSources(int sources) {
    for (int i = 0; i < sources; i++) {
      var player = new GameObject();
      var soundPlayer = new SoundPlayer(player.AddComponent<AudioSource>());
      this.players.Add(soundPlayer);
      soundPlayer.player.playOnAwake = false;
      player.transform.parent = transform;
    }
  }

  private IEnumerator FreeUpSourceAfterSoundEnds(SoundPlayer player, AudioClip sound, SoundType soundType) {
    yield return new WaitForSeconds(sound.length);
    this.currentlyPlayingSounds.Remove(soundType);
    player.inUse = false;
  }
}
