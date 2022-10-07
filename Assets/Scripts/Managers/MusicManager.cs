using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class MusicTrack {
  public AudioClip music;
  [HideInInspector]
  public AudioSource source;
  [HideInInspector]
  public float volume = 0;
}
public class MusicManager : MonoBehaviour {
  public List<MusicTrack> tracks;
  public GameObject audioSourcePrefab;
  // Start is called before the first frame update
  void Start() {
    for (int i = 0; i < tracks.Count; i++) {
      AudioSource audioSource = Instantiate(audioSourcePrefab).GetComponent<AudioSource>();
      audioSource.clip = tracks[i].music;
      audioSource.volume = 0;
      audioSource.transform.parent = this.transform;
      audioSource.loop = true;
      audioSource.Play();
      tracks[i].source = audioSource;
    }
  }

  // Update is called once per frame
  void Update() {
  }
  public void StopAll() {
    for (int i = 0; i < tracks.Count; i++) {
      tracks[i].source.Stop();
    }
  }
}
