using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AchTexting : MonoBehaviour
{
    [Range(0, 1)]
    public int GamePart;

    [Header("Text Localization Elements")]

    public Text textUI;

    public string textRus = "";
    public string textEng = "";

    private void Start()
    {
        string language = PlayerPrefs.GetString("Language");
        GamePart = PlayerPrefs.GetInt("GamePart");
    }

    public void ClickShow()
    {
        if (GamePart <= 0)
        {
            StartCoroutine("ShowTextRus", textRus);
        }

        else if (GamePart >= 1)
        {
            StartCoroutine("ShowTextEng", textEng);
        }
    }

    IEnumerator ShowTextRus(string textRus)
    {
        int i = 0;
        while (i <= textRus.Length)
        {
            textUI.text = textRus.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.07f);
        }
    }

    IEnumerator ShowTextEng(string textEng)
    {
        int i = 0;
        while (i <= textEng.Length)
        {
            textUI.text = textEng.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.07f);
        }
    }
}
