using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;





public class CubeRotation : MonoBehaviour
{
    public GameObject rotateBoard;
    public float speed = (float)0.001;
    Vector3 mousePos = new Vector3();
    Vector3 NEWmousePos = new Vector3();
    Vector3 diff;
    public GameObject player;
    public bool dragging = false;


    private void Start()
    {
        rotateBoard = GameObject.Find("AllCubes");
        mousePos = Input.mousePosition; 
        NEWmousePos = Input.mousePosition;
        speed = player.GetComponent<GamePlayer>().rotspeed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<GamePlayer>().rotspeed;

        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            diff = new Vector3((NEWmousePos.y - mousePos.y) * speed, (mousePos.x - NEWmousePos.x) * speed, (NEWmousePos.z - mousePos.z) * speed);
            NEWmousePos = Input.mousePosition;
            dragging = true;
                //Debug.Log("On\nDown Position = X: " + mousePos.x + ", Y: " + mousePos.y);
                //Debug.Log("\nNew Position = X: " + NEWmousePos.x + ", Y: " + NEWmousePos.y);
            rotateBoard.transform.Rotate(diff, Space.World);
        }
        if(Input.GetMouseButtonUp(0))
        {
                //Debug.Log("Off\nDown Position = X: " + mousePos.x + ", Y: " + mousePos.y);
                //Debug.Log("\nNew Position = X: " + NEWmousePos.x + ", Y: " + NEWmousePos.y);
                dragging = false;
        }

    }
}
