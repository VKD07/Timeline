using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] clips;

    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (AudioClip clip in clips)
            {
                if (clip != null && !clipDictionary.ContainsKey(clip.name))
                    clipDictionary.Add(clip.name, clip);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string clipName)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
            sfxSource.PlayOneShot(clip);
        else
            Debug.LogWarning("Audio clip not found: " + clipName);
    }

    public void PlayMusic(string clipName)
    {
        if (clipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
            Debug.LogWarning("Music clip not found: " + clipName);
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
}
