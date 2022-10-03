using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenRules : MonoBehaviour
{
    public int rulesV = 1;
    public void openObject(GameObject obj)
    {
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
        }
        
    }

    public void nextRules(Text rules)
    {
        if (rulesV == 0)
        {
            rules.text = "In the Game of Tic-Tac-Toe each player takes turns selecting a square until one player creates a line of three in a row of their color, " +
                "or until all the squares are selected and no line of three has been formed. " +
                "The first player to create a line of three wins!\n\nLines can be made horizontally,vertially, and diagonally, " +
                "on any plane in order to win.\n\nSelect a square by clicking on it. " +
                "Rotate the cube by clicking off of the cube and dragging in the direction you want to rotate it.";
            rulesV = 1;
        }
        else if(rulesV == 1)
        {
            rules.text = "In Multi-Dimensional Tick-Tack-Toe, the only way to claim a cube is to first win a game of \"normal\", 2D, tick-tack-toe first.\n\n" +
                "These 2D games play exactly as one would expect, get three squares in a row to win, but with one added twist.\n\n" +
                "Each player gets a limited number of Override Moves for each two-dimensional game. " +
                "These Override Moves allow the current player to claim a square already claimed by their opponent. ";
            rulesV = 0;
        }
    }
}
