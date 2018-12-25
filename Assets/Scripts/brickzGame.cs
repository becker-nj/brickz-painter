using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brickzGame : MonoBehaviour {

    //The brick prefab to spawn in a grid.
    public GameObject brickzObj;
    //The brick that previews the next color that can be applied
    public GameObject nextColorBrickDisplay;
    //The array of bricks shown on the screen.
    private GameObject[,] brickArray;
    //The list of bricks that were matched and need to be removed 
    //and replaced. 
    private HashSet<GameObject> brickzToDestroy;

    //Offsets for brick spawn locations. 
    private int X_OFFSET = -2;
    private int Y_OFFSET = -4;

    //These are applied to the next clicked brick. 
    //The next color to be shown on the nextColorBrickDisplay 
    private Color nextColor;
    //The next color to be shown on the nextColorBrickDisplay
    private float nextSpeed;

    // Use this for initialization
    void Start () {
        brickzToDestroy = new HashSet<GameObject>();
        nextColorBrickDisplay = Instantiate(nextColorBrickDisplay, new Vector3(2.35f, 4.35f, 0), Quaternion.identity) as GameObject;
        spawnBrickz();
    }

    // Update is called once per frame
    void Update () {

        //If the user clicks a something, make sure they're allowed to click right now.
        if( Input.GetMouseButtonUp(0) && allBricksClickable())
        {
            //Get the object the user clicked on.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                
                //Check if the object clicked as a brick. 
                if (bc.gameObject.name == "brick")
                {
                    //Apply the next color to the clicked brick. 
                    bc.GetComponent<colorChange>().applyNewColor(nextColorBrickDisplay.gameObject.GetComponent<Renderer>().material.GetColor("_Color"));
                    //Apply next spin speed to the clicked brick.
                    bc.GetComponent<spinBrick>().setYSpeed(nextColorBrickDisplay.gameObject.GetComponent<spinBrick>().getYSpeed());

                    //Get a new color and speed for the next turn.
                    nextColorBrickDisplay.GetComponent<initializeDisplayBrick>().setRandomColorandSpeed();
                    //Check if a match was made.
                    matchChecker();
                }
            }
        }
    }

    //Fill the brick array with 5x8 brickz.
    private void spawnBrickz()
    {
        brickArray = new GameObject[5, 8];

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                spawnBrick(x, y);
            }
        }
    }

    //Create a new brick in the brick array. 
    private void spawnBrick(int x, int y)
    {
        brickArray[x, y] = Instantiate(brickzObj, new Vector3(x + X_OFFSET, y + Y_OFFSET, 0), Quaternion.identity) as GameObject;
    }

    //See if a triplet or greater exists in a vertical or horizontal direction in the array of brickz.
    private void matchChecker()
    {
        brickzToDestroy.Clear();

        //*HORIZONTAL*//
        for (int y = 0; y < brickArray.GetLength(1); y++)//For each row
        {
            for (int x = 0; x < brickArray.GetLength(0) - 2; x++)//For each horizontal triplet
            {
                if (brickArray[x + 1, y].GetComponent<Renderer>().material.GetColor("_Color") == brickArray[x, y].GetComponent<Renderer>().material.GetColor("_Color")
                    && brickArray[x + 2, y].GetComponent<Renderer>().material.GetColor("_Color") == brickArray[x, y].GetComponent<Renderer>().material.GetColor("_Color"))
                {
                    brickzToDestroy.Add(brickArray[x, y]);
                    brickzToDestroy.Add(brickArray[x + 1, y]);
                    brickzToDestroy.Add(brickArray[x + 2, y]);
                }
            }
        }

        //*VERTICAL*//
        for (int x = 0; x < brickArray.GetLength(0); x++)//For each column
        {
            for (int y = 0; y < brickArray.GetLength(1) - 2; y++)//For each vertical triplet
            {
                if (brickArray[x, y + 1].GetComponent<Renderer>().material.GetColor("_Color") == brickArray[x, y].GetComponent<Renderer>().material.GetColor("_Color")
                    && brickArray[x, y + 2].GetComponent<Renderer>().material.GetColor("_Color") == brickArray[x, y].GetComponent<Renderer>().material.GetColor("_Color"))
                {
                    brickzToDestroy.Add(brickArray[x, y]);
                    brickzToDestroy.Add(brickArray[x, y + 1]);                   
                    brickzToDestroy.Add(brickArray[x, y + 2]);
                }
            }
        }

        //Destroy the found matches.
        killMatchedBrickz();
    }

    //Destroy the bricks in the brickzToDestroy HashSet that were determined as matches.
    public void killMatchedBrickz()
    {
        foreach(GameObject brick in brickzToDestroy)
        {
            if(brick != null)
            {
                brick.GetComponent<brickAnimController>().killBrickAnim();
            }
        }
    }

    //Put a replacement brick in the array if one was destoryed in it's location.
    //Called by bricks in process of being destroyed. 
    public void replaceBrick(int x, int y)
    {
        brickArray[x - X_OFFSET, y - Y_OFFSET] = Instantiate(brickzObj, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
    }

    //Check if all bricks are in a clickable state
    //Don't let the user click if not all brick's are clickable/finished animating.
    private bool allBricksClickable()
    {
        for (int i = 0; i < brickArray.GetLength(0); i++)
        {
            for (int j = 0; j < brickArray.GetLength(1); j++)
            {
                if (brickArray[i, j] != null)
                {
                    if(brickArray[i, j].GetComponent<BoxCollider>().enabled == false)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}
