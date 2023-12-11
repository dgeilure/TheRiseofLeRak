using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGesture : MonoBehaviour
{

    //once button is pressed which you press in order to do the gesture
    //spawn gameobject in room (not a child of the controller i dont think)
    //possibly a child of the wand (would be the most logical)
    //
    //--> coroutine that spawns an object (for easy thing: cube in random rotation, probably better if sprite because no shadows??)
    //--> every 1/xth of a second
    //-> at the position of the tip of the wand 
    //when button released stop adding objects

    private bool gesturing = false;
    //cor as a variable in order to save it and stop it



    //event function: button pressed
    //get gameobject of wand
    //start coroutine
    //set gesturing bool to true

    //event function: button released
    //set gesturing bool to false
    //stop coroutine
    //start new countdown cor (like maybe 2 seconds) where gesture stays, then disappears
    //(((maybe do the reactions to the id here as well? or rather keep it in the other class to separate it thematically)))--> like change color of the gesture visuals



    private IEnumerator AddObject()//time (what time between the addition of an object), gameobject of wand
    {
        while (gesturing)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
