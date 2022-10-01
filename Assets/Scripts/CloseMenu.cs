using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    public void closeObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
