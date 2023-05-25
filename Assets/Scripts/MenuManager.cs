using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Text[] revenuePanelTexts;
    public Text timerText;
    public TextMeshProUGUI rewardText;
    public ShopManager shopManager;
    public GameObject ReturnPanel;
    public float exitTime;
    public float timeSinceExit;
    public TimeSpan currentTime;
    private float _reward = 0;

    public void Start ()
    {
        currentTime = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("lastSessionEndTime", DateTime.Now.ToString()));
        timeSinceExit = (float)currentTime.TotalSeconds;
        if (timeSinceExit > 6)
        {
            CollectTheReward();
        } else
        {
            StartCoroutine(SaveTheMoment());
        }
    }
    public void CollectTheReward ()
    {
        timerText.text = $"You were not in the game: {$"{(int)currentTime.TotalHours}:{currentTime.TotalMinutes % 60:00}:{currentTime.TotalSeconds % 60:00}"}";


        foreach(ProductPanelManager[] productPanelManagers in shopManager.productPanelsArray)
        {
            foreach(ProductPanelManager productPanelManager in productPanelManagers)
            {
                if(productPanelManager.manager)
                {
                    float rewardMultiplier = timeSinceExit/productPanelManager.productSO.initialTime * productPanelManager.multiplierInitialTime;
                    _reward += productPanelManager.productRevenue * rewardMultiplier;
                    ReturnPanel.SetActive(true);
                }
            }
        }
        NumberFormatter.FormatAndRedraw(_reward, rewardText);
        StartCoroutine(SaveTheMoment());
    }

    public void AcceptTheAward()
    {
        shopManager.coins += _reward;
        ReturnPanel.SetActive(false);
    }

    public IEnumerator SaveTheMoment ()
    {
        while(true)
        {
            PlayerPrefs.SetString("lastSessionEndTime",  DateTime.Now.ToString());
            PlayerPrefs.Save();
            yield return new WaitForSeconds(5);
        }
    } 

    public void DeletedEndExit ()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit ()
    {
        Application.Quit();
    }
}