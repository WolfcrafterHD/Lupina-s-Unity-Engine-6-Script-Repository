/*
 * 09.06.2026
 * Lupinus
 * In this Scriptable Object all audio clips that are of a certain group are saved here.
 * Should the category be set to Music or Ambiente: All Clips will be set to Loop.
 * Should the category be set to !MUSIC or !Ambiente: All Clips will be set to self destroy at the End of their Playtime.
 * If they are a 3D Sound, what output they have or PlayOnAwake = true WILL be set Automatically.
 * This is connected to the category Enum in the "AudioHelperClasses.cs" file.
 */
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroup", menuName = "Scriptable Objects/AudioGroup")]
public class AudioGroup : ScriptableObject
{
    /***Attribute***/

    [SerializeField] 
    private Category _category; public Category GetCategory() { return _category; }
    
    [SerializeField] 
    private GameObject[] _audioClips; public GameObject[] GetAudioClips() {return _audioClips; }
}