using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGestureManager : MonoBehaviour
{
    [SerializeField]//d
    private GameObject table;//d
    private Material tableMaterial;//d

    // Start is called before the first frame update
    void Start()
    {
        tableMaterial = table.GetComponent<Renderer>().material;//d
        tableMaterial.SetColor("_Color", Color.white);//d
    }


    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)//s
    {
        if (gestureCompletionData.gestureID < 0)//s
        {
            string errorMessage = GestureRecognition.getErrorMessage(gestureCompletionData.gestureID);//s
            Debug.Log("Gesture not recognized " + errorMessage);//d
            tableMaterial.SetColor("_Color", Color.red);//d
            return;
        } 

        if (gestureCompletionData.similarity >= 0.5) //s
        {
            Debug.Log("Gesture ID: " + gestureCompletionData.gestureID); //d

            //OR (||) for the future: gesture able to be done with left and right hand,
            //all numbers besides 0 are placeholders for the actual IDs of the gestures once they get trained
            if (gestureCompletionData.gestureID == 0 || gestureCompletionData.gestureID == 3)//d 
            {
                tableMaterial.SetColor("_Color", Color.green); //d
                Debug.Log("Gesture = Spiral");//d
            }
            if (gestureCompletionData.gestureID == 1 || gestureCompletionData.gestureID == 4) //d
            {

            }            
            if (gestureCompletionData.gestureID == 2 || gestureCompletionData.gestureID == 5)//d
            {

            }
        }
    }
}
