/*
 * 09.06.2026
 * Lupinus
 * This is the AudioManager, it's a singleton.
 * All audio clips are saved in Scriptable Objects.
 * Any currently playing audios are put in the respective audio caches.
 * If you wish to genuinely use this, you'll need a child class of the AudioSocket.
 * Audio clips which are done playing destroy themselves except for anything running in the music or Ambiente category,
 * these will loop, and only one can be in the cache at any give time, or it will be destroyed.
 */

using System.Collections.Generic;
using SaintsField;
using SaintsField.Playa;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /***Attributes***/
    public static AudioManager INSTANCE { get; private set; }
    
    [SerializeField, FieldLabelText("$" + nameof(AudioLibrariesFieldLabels))]
    private AudioLibrary[] _audioLibraries; public AudioLibrary[] GetAudioLibraries() { return _audioLibraries; }
    private string AudioLibrariesFieldLabels(object _, int index) => AudioMixerGroupDataFieldLabelsCheck(index);
    
    [SerializeField]
    private AudioMixer _mainMixer; public AudioMixer GetAudioMixer() {return _mainMixer;}
    
    [SerializeField, FieldLabelText("$" + nameof(AudioMixerGroupDataFieldLabels))]
    private AudioMixerGroupData[] _audioMixerGroupData; public AudioMixerGroupData[] GetAudioMixerGroupData() { return _audioMixerGroupData; }
    private string AudioMixerGroupDataFieldLabels(object _,int index) => _audioMixerGroupData[index].AudioMixerGroup.name;
    public Dictionary<string, AudioMixerGroupData> AudioMixerGroupDataDictionary => ReturnAudioMixerGroupDataDictionary();

    [LayoutStart("Audio Caches", ELayout.FoldoutBox), ReadOnly, SerializeField]
    private List<GameObject> _sfxCache, _voiceCache, _uiCache, _ambienceCache;
    
    [SerializeField, ReadOnly]
    private GameObject _musicCache;
    [LayoutEnd]
    
    /***Entry***/
    void Awake()
    {
        if (INSTANCE is not null && INSTANCE != this) Destroy(this.gameObject);
        else INSTANCE = this;
    }
    
    /***Public***/
    public void PlayAudio(GameObject pAudioClip, Transform pLocation,Category pCategory)
    {
        GameObject temp1 = Instantiate(pAudioClip, pLocation);
        switch (pCategory)
        {
            case Category.Music:
            case Category.Ambience:
                //no need for the destruct call
                break;
            default:
                Destroy(temp1, temp1.GetComponent<AudioSource>().clip.length);
                break;
        }
        AssigningToCache(temp1, pCategory);
    }

    public void PlayRandomAudio(AudioGroup pAudioGroup, Transform pLocation)
    {
        Category temp2 = pAudioGroup.GetCategory();
        GameObject temp1 = pAudioGroup.GetAudioClips()[Random.Range(0, pAudioGroup.GetAudioClips().Length-1)]; 
        PlayAudio(temp1, pLocation, temp2);
    }

    public void ClearCache(AudioCache pAudioCache)
    {
        switch (pAudioCache)
        {
            case AudioCache.Ambience:
                ClearListCache(_ambienceCache);
                break;
            case AudioCache.SFX:
                ClearListCache(_sfxCache);
                break;
            case AudioCache.Voice:
                ClearListCache(_voiceCache);
                break;
            case AudioCache.UI:
                ClearListCache(_uiCache);
                break;
            case AudioCache.Music:
                Destroy(_musicCache);
                _musicCache = null;
                break;
            case AudioCache.All:
                ClearListCache(_ambienceCache);
                ClearListCache(_sfxCache);
                ClearListCache(_voiceCache);
                ClearListCache(_uiCache);
                Destroy(_musicCache);
                _musicCache = null;
                break;
        }
    }

    public void LetLoopPlayAndEnd(AudioCache pCategory)
    {
        switch (pCategory)
        {
            case AudioCache.Music:
                _musicCache.GetComponent<AudioSource>().loop = false;
                Destroy(_musicCache, _musicCache.GetComponent<AudioSource>().clip.length);
                break;
            case AudioCache.Ambience:
                foreach (GameObject entry in _ambienceCache)
                {
                    entry.GetComponent<AudioSource>().loop = false;
                    Destroy(entry, entry.GetComponent<AudioSource>().clip.length);
                }
                break;
            case AudioCache.All:
                foreach (GameObject entry in _ambienceCache)
                {
                    entry.GetComponent<AudioSource>().loop = false;
                    Destroy(entry, entry.GetComponent<AudioSource>().clip.length);
                }
                _musicCache.GetComponent<AudioSource>().loop = false;
                Destroy(_musicCache, _musicCache.GetComponent<AudioSource>().clip.length);
                break;
            default:
                Debug.LogWarning("<LetLoopsPlayOutAndEnd> only has effect when given the parameters:\n" +
                                 "<AudioCache.Music>\n" +
                                 "<AudioCache.Ambience>\n" +
                                 "<AudioCache.All>");
                break;
        }
    }
    
    /***Private***/
    private string AudioMixerGroupDataFieldLabelsCheck(int index)
    {
        if (_audioLibraries[index] is null) return "No Libraries Assigned";
        return _audioLibraries[index].name;
    }
    
    private Dictionary<string, AudioMixerGroupData> ReturnAudioMixerGroupDataDictionary()
    {
        Dictionary<string, AudioMixerGroupData> audioMixerGroupDataDictionary = new Dictionary<string, AudioMixerGroupData>();
        foreach (AudioMixerGroupData entry in _audioMixerGroupData)
        {
            audioMixerGroupDataDictionary.Add(entry.AudioMixerGroup.name, entry);
        }
        return audioMixerGroupDataDictionary;
    }
    
    private void AssigningToCache(GameObject pAudioClip, Category pCategory)
    {
        switch (pCategory)
        {
            case Category.SFX:
                _sfxCache.Add(pAudioClip);
                break;
            case Category.Voice:
                _voiceCache.Add(pAudioClip);
                break;
            case Category.Music:
                Destroy(_musicCache);
                _musicCache = pAudioClip;
                break;
            case Category.UI:
                _uiCache.Add(pAudioClip);
                break;
            case Category.Ambience:
                _ambienceCache.Add(pAudioClip);
                break;
        }
    }

    private void ClearListCache(List<GameObject> pCache)
    {
        foreach (GameObject cacheEntry in pCache)
        {
            Destroy(cacheEntry);
        }
        pCache.Clear();
    }

    [Button("UPDATE AUDIO SYSTEM")]
    // ReSharper disable once UnusedMember.Local
    private void UpdateAudioManagerSystem()
    {
        UpdateAudioLibraries();
    }
    
    private void UpdateAudioLibraries()
    {
        foreach (AudioLibrary entry in _audioLibraries)
        {
            foreach (AudioGroup entry2 in entry.GetAudioGroups())
            {
                UpdateAudioGroup(entry2);
            }
        }
    }
    
    private void UpdateAudioGroup(AudioGroup pAudioGroup)
    {
        Category tempCategory = pAudioGroup.GetCategory();
        switch (tempCategory)
        {
            case Category.SFX:
                SetBehavior(false, false, tempCategory, pAudioGroup);
                break;
            case Category.Voice:
                SetBehavior(false, false, tempCategory, pAudioGroup);
                break;
            case Category.Ambience:
                SetBehavior(true, false, tempCategory, pAudioGroup);
                break;
            case Category.Music:
                SetBehavior(true, true, tempCategory, pAudioGroup);
                break;
            case Category.UI:
                SetBehavior(false, true, tempCategory, pAudioGroup);
                break;
        }
    }
    
    private void SetBehavior(bool pIsLoop, bool pIs2D, Category pCategory, AudioGroup pAudioGroup)
    {
        AudioMixerGroup tempAudioMixerGroup = null;
        foreach (AudioMixerGroupData entry in _audioMixerGroupData)
        {
            if (entry.AssingToCategory && entry.Category == pCategory)
            {
                tempAudioMixerGroup = entry.AudioMixerGroup;
                break;
            }
        }
        
        foreach (GameObject entry in pAudioGroup.GetAudioClips())
        {
            AudioSource tempAudioSource =  entry.GetComponent<AudioSource>();
            if (pIs2D)
            {
                tempAudioSource.spatialBlend = 0.0f;
            }
            else
            {
               tempAudioSource.spatialBlend += 0.1f;
            }
            tempAudioSource.loop = pIsLoop;
            tempAudioSource.playOnAwake = true;
            tempAudioSource.outputAudioMixerGroup = tempAudioMixerGroup;
        }
    }
    
    /***Editor_Only***/
    #if UNITY_EDITOR
    public static void AssingEditorOnlyInstance()
    {
        if (INSTANCE is not null) return;
        INSTANCE = FindAnyObjectByType<AudioManager>();
        Debug.Log($"{INSTANCE.name} has been forcefully instantiated, due to it Running in the Editor." +
                  $"Should you see this in the Console it should be fine\n" +
                  $"should you see it the Logs of a compiled Game then I fucked up");
    }
    #endif
}