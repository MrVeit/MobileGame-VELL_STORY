using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressFText : MonoBehaviour
{
    public Text textUI;

    public string text = "PRESS F TO PAY RESPECT...";

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

            yield return new WaitForSeconds(0.2f);
        }
    }
}
