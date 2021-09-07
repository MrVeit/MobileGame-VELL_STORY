using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CheckerSkinBase : MonoBehaviour
{
    [Header("OtherScripts")]

    [SerializeField] private MController.MainController ClassicContr;
    [SerializeField] private MController_1.ControllerMonochrome MonoContr;
    [SerializeField] private MController_2.ControllerBroken BrokenContr;

    [Header("SkinBase")]

    public GameObject VellClassic;
    public GameObject VellBroken;
    public GameObject VellMonochorme;

    [Header("Prefabs RoadSpawners")]

    public GameObject RoadSpawnerClassic;
    public GameObject RoadSpawnerBroken;
    public GameObject RoadSpawnerMonochrome;

    [Header("JumpControllers")]

    public GameObject JumpClassic_1;
    public GameObject JumpClassic_2;
    public GameObject JumpBroken_1;
    public GameObject JumpBroken_2;
    public GameObject JumpMonochrome_1;
    public GameObject JumpMonochrome_2;

    [Header("INT SkinBase")]

    [Range(0, 2)]
    public int VellSkin = 0;
    [Range(0, 1)]
    public int OtherVell = 0;

    [Header("WayVellImg")]

    public GameObject WayBroken;
    public GameObject WayMono;
    public GameObject WayVell;

    [Header("PpVell")]

    public GameObject PpClassic;
    public GameObject PpMono;
    public GameObject PpBroken;

    string m_ReachabilityText;

    private void Awake()
    {
        StartCoroutine(CheckInternetConnection((isConnected) => {
            Debug.Log(isConnected);                                 //старт корутины в любом методе на проверку подключения к сети.
        }));
    }

    void Start()
    {
        CheckPortEnable();

        if (PlayerPrefs.HasKey("VellSkin"))
        {
            VellSkin = PlayerPrefs.GetInt("VellSkin");

            if (VellSkin == 0)
            {
                JumpClassic_1.SetActive(true);
                JumpClassic_2.SetActive(true);
                JumpMonochrome_1.SetActive(false);
                JumpMonochrome_2.SetActive(false);
                JumpBroken_1.SetActive(false);
                JumpBroken_2.SetActive(false);

                VellClassic.SetActive(true);
                VellBroken.SetActive(false);
                VellMonochorme.SetActive(false);

                RoadSpawnerClassic.SetActive(true);
                RoadSpawnerBroken.SetActive(false);
                RoadSpawnerMonochrome.SetActive(false);

                WayBroken.SetActive(false);
                WayMono.SetActive(false);
                WayVell.SetActive(true);
            }

            else if (VellSkin == 1)
            {
                JumpClassic_1.SetActive(false);
                JumpClassic_2.SetActive(false);
                JumpMonochrome_1.SetActive(true);
                JumpMonochrome_2.SetActive(true);
                JumpBroken_1.SetActive(false);
                JumpBroken_2.SetActive(false);

                VellClassic.SetActive(false);
                VellBroken.SetActive(false);
                VellMonochorme.SetActive(true);

                RoadSpawnerClassic.SetActive(false);
                RoadSpawnerBroken.SetActive(false);
                RoadSpawnerMonochrome.SetActive(true);

                WayBroken.SetActive(false);
                WayMono.SetActive(true);
                WayVell.SetActive(false);
            }
        }

        if (PlayerPrefs.HasKey("OtherVell"))
        {
            OtherVell = PlayerPrefs.GetInt("OtherVell");

            if (OtherVell == 1 && VellSkin == 2)
            {
                JumpClassic_1.SetActive(false);
                JumpClassic_2.SetActive(false);
                JumpMonochrome_1.SetActive(false);
                JumpMonochrome_2.SetActive(false);
                JumpBroken_1.SetActive(true);
                JumpBroken_2.SetActive(true);

                VellClassic.SetActive(false);
                VellBroken.SetActive(true);
                VellMonochorme.SetActive(false);

                RoadSpawnerClassic.SetActive(false);
                RoadSpawnerBroken.SetActive(true);
                RoadSpawnerMonochrome.SetActive(false);

                WayBroken.SetActive(true);
                WayMono.SetActive(false);
                WayVell.SetActive(false);
            }
        }

        if (!PlayerPrefs.HasKey("VellSkin") && !PlayerPrefs.HasKey("OtherVell"))
        {
            JumpClassic_1.SetActive(true);
            JumpClassic_2.SetActive(true);
            JumpMonochrome_1.SetActive(false);
            JumpMonochrome_2.SetActive(false);
            JumpBroken_1.SetActive(false);
            JumpBroken_2.SetActive(false);

            VellClassic.SetActive(true);
            VellBroken.SetActive(false);
            VellMonochorme.SetActive(false);

            RoadSpawnerClassic.SetActive(true);
            RoadSpawnerBroken.SetActive(false);
            RoadSpawnerMonochrome.SetActive(false);

            WayBroken.SetActive(false);
            WayMono.SetActive(false);
            WayVell.SetActive(true);

            print("Нужно больше энергии, вам предстоит пройти тяжёлый путь...");
        }
    }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            action(false);
        }

        else
        {
            action(true);
        }

    }

    public void CheckPortEnable()
    {
        //Вывод информации о доступе к сети в консоль
        print("Internet : " + m_ReachabilityText);
        //проверка доступа к сети интернет на устройстве
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Change the Text
            m_ReachabilityText = "Выход в сеть запрещён";
        }
        //Проверка возможности выхода в интернет через мобильную сеть
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            m_ReachabilityText = "Доступ к сети через передачу данных";
        }
        //Проверка выхода в сеть через проводное подключение на устройстве
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            m_ReachabilityText = "Доступ через локальную сеть.";
        }
    }

    public void CheckerPausePanel()
    {
        if (VellSkin == 0)
        {
            PpBroken.SetActive(false);
            PpMono.SetActive(false);

            ClassicContr.OnClickPause();
        }

        if (VellSkin == 1 && OtherVell == 0)
        {
            PpClassic.SetActive(false);
            PpBroken.SetActive(false);

            MonoContr.OnClickPause();
        }

        if (VellSkin == 1 && OtherVell == 1)
        {
            PpClassic.SetActive(false);
            PpBroken.SetActive(false);

            MonoContr.OnClickPause();
        }

        if (VellSkin == 2 && OtherVell == 1)
        {
            PpClassic.SetActive(false);
            PpMono.SetActive(false);

            BrokenContr.OnClickPause();
        }
    }

    public void CheckOnPause()
    {
        if (VellSkin == 0)
        {
            JumpClassic_1.SetActive(true);
            JumpClassic_2.SetActive(true);
            JumpMonochrome_1.SetActive(false);
            JumpMonochrome_2.SetActive(false);
            JumpBroken_1.SetActive(false);
            JumpBroken_2.SetActive(false);
        }

        if (VellSkin == 1)
        {
            JumpClassic_1.SetActive(false);
            JumpClassic_2.SetActive(false);
            JumpMonochrome_1.SetActive(true);
            JumpMonochrome_2.SetActive(true);
            JumpBroken_1.SetActive(false);
            JumpBroken_2.SetActive(false);
        }

        if (OtherVell == 1 && VellSkin == 2)
        {
            JumpClassic_1.SetActive(false);
            JumpClassic_2.SetActive(false);
            JumpMonochrome_1.SetActive(false);
            JumpMonochrome_2.SetActive(false);
            JumpBroken_1.SetActive(true);
            JumpBroken_2.SetActive(true);
        }
    }
}
