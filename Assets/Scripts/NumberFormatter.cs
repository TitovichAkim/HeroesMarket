using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static class NumberFormatter
{

    public static int activeRoom = 0;
    public static int[] activeProductsRoom = {0, 0, 0, 0, 0};
    //static string[] names = { "", "", "", "����", "����", "������", "������", "�������", "������", "�����", "�����", "������", "���-������"};
    static string[] names = { "", "", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion"};
    public static void FormatAndRedraw (double inputNumber, TextMeshProUGUI floatText, TextMeshProUGUI stringText = null)
    {
        int n = 0;

        if (inputNumber >= 1000000f)
        {
            while(n + 1 < names.Length && inputNumber >= 1000f)
            {
                inputNumber /= 1000f;
                n++;
            }
        }
        if (n < 3)
        {
            if(stringText != null)
            {
                floatText.text = inputNumber.ToString("N2", CultureInfo.InvariantCulture);
                stringText.text = names[n];
            }
            else
            {
                floatText.text = $"{inputNumber.ToString("N2", CultureInfo.InvariantCulture)} {names[n]}";
            }
        }   
        else
        {
            if(stringText != null)
            {
                floatText.text = inputNumber.ToString("N3", CultureInfo.InvariantCulture);
                stringText.text = names[n];
            }
            else
            {
                floatText.text = $"{inputNumber.ToString("N3", CultureInfo.InvariantCulture)} {names[n]}";
            }
        }
    }
}
