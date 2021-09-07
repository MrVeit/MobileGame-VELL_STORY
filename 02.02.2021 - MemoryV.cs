using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryV : MonoBehaviour
{
    public Text textUI;

    public string text = "ПОСЛЕ ШИРОКОГО АЖИОТАЖА ВОКРУГ БИОНИЧЕСКИХ РОБОТОВ V.E.L.L, КОМПАНИЯ СОЗДАТЕЛЯ ЕЩЁ ДОЛГО НЕ МОГЛА ВОЗОБНОВИТЬ ПРОИЗВОДСТВО ИЗ-ЗА НЕХВАТКИ РЕСУРСОВ...";

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
