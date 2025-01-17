using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money = 0;
    public int lives = 10;

    public int score = 0;

    [SerializeField] private TMP_Text MoneyUI;
    [SerializeField] private TMP_Text LivesUI;
    [SerializeField] private TMP_Text RoundsUI;
    [SerializeField] private TMP_Text scoreUI;

    [SerializeField] private GameObject BuyDefense;
    [SerializeField] private GameObject GameOver;

    [SerializeField] private GameObject mainCam;

    public GameObject ActiveWindow;

    private bool gmover = false;

    public void GainMoney(int bounty)
    {
        money += bounty;
        MoneyUI.text = money.ToString();
    }
    public void GainScore(int bounty)
    {
        score += bounty;
    }

    public void LoseHealth()
    {
        lives -= 1;
    }

    public void window(GameObject activewindow)
    {

        ActiveWindow = activewindow;
        BuyDefense.SetActive(true);

    }

    public void createDefender(GameObject selection)
    {
        Instantiate(selection, ActiveWindow.transform.position, Quaternion.identity);
        Destroy(ActiveWindow);
        mainCam.GetComponent<CameraScript>().menu = false;
        money -= selection.GetComponent<defenderBehaviour>().cost;
    }


    // Start is called before the first frame update
    void Start()
    {
        MoneyUI.text = money.ToString();
        LivesUI.text = lives.ToString();
        BuyDefense.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gmover)
        {
            MoneyUI.text = money.ToString();
            LivesUI.text = lives.ToString();
            if (lives <= 0)
            {
                scoreUI.text = "Game Over\n" +
                    "Score:\n" +
                    score.ToString();
                GameOver.SetActive(true);
                gmover = true;
            }
        }
    }
}
