/*
 * 09.06.2026
 * Lupinus
 * In here are all helper classes that are used by the Audio Manager System.
 */

using System.Collections.Generic;
using UnityEngine;
using SaintsField;
using SaintsField.Playa;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
public enum Category
{
    SFX,
    Voice,
    Ambience,
    Music,
    UI
}

public enum AudioCache
{
    SFX,
    Voice,
    UI,
    Ambience,
    Music,
    All
}

[System.Serializable]
public struct SnapshotBlendElement
{
    public AudioMixerSnapshot Snapshot;
    public float Wight;
}

[System.Serializable]
public struct AudioMixerGroupData
{
    public AudioMixerGroup AudioMixerGroup;
    public bool AssingToCategory;
    [ShowIf("AssingToCategory", true)]
    public Category Category;
    public string Volume;
    public string Pitch;
}

[System.Serializable]
public class Audio
{
    /***Attributes***/
    [SerializeField] 
    private string _name;
    
    private Transform _location; public void SetLocation(Transform pTransform) { _location = pTransform; }
    
    [SerializeField, Dropdown(nameof(AudioLibraryDrop))]
    private AudioLibrary _audioLibrary;  public AudioLibrary GetAudio() { return _audioLibrary; }
    
    [HideIf("HasNoAudioLib", true), SerializeField, Dropdown(nameof(AudioGroupDrop))]
    private AudioGroup _audioGroup; public AudioGroup GetAudioGroup() { return _audioGroup; }

    [HideIf("HasNoAudioLib", true), SerializeField]
    private bool _random;
    
    [HideIf("HasNoAudioGroup", true), SerializeField, Dropdown(nameof(AudioClipDrop))]
    private GameObject _audioClip; public GameObject GetAudioClip() { return _audioClip;}
    
    // ReSharper disable once UnusedMember.Local
    private bool HasNoAudioLib => _audioLibrary is null;
    // ReSharper disable once UnusedMember.Local
    private bool HasNoAudioGroup => _audioGroup is null || _random;

    [ReadOnly, SerializeField] 
    private string _systemName; public string GetSystemName() { return _systemName; }
    
    /***Public***/
    public void Play()
    {
        if (_random) AudioManager.INSTANCE.PlayRandomAudio(_audioGroup, _location);
        else AudioManager.INSTANCE.PlayAudio(_audioClip, _location, _audioGroup.GetCategory());
    }
    
    /***Private***/
    [Button("Confirm System Name")]
    private void BuildSystemName()
    {
        _systemName = "";
        if (_random) _systemName += "Random";
        _systemName += _audioGroup.GetCategory();
        _systemName += _name;
    }

    private Dropdown<GameObject> AudioClipDrop()
    {
        if (_audioGroup is null) return new Dropdown<GameObject>();
        Dropdown<GameObject> dropdown = new Dropdown<GameObject>();
        foreach (GameObject entry in _audioGroup.GetAudioClips())
        {
            dropdown.Add(entry.GetComponent<AudioSource>().clip.name,entry);
        }
        return dropdown;
    }

    private IEnumerable<AudioLibrary> AudioLibraryDrop()
    {
        AudioManager.AssingEditorOnlyInstance();

        AudioLibrary[] temp = AudioManager.INSTANCE.GetAudioLibraries();
        if (temp is null) return new AudioLibrary[1];
        return temp;
    }
    
    private IEnumerable<AudioGroup> AudioGroupDrop()
    {
        if (_audioLibrary is null) return new AudioGroup[1]; //prevent console NullReference Error spam
        return _audioLibrary.GetAudioGroups();
    }
}

[System.Serializable]
public class TransitionToSnapshot
{
    /***Attributes***/
    [SerializeField]
    private AudioMixerSnapshot _snapshot;

    [SerializeField] 
    private float _timeToReach;
    
    [SerializeField,ReadOnly]
    private string _systemName; public string GetSystemName() { return _systemName; }
    
    /***Public***/
    public void Transition()
    {
        _snapshot.TransitionTo(_timeToReach);
    }
    
    /***Private***/
    [Button("Update System Name")]
    private void BuildSystemName()
    {
        _systemName = "";
        _systemName += _snapshot.name;
        _systemName += $"In{_timeToReach}f";
    }
}

[System.Serializable]
public class BlendSnapshots
{
    /***Attributes***/
    [SerializeField] 
    private string _name; 
    
    [SerializeField,NoLabel]
    private SnapshotBlendElement[] _snapshotBlends;
    
    private List<AudioMixerSnapshot> _snapshots;
    
    private List<float> _wights;
    
    [SerializeField]
    private float _timeToReach;
    
    [SerializeField,ReadOnly]
    private string _systemName; public string GetSystemName() { return _systemName; }
    
    /***Public***/
    public void Blend()
    {
        _snapshots = new List<AudioMixerSnapshot>();
        _wights = new List<float>();
        foreach (SnapshotBlendElement entry in _snapshotBlends)
        {
            _snapshots.Add(entry.Snapshot);
            _wights.Add(entry.Wight);
        }
        _snapshots[0].audioMixer.TransitionToSnapshots(_snapshots.ToArray(), _wights.ToArray(), _timeToReach);
    }
    
    /***Private***/
    [Button("Update System Name")]
    private void BuildSystemName()
    {
        _systemName = "";
        _systemName += _name;
        _systemName += $"Blends{_snapshotBlends.Length}";
        _systemName += $"In{_timeToReach}f";
    }
}