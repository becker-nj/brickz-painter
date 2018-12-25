using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Constantly rotates the object this class is attached to
//at the speeds set in x_speed, y_speed, z_speed.
public class spinBrick : MonoBehaviour {

    private float x_speed = 0;
    private float y_speed = 2.5f;
    private float z_speed = 0;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x_speed * Time.deltaTime, y_speed * Time.deltaTime, z_speed * Time.deltaTime);
    }

    public void setXSpeed (float speed)
    {
        x_speed = speed;
    }
    public void setYSpeed(float speed)
    {
        y_speed = speed;
    }

    public void setZSpeed(float speed)
    {
        z_speed = speed;
    }

    public float getXSpeed()
    {
        return x_speed;
    }

    public float getYSpeed()
    {
        return y_speed;
    }

    public float getZSpeed()
    {
        return z_speed;
    }

}
