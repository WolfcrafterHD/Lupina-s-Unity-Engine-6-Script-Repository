/*
 * 13.06.2026
 * Lupinus
 * Just a simple file to Demo the capability of the AudioManager System.
 */
using System.Collections.Generic;
using UnityEngine;
using SaintsField.Playa;
using SaintsField;
using UnityEngine.Audio;

// ReSharper disable UnusedMember.Local

public class AudioTester : MonoBehaviour
{
    private AudioSocket _audioSocket;
    private Dictionary<string, Audio> _audio;
    private Dictionary<string, TransitionToSnapshot> _toSnapshot;
    private Dictionary<string, BlendSnapshots> _toSnapshots;
    [SerializeField] private AudioCache _clearSelection;
    [SerializeField] private AudioCache _playAndEndSelection;
    [SerializeField] private string _testAudio;
    [SerializeField] private string _testTransition;
    [SerializeField] private string _testBlendSnapshots;
    [SerializeField] private AudioMixerGroup _testAudioMixerGroup;
    [SerializeField] private float _testAudioVolume = 1.0f;
    [SerializeField] private float _testAudioPitch = 1.0f;
    
    void Start()
    {
        _audioSocket = GetComponent<AudioSocket>();
        _audio = _audioSocket.Audios;
        _toSnapshot = _audioSocket.SnapshotTransitions;
        _toSnapshots = _audioSocket.BlendSnapshots;
    }

    [Button("Clear Selected Cache")]
    private void ClearSelection()
    {
        _audioSocket.ClearCache(_clearSelection);
    }

    [Button("End Current Loops")]
    private void EndCurrentLoops()
    {
        _audioSocket.LetLoopPlayAndEnd(_playAndEndSelection);
    }

    [Button("Blend Snapshots")]
    private void BlendSnapshots()
    {
        _toSnapshots[_testBlendSnapshots].Blend();
    }
    
    [Button("Transition Snapshot")]
    private void TransitionSnapshot()
    {
        _toSnapshot[_testTransition].Transition();
    }
    
    [Button("Set Volume")]
    private void SetVolume()
    {
        _audioSocket.SetAudioMixerGroupVolume(_testAudioMixerGroup, _testAudioVolume);
    }

    [Button("Set Pitch")]
    private void SetPitch()
    {
        _audioSocket.SetAudioMixerGroupPitch(_testAudioMixerGroup, _testAudioPitch);
    }
    
    [Button("Play Test Audio")]
    private void PlayAudio()
    {
        _audio[_testAudio].Play();
    }
}
