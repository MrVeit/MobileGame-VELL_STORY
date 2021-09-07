using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCardii : MonoBehaviour
{
    public Text textUI;

    public string text = "Чаще всего используется для подачи освещения, заряда механизмов, а также периодически используется для апгрейдов того или иного механизма и телепортации.";

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
