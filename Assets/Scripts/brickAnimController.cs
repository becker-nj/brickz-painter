using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickAnimController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    //Begin the destruction animation of the object this script
    //is attached to.  
    public void killBrick()
    {
        anim.SetTrigger("Destroy");
    }

    //Destroy the object this script is attached to.
    //Called from animation event. 
    public void finishBrick()
    {
        Camera.main.GetComponent<brickzGame>().replaceBrick((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
        Destroy(this.gameObject);
    }
}
