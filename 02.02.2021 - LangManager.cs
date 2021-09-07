using UnityEngine;
using UnityEngine.SceneManagement;

public class LangManager : MonoBehaviour
{
    [Range(0, 1)]
    public int GamePart;

    private void Start()
    {
        string language = PlayerPrefs.GetString("Language");
    }

    void Update()
    {
       GamePart = PlayerPrefs.GetInt("GamePart");
    }
    public void Language_Rus()
    {
        GamePart = 0;

        string language = "Rus";
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.SetInt("GamePart", GamePart);
        PlayerPrefs.Save();

        print("Rus");
    }

    public void Language_Eng()
    {
        GamePart = 1;

        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.SetInt("GamePart", GamePart);
        PlayerPrefs.Save();

        print("Eng");
    }
}
