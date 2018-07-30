using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class AudioManager : Service
{
    public List<AudioClip> sfxClips;
    public List<AudioClip> bgmClips;

    private Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> bgmDictionary = new Dictionary<string, AudioClip>();

    public const int POOL_SIZE = 3;
    private AudioSource[] sources = new AudioSource[POOL_SIZE];

    private List<AudioSource> dynamicSources = new List<AudioSource>();

    protected override void Awake()
    {
        base.Awake();
        foreach(AudioClip clip in sfxClips)
        {
            sfxDictionary.Add(clip.name, clip);
        }

        foreach (AudioClip clip in bgmClips)
        {
            bgmDictionary.Add(clip.name, clip);
        }
    }

    // Use this for initialization
    void Start ()
    {

        for (int ii = 0; ii < POOL_SIZE; ii++)
        {
            GameObject src = new GameObject("AudioSource_" + ii);
            sources[ii] = src.AddComponent<AudioSource>();
            sources[ii].loop = false;
            sources[ii].playOnAwake = false;
        }

        StartCoroutine(CleanupCoroutine(3));

        EventManager em = ServiceLocator.GetService<EventManager>();
        em.AddListener(ConstManager.EVENT_PLAYER_FIRE, PlayPlayerFire);
        em.AddListener(ConstManager.EVENT_PLAYER_SGFIRE, PlayPlayerSGFire);
        em.AddListener(ConstManager.EVENT_PLAYER_DEATH, PlayDeath);
        em.AddListener(ConstManager.EVENT_PLAY_BGM, PlayBGM);
        em.AddListener(ConstManager.EVENT_PLAY_BGM, PlayBGM);



    }

    public void PlaySFX(string name)
    {
        AudioClip clip = null;
        if(sfxDictionary.TryGetValue(name, out clip))
        {
            if(clip != null)
            {
                PlayAudioDynamically(clip);
            }

            // Find valid source and play clip.
            //AudioSource src = FindValidSource(clip);
            //if(src != null)
            //{
            //    src.clip = clip;
            //    src.Play();
            //}
        }
    }

    public void PlayPlayerFire()
    {
        PlaySFX(ConstManager.SFX_PLAYER_FIRE);
    }

    public void PlayPlayerSGFire()
    {
        PlaySFX(ConstManager.SFX_PLAYER_SGFIRE);
    }

    public void PlayEnemyHit()
    {
        PlaySFX(ConstManager.SFX_ENEMY_HIT);
    }

    public void PlayBGM()
    {
        PlaySFX(ConstManager.BGM_MAIN);
    }

    public void PlayDeath()
    {
        PlaySFX(ConstManager.SFX_PLAYER_DEATH);
    }


    private AudioSource FindValidSource(AudioClip clip = null)
    {
        AudioSource source = null;

        for(int ii = 0; ii < POOL_SIZE; ii++)
        {
            if(sources[ii].isPlaying)
            {
                source = sources[ii];
                break;
            }
        }

        if(source == null)
        {
            // Find based on audio clip
            if(clip != null)
            {
                for(int ii = 0; ii < POOL_SIZE; ii++)
                {
                    if(clip == sources[ii].clip)
                    {
                        source = sources[ii];
                        break;
                    }
                }
            }
            else
            {
                // Find based on length remaining
                float baseline = 0.5f;
                for (int ii = 0; ii < POOL_SIZE; ii++)
                {
                    float progress = Mathf.Clamp01(sources[ii].time / sources[ii].clip.length);
                    if (progress > baseline)
                    {
                        baseline = progress;
                        source = sources[ii];
                    }
                }
            }
        }

        return source;
    }

    private void PlayAudioDynamically(AudioClip clip)
    {
        bool succeded = false;

        foreach(AudioSource source in dynamicSources)
        {
            // First look for non-playing sources with the same clip
            if(!source.isPlaying && (clip != null && clip == source.clip))
            {
                succeded = true;
                source.Play();
                break;
            }
        }

        if(!succeded)
        {
            // Then look for non-playing sources regardless of clip
            foreach (AudioSource source in dynamicSources)
            {
                if (!source.isPlaying)
                {
                    source.clip = clip;
                    source.Play();
                    break;
                }
            }
        }

        if (!succeded)
        {
            // Generate new source and play the clip
            GameObject clone = new GameObject("AudioSource_" + clip.name);
            clone.transform.parent = transform;
            AudioSource src = clone.AddComponent<AudioSource>();
            src.clip = clip;
            src.loop = false;
            dynamicSources.Add(src);
            src.Play();
            
        }
            
    }

    public IEnumerator CleanupCoroutine(int seconds)
    {
        while(true)
        {
            List<AudioSource> invalidSrcs = new List<AudioSource>();
            for(int i = 0; i < dynamicSources.Count; i++)
            { 
                AudioSource src = dynamicSources[i];
                if (!src.isPlaying)
                {
                    invalidSrcs.Add(src);
                }
            }
            foreach(AudioSource ind in invalidSrcs)
            {
                dynamicSources.Remove(ind);
                Destroy(ind.gameObject);
            }
            invalidSrcs.Clear();
            yield return new WaitForSeconds(seconds);
        }
    }

}
