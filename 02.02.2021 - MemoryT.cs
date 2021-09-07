using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryT : MonoBehaviour
{
    public Text textUI;

    public string text = "Затем данную информацию можно просканировать и записать на любой современный носитель, для поддержания питания данного растения используется почва, совмещённая с частицами кардия.";

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
