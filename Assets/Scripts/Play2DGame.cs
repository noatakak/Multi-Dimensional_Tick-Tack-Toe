using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Play2DGame : MonoBehaviour
{

    public Material firstTurn;
    public Material redMat;
    public Material blueMat;
    public Material currentTurn;
    public Material defaultMat;
    public GameObject player;
    public bool flatStarted = false;
    public Material flatWinner;
    public GameObject flatBoard;
    public GameObject cubes;
    public GameObject flatRound;
    public GameObject selectedCube;
    public GameObject slidingUI;

    bool scaleBoard = false;


    public GameObject sq0;
    public GameObject sq1;
    public GameObject sq2;
    public GameObject sq3;
    public GameObject sq4;
    public GameObject sq5;
    public GameObject sq6;
    public GameObject sq7;
    public GameObject sq8;

    public GameObject[] squareList = new GameObject[9];

    public Text mark;

    ArrayList flatAllSelected = new ArrayList();
    ArrayList flatBlueSelected = new ArrayList();
    ArrayList flatRedSelected = new ArrayList();

    public bool flatredWin;
    public bool flatblueWin;
    public bool flatgameWon;
    public bool flatdraw;


    // Start is called before the first frame update
    void Start()
    {
        squareList[0] = sq0;
        squareList[1] = sq1;
        squareList[2] = sq2;
        squareList[3] = sq3;
        squareList[4] = sq4;
        squareList[5] = sq5;
        squareList[6] = sq6;
        squareList[7] = sq7;
        squareList[8] = sq8;
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleBoard)
        {
            StartCoroutine(growboard(1.0f, 1));
            StartCoroutine(slideSquares(1.0f, 1));
            scaleBoard = false;
        }
        
        checkForFlatWin();
        if (flatblueWin || flatredWin || flatdraw)
        {
            if (flatredWin)
            {
                flatRound.GetComponent<Image>().color = Color.Lerp(Color.grey, Color.red, Time.deltaTime * 0.5f);
                selectedCube.GetComponent<MeshRenderer>().material = redMat;
            }else if (flatblueWin)
            {
                flatRound.GetComponent<Image>().color = Color.Lerp(Color.grey, Color.blue, Time.deltaTime * 0.5f);
                selectedCube.GetComponent<MeshRenderer>().material = blueMat;
            }
            else
            {
                selectedCube.GetComponent<MeshRenderer>().material = defaultMat;
            }
            
            StartCoroutine(slideSquares(1.0f, -1));
            endFlatGame();
        }

    }

    private void endFlatGame()
    {
        flatStarted = false;
        player.GetComponent<GamePlayer>().isFlat = false;


        if (firstTurn == blueMat)
        {
            player.GetComponent<GamePlayer>().activePlayerColor = redMat;
        }
        else
        {
            player.GetComponent<GamePlayer>().activePlayerColor = blueMat;
        }

        flatgameWon = false;
        flatblueWin = false;
        flatredWin = false;
        flatdraw = false;
        StartCoroutine(growboard(1.0f, -1));

        /*for (int i = 0; i < squareList.Length; i ++)
        {
            squareList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            squareList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }*/
        flatAllSelected = new ArrayList();
        flatBlueSelected = new ArrayList();
        flatRedSelected = new ArrayList();
        //Destroy(flatRound);
        player.GetComponent<GamePlayer>().gameInProgress = true;

    }

    IEnumerator slideSquares(float time, int direc)
    {


        if (direc == 1)
        {
            float i = 0;
            float rate = .5f / time;

            Vector3 toPos = new Vector3(0, 25, 0);
            Vector3 fromPos = new Vector3(0, -1000, 0);
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                slidingUI.transform.localPosition = Vector3.Lerp(fromPos, toPos, i);
                yield return 0;
            }
        }
        else
        {
            float i = 0;
            float rate = .5f / time;

            Vector3 fromPos = new Vector3(0, 25, 0);
            Vector3 toPos = new Vector3(0, -1000, 0);
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                slidingUI.transform.localPosition = Vector3.Lerp(fromPos, toPos, i);

                yield return 0;
            }
        }
    }
    

    IEnumerator growboard(float time, int direc)
    {
        if (direc == 1)
        {
            float i = 0;
            float rate = .5f / time;

            Vector3 toSize = transform.localScale;
            Vector3 fromZero = Vector3.zero;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                flatRound.transform.localScale = Vector3.Lerp(fromZero, toSize, i);
                yield return 0;
            }
        }
        else
        {
            float i = 0;
            float rate = .5f / time;

            Vector3 fromSize = transform.localScale;
            Vector3 toZero = Vector3.zero;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                flatRound.transform.localScale = Vector3.Lerp(fromSize, toZero, i);
                if (i == .99)
                {
                    Destroy(flatRound);
                }
                yield return 0;
            }
        }
    }

    public void startFlat()
    {
        currentTurn = player.GetComponent<GamePlayer>().activePlayerColor;
        if (currentTurn.name == "Blue0Mat")
        {
            firstTurn = blueMat;
        }
        else if((currentTurn.name == "RedXMat"))
        {
            firstTurn = redMat;
        }
        flatStarted = true;
        selectedCube = player.GetComponent<GamePlayer>().clickedCube.transform.gameObject;
        //player.SetActive(false);
        cubes.GetComponent<CubeRotation>().speed = 0f;
        this.gameObject.SetActive(true);
        Vector3 OpenLocation = Input.mousePosition;
        flatRound = Instantiate(flatBoard, OpenLocation, Quaternion.identity, this.transform);
        flatRound.SetActive(true);
        scaleBoard = true;
    }

    public void markSquare(Button square)
    {
        if (currentTurn == blueMat)
        {
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "O";
            flatBlueSelected.Add(square.gameObject.name);
            flatAllSelected.Add(square.gameObject.name);
            player.GetComponent<GamePlayer>().flatTurnPlayed = true;
            player.GetComponent<GamePlayer>().turnPlayed = true;
            player.GetComponent<GamePlayer>().activePlayerColor = redMat;
            currentTurn = redMat;
        }
        else
        {
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            square.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "X";
            flatRedSelected.Add(square.gameObject.name);
            flatAllSelected.Add(square.gameObject.name);
            player.GetComponent<GamePlayer>().flatTurnPlayed = true;
            player.GetComponent<GamePlayer>().activePlayerColor = blueMat;
            currentTurn = blueMat;
        }
    }

    void checkForFlatWin()
    {
        int[][] winArray =
        {
            //Rows on single boards
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            //Columns on single boards
            new int[] {0, 3, 6 },
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            //Diagonals on single board
            new int[] {0, 4, 8},
            new int[] {2, 4, 6},
        };


        for (int i = 0; i <= winArray.Length - 1; i++)
        {
            if (flatBlueSelected.Contains("sq" + (winArray[i][0])) && flatBlueSelected.Contains("sq" + (winArray[i][1])) && flatBlueSelected.Contains("sq" + (winArray[i][2])))
            {
                flatblueWin = true;
                flatgameWon = true;
                flatdraw = false;

            }

            if (flatRedSelected.Contains("sq" + (winArray[i][0])) && flatRedSelected.Contains("sq" + (winArray[i][1])) && flatRedSelected.Contains("sq" + (winArray[i][2])))
            {
                flatredWin = true;
                flatgameWon = true;
                flatdraw = false;

            }
        }

        if (flatAllSelected.Count == 9 && !flatgameWon)
        {
            flatdraw = true;
        }
    }









}
