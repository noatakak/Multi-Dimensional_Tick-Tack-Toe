using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlayer : MonoBehaviour
{
    public bool isMDGame = false;
    public bool isFlat;
    public Material flatWinner;
    public GameObject flatBoard;
    public bool flatTurnPlayed;

    public Material activePlayerColor;
    public Material redMat;
    public Material blueMat;
    public Material hoverMat;
    public Material deafultMat;
    public Material wallMat;
    public GameObject redButton;
    public GameObject blueButton;
    public GameObject cubes;
    public bool gameInProgress;
    public bool onButton = false;
    public float rotspeed;
    public GameObject oldHover;
    ArrayList AllSelected = new ArrayList();
    ArrayList BlueSelected = new ArrayList();
    ArrayList RedSelected = new ArrayList();
    public bool turnPlayed;
    public bool gameWon = false;
    public bool blueWin;
    public bool redWin;
    public bool draw;
    public GameObject winScreen;

    public Vector3 winLineCenter;


    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject cube10;
    public GameObject cube11;
    public GameObject cube12;
    public GameObject cube13;
    public GameObject cube14;
    public GameObject cube15;
    public GameObject cube16;
    public GameObject cube17;
    public GameObject cube18;
    public GameObject cube19;
    public GameObject cube20;
    public GameObject cube21;
    public GameObject cube22;
    public GameObject cube23;
    public GameObject cube24;
    public GameObject cube25;
    public GameObject cube26;

    public GameObject clickedCube;


    public bool readytoWait = false;





    public void selectColor(Material mat)
    {
        activePlayerColor = mat;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameInProgress = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkForWin();

        if (gameWon)
        {
            gameInProgress = false;
            rotspeed = 0;
            winScreen.SetActive(true);

        }

        if (isFlat)
        {
            gameInProgress = false;
            rotspeed = 0;
        }


        if (gameInProgress)
        {
            if (onButton)
            {
                rotspeed = 0;
            }
            if (!onButton)
            {
                rotspeed = (float)0.01;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit clickedObj = new RaycastHit();
                bool hit1 = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out clickedObj);
                //Debug.Log(clickedObj.transform.gameObject.name);
                if (hit1 && AllSelected.Contains(clickedObj.transform.gameObject) == false)
                {
                    if (clickedObj.transform.gameObject.name != "BackWall" && clickedObj.transform.gameObject.GetComponent<MeshRenderer>().material.name != redMat.name && clickedObj.transform.gameObject.GetComponent<MeshRenderer>().material.name != blueMat.name)
                    {
                        if (!isMDGame)
                        {
                            clickedObj.transform.gameObject.GetComponent<MeshRenderer>().material = activePlayerColor;
                            AllSelected.Add(clickedObj.transform.gameObject);
                            turnPlayed = true;

                            if (activePlayerColor == blueMat)
                            {
                                BlueSelected.Add(clickedObj.transform.gameObject.name);
                            }
                            else
                            {
                                RedSelected.Add(clickedObj.transform.gameObject.name);
                            }
                        }
                        else
                        {
                            isFlat = true;
                            flatBoard.GetComponent<Play2DGame>().currentTurn = activePlayerColor;
                            clickedCube = clickedObj.transform.gameObject;
                            clickedObj.transform.gameObject.GetComponent<MeshRenderer>().material = deafultMat;
                            flatBoard.GetComponent<Play2DGame>().startFlat();
                            readytoWait = flatBoard.GetComponent<Play2DGame>().flatGameWon;
                            
                            flatWinner = flatBoard.GetComponent<Play2DGame>().flatWinner;
                            clickedObj.transform.gameObject.GetComponent<MeshRenderer>().material = flatWinner;
                            AllSelected.Add(clickedObj.transform.gameObject);
                            turnPlayed = true;

                            if (flatWinner == blueMat)
                            {
                                BlueSelected.Add(clickedObj.transform.gameObject.name);
                            }
                            else
                            {
                                RedSelected.Add(clickedObj.transform.gameObject.name);
                            }
                        }
                    }

                }
            }

            if (!cubes.transform.gameObject.GetComponent<CubeRotation>().dragging)
            {

                RaycastHit hoverObj = new RaycastHit();
                bool hover = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hoverObj);
                if (hover)
                {
                    //Debug.Log("Hovered Mat: " + hoverObj.transform.gameObject.GetComponent<MeshRenderer>().material.name);
                    if (hoverObj.transform.gameObject.name == "BackWall")
                    {
                        hoverObj.transform.gameObject.GetComponent<MeshRenderer>().material = wallMat;
                        onButton = false;
                    }
                    if (oldHover.name == "BackWall")
                    {
                        oldHover.gameObject.GetComponent<MeshRenderer>().material = wallMat;
                    }
                    if (oldHover.name == "BackWall" && hoverObj.transform.gameObject.name != oldHover.name && !AllSelected.Contains(hoverObj.transform.gameObject))
                    {
                        oldHover.gameObject.GetComponent<MeshRenderer>().material = wallMat;
                        hoverObj.transform.gameObject.GetComponent<MeshRenderer>().material = hoverMat;
                        oldHover = hoverObj.transform.gameObject;
                        onButton = true;
                    }
                    if (oldHover.name != "BackWall" && hoverObj.transform.gameObject.name != oldHover.name && !AllSelected.Contains(hoverObj.transform.gameObject))
                    {
                        if (!AllSelected.Contains(oldHover.transform.gameObject))
                        {
                            oldHover.gameObject.GetComponent<MeshRenderer>().material = deafultMat;
                        }
                        hoverObj.transform.gameObject.GetComponent<MeshRenderer>().material = hoverMat;
                        oldHover = hoverObj.transform.gameObject;
                        onButton = true;
                    }
                    if (oldHover.name != "BackWall" && hoverObj.transform.gameObject.name != oldHover.name && AllSelected.Contains(hoverObj.transform.gameObject))
                    {
                        if (!AllSelected.Contains(oldHover.transform.gameObject))
                        {
                            oldHover.gameObject.GetComponent<MeshRenderer>().material = deafultMat;
                        }
                        oldHover = hoverObj.transform.gameObject;
                        onButton = true;
                    }
                    if (oldHover.name == "BackWall" && hoverObj.transform.gameObject.name == "BackWall")
                    {
                        hoverObj.transform.gameObject.GetComponent<MeshRenderer>().material = wallMat;
                        oldHover = hoverObj.transform.gameObject;
                    }



                }
                
                

            }

        }

    }

    private void checkForWin()
    {
        int[][] winArray =
        {
            //Rows on single boards
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            new int[] {9, 10, 11},
            new int[] {12, 13, 14},
            new int[] {15, 16, 17},
            new int[] {18, 19, 20},
            new int[] {21, 22, 23},
            new int[] {24, 25, 26},
            //Columns on single boards
            new int[] {0, 3, 6 },
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            new int[] {9, 12, 15},
            new int[] {10, 13, 16},
            new int[] {11, 14, 17},
            new int[] {18, 21, 24},
            new int[] {19, 22, 25},
            new int[] {20, 23, 26},
            //Diagonals on single board
            new int[] {0, 4, 8},
            new int[] {2, 4, 6},
            new int[] {9, 13, 17},
            new int[] {11, 13, 15},
            new int[] {18, 22, 26},
            new int[] {20, 22, 24},
            //Vertical lines through boards
            new int[] {0, 9, 18},
            new int[] {1, 10, 19},
            new int[] {2, 11, 20},
            new int[] {3, 12, 21},
            new int[] {4, 13, 22},
            new int[] {5, 14, 23},
            new int[] {6, 15, 24},
            new int[] {7, 16, 25},
            new int[] {8, 17, 26},
            //Diagonal lines through boards
            new int[] {0, 12, 24},
            new int[] {1, 13, 25},
            new int[] {2, 14, 26},
            new int[] {6, 12, 18},
            new int[] {7, 13, 19},
            new int[] {8, 14, 20},
            new int[] {0, 10, 20},
            new int[] {3, 13, 23},
            new int[] {6, 16, 26},
            new int[] {2, 10, 18},
            new int[] {5, 13, 21},
            new int[] {8, 16, 24},
            new int[] {0, 13, 26},
            new int[] {2, 13, 24},
            new int[] {6, 13, 20},
            new int[] {8, 13, 18}
        };


        for (int i = 0; i <=winArray.Length-1; i++)
        {
            if (BlueSelected.Contains("cube" + (winArray[i][0])) && BlueSelected.Contains("cube" + (winArray[i][1])) && BlueSelected.Contains("cube" + (winArray[i][2])))
            {
                blueWin = true;
                gameWon = true;
                draw = false;

                winLineCenter = GameObject.Find("cube" + (winArray[i][1])).transform.position;
            }

            if (RedSelected.Contains("cube" + (winArray[i][0])) && RedSelected.Contains("cube" + (winArray[i][1])) && RedSelected.Contains("cube" + (winArray[i][2])))
            {
                redWin = true;
                gameWon = true;
                draw = false;

                winLineCenter = GameObject.Find("cube" + (winArray[i][1])).transform.position;
            }
        }

        if (AllSelected.Count == 26)
        {
            draw = true;
            gameWon = true;
            redWin = false;
            blueWin = false;
        }

        

    }

    public void makeMDGame()
    {
        isMDGame = true;
    }

    public void startGame()
    {
        if (activePlayerColor.name != "GreyMat")
        {
            gameInProgress = true;
        }
        
    }

}
