using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerScript : MonoBehaviour
{
    Material material;
    Renderer renderer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (renderer != null)
        {
            renderer.material = material;
            renderer.enabled = false;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerEnemy = (1 << 12);
        if (Physics.Raycast(ray, out hit, 1000, layerEnemy))
        {
            renderer = hit.collider.GetComponent<Renderer>();
            renderer.enabled = true;
            material = renderer.material;
            Material m = new Material(renderer.material.shader);
            m.color = Color.red;
            renderer.material = m;
        }


    }
}
