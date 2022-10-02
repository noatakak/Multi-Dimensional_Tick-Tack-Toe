using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    public GameObject playerManager;
    public GameObject FlatManager;
    public string whichTurn;
    public GameObject redInd;
    public GameObject blueInd;


    // Update is called once per frame
    void Update()
    {
        if (!FlatManager.GetComponent<Play2DGame>().flatStarted)
        {
            if (playerManager.GetComponent<GamePlayer>().activePlayerColor == playerManager.GetComponent<GamePlayer>().blueMat)
            {
                whichTurn = "Blue";
                blueInd.SetActive(true);
                redInd.SetActive(false);
            }
            else
            {
                whichTurn = "Red";
                blueInd.SetActive(false);
                redInd.SetActive(true);
            }


            if (playerManager.GetComponent<GamePlayer>().turnPlayed)
            {
                if (playerManager.GetComponent<GamePlayer>().activePlayerColor == playerManager.GetComponent<GamePlayer>().blueMat)
                {
                    playerManager.GetComponent<GamePlayer>().activePlayerColor = playerManager.GetComponent<GamePlayer>().redMat;
                }
                else
                {
                    playerManager.GetComponent<GamePlayer>().activePlayerColor = playerManager.GetComponent<GamePlayer>().blueMat;
                }
                playerManager.GetComponent<GamePlayer>().turnPlayed = false;
            }
        }
        else
        {
            if (playerManager.GetComponent<GamePlayer>().flatTurnPlayed)
            {
                if (FlatManager.GetComponent<Play2DGame>().currentTurn == playerManager.GetComponent<GamePlayer>().blueMat)
                {
                    whichTurn = "Blue";
                    blueInd.SetActive(true);
                    redInd.SetActive(false);
                }
                else
                {
                    whichTurn = "Red";
                    blueInd.SetActive(false);
                    redInd.SetActive(true);
                }
                playerManager.GetComponent<GamePlayer>().flatTurnPlayed = false;
            }
        }


        
    }
}
