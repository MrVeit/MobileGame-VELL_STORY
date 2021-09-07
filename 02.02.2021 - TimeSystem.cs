using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeSystem : MonoBehaviour
{
    [Header("Text components")]

    public GameObject TextObj;
    public GameObject ContinueDialog;
    public GameObject OtherDialogs;
    public GameObject Continue_2;
    public GameObject TimeDate_Text;

    [Range(0, 1)]
    public int TimeDialog = 0;
    [Range(0, 1)]
    public static int GamePart = 0;

    [Header("Text controller")]

    [SerializeField] public Text textUI;

    [Header("ExitSwipe")]
    bool isPaused = false;


    void Awake()
    {
        CheckOffline();
        EnableText();
    }

    private void Start()
    {
        GamePart = PlayerPrefs.GetInt("GamePart", GamePart);
    }

    private void CheckOffline()
    {
        if (PlayerPrefs.HasKey("LastSession"))
        {
            TimeSpan ts;
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
            print(string.Format("Вы отсутствовали: {0} д, {1} ч, {2} м, {3} c", ts.Days, ts.Hours, ts.Minutes, ts.Seconds));

            string p1 = " ";
            string D = (ts.Days.ToString());
            string H = (ts.Hours.ToString());
            string M = (ts.Minutes.ToString());
            string S = (ts.Seconds.ToString());

            if (GamePart <= 0)
            {
                textUI.text = D + p1 + ("ДН.") + p1 + H + p1 + ("ЧАС.") + p1 + M + p1 + ("МИН.") + p1 + S + p1 + ("СЕК.");
            }

            else if (GamePart >= 1)
            {
                textUI.text = D + p1 + ("D.") + p1 + H + p1 + ("H.") + p1 + M + p1 + ("MIN.") + p1 + S + p1 + ("SEC.");
            }
        }
    }

    public void EnableText()
    {
        if (PlayerPrefs.HasKey("TimeDialog"))
        {
            TimeDialog = PlayerPrefs.GetInt("TimeDialog");

            print("Переменная даты успешно получена.");

            if (TimeDialog >= 1)
            {
                Continue_2.SetActive(false);
                TextObj.SetActive(true);
                TimeDate_Text.SetActive(true);
                OtherDialogs.SetActive(true);
                ContinueDialog.SetActive(false);

                print("Данные успешно выведены в текстовые переменные.");
            }
        }

        else if (!PlayerPrefs.HasKey("TimeDialog"))
        {
            print("Ошибка в получении текстовых данных!");

            if (TimeDialog <= 0)
            {
                Continue_2.SetActive(true);
                TextObj.SetActive(true);
                TimeDate_Text.SetActive(false);
                OtherDialogs.SetActive(false);
                ContinueDialog.SetActive(true);
            }
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;

        TimeDialog += 1;

        PlayerPrefs.SetInt("TimeDialog", TimeDialog);
    }
}
