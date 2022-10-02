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
    public GameObject player;
    public bool flatStarted = false;
    public Material flatWinner;
    public GameObject flatBoard;
    public GameObject cubes;
    public GameObject flatRound;
    public GameObject selectedCube;
    public GameObject slidingUI;

    public bool flatGameWon = false;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleBoard)
        {
            StartCoroutine(growboard(1.0f));
            StartCoroutine(slideSquares(1.0f,1));
            scaleBoard = false;
        }

        if (currentTurn == blueMat)
        {

        }
        else
        {

        }








        checkForFlatWin();
        if (flatGameWon)
        {
            StartCoroutine(slideSquares(1.0f, -1));
        }

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
    

    IEnumerator growboard(float time)
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

    public void startFlat()
    {
        currentTurn = player.GetComponent<GamePlayer>().activePlayerColor;
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

    public void markSqaure(Button square)
    {
        if (currentTurn == blueMat)
        {
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "O";
            flatBlueSelected.Add(square.gameObject.name);
            flatAllSelected.Add(square.gameObject.name);
            player.GetComponent<GamePlayer>().flatTurnPlayed = true;
        }
        else
        {
            square.transform.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            square.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "X";
            flatRedSelected.Add(square.gameObject.name);
            flatAllSelected.Add(square.gameObject.name);
            player.GetComponent<GamePlayer>().flatTurnPlayed = true;
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

        if (flatAllSelected.Count == 9 && !flatGameWon)
        {
            flatdraw = true;
        }
    }









}
