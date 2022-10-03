using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
   public void restartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void openObject(GameObject obj)
    {
        obj.SetActive(true);
    }
}
