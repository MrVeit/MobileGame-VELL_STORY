using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArtText : MonoBehaviour
{
    public Text textUI;

    public string text = "Ремонт роботa V.E.L.L после первых тестов - 12.12.20xx год.";

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
