using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whoWins : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerCon;
    public GameObject redWins;
    public GameObject blueWins;
    public GameObject InGameMenu;
    public GameObject draw;

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            InGameMenu.SetActive(false);
            if (playerCon.GetComponent<GamePlayer>().blueWin)
            {
                blueWins.SetActive(true);
                redWins.SetActive(false);
            }
            else if(playerCon.GetComponent<GamePlayer>().redWin)
            {
                blueWins.SetActive(false);
                redWins.SetActive(true);
            }
            else{
                blueWins.SetActive(false);
                redWins.SetActive(false);
                draw.SetActive(true);
            }


        }
    }
}
