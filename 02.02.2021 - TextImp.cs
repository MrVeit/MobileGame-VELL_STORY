using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextImp : MonoBehaviour
{
    public Text textUI;

    public string text = "Импульсный модуль не так прост, как может показаться на первый взгляд, обвивающие его соединения кардия, дают постоянный поток энергии для заряда оболочки";

    void Start()
    {

        StartCoroutine("showText", text);
    }

    IEnumerator showText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
