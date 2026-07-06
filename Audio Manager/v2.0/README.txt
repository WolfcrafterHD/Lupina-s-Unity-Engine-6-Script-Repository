Hello, 
this is the documentation of the Audio Manager V2.0
You might also find some comments in the code that can help you out.
You'll need the SaintsFields Extension (around Version 5.18.6) for this to work, 
you can get it for free here: https://assetstore.unity.com/packages/tools/utilities/saints-field-269741

Some Qirks of this Audio Manager System:
- Only one Audio of the Music Category can play at any given time.
- It's recomended to clear the AmbienceCache often.
- Musi amd Ambience will loop unless you spesifically tell them to stop. 
- Any Audio of a different Category will have thier Loop Property set to false.
- Audio Sapwn at the location of the GameObject with the AudioSocket Component it was called from, not from where the comand was execued.
- Only Audio of the Category Music can be 2D, everything else will be set to 3D sound.
- ALL Audio will play on Awake.
- You may only work with ONE AudioMixer, you need to Organize with AudioMixerGroups
- If you want to assing a Reference at game start via GetCompnent NEVER do in void Awake, do so in void Start, else you get the NullReference Error

How to use:
- prepare audio mixers
  - create a audio mixer
  - create audio mixer subgroups, following structure is recomended:
{
  - Master
    - Concrete
      - SFX
      - Voice
      - Ambiente
    - Meta
      - Music
      - UI
}
  - expose the parameters for Volume and Pitch for every Audio Mixer Groups, following parameter naming recomended:
{
  PitchOfxy for Pitch Parameter
  VolumeOfxy for Volume Parameter
}
  - create as many Snapshots with different effects
    WARNING: Do not Snapshot Pitch or Volume changes

- prepare Audios
  - spawn in a empty object
  - reset the transform component
  - add a audio source
  - turn it into a prefab
  - copy and past the prefab until you have enougth to accommodate all your audio clips
  - turn the settings to your liking, however folloing paramters ought to be Ignored: Output, PlayOnAwake, Loop, Spatial Blend
    WARNING: if you set the named Parameters they WILL be overwirtten
    INFO: The system adds 0.1f to spacial blend parameter when it's supposed to be 3D, so you have Room to play around with it in a Range of 0.1f to 1.0f

- prepare AudioLibraries
  - create as many Instances of the Scriptable Object to accommodate your needs, I recomend Naming (and later ordering them) like this:
  - WARNING: All AudioLibraries MUST be put into the Serelized Array on the AudioManager Script to work (more on that later).
{
  BiomeAmbientes
  HurtSounds
  Footsteps
  ....
}

- prepare AudioGroups
  - create as many Instances of the Scriptable Object to accommodate your needs, I recomend naming (and later ordering them) like this:
{
  FemaleHurtSounds
  MaleHurtSounds
  CowHurtSounds
}
  - set the Category to the fitting choise, I recomend following:
{
  SFX -> ment for non Vocal Sounds e.g. A door Opening, Footsteps, a Radio Prop
  Voice -> ment for Vocal Sounds with a directly discernable source e.g. Voice Lines of Characters, HurtSounds, Barks of a Pet Dog
  Ambience -> ment for long, built to loop localized noises without a discernable source e.g. the wind blowing, the noise of a water fall, birds chirping
  Music -> ment for long, bult to loop Global (2D Audio) Music e.g. Background Music
  UI -> ment for Global (2D Audio) sfx in the context of the UI and Menus e.g. a button press confirm sound  
}
  - WARNING: Changes to anything only Applay after pressing the [UPDATE AUDIO SYSTEM BUTTON]
  - ANOTHER WARNING: If you create a Library for the FIRST TIME, ENSURE that there is a component AudioManager somewhere, with exactly ONE AudioMixerGroup assinged to every Catergory (more on that later)
  - Add the Audio Goups to the fitting AudioLibraries.

- prepare the AudioManager Script
  - fill all refrences out.
  - Under Audio Mixer Group Data, Fill out the Volume / Pitch sections out with the exposed parameter of that Audio Mixer Group, A TYPRO WILL RESULT IN A ERROR/ NONE              FUNCTUNALITY
  - IMPORTANT: Ensure every Category has NOT MORE OR LESS than ONE Audio Mixer Group
  - ANOTHER IMORTANT: Note that only AudioLibrarys are ables to bne used wich are added into the List.
  - INCREDABLY FUCKING IMPORTANT: No mather where you do a change on the Audio ALAWAYS press die UPDATE AUDIO SYSTEM Button after you are done, else the changes don't apply

- prepare the Audio Socket Script on all you Game Objects which you want to have play audio.
- IMPORTANT: After adding emelents press: "CONFIRM SYSTEM NAME" if you want to acsess that element you'll bed the String that apears in the System Name Slot above it.
- TIPP: Name the Elements in a way were you can read recognise them by name alone later. Note that the System Name will have additional Suffixes depending on you choosen        Options.

- prepare you Coustom Script
  - how this is too be done can be seen in the Audio Demo Script /Audio Tester Script.
    Note that the functions below start are only show how you'd call the funcitons within them, theres no need to copy the functions Labeld with [Button("...")] one to one.

AVILABLE FUNCTIONS:
- Note that the Audio Manager is a Singelton, so you *could* call the functions from there if oyu want more control, HOWEVER I do NOT recomend this, Instead call the functions   via the Audio Soket Component like shown in the Audio Demo / Audio Tester Script.

- ClearCache(AudioCache)
  - this clears the selected Audio Cache, this should also kill all sounds playing in it
  - AduioCache is a coustom Enum
  - call it so -> nameOfAudioSocketVar.ClearCache(AudioCache.AudioCacheOfYourChoise);

- LetLoopPlayAndEnd(AudioCache)
  - this clears the selected Audio Cache, however the currently playing sounds are allowed to play out before being killed
  - AudioCAche is a coustom Enum
  - call it so -> nameOfAudioSocketVar.LetLoopPlayAndEnd(AudioCache.AudioCacheOfYourChoise);

- Blend()
  - this blends two or more snapshots
  - note that you can only use elements you've added in the corresponding AudioSocket
  - call it so -> DictonaryYouSavedTheDataToFromTheAudioSocket[SystemNameOfYourBlendableElement].Blend();

- Transition()
  - this transitions from one to another Snapshot
  - note that you can only use elements you've added in the corresponding AudioSocket
  - call it so -> DictonaryYouSavedTheDataToFromTheAudioSocket[SystemNameOfYourTransitionElement].Transition();

- SetAudioMixerGroupVolume(AudioMixerGroup, Float)
  - this sets the Volume from you AudioMixer to the Wanted Volume
  - call it so -> nameOfAudioSocketVar.SetAudioMixerGroupVolume(YourAudioMixerOfChoise, YourFloatValueOfChoise)

- SetAudioMixerGroupPitch(AudioMixerGroup, Float)
  - this sets the Pitch from you AudioMixer to the Wanted Pitch
  - call it so -> nameOfAudioSocketVar.SetAudioMixerGroupPitch(YourAudioMixerOfChoise, YourFloatValueOfChoise)

- Play()
 - this plays any wanted Audio Element
 - call it so -> DictonaryYouSavedTheDataToFromTheAudioSocket[SystemNameOfYourAudioElement].Play();
