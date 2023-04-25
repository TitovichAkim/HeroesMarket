using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageChanger :MonoBehaviour
{
    public Dropdown dropdown;
    public void Start ()
    {// Не забыть удалить строку 10
        PlayerPrefs.SetString("GameLanguage", "EN");
        switch(PlayerPrefs.GetString("GameLanguage"))
        {
            case "RU":
                dropdown.value = 0;
                break;
            case "EN":
                dropdown.value = 1;
                break;
        }
        dropdown.onValueChanged.AddListener(delegate { SetLanguage(); });
    }
    public void SetLanguage ()
    {
        switch(dropdown.value)
        {
            case 0:
                PlayerPrefs.SetString("GameLanguage", "RU");
                break;
            case 1:
                PlayerPrefs.SetString("GameLanguage", "EN");
                break;
        }
        Debug.Log(dropdown.value);
        Debug.Log(PlayerPrefs.GetString("GameLanguage"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}