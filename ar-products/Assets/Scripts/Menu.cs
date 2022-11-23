using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape

public class Menu : MonoBehaviour
{   
    // Function that listen the exit event
    public void Exit()
    {
        Application.Quit();
    }
}