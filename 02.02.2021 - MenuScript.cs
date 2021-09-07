 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class MenuScript : MonoBehaviour
{
    [Header("Other scripts")]

    [SerializeField] ScrollChange ScrollCh;

    [Header("Other buttons")]

    public GameObject ExitBtn;
    public GameObject InfoBtn;
    public GameObject Chucky_1;
    public GameObject Chucky_2;
    public Button AdsButton;
    public Button ArcadeBtn;
    public Button MonoChromeBtn;
    public Button BrokenBtn;
    public GameObject InfoText;
    public GameObject BuyText_1;
    public GameObject BuyText_2;
    public Button ClassicIcon;
    public Button MonoIcon;
    public Button BrokenIcon;
    public GameObject ClassicBtn;
    public GameObject ErrorText;
    public GameObject MainMenuT;
    public GameObject ErrorBtn;
    public GameObject ErrorBtn_1;
    public GameObject ErrorBtn_2;
    public GameObject ErrorBtn_3;
    public GameObject ErrorBtn_4;

    string m_ReachabilityText;

    [Header("Text Elements")]

    [SerializeField] public Text CardiiT;
    [SerializeField] public Text ImpulseT;
    [SerializeField] public Text MadnessText;
    [SerializeField] public Text FriendText;
    [SerializeField] public Text WayVell;

    [Header("Audio components")]

    public AudioClip ChuckyActivate;

    [Header("ExitSwipe")]

    bool isPaused;

    [Header("Main Elements")]

    public static int CardiiInt = 0;
    public static int ImpulseInt = 0;
    public static float TotalDistance = 0f;
    [Range(0, 20)]
    public static int FriendChucky = 0;
    [Range(0, 1)]
    public static int Madness = 0;
    [Range(0, 1)]
    public int TimeDialog = 0;
    [Range(0f, 1f)]
    public float MusicFloat = 0.25f;
    [Range(0f, 1f)]
    public float SoundFloat = 0.25f;
    [Range(0, 5)]
    public int GraphicSettigs;
    [Range(0, 3)]
    public int Vsync;
    [Range(0, 1)]
    public int Part1_End = 0;
    [Range(0, 2)]
    public int VellSkin = 0;
    [Range(0, 1)]
    public int OtherVell = 0;
    [Range(0, 1)]
    public int GamePart;

    void Start()
    {
        LoadAffterGame();
        ChuckyDance();

        ArcadeBtn.interactable = true;
        InfoText.SetActive(false);

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                print("Вход в учётную запись был выполнен успешно!");
            }

            else
            {
                print("ОШИБКА! Вы пролили ртуть в квартире, срочно бегите в бункер!");
            }
        });

        GamePart = PlayerPrefs.GetInt("GamePart", GamePart);

        if (PlayerPrefs.HasKey("VSync"))
        {
            Vsync = PlayerPrefs.GetInt("VSync");

            print("Параметр VSync был успешно загружен в вашу систему!");
        }

        if (!PlayerPrefs.HasKey("VSync"))
        {
            print("Ошибка, параметр VSync не был загружен!");
        }

        if (PlayerPrefs.HasKey("Part1End"))
        {
            Part1_End = PlayerPrefs.GetInt("Part1End");
        }

        if (!PlayerPrefs.HasKey("Part1End"))
        {
            print("Доступ в новому режиму запрещён, необходимо завершить 1 главу!");
        }

        else print("Доступ в новому режиму запрещён, необходимо завершить 1 главу!");

        if (!PlayerPrefs.HasKey("VellSkin"))
        {
            VellSkin = 0;

            BrokenIcon.interactable = false;
            MonoIcon.interactable = false;
            ClassicIcon.interactable = true;

            print("Нужно больше энергии, вам предстоит пройти тяжёлый путь...");
        }

        if (!PlayerPrefs.HasKey("Other Vell"))
        {
            VellSkin = 0;

            BrokenIcon.interactable = false;
            MonoIcon.interactable = false;
            ClassicIcon.interactable = true;

            print("Нужно больше энергии, вам предстоит пройти тяжёлый путь...");
        }

        TimeDialog += 1;

        SoundFloat = 0.25f;
        MusicFloat = 0.25f;
    }

    void Update()
    {
        ImpulseT.text = ImpulseInt.ToString();
        CardiiT.text = CardiiInt.ToString();
        MadnessText.text = Madness.ToString();
        FriendText.text = FriendChucky.ToString();
        WayVell.text = TotalDistance.ToString("0");

        GamePart = PlayerPrefs.GetInt("GamePart", GamePart);

        MadnessGames();
        CheckConnection();


        if (PlayerPrefs.HasKey("VellSkin"))
        {
            VellSkin = PlayerPrefs.GetInt("VellSkin");

            if (VellSkin == 1)
            {
                MonoIcon.interactable = true;
                BrokenIcon.interactable = false;
                ClassicIcon.interactable = true;
                BuyText_1.SetActive(false);
            }

            if (VellSkin == 2)
            {
                BrokenIcon.interactable = true;
                MonoIcon.interactable = false;
                ClassicIcon.interactable = true;
                BuyText_2.SetActive(false);
            }

            if (VellSkin >= 1 && OtherVell == 1)
            {
                BrokenIcon.interactable = true;
                MonoIcon.interactable = true;
                ClassicIcon.interactable = true;
                BuyText_1.SetActive(false);
                BuyText_2.SetActive(false);
            }
        }

        if (PlayerPrefs.HasKey("OtherVell"))
        {
            OtherVell = PlayerPrefs.GetInt("OtherVell");
            VellSkin = PlayerPrefs.GetInt("VellSkin");

            if (VellSkin == 1)
            {
                MonoIcon.interactable = true;
                BrokenIcon.interactable = false;
                ClassicIcon.interactable = true;
                BuyText_1.SetActive(false);
            }

            if (VellSkin == 2)
            {
                BrokenIcon.interactable = true;
                MonoIcon.interactable = false;
                ClassicIcon.interactable = true;
                BuyText_2.SetActive(false);
            }

            if (VellSkin >= 1 && OtherVell == 1)
            {
                BrokenIcon.interactable = true;
                MonoIcon.interactable = true;
                ClassicIcon.interactable = true;
                BuyText_1.SetActive(false);
                BuyText_2.SetActive(false);
            }
        }
    }

    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void AdsController(bool isActive)
    {
        NoAds = isActive;
        PlayerPrefs.SetInt("NoAds", (true ? 1 : 0));
    }

    public void BuyMonochromeSkin()
    {
        if (CardiiInt >= 2500)
        {
            CardiiInt -= 2500;
            VellSkin = 1;

            BuyText_1.SetActive(false);
            MonoChromeBtn.interactable = true;

            PlayerPrefs.SetInt("VellSkin", VellSkin);
            PlayerPrefs.SetInt("Cardii", CardiiInt);
            PlayerPrefs.SetInt("Impulse", ImpulseInt);
        }

        else print("Нужно больше энергии, вам предстоит пройти тяжёлый путь...");
    }

    public void BuyBrokenSkin()
    {
        if (CardiiInt >= 5000)
        {
            CardiiInt -= 5000;
            VellSkin = 2;
            OtherVell = 1;

            BuyText_2.SetActive(false);
            BrokenBtn.interactable = true;

            PlayerPrefs.SetInt("OtherVell", OtherVell);
            PlayerPrefs.SetInt("VellSkin", VellSkin);
            PlayerPrefs.SetInt("Cardii", CardiiInt);
            PlayerPrefs.SetInt("Impulse", ImpulseInt);
        }

        else print("Нужно больше энергии, вам предстоит пройти тяжёлый путь...");
    }

    public void ReturnSkinMono()
    {
        VellSkin = 1;
        OtherVell = 0;

        PlayerPrefs.SetInt("Cardii", CardiiInt);
        PlayerPrefs.SetInt("Impulse", ImpulseInt);
        PlayerPrefs.SetInt("VellSkin", VellSkin);

        ShowLeaderBoard();

        if (OtherVell == 1)
        {
            PlayerPrefs.SetInt("OtherVell", OtherVell);
        }
    }

    public void ReturnSkinBroken()
    {
        VellSkin = 2;
        OtherVell = 1;

        PlayerPrefs.SetInt("OtherVell", OtherVell);
        PlayerPrefs.SetInt("VellSkin", VellSkin);
        PlayerPrefs.SetInt("Cardii", CardiiInt);
        PlayerPrefs.SetInt("Impulse", ImpulseInt);

        ShowLeaderBoard();
    }

    public void ReturnSkin()
    {
        VellSkin = 0;

        ShowLeaderBoard();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("LoadMenu");
        PlayerPrefs.SetInt("GamePart", GamePart);
    }

    public void RestartMenu()
    {
        PlayerPrefs.SetInt("GamePart", GamePart);;
    }

    public void OnClickExit()
    {
        Application.Quit();
        PlayerPrefs.SetInt("Cardii", CardiiInt);
        PlayerPrefs.SetInt("Impulse", ImpulseInt);
        PlayerPrefs.SetInt("Madness", Madness);
        PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
        PlayerPrefs.SetFloat("WayVell", TotalDistance);
        PlayerPrefs.SetInt("TimeDialog", TimeDialog);
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
        PlayerPrefs.SetInt("VSync", Vsync);
        PlayerPrefs.SetInt("GamePart", GamePart);
        PlayGamesPlatform.Instance.SignOut();
        PlayerPrefs.Save();

        print("Ваш последний визит был сохранён.");
    }

    public void OnClickStory()
    {
        SceneManager.LoadScene("CrossBar");
    }

    public void OnClickArcade()
    {
        SceneManager.LoadScene("CrossBarArcade");
    }

    public void CheckConnection()
    {
        print("Internet : " + m_ReachabilityText);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            m_ReachabilityText = "Выход в сеть запрещён";

            ErrorBtn.SetActive(true);
            ErrorText.SetActive(true);
            MainMenuT.SetActive(false);
            ErrorBtn_1.SetActive(true);
            ErrorBtn_2.SetActive(true);
            ErrorBtn_3.SetActive(true);
            ErrorBtn_4.SetActive(true);
        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            ErrorBtn.SetActive(false);
            ErrorText.SetActive(false);
            MainMenuT.SetActive(true);
            ErrorBtn_1.SetActive(false);
            ErrorBtn_2.SetActive(false);
            ErrorBtn_3.SetActive(false);
            ErrorBtn_4.SetActive(false);

            m_ReachabilityText = "Разрешён доступ к сети через передачу данных";
        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            ErrorBtn.SetActive(false);
            ErrorText.SetActive(false);
            MainMenuT.SetActive(true);
            ErrorBtn_1.SetActive(false);
            ErrorBtn_2.SetActive(false);
            ErrorBtn_3.SetActive(false);
            ErrorBtn_4.SetActive(false);

            m_ReachabilityText = "Разрешён доступ к сети через локальное подключение";
        }
    }

    public void VSync120()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

        Vsync = 3;

        PlayerPrefs.SetInt("VSync", Vsync);
        PlayerPrefs.Save();
    }

    public void VSync60()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        Vsync = 2;

        PlayerPrefs.SetInt("VSync", Vsync);
        PlayerPrefs.Save();
    }

    public void VSync30()
    {
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = 30;

        Vsync = 1;

        PlayerPrefs.SetInt("VSync", Vsync);
        PlayerPrefs.Save();
    }
    public void LoadAffterGame()
    {
        CardiiInt += PlayerPrefs.GetInt("Cardii"); //после закрытия уровня.
        ImpulseInt += PlayerPrefs.GetInt("Impulse");
        TotalDistance += PlayerPrefs.GetFloat("WayVell");
        Madness = PlayerPrefs.GetInt("Madness");
        FriendChucky = PlayerPrefs.GetInt("FriendShipChucky");
        GraphicSettigs = PlayerPrefs.GetInt("UnityGraphicsQuality");
    }
    public void MadnessGames()
    {
        if (Madness == 1)
        {
            ExitBtn.SetActive(false);
            InfoBtn.SetActive(false);
            ScrollCh.imageContent.SetActive(false);
        }

        else if (Madness == 0)
        {
            ExitBtn.SetActive(true);
            InfoBtn.SetActive(true);
            ScrollCh.imageContent.SetActive(true);
        }
    }

    public void ChuckyDance()
    {
        if (FriendChucky <= 0 && Madness == 1)
        {
            Chucky_1.SetActive(false);
            Chucky_2.SetActive(false);
        }

        else if (FriendChucky >= 10 && Madness == 0)
        {
            Chucky_1.SetActive(true);
            Chucky_2.SetActive(true);

            GetComponent<AudioSource>().PlayOneShot(ChuckyActivate, 0.5f);
        }
    }

    public void OpenParts()
    {
        Part1_End = 1;

        PlayerPrefs.SetInt("Part1End", Part1_End);
        SceneManager.LoadScene("LoadMenu");
        PlayerPrefs.SetInt("GamePart", GamePart);
    }


    public void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;

        TimeDialog += 1;

        PlayerPrefs.SetInt("Cardii", CardiiInt); // Cохранение.
        PlayerPrefs.SetInt("Impulse", ImpulseInt);
        PlayerPrefs.SetFloat("WayVell", TotalDistance);
        PlayerPrefs.SetInt("Madness", Madness);
        PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
        PlayerPrefs.SetInt("TimeDialog", TimeDialog);
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
        PlayerPrefs.SetInt("GamePart", GamePart);
        PlayGamesPlatform.Instance.SignOut();
        PlayerPrefs.Save();

        print("Ваш последний визит был сохранён.");
    }
}
