using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : MonoBehaviour
{
    public string Language;
    Text text;

    public string textRus;
    public string textEng;

    void Start()
    {
        text = GetComponent<Text>();
        Language = PlayerPrefs.GetString("Language");

        if (Language == "" || Language == "Rus")
        {
            text.text = textRus;
        }
        else if (Language == "Eng")
        {
            text.text = textEng;
        }
    }
}
