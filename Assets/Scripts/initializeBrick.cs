using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeBrick : MonoBehaviour {

    Renderer REND;
    
    // Use this for initialization
    void Start () {
        stopRender();
        makeUnclickable();
        REND = this.GetComponent<Renderer>();
        this.transform.rotation = Quaternion.Euler(-45, 0, 0);
        setRandomColorandSpeed();
        name = "brick";
    }
	
    void setRandomColorandSpeed()
    {
        //Get New Random Color and Corresponding Speed
        Color newRandColor = globalVars.BRICKCOLORS[Random.Range(0, globalVars.BRICKCOLORS.Length)];
        float newSpeed = globalVars.SPINSPEEDS[System.Array.IndexOf(globalVars.BRICKCOLORS, newRandColor)];

        REND.material.SetColor("_Color", newRandColor);
        this.GetComponent<spinBrick>().setYSpeed(newSpeed);
    }

    //Stop rendering object this class is attached to. 
    public void startRender()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    //Start rendering object this class is attached to. 
    public void stopRender()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    //Make the object this class is attached to clickable. 
    public void makeClickable()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }

    //Make the object this class is attached to not clickable. 
    public void makeUnclickable()
    {
        this.GetComponent<BoxCollider>().enabled = false;
    }
}
