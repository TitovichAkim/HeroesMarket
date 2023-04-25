using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public static class Localizator
{
    public static void LocalizedText (Text targetTextObject, string key, int num = 0, string Insert0 = "")
    {
        if(PlayerPrefs.HasKey("GameLanguage"))
        {
            string GameLanguage = PlayerPrefs.GetString("GameLanguage");  //  RU/EN
            targetTextObject.text = localized_text(key, GameLanguage, num, Insert0);
        }
        else
        {  // если язык не выбран то английский по умолчанию
            targetTextObject.text = localized_text(key, "EN", num, Insert0);
        }
    }
    private static string localized_text (string key, string lang, int num, string Insert0 = "")
    {
        string[] fileKey = key.Split('.');
       // вытаскиваем из таблицы значение
       // читаем из Resources/Localization/{Название файла, соответствующее началу ключа}.csv
        TextAsset mytxtData=(TextAsset)Resources.Load($"Localization/{fileKey[0]}");
        string loc_txt=mytxtData.text;
        string[] rows = loc_txt.Split('\n');
        for(int i = 1; i < rows.Length; i++)
        {
            string[] cuted_row = Regex.Split(rows[i], "	");
            if(key == cuted_row[0])
            {
                if(lang == "RU")
                {

                    return string.Format(cuted_row[1].Replace("_", "\n").Split("\n")[num], Insert0);
                }
                else if(lang == "EN")
                {
                    return string.Format(cuted_row[2].Replace("_", "\n").Split("\n")[num], Insert0);
                }
                break;
            }
        }
        return "translation not found";  // если перевод не найден в таблице
    }
}