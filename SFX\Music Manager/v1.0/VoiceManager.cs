/*
 * Lupinus
 * 3.9.25
 * This is the script for the VoiceManager.
 * This a singeltong. It can be called from anywhere.
 * All sound clips are on this Manager
 */
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class VoiceEntry
{
    [SerializeField] private string key;
    [SerializeField] private AudioClip clip;
    
    [Header("3D Settings")]
    [SerializeField] private bool is3D = false;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 7f;
    [SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;
    
    public string Key => key;
    public AudioClip Clip => clip;
    public bool Is3D => is3D;
    public float MinDistance => minDistance;
    public float MaxDistance => maxDistance;
    public AudioRolloffMode RolloffMode => rolloffMode;
}
public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;
    [SerializeField] private AudioSource soundVoiceObject;
    //TODO: Add voice acting
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }
    
    //***Public Methods***//
    
    //TODO: Add voice acting
    
    //***Private Methods***//
    private void PlayMusic(Dictionary<string, VoiceEntry> dictionary, string key, Transform spawnTransform)
    {
        if (dictionary.TryGetValue(key, out VoiceEntry entry))
        {
            AudioSource audioSource = Instantiate(soundVoiceObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = entry.Clip;
            if (!entry.Is3D) audioSource.spatialBlend = 0f;
            else
            {
                audioSource.spatialBlend = 1f;
                audioSource.minDistance = entry.MinDistance;
                audioSource.maxDistance = entry.MaxDistance;
                audioSource.rolloffMode = entry.RolloffMode;
            }
            audioSource.Play();
            Destroy(audioSource.gameObject, entry.Clip.length);
        }
        else Debug.LogWarning($"VoiceManager didn't find a clip for key {key}.");
    }
    
    private void PlayRandomMusic(Dictionary<string, VoiceEntry> dictionary, string[] keys, Transform spawnTransform) //enter like this: new string[] = {"key1","key2","key3"}
    {
        if (keys.Length == 0) return;
        string randomKey = keys[Random.Range(0, keys.Length)];
        PlayMusic(dictionary, randomKey, spawnTransform);
    }
    
    private Dictionary<string, VoiceEntry> BuildDictionary(List<VoiceEntry> list)
    {
        Dictionary<string, VoiceEntry> dictionary = new Dictionary<string, VoiceEntry>();
        foreach (var entry in list) if (!dictionary.ContainsKey(entry.Key) && entry.Clip != null) dictionary.Add(entry.Key, entry);
        return dictionary;
    }
}
