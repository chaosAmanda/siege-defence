using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class purchaseManager : MonoBehaviour
{

    [SerializeField] private GameObject BuyDefense;

    [SerializeField] private GameManager GameManager;

    [SerializeField] private TMP_Text CostUI;

    [SerializeField] private GameObject[] Defenders;

    [SerializeField] private GameObject mainCam;

    public int activeOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void purchase()
    {
        if (Defenders[activeOption].GetComponent<defenderBehaviour>().cost <= GameManager.GetComponent<GameManager>().money)
        {
            BuyDefense.SetActive(false);
            GameManager.GetComponent<GameManager>().createDefender(Defenders[activeOption]);
        }
    }
    public void close()
    {
        BuyDefense.SetActive(false);
        mainCam.GetComponent<CameraScript>().menu = false;
    }

    // Update is called once per frame
    void Update()
    {
        CostUI.text = Defenders[activeOption].GetComponent<defenderBehaviour>().cost.ToString();
        CostUI.color = Color.yellow;
        if (GameManager.GetComponent<GameManager>().money < Defenders[activeOption].GetComponent<defenderBehaviour>().cost)
        {
            CostUI.color = Color.red;
        }


    }
}
