using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void endGame()
    {
        SceneManager.LoadScene(0);
    }

}