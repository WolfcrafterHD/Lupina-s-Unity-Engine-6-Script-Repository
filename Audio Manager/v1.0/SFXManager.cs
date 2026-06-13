/*
 * Lupinus
 * 3.9.25
 * This is the script for the SFXManager.
 * This a singeltong. It can be called from anywhere.
 * All sound clips are on this Manager
 *
 * To have thing more tidy, every sound type (Voice, SFX , Music) have seperate Scripts
 *
 * The SFX Manager has exsaple code and comments
 */
using UnityEngine;
using System.Collections.Generic;
//this is a hittle helper class, every option you need can be enterd here in a neat and tidy way
[System.Serializable]
public class SFXEntry
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
//here we have the actuall Manager Class
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    
    //this is the AudioSource we use for all SFX, you need to link a prefab here in the Inspector, but do not leave it in the scene. 
    [SerializeField] private AudioSource soundSFXObject; 
    
    
    //Here we have exsapmle code,
    //you need to add a list of the type of the Helper Class (here) SFXEntry,
    //be sure to remember the string names you enterd for the Helper Class entrys, because you'll be using them to call a sound function.
    [SerializeField] private List<SFXEntry> footstepSFX = new List<SFXEntry>();
    
    //you need to declare a dictonary with string (the one you enterd in the Helper Class) and the Helper Class (here: SFXEntry)
    private Dictionary<string,SFXEntry> _footstep;
    void Awake()
    {
        //here we make it a Singelton, meaning you can accses it from other scripts via: SFXManager.Instance.Function()
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
        
        //this builds the dictionary from the list of Helper Class entries with a coustome function
        _footstep = BuildDictionary(footstepSFX);
    }
    
    //***Public Methods***//
    
    //Here we have exsapmle code, you can call the function like this: SFXManager.Instance.PlayFootstep("key1", transform);
    //Remember the Function Name because will be using from other Scripts
    //In these other scripts just make a viabale that you can change in the Ispector and enter it as parameter into this Funktion
    public void PlayFootstep(string key, Transform spawnTransform) => PlaySFX(_footstep, key, spawnTransform);
    
    //Here we have exsapmle code, you can call the function like this: SFXManager.Instance.PlayDifferentFootsteps(new string[] = {"key1","key2","key3"}, transform);
    //It's made to enter a array of string as keys and will pick a randome one to play.
    public void PlayDifferentFootsteps(string[] keys, Transform spawnTransform) => PlayRandomSFX(_footstep, keys, spawnTransform);
    
    //if you want to add more do it like this:
    
    /*
     * public void FuncName(string[] ArrayOfKeys, Transform spawnTransform) => PlayRandomSFX(_dictionary, ArrayOfKeys, spawnTransform);
     *
     * what it does here is that it makes a Lambda-Function (mini-function) that takes the ArrayOfKeys and spawnTransform as parameters
     * and calls the PlayRandomSFX function with the _dictionary and the ArrayOfKeys (the parameters).
     *
     * If you only want to play one sound then trow in a dictonary anyways
     */
    
    //***Private Methods***//
    
    //you COULD trouch this code, but you REALLY SCHOULD NOT
    
    //this is the function that plays a singel sound
    private void PlaySFX(Dictionary<string, SFXEntry> dictionary, string key, Transform spawnTransform)
    {
        if (dictionary.TryGetValue(key, out SFXEntry entry))
        {
            AudioSource audioSource = Instantiate(soundSFXObject, spawnTransform.position, Quaternion.identity);
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
        else Debug.LogWarning($"SFXManager didn't find a clip for key {key}.");
    }
    
    //this is a function that plays a random sound from an array of keys
    private void PlayRandomSFX(Dictionary<string, SFXEntry> dictionary, string[] keys, Transform spawnTransform) //enter like this: new string[] = {"key1","key2","key3"}
    {
        if (keys.Length == 0) return;
        string randomKey = keys[Random.Range(0, keys.Length)];
        PlaySFX(dictionary, randomKey, spawnTransform);
    }
    
    //this builds the dictionary from the list of Helper Class entries
    private Dictionary<string, SFXEntry> BuildDictionary(List<SFXEntry> list)
    {
        Dictionary<string, SFXEntry> dictionary = new Dictionary<string, SFXEntry>();
        foreach (var entry in list) if (!dictionary.ContainsKey(entry.Key) && entry.Clip != null) dictionary.Add(entry.Key, entry);
        return dictionary;
    }
}
