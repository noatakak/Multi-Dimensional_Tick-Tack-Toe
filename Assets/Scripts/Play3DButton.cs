using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play3DButton : MonoBehaviour
{
    public bool colorSelected = false;
    public Button redButton;
    public Button blueButton;

    public void openObject(GameObject obj)
    {
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
        }

    }
    public void selectColor()
    {
        colorSelected = true;
    }
    public void changeRedButton()
    {
        redButton.GetComponent<Image>().color = Color.red;
        blueButton.GetComponent<Image>().color = Color.gray;
    }
    public void changeBlueButton()
    {
        blueButton.GetComponent<Image>().color = Color.blue;
        redButton.GetComponent<Image>().color = Color.gray;
    }
    public void closeMenu(GameObject menu)
    {
        if (colorSelected)
        {
            menu.SetActive(false);
        }
    }
    public void createUI(GameObject UI)
    {
        if (colorSelected)
        {
            UI.SetActive(true); ;    
        }
    }
}
