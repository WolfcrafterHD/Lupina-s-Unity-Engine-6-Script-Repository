/*
 * 11.06.2026
 * Lupinus
 * In here all Audio 
 */
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Scriptable Objects/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    /***Attributes***/
    [SerializeField]
    private AudioGroup[] _audioGroups; public AudioGroup[] GetAudioGroups() { return _audioGroups; } 
}
