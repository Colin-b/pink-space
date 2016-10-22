using UnityEngine;
using System.Collections;

public class WanndController : MonoBehaviour {
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerUp=false;
    public bool triggerDown=false;
    public bool triggerPressed=false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
	// Use this for initialization
	void Start () {
        trackedObj.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (controller != null)
        {
            triggerDown = controller.GetPressDown(triggerButton);
            triggerUp = controller.GetPressDown(triggerButton);
            triggerPressed = controller.GetPressDown(triggerButton);
            if (triggerDown)
            {

            }else if(triggerPressed){

            } else if (triggerUp){

            }
        }
	}
}
