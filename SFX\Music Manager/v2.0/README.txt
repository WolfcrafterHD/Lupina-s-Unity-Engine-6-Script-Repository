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
  - add a audio source
  - turn it into a prefab
  - copy and past the prefab until you have enougth to accommodate all your audio clips
  - turn the settings to your liking, however folloing paramters ought to be Ignored: Output, PlayOnAwake, Loop, Spatial Blend
    WARNING: if you set the named Parameters they WILL be overwirtten

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
    WARNING: Changes to anything only Applay after pressing the [UPDATE AUDIO SYSTEM BUTTON]
    ANOTHER WARNING: If you create a Librari for the FIRST TIME, ENSURE that there is a component AudioManager somewhere,
    with exactly ONE AudioMixerGroup assinged to every Catergory (more on that later)
  - Add the Audio Goups to the fitting AudioLibraries.
    
  
>>so I dont fogo<<

You will need add a empty game Object and add you Audio Source with your prefered Settings
Don't worry about the loop parameter this'll be done / undone depending if you set the category of the AudioLib (more about that later) to Music / !Music.
Then turn it into a prefab. 
Since Unity can't spawn in AudioSources by them self, the "Audio Clips" (wich some Vars in the Code are named after) are actually the GameObjects / Prefabs 
we're making right now!
I suggest you copy and paste it a few times and exchange Audio Clips as needed for a faster Workflow.

Then you add them to Instances of the "AudioLib" Scriptable Object. Group them like "CharXYVoicelines" "FemaleHurtSounds" "ForestBiomMusic" etc.
Set the Category in the Dropdown to the fitting Category (e.g. set "ForestBiomMusic" to Category Music).
However know that only one Sound of the Category Music can play at a time, should you call 2 then the older one gets deleated.

>Work in Progress<
