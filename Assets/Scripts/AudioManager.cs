using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private List<SoundClipPair> soundClips = new List<SoundClipPair>();
    private Dictionary<Sound, SoundClipPair> soundDictionary;
    private AudioSource audioSource;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        soundDictionary = new Dictionary<Sound, SoundClipPair>();

        foreach(var pair in soundClips)
        {
            if (!soundDictionary.ContainsKey(pair.sound))
            {
                soundDictionary.Add(pair.sound, pair);
            }
        }
    }

    public void PlayAudio(Sound sound)
    {
        if(soundDictionary.TryGetValue(sound, out SoundClipPair pair))
        {
            audioSource.PlayOneShot(pair.clip, pair.volume);
        }
    }

    [Serializable]
    private class SoundClipPair
    {
        public Sound sound;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 0.7f;
    }
}
