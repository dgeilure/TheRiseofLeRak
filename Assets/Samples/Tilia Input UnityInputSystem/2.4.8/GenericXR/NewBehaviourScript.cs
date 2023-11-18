using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        if (gestureCompletionData.gestureID < 0)
        {
            string errorMessage = GestureRecognition.getErrorMessage(gestureCompletionData.gestureID);

            return;
        }
        if (gestureCompletionData.similarity >= 0.5) { }
    }
}
