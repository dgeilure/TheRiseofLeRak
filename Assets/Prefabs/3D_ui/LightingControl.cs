using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingControl : MonoBehaviour
{
    public Material onMaterial;
    public Material offMaterial;
    public GameObject[] spheres;

    void Start()
    {        
        spheres = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spheres[i] = transform.GetChild(i).gameObject;
        }
    }

    //!!! amount: 0 to 12
    public void UpdateLight(int amount)
    {
        int clampedAmount = Mathf.Clamp(amount,0,12);

        for (int i = 0; i < spheres.Length; i++)
        {
            Renderer renderer = spheres[i].GetComponent<Renderer>();
            Light light = spheres[i].GetComponentInChildren<Light>();

            if (i < clampedAmount)
            {
                renderer.material = onMaterial;
            }
            else
            {
                light.enabled = false;
                renderer.material = offMaterial;
            }
            if (i == (clampedAmount - 1))
            {
                light.enabled = true;
            }
        }
    }
}
