using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play2DGame : MonoBehaviour
{
    public Material redMat;
    public Material blueMat;
    public Material currentTurn;
    public GameObject player;
    public GameObject background;
    public bool flatStarted = false;
    public Material flatWinner;
    public GameObject flatBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flatStarted)
        {
            GameObject.Instantiate(background, this.transform);
        }
    }

    public void startFlat()
    {
        flatStarted = true;
    }
}
