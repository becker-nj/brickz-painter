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

    public void killBrick()
    {
        anim.SetTrigger("Destroy");
        Camera.main.GetComponent<brickzGame>().setBrickNull(gameObject);
    }

    public void finishBrick()
    {
        Camera.main.GetComponent<brickzGame>().replaceBrick((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
        Debug.Log(gameObject.transform.position);
        Destroy(this.gameObject);
    }

    public void applyNewColor(Color nextC, float nextSpeed)
    {
        GetComponent<colorChange>().setNewColor(nextC);
        GetComponent<spinBrick>().setYSpeed(nextSpeed);
    }
}
