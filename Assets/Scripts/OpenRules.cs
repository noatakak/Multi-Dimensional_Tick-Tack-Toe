using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRules : MonoBehaviour
{
    public void openObject(GameObject obj)
    {
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
        }
        
    }
}
