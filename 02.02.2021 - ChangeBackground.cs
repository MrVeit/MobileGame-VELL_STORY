using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class ChangeBackground : MonoBehaviour
{
    [Header("Close components")]

    bool fadeIn; 
    bool fadeOut;  
    float alphaColor; 

    [Header("Main elements")]

    public Image fadeImage;

    [Range(0, 3)]
    public int loadScene = 1;

    [Range(0, 1)]
    public int Second = 0;

    [Range(0, 1)]
    public static int TimeDialog = 0;

    bool Show;

    public Color fadeInColor; //Выбираем желаемый цвет при окончании сцены
    public Color fadeOutColor; //Выбираем желаемый цвет при старте сцены

    public GameObject BBVell;
    public GameObject BWVell;

    string m_ReachabilityText;

    [Header("Audio components")]

    public AudioClip VellActivate;

    void OnGUI()
    {
        Debug.Log("Internet : " + m_ReachabilityText);

        //Проверка доступа к сети интернет на устройстве

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            m_ReachabilityText = "Выход в сеть запрещён";
        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            m_ReachabilityText = "Доступ к сети через передачу данных";

        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            m_ReachabilityText = "Доступ к сети через локальную сеть.";

        }
    }

    private void Start()
    {
        fadeImage.gameObject.SetActive(true); //Включаем картинку
        fadeImage.color = new Color(fadeOutColor.r, fadeOutColor.g, fadeOutColor.b, 30f); // Ставим стартовый цвет
        fadeOut = true; //Запускаем процесс
        StartCoroutine(Wait(6f));

        
    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds); // таймер, через n секунд

        Second = 1;
    }

    public void ButtonStartGame()
    { //Вызывается из UI
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, 0);
        fadeIn = true;

        SceneManager.LoadScene(1);
    }


    private void StartScene()
    {
        SceneManager.LoadScene(loadScene);
    }

    private void Update()
    {
        if (Second == 1)
        {
            ButtonStartGame();

            BBVell.SetActive(true);
            BWVell.SetActive(false);
        }

        if (fadeIn)
        {
            alphaColor = Mathf.Lerp(fadeImage.color.a, 1, Time.deltaTime * 30f);
            fadeImage.color = new Color(fadeInColor.r, fadeInColor.g, fadeInColor.b, alphaColor);
        }

        if (fadeOut)
        {
            alphaColor = Mathf.Lerp(fadeImage.color.a, 0, Time.deltaTime * 30f);
            fadeImage.color = new Color(fadeOutColor.r, fadeOutColor.g, fadeOutColor.b, alphaColor);
        }

        if (alphaColor > 0.95 && fadeIn)
        {
            fadeIn = false;
            StartScene(); // Вызываем метод со стартом нужной сцены
        }

        if (alphaColor < 0.05 && fadeOut)
        {
            fadeOut = false;
            fadeImage.gameObject.SetActive(false); // Отключаем картинку, чтобы не было прозрачной картинки на весь экран во время игры, иначе может негативно сказаться на производительности
        }
    }
}