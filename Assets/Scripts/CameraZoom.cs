using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public int speed;
    public double forSTOP;
    public double backSTOP;

    // Update is called once per frame
    void Update()
    {
        Vector2 v = Input.mouseScrollDelta;
        if (v[1] > 0)
        {
            if (this.transform.position.z <= forSTOP)
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            
        }
        if (v[1] < 0)
        {
            if (this.transform.position.z >= backSTOP)
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }

    }
}
