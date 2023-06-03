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

    public TextMeshProUGUI volumeNumberText;
    public TextMeshProUGUI musicNumberText;
    public GameObject volumeIconImage;
    public GameObject volumeOffIconImage;
    public GameObject MusicIconImage;
    public GameObject MusicOffIconImage;
    public AudioSource backgroundAudioSource;
    public AudioListener audioListener;
    public Slider volumeSlider;
    public Slider musicSlider;


    public TextMeshProUGUI rewardText;
    public ShopManager shopManager;
    public GameObject ReturnPanel;
    public float exitTime;
    public float timeSinceExit;
    public TimeSpan currentTime;
    private double _reward = 0;

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

        if(PlayerPrefs.HasKey("Volume"))
        {
            SetTheVolume(true);
        }
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            SetTheVolumeOfTheMusic(true);
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
        Debug.Log($"Добавлено {_reward}");
    }

    public IEnumerator SaveTheMoment ()
    {
        while(true)
        {
            PlayerPrefs.SetString("lastSessionEndTime",  DateTime.Now.ToString());
            PlayerPrefs.Save();
            Debug.Log("Сохранено");
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

    public void SetTheVolume (bool loading)
    {
        float number = 10;
        if (loading)
        {
            number = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = number;
        }
        else
        {
            number = volumeSlider.value;
        }
        PlayerPrefs.SetFloat("Volume", number);
        AudioListener.volume = (float)number/10;
        volumeNumberText.text = number.ToString();
        volumeIconImage.SetActive(number > 0);
        volumeOffIconImage.SetActive(number <= 0);
    }
    public void SetTheVolumeOfTheMusic (bool loading)
    {
        float number = 10;
        if(loading)
        {
            number = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = number;
        }
        else
        {
            number = musicSlider.value;
        }
        PlayerPrefs.SetFloat("MusicVolume", number);
        backgroundAudioSource.volume = number/10;
        musicNumberText.text = number.ToString();
        MusicIconImage.SetActive(number > 0);
        MusicOffIconImage.SetActive(number <= 0);
    }
}