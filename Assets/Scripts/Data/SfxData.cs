using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SfxData")]
  

public class SfxData : ScriptableObject
{
    public Sound[] sounds;
    public bool initialized = false;

    public AudioClip GetClip(SoundType type) {
        foreach (Sound s in sounds) {
            if (s.type == type) {
                return s.sound;
            }
        }
        return null;
    }
}


