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
    public GameObject overridePanel;

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

    String initiater = "";


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
    float t = 0;
    // Update is called once per frame
    void Update()
    {
        if (currentTurn == redMat)
        {
            blueOverrideButton.SetActive(false);
            if (redOverride > 0)
            {
                overridePanel.SetActive(true);
                redOverrideButton.SetActive(true);
            }
            else
            {
                redOverrideButton.SetActive(false);
                overridePanel.SetActive(false);
            }
        }
        if (currentTurn == blueMat)
        {
            redOverrideButton.SetActive(false);
            if (blueOverride > 0)
            {
                overridePanel.SetActive(true);
                blueOverrideButton.SetActive(true);
            }
            else
            {
                blueOverrideButton.SetActive(false);
                overridePanel.SetActive(false);
            }
        }
        

        if (scaleBoard)
        {
            StartCoroutine(growboard(1.0f, 1));
            StartCoroutine(slideSquares(1.0f, 1));
            scaleBoard = false;
        }
        
        checkForFlatWin();
        if (flatblueWin || flatredWin || flatdraw)
        {
            t += Time.deltaTime / 1.0f;


            if (flatredWin)
            {
                StartCoroutine(colorSlide(Color.grey, Color.red, 1));
                //flatRound.GetComponent<Image>().color = Color.Lerp(Color.red, Color.grey, t);
                selectedCube.GetComponent<MeshRenderer>().material = redMat;
                player.GetComponent<GamePlayer>().RedSelected.Add(selectedCube.name);
                player.GetComponent<GamePlayer>().AllSelected.Add(selectedCube.name);
            }
            else if (flatblueWin)
            {
                StartCoroutine(colorSlide(Color.grey, Color.blue, 1));
                //flatRound.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.grey, t);
                selectedCube.GetComponent<MeshRenderer>().material = blueMat;
                player.GetComponent<GamePlayer>().BlueSelected.Add(selectedCube.name);
                player.GetComponent<GamePlayer>().AllSelected.Add(selectedCube.name);
            }
            else
            {
                selectedCube.GetComponent<MeshRenderer>().material = defaultMat;
                player.GetComponent<GamePlayer>().AllSelected.Remove(selectedCube);
            }
            
            StartCoroutine(slideSquares(1.0f, -1));
            endFlatGame();
        }

    }

    IEnumerator colorSlide(Color start, Color end, float time)
    {
        float i = 0;
        float rate = .7f / time;

        Vector3 toPos = new Vector3(-175, 0, 0);
        Vector3 fromPos = new Vector3(-175, -1000, 0);
        while (i < 1)
        {
            i += Time.deltaTime * rate;
            flatRound.GetComponent<Image>().color = Color.Lerp(start, end, i);
            yield return 0;
        }
    }

    private void endFlatGame()
    {
        flatStarted = false;
        player.GetComponent<GamePlayer>().isFlat = false;


        

        flatgameWon = false;
        flatblueWin = false;
        flatredWin = false;
        flatdraw = false;
        StartCoroutine(growboard(1.0f, -1));

        for (int i = 0; i < squareList.Length; i ++)
        {
            squareList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            squareList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        flatAllSelected = new ArrayList();
        flatBlueSelected = new ArrayList();
        flatRedSelected = new ArrayList();
        player.GetComponent<GamePlayer>().gameInProgress = true;
        if (initiater == "blue")
        {
            player.GetComponent<GamePlayer>().activePlayerColor = player.GetComponent<GamePlayer>().blueMat;
        }
        else if (initiater == "red")
        {
            player.GetComponent<GamePlayer>().activePlayerColor = player.GetComponent<GamePlayer>().redMat;
        }

    }

    IEnumerator slideSquares(float time, int direc)
    {


        if (direc == 1)
        {
            float i = 0;
            float rate = .7f / time;

            Vector3 toPos = new Vector3(-175, 0, 0);
            Vector3 fromPos = new Vector3(-1000, 0, 0);
            Vector3 toOver = new Vector3(300, 0, 0);
            Vector3 fromOver = new Vector3(1000, 0, 0);
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                slidingUI.transform.localPosition = Vector3.Lerp(fromPos, toPos, i);
                overridePanel.transform.localPosition = Vector3.Lerp(fromOver, toOver, i);
                yield return 0;
            }

            
        }
        else
        {
            float i = 0;
            float rate = .7f / time;

            Vector3 fromPos = new Vector3(-175, 0, 0);
            Vector3 toPos = new Vector3(-1000, 0, 0);
            Vector3 fromOver = new Vector3(300, 0, 0);
            Vector3 toOver = new Vector3(1000, 0, 0);
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                slidingUI.transform.localPosition = Vector3.Lerp(fromPos, toPos, i);
                overridePanel.transform.localPosition = Vector3.Lerp(fromOver, toOver, i);
                yield return 0;
            }

            
        }
    }
    

    IEnumerator growboard(float time, int direc)
    {
        if (direc == 1)
        {
            float i = 0;
            float rate = .6f / time;

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
            float rate = .6f / time;

            Vector3 fromSize = transform.localScale;
            Vector3 toZero = Vector3.zero;
            while (i < 1)
            {
                i += Time.deltaTime * rate;
                flatRound.transform.localScale = Vector3.Lerp(fromSize, toZero, i);
                yield return 0;
            }
        }
    }

    public void startFlat()
    {
        Destroy(flatRound);
        if (player.GetComponent<GamePlayer>().activePlayerColor == blueMat)
        {
            initiater = "blue";
        } 
        else if (player.GetComponent<GamePlayer>().activePlayerColor == redMat)
        {
            initiater = "red";
        }
        flatStarted = true;
        selectedCube = player.GetComponent<GamePlayer>().clickedCube.transform.gameObject;
        cubes.GetComponent<CubeRotation>().speed = 0f;
        this.gameObject.SetActive(true);
        Vector3 OpenLocation = Input.mousePosition;
        flatRound = Instantiate(flatBoard, OpenLocation, Quaternion.identity, this.transform);
        flatRound.SetActive(true);
        scaleBoard = true;
        redOverride = 0;
        blueOverride = 0;
        redOverrideButton.SetActive(false);
        blueOverrideButton.SetActive(false);
        if (currentTurn == blueMat)
        {
            redOverride = 1;
            blueOverride = 1;
        }
        else if (currentTurn == redMat)
        {
            blueOverride = 1;
            redOverride = 1;
        }

    }


    public bool overrideMode = false;
    public int redOverride = 0;
    public int blueOverride = 0;
    public GameObject redOverrideButton;
    public GameObject blueOverrideButton;


    public void startOverRide()
    {
        if (currentTurn == blueMat)
        {
            overrideMode = true;
            blueOverride--;
        }
        else if (currentTurn == redMat)
        {
            overrideMode = true;
            redOverride--;
        }
    }




    public void markSquare(Button square)
    {
        if (overrideMode)
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
            else if (currentTurn == redMat)
            {
                square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                square.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "X";
                flatRedSelected.Add(square.gameObject.name);
                flatAllSelected.Add(square.gameObject.name);
                player.GetComponent<GamePlayer>().flatTurnPlayed = true;
                player.GetComponent<GamePlayer>().activePlayerColor = blueMat;
                currentTurn = blueMat;
            }
            overrideMode = false;
        }
        else
        {
            if (square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "")
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
                else if (currentTurn == redMat)
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

        if (flatAllSelected.Contains("sq0") && flatAllSelected.Contains("sq1") && flatAllSelected.Contains("sq2") && flatAllSelected.Contains("sq3") &&
            flatAllSelected.Contains("sq4") && flatAllSelected.Contains("sq5") && flatAllSelected.Contains("sq6") && flatAllSelected.Contains("sq7") &&
            flatAllSelected.Contains("sq8"))
        {
            flatdraw = true;
        }
    }









}
