using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioCategory[] categories;

    public void Play(string category, string audio, AudioType type)
    {
        for (int i = 0; i < categories.Length; i++)
        {
            if (category == categories[i].name)
            {
                for (int j = 0; j < categories[i].audios.Length; j++)
                {
                    if (categories[i].audios[j].name == audio)
                    {
                        switch (type)
                        {
                            case AudioType.Ambience:
                                break;
                            case AudioType.Music:
                                musicSource.clip = categories[i].audios[j].RandomClip;
                                musicSource.loop = true;
                                musicSource.Play();
                                break;
                            case AudioType.SFX:
                                sfxSource.PlayOneShot(categories[i].audios[j].RandomClip);
                                break;
                            case AudioType.Voice:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}

public enum AudioType
{
    Ambience,
    Music,
    SFX,
    Voice
}

[System.Serializable]
public class AudioCategory
{
    public string name;
    public PlayableAudio[] audios;
}

[System.Serializable]
public class PlayableAudio
{
    public string name;
    public AudioClip[] clips;
    [Range(0, 1f)]
    public float volume;
    [Range(0, 2f)]
    public float pitch;

    public AudioClip RandomClip {
        get {
            return clips[Random.Range(0, clips.Length)];
        }
    }
}
