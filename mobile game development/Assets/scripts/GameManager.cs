using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money = 0;

    [SerializeField] private TMP_Text MoneyUI;

    [SerializeField] private GameObject BuyDefense;

    [SerializeField] private GameObject mainCam;

    public GameObject ActiveWindow;

    public void GainMoney(int bounty)
    {
        money += bounty;
        MoneyUI.text = money.ToString();
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
        BuyDefense.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoneyUI.text = money.ToString();
    }
}
