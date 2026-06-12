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
