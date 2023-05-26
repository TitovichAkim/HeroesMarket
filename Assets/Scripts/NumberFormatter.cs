using System.Globalization;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public static class NumberFormatter
{
    //static string[] names = { "", "", "", "����", "����", "������", "������", "�������", "������", "�����", "�����", "������", "���-������"};
    static string[] names = { "", "", "", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion"};
    public static void FormatAndRedraw (Decimal inputNumber, TextMeshProUGUI floatText, TextMeshProUGUI stringText = null)
    {
        int n = 0;

        if(inputNumber >= 1000000000m)
        {
            while(n + 1 < names.Length && inputNumber >= 1000m)
            {
                inputNumber /= 1000m;
                n++;
            }
        }
        if(n < 3)
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

    public static decimal StringToDecimal (string inputString)
    {
        return (decimal.Parse(inputString));
    }
}