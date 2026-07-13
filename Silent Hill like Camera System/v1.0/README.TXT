Dependencies: Saints Field (you can just get it in the unity asset store)
There is a jpeg inwich you can see how this is supposed to work, again a lot less drop and drag and more system concept you can copy for yourself.

This is Supposed to be a System wich makes the Cameras look like the ones in Silent Hill.

The CamSwitchBrush Script is for the Trigger zones wich are simply a isTrigger Box colider, you do need to set the same tag as on the moddle wich should trigger the Cam,
most cammonly "Player".

In here the Script thats supposed to set the camera is called SceneCameraNexus, it's important that you have a refrence that sets the Start Camera,
this can be a fancy dropdown via Saints field for that and InstanciateStartCamera(). 
Do that in the Start() func and use the Setter for _startCamera.

Should you want to back up a Camera and erturn to it later for whatever reason you can do that via the Functions around _cameraBackup, note that the backed up camera will
disable itself

IMPORTANT: Every Scene needs it's own SceneCameraNexus Script, there you'll put all the camereas into the array,apon scene load /game start this array will be give to the
camera manager wich will be the place were you can see all of them. This is necessary because CrossSceneRefrences are not supportet because the refrence will be lost otherwise 

Note that once a scene is loaded with a Scene Camera Nexus, the camera in there will imediatly Start up, so make sure to back up the current camera before you load in a new scene.

The player must have a rigidbody.
