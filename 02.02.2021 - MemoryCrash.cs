using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCrash : MonoBehaviour
{
    public Text textUI;

    public string text = "обнаруженно неизвестное подключение... производится перезапуск системы v.e.l.l, текущий прогресс сеанса будет стёрт навсегда.";

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

