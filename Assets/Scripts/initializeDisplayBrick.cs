using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeDisplayBrick : MonoBehaviour {

    Renderer REND;

    // Use this for initialization
    void Start()
    {
        REND = this.GetComponent<Renderer>();
        this.transform.rotation = Quaternion.Euler(-45, 0, 0);
        setRandomColorandSpeed();
        name = "nextColorBrickDisplay";
    }

    //Set the color of the object this script is applied to
    //from the colors listed in globalVars file. 
    public void setRandomColorandSpeed()
    {
        //Get New Random Color and Corresponding Speed
        Color newRandColor = globalVars.BRICKCOLORS[Random.Range(0, globalVars.BRICKCOLORS.Length)];
        float newSpeed = globalVars.SPINSPEEDS[System.Array.IndexOf(globalVars.BRICKCOLORS, newRandColor)];

        REND.material.SetColor("_Color", newRandColor);
        this.GetComponent<spinBrick>().setYSpeed(newSpeed);
    }

}
