using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryVell : MonoBehaviour
{
    public Text textUI;

    public string text = "Так как изначально создание данных моделей планировалось для узконаправленных отраслей, а не для массового потребления, но ситуация вышла из под контроля.";

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

