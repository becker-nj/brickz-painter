using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour {

    Renderer REND;

    void Start()
    {
        REND = this.GetComponent<Renderer>();
    }

    //Set new passed color to the object this script is attached to. 
    public void applyNewColor(Color nextColor)
    {
        REND.material.SetColor("_Color", nextColor);
    }
}
