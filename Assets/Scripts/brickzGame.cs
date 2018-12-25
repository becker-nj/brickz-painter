using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brickzGame : MonoBehaviour {

    public GameObject brickzObj;
    public GameObject nextColorBrickDisplay;
    public Text displayTimer;
    private string niceTime;
    private GameObject[,] brickArray;
    private HashSet<GameObject> brickzToDestroy;

    private int X_OFFSET = -2;
    private int Y_OFFSET = -4;

    private Color nextColor;
    private float nextSpeed;

    private float timer = 0.0f;
    private int minutes = 0;
    private int seconds = 0;

    // Use this for initialization
    void Start () {
        brickzToDestroy = new HashSet<GameObject>();
        nextColorBrickDisplay = Instantiate(nextColorBrickDisplay, new Vector3(2.35f, 4.35f, 0), Quaternion.identity) as GameObject;
        spawnBrickz();
    }

    // Update is called once per frame
    void Update () {

        updateTime();

        if( Input.GetMouseButtonDown(0) && allBricksClickable())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                
                if (bc != null)
                {
                    if (bc.gameObject.name == "brick")
                    {
                        bc.GetComponent<brickAnimController>().applyNewColor(nextColorBrickDisplay.gameObject.GetComponent<Renderer>().material.GetColor("_Color"), 
                            nextColorBrickDisplay.gameObject.GetComponent<spinBrick>().getYSpeed());

                        nextColorBrickDisplay.GetComponent<initializeDisplayBrick>().setRandomColorandSpeed();
                        matchChecker();
                    }
                } 
            }
        }
    }

    private void OnGUI()
    {
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        displayTimer.text = string.Format("{0:HH:mm:ss}", niceTime);
    }

    private void updateTime()
    {
        timer += Time.deltaTime;
    }

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

    public void setBrickNull(GameObject clickedBrick)
    {
        for(int i = 0; i < brickArray.GetLength(0); i++)
        {
            for(int j = 0; j < brickArray.GetLength(1); j++)
            {
                if(brickArray[i,j] == clickedBrick)
                {
                    brickArray[i, j] = null;
                }
            }
        }
    }

    private void matchChecker()
    {
        brickzToDestroy.Clear();

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

        killMatchedBrickz();
    }

    public void killMatchedBrickz()
    {
        foreach(GameObject brick in brickzToDestroy)
        {
            if(brick != null)
            {
                brick.GetComponent<brickAnimController>().killBrick();
            }
        }
    }

    private void spawnBrick(int x, int y)
    {
        brickArray[x, y] = Instantiate(brickzObj, new Vector3(x + X_OFFSET, y + Y_OFFSET, 0), Quaternion.identity) as GameObject;
    }

    public void replaceBrick(int x, int y)
    {
        brickArray[x - X_OFFSET, y - Y_OFFSET] = Instantiate(brickzObj, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
    }

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
