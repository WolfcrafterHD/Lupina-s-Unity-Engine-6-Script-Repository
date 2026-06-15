/*
 * Lupinus
 * 13.06.2026
 * This Script (or an Instance of it) is what you Reference to call upon the Audio Functions.
 */
using System.Collections.Generic;
using SaintsField;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSocket : MonoBehaviour
{
    /***Attributes***/
    [SerializeField]
    private List<Audio> _selectedAudio;
    
    [SerializeField]
    private List<BlendSnapshots> _blendSnapshots;
    
    [SerializeField, NoLabel]
    private List<TransitionToSnapshot> _transitionToSnapshots;
    
    public Dictionary<string, Audio> Audios => ReturnAudiosDictionary(); 
    
    public Dictionary<string, TransitionToSnapshot> SnapshotTransitions => ReturnSnapshotTransitionsDictionary();
    
    public Dictionary<string, BlendSnapshots> BlendSnapshots => ReturnBlendSnapshotsDictionary();
    
    private Dictionary<string, AudioMixerGroupData> _audioMixerGroups; 
    
    /***Entry***/
    void Start()
    { 
        _audioMixerGroups = AudioManager.INSTANCE.AudioMixerGroupDataDictionary;
    }
    
    /***Public***/
    public void ClearCache(AudioCache pAudioCache)
    {
        AudioManager.INSTANCE.ClearCache(pAudioCache);
    }

    public void LetLoopPlayAndEnd(AudioCache pCategory)
    {
        AudioManager.INSTANCE.LetLoopPlayAndEnd(pCategory);
    }
    
    public void SetAudioMixerGroupVolume(AudioMixerGroup pAudioMixerGroup, float pVolume)
    {
        AudioManager.INSTANCE.GetAudioMixer().SetFloat(_audioMixerGroups[pAudioMixerGroup.name].Volume, pVolume);
    }

    public void SetAudioMixerGroupPitch(AudioMixerGroup pAudioMixerGroup, float pPitch)
    {
        AudioManager.INSTANCE.GetAudioMixer().SetFloat(_audioMixerGroups[pAudioMixerGroup.name].Pitch, pPitch);
    }
    
    /***Private***/
    private Dictionary<string, Audio> ReturnAudiosDictionary()
    {
        Dictionary<string, Audio> audios = new Dictionary<string, Audio>();
        foreach (Audio entry in _selectedAudio)
        {
            entry.SetLocation(this.transform);
            audios.Add(entry.GetSystemName(), entry);
        }
        return audios;
    }

    private Dictionary<string, TransitionToSnapshot> ReturnSnapshotTransitionsDictionary()
    {
        Dictionary<string, TransitionToSnapshot> transitions = new Dictionary<string, TransitionToSnapshot>();
        foreach (TransitionToSnapshot entry in _transitionToSnapshots)
        {
            transitions.Add(entry.GetSystemName(), entry);
        }
        return transitions;
    }

    private Dictionary<string, BlendSnapshots> ReturnBlendSnapshotsDictionary()
    {
        Dictionary<string, BlendSnapshots> blendSnapshots = new Dictionary<string, BlendSnapshots>();
        foreach (BlendSnapshots entry in _blendSnapshots)
        {
            blendSnapshots.Add(entry.GetSystemName(), entry);
        }
        return blendSnapshots;
    }
}
