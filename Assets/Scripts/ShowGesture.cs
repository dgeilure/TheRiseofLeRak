using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ShowGesture : MonoBehaviour
{
    //plan/description

    //once button is pressed which you press in order to do the gesture
    //spawn gameobjects in room
    //not as a child of the controller i dont think
    //possibly a child of the wand (would be the most logical)
    //
    //--> coroutine that spawns an object (for easy thing: cube in random rotation, probably better if sprite because no shadows??)
    //--> every 1/xth of a second
    //-> at the position of the tip of the wand 
    //when button released stop adding objects

    //variables

    private bool gesturing = false;

    private Coroutine showGestureCor; 

    [SerializeField][Tooltip("Empty GameObject positioned on the tip of the wand")] 
    private GameObject wandTip;

    [SerializeField][Tooltip("Prefab of the object that is displayed as the trail of the wand (to visualize the gesture)")]
    private GameObject gestureVisualisationPrefab;

    [SerializeField][Tooltip("How quick the object in the prefab gets spawned, in seconds")]
    [Range(0.0001f,1)]
    private float timeBetweenSpawns;

    // functions


    // TO DO send events from the input manager in the scene to this function :)
    // Inspired from MiVRy script
    // gets called when left trigger gets pressed
    // parameter is info from event
    public void OnInputLeftTrigger(InputAction.CallbackContext context)
    {
        w_i_p(context);
        Debug.Log("LEFT TRIGGER");
    }    
    
    // gets called when right trigger gets pressed
    // parameter is info from event
    public void OnInputRightTrigger(InputAction.CallbackContext context)
    {
        w_i_p(context);
        Debug.Log("RIGHT TRIGGER");
    }

    private void w_i_p(InputAction.CallbackContext callbackContext) //TO DO give it a name lol
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            Debug.Log("BUTTON");
            if (callbackContext.started)
            {
                Debug.Log("STARTED");
                StopGestureCor();
                DestroyObjects();
                gesturing = true;
                showGestureCor = StartCoroutine(AddObject(timeBetweenSpawns));
            }
            if (callbackContext.canceled)
            {
                Debug.Log("STOPPED");
                StopGestureCor();
                gesturing = false;
                //TO DO start new countdown cor (like maybe 2 seconds) where gesture stays, then disappears --> in there: gesture disappears (gameobjects get removed)
                DestroyObjects();
            }
            return;
        }
        //not sure if these are needed but I keep these in case
        //this.LeftTriggerValue = this.getInputControlValue(callbackContext.action.activeControl);
        //this.InputAction_LeftTriggerPressed = this.InputAction_LeftTriggerPressed ? this.LeftTriggerValue >= this.LeftTriggerThreshold * 0.9f: this.LeftTriggerValue >= this.LeftTriggerThreshold;
    }

    private void StopGestureCor()
    {
        if (showGestureCor != null) StopCoroutine(showGestureCor);
    }

    //adds objects at the tip of the wand over time while you are gesturing
    //parameter: time (what time between the addition of an object, for example 0.1f = 0.1 seconds)
    private IEnumerator AddObject(float time) 
    {
        Vector3 tipPosition;
        Quaternion objectRotation = Quaternion.identity; //corresponds to 0 rotation, is aligned with parent or world axes
        //above can be randomized later if 3d object is chosen
        Debug.Log("gonna add objects");
        while (gesturing)
        {
            tipPosition = wandTip.transform.position;

            //instantiate object at tip position
            //should also have a transform parent as the last parameter so that the objects don't just float around randomly in the hierarchy //this.transform if the script is on another gameobject
            Instantiate(gestureVisualisationPrefab, tipPosition, objectRotation, this.transform);
            Debug.Log("GESTURING");
            yield return new WaitForSeconds(time);
        }
        yield return new WaitForSeconds(time);
    }

    private void DestroyObjects()
    {
        int particleCount = this.transform.childCount;
        if (particleCount > 0)
        {
            for (int i = 0; i < particleCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }
}
