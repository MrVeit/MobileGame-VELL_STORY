using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryText : MonoBehaviour
{
    public Text textUI;

    public string text = "Древо памяти одно из революционных изобретений человечества, оно способно записывать фрагменты памяти живого существа, либо искуственного интеллекта в небольшом объёме.";

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
