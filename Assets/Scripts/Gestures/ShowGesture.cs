using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowGesture : MonoBehaviour
{
    //author: daniela

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

    [SerializeField]
    [Tooltip("Empty GameObject positioned on the tip of the wand")]
    private GameObject wandTip;

    [SerializeField]
    [Tooltip("How quick the object in the prefab gets spawned, in seconds")]
    [Range(0.0001f, 1)]
    private float timeBetweenPoints = 0.1f;

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        // Set line renderer properties as needed
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Create a gradient with rainbow colors
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Color.red, 0.0f),
                new GradientColorKey(Color.yellow, 0.2f),
                new GradientColorKey(Color.green, 0.4f),
                new GradientColorKey(Color.cyan, 0.6f),
                new GradientColorKey(Color.blue, 0.8f),
                new GradientColorKey(Color.magenta, 1.0f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 1.0f)
            }
        );
        lineRenderer.colorGradient = gradient;
    }

    // functions

    // gets called when left trigger gets pressed
    // parameter is info from event
    public void OnInputLeftTrigger(InputAction.CallbackContext context)
    {
        InputHandler(context);
        Debug.Log("LEFT TRIGGER");
    }

    // gets called when right trigger gets pressed
    // parameter is info from event
    public void OnInputRightTrigger(InputAction.CallbackContext context)
    {
        InputHandler(context);
        Debug.Log("RIGHT TRIGGER");
    }

    // Inspired from MiVRy script
    private void InputHandler(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            Debug.Log("BUTTON");
            if (callbackContext.started)
            {
                Debug.Log("STARTED");
                StopGestureCor();
                ClearLine();
                gesturing = true;
                showGestureCor = StartCoroutine(AddPoint(timeBetweenPoints));
            }
            if (callbackContext.canceled)
            {
                Debug.Log("STOPPED");
                StopGestureCor();
                gesturing = false;
                //TO DO start new countdown cor (like maybe 2 seconds) where gesture stays, then disappears
                //--> in there: gesture disappears (gameobjects get removed)
                ClearLine();
            }
            return;
        }
    }

    private void StopGestureCor()
    {
        if (showGestureCor != null) StopCoroutine(showGestureCor);
    }

    //adds points to the line renderer over time while you are gesturing
    //parameter: time (what time between the addition of an object, for example 0.1f = 0.1 seconds)
    private IEnumerator AddPoint(float time)
    {
        Debug.Log("gonna add points");
        while (gesturing)
        {
            Vector3 tipPosition = wandTip.transform.position;
            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], tipPosition) > 0.01f)
            {
                points.Add(tipPosition);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, tipPosition);
            }
            yield return new WaitForSeconds(time);
        }
        yield return new WaitForSeconds(time);
    }

    // Clears the line renderer
    private void ClearLine()
    {
        points.Clear();
        lineRenderer.positionCount = 0;
    }
}
