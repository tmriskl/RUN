using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayScript : MonoBehaviour
{
    public GameObject glass1;
    public GameObject glass2;
    public GameObject glass3;
    public GameObject glass4;
    public GameObject glass5;

    public void XrayOn()
    {
        glass1.GetComponent<MeshRenderer>().enabled = true;
        glass2.GetComponent<MeshRenderer>().enabled = true;
        glass3.GetComponent<MeshRenderer>().enabled = true;
        glass4.GetComponent<MeshRenderer>().enabled = true;
        glass5.GetComponent<MeshRenderer>().enabled = true;
    }
    public void XrayOff()
    {
        glass1.GetComponent<MeshRenderer>().enabled = false;
        glass2.GetComponent<MeshRenderer>().enabled = false;
        glass3.GetComponent<MeshRenderer>().enabled = false;
        glass4.GetComponent<MeshRenderer>().enabled = false;
        glass5.GetComponent<MeshRenderer>().enabled = false;
    }


}
