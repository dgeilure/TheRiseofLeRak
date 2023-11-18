using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private GameObject table;
    private Material tableMaterial;

    // Start is called before the first frame update
    void Start()
    {
        tableMaterial = table.GetComponent<Renderer>().material;
        tableMaterial.SetColor("_Color", Color.white);
    }


    public void OnGestureCompleted(GestureCompletionData gestureCompletionData)
    {
        if (gestureCompletionData.gestureID < 0)
        {
            string errorMessage = GestureRecognition.getErrorMessage(gestureCompletionData.gestureID);
            Debug.Log("Gesture not recognized " + errorMessage);
            tableMaterial.SetColor("_Color", Color.red);
            return;
        }

        if (gestureCompletionData.similarity >= 0.5) 
        {
            tableMaterial.SetColor("_Color", Color.green);
        }
    }
}
