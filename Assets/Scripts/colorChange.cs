using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour {

    Renderer REND;

    void Start()
    {
        REND = this.GetComponent<Renderer>();
    }

    public void setNewColor(Color newColor)
    {
        REND.material.SetColor("_Color", newColor);
    }
}
