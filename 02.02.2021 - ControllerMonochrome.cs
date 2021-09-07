using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace MController_1
{
    [RequireComponent(typeof(Animator))] //автоматическое добавление контроллера и аниматора, при закреплении скрипта к тому или иному предмету.
    [RequireComponent(typeof(CharacterController))]

    public class ControllerMonochrome : MonoBehaviour, INonSkippableVideoAdListener, IInterstitialAdListener
    {
        [Header("Main Components")]

        private CharacterController _controller;
        private Animator _animator;
        public Transform Vell;

        [Header("Movement Elements")]

        public float Speed = 5, JumpForce = 2;
        private Vector3 dir, gravity, oldPos;
        private bool _canJumping;
        bool _canCrouch = false;
        float _controllerRadiusNorm = 1.6f,
              _controllerRadiusCrouch = 1.10f;
        Vector3 _controllerNormal = new Vector3(0, 1.61f, 0),
                _controllerCrouch = new Vector3(0, 1.25f, 0);

        [Header("Active elements")]

        public int CardiiInt = 0;
        public float TotalDistance = 0f;
        [Range(0f, 1.5f)]
        public float HP = 1.25f;

        [Header("Text Elements")]

        [SerializeField] public Text WayText_Mono;
        [SerializeField] public Text CardiiText;
        [SerializeField] public Text HPInfo;
        [HideInInspector] private const string leaderBoard = "CgkIhqrP0qYJEAIQAQ";

        [Header("PausePanel components")]

        private bool IsPaused = false;
        public GameObject Pp;

        [Header("UI Elements")]

        [SerializeField] public Image UIHP;
        [SerializeField] public GameObject FpsImg;
        [SerializeField] public GameObject PauseBtn;
        [SerializeField] public GameObject JumpBtn;
        [SerializeField] public GameObject CardiiImg;
        [SerializeField] public GameObject WayImg;
        [SerializeField] public GameObject WayImgClassic;
        [SerializeField] public GameObject WayImgBroken;
        [SerializeField] public GameObject CrouchBtn;
        [SerializeField] public GameObject DeadPanel;
        [SerializeField] public GameObject CancelText;
        [SerializeField] public GameObject CancelAdsText;
        [SerializeField] public GameObject CancelExitText;
        [SerializeField] public GameObject MainText;
        [SerializeField] public Button CardiiBtn;
        [SerializeField] public GameObject MainTextAds;
        [SerializeField] public GameObject MainTextExit;
        [SerializeField] public Button AdsBtn;
        [SerializeField] public Button ExitBtn;

        [Header("Other elements")]

        [Range(0, 1)]
        public int JumpController = 0;
        [Range(0, 1)]
        public int RollingController = 0;
        [Range(0, 1)]
        public int FpsS;
        [Range(0, 5)]
        public int GraphicSettigs;
        [Range(0f, 1f)]
        public float MusicFloat = 0.25f;
        [Range(0f, 1f)]
        public float SoundFloat = 0.25f;


        [Header("Effects")]

        public GameObject Ps_Cardii;
        public GameObject Ps_Cardii2;
        public GameObject Ps_Alive;

        [Header("OBJ elements")]

        public GameObject CardiiAvatar1;
        public GameObject CardiiAvatar2;

        [Header("AudioClips animations")]

        public AudioClip[]
        sound_jump,
        sound_dead,
        sound_crouch,
        sound_alive;

        [Header("AudioContent")]

        public AudioClip CardiiTakes;
        public AudioClip ApplyBtn;
        public AudioClip ChangeGraphisc;
        public AudioClip ChangeController;
        public AudioClip CancelContinue;
        public AudioClip AliveSound;

        [Header("COMPONENTS Audio")]

        [SerializeField] public Slider AudioSlider;
        [SerializeField] public Slider MusicSetting;

        [Header("AUDIO CONTROLLER")]

        [SerializeField] public AudioSource MusicSlider;
        [SerializeField] public AudioSource AudioControl;
        [SerializeField] public AudioSource GraphicsSound;
        [SerializeField] public AudioSource FpsToggleSound;
        [SerializeField] public AudioSource OtherClicks;

        [Header("ADS_appodeal Controller")]

        private const string APP_KEY = "b5e53e3288cba260595d625c78ef0f20be877a365950ac5c";

        string m_ReachabilityText;

        [Header("ExitSwipe")]
        bool isPaused = false;
        public bool FinishedAds = false;
        public bool Precached = false;
        private bool PrecacheIntersticial;
        private bool FinishedIntersticial;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();

            dir = new Vector3(0, 0, 1);
            gravity = Vector3.zero;

            FpsS = 1;

            Initialized(true);

            if (PlayerPrefs.HasKey("UnityGraphicsQuality"))
            {
                GraphicSettigs = PlayerPrefs.GetInt("UnityGraphicsQuality");
                MusicSetting.value = PlayerPrefs.GetFloat("MusicGame");
                AudioSlider.value = PlayerPrefs.GetFloat("SoundGame");

                print("Игровые данные были успешно загружены в вашу систему!");
            }

            else if (!PlayerPrefs.HasKey("UnityGraphicsQuality"))
            {
                print("Ошибка в получении игровых настроек!");
            }
        }

        void Update()
        {
            if (_controller.isGrounded)
            {
                if (_canJumping)
                {
                    gravity = Vector3.zero;
                    _animator.SetTrigger("JumpMono");
                    _canJumping = false;
                    gravity.y = JumpForce;

                    if (Input.GetAxisRaw("Vertical") > 0)
                        _canJumping = false;
                }

                if (_canCrouch)
                {
                    if (Input.GetAxisRaw("Vertical") > 0)
                        _canJumping = false;
                        StartCoroutine(DoRoll());
                }
            }
            else
                gravity += Physics.gravity * Time.deltaTime * 3;

            dir.z = Speed;
            dir += gravity;
            dir *= Time.deltaTime;

            _controller.Move(dir);

            WayText_Mono.text = TotalDistance.ToString("0");
            CardiiText.text = CardiiInt.ToString();
            UIHP.fillAmount = HP;

            int v = (int)(Vell.position.z);
            TotalDistance = v;

            if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
            {
                AudioListener.volume = .75f;
                Pp.SetActive(true);
                Time.timeScale = 0;
                IsPaused = true;
                _canJumping = false;
                gameObject.GetComponent<CharacterController>().enabled = false;

                WayImgBroken.SetActive(false); WayImgClassic.SetActive(false);
                WayImg.SetActive(false); CardiiImg.SetActive(false); PauseBtn.SetActive(false); JumpBtn.SetActive(false); CrouchBtn.SetActive(false); FpsImg.SetActive(false);
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused)
            {
                AudioListener.volume = 1f;
                Pp.SetActive(false);
                Time.timeScale = 1;
                IsPaused = false;
                _canJumping = false;
                gameObject.GetComponent<CharacterController>().enabled = true;

                WayImgBroken.SetActive(false); WayImgClassic.SetActive(false);
                WayImg.SetActive(true); CardiiImg.SetActive(true); PauseBtn.SetActive(true); JumpBtn.SetActive(true); CrouchBtn.SetActive(true);

                if (FpsS >= 1)
                {
                    FpsImg.SetActive(true);
                }

                else if (FpsS <= 0)
                {
                    FpsImg.SetActive(false);
                }

            }

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                ExitBtn.interactable = true;
                CancelExitText.SetActive(false);
                MainTextExit.SetActive(true);
            }

            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                ExitBtn.interactable = true;
                CancelExitText.SetActive(false);
                MainTextExit.SetActive(true);
            }

            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                AdsBtn.interactable = true;
                CancelAdsText.SetActive(false);
                MainTextAds.SetActive(true);
            }

            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                AdsBtn.interactable = true;
                CancelAdsText.SetActive(false);
                MainTextAds.SetActive(true);
            }

            if (TotalDistance >= 1000)
            {
                Speed = 10f;
                JumpForce = 14;

                _animator.SetFloat("Speed", 5f);
            }

            if (TotalDistance >= 2500)
            {
                Speed = 12.5f;
                JumpForce = 13;

                _animator.SetFloat("Speed", 6f);
            }

            if (TotalDistance >= 5000)
            {
                Speed = 14f;
                JumpForce = 13;

                _animator.SetFloat("Speed", 8f);
            }

            if (TotalDistance >= 10000)
            {
                Speed = 15f;
                JumpForce = 13;

                _animator.SetFloat("Speed", 10f);
            }

            if (TotalDistance >= 15000)
            {
                Speed = 17.5f;
                JumpForce = 13;

                _animator.SetFloat("Speed", 12.5f);
            }

            if (TotalDistance >= 20000)
            {
                Speed = 22.5f;
                JumpForce = 13;

                _animator.SetFloat("Speed", 15f);
            }

        }

        //ADS

        public void Initialized(bool isTesting)
        {
            Appodeal.setTesting(isTesting);
            Appodeal.setInterstitialCallbacks(this);
            Appodeal.setNonSkippableVideoCallbacks(this);
            Appodeal.muteVideosIfCallsMuted(true);
            Appodeal.initialize(APP_KEY , Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.INTERSTITIAL);
        }

        public IEnumerator DoRoll()
        {
            if (TotalDistance <= 2500)
            {
                print("IsRolling");
                _canCrouch = true;
                _animator.SetBool("CrouchMono", true);
                _controller.center = _controllerCrouch;
                _controller.radius = _controllerRadiusCrouch;

                yield return new WaitForSeconds(1.5f);

                print("Not rolling");
                _canCrouch = false;
                _animator.SetBool("CrouchMono", false);
                _controller.center = _controllerNormal;
                _controller.radius = _controllerRadiusNorm;

                if (!_canCrouch)
                {
                    print("Not rolling");
                    _canCrouch = false;
                    _animator.SetBool("CrouchMono", false);
                    _controller.center = _controllerNormal;
                    _controller.radius = _controllerRadiusNorm;
                }
            }

            else if (TotalDistance >= 5000)
            {
                print("IsRolling");
                _canCrouch = true;
                _animator.SetBool("CrouchMono", true);
                _controller.center = _controllerCrouch;
                _controller.radius = _controllerRadiusCrouch;

                yield return new WaitForSeconds(1f);

                print("Not rolling");
                _canCrouch = false;
                _animator.SetBool("CrouchMono", false);
                _controller.center = _controllerNormal;
                _controller.radius = _controllerRadiusNorm;

                if (!_canCrouch)
                {
                    print("Not rolling");
                    _canCrouch = false;
                    _animator.SetBool("CrouchMono", false);
                    _controller.center = _controllerNormal;
                    _controller.radius = _controllerRadiusNorm;
                }
            }

            else if (TotalDistance >= 10000)
            {
                print("IsRolling");
                _canCrouch = true;
                _animator.SetBool("CrouchMono", true);
                _controller.center = _controllerCrouch;
                _controller.radius = _controllerRadiusCrouch;

                yield return new WaitForSeconds(0.75f);

                print("Not rolling");
                _canCrouch = false;
                _animator.SetBool("CrouchMono", false);
                _controller.center = _controllerNormal;
                _controller.radius = _controllerRadiusNorm;

                if (!_canCrouch)
                {
                    print("Not rolling");
                    _canCrouch = false;
                    _animator.SetBool("CrouchMono", false);
                    _controller.center = _controllerNormal;
                    _controller.radius = _controllerRadiusNorm;
                }
            }
        }

        public void EnabledJumping(bool isActive) => _canJumping = isActive; //смена состояния прыжка.

        public void EnableCrouch(bool isCrouch) => _canCrouch = isCrouch;

        //Game random sound.

        public void Volume()
        {
            AudioControl.volume = AudioSlider.value;
            FpsToggleSound.volume = AudioSlider.value;
            GraphicsSound.volume = AudioSlider.value;
            OtherClicks.volume = AudioSlider.value;

            PlayerPrefs.SetFloat("SoundGame", AudioSlider.value);
        }

        public void Music()
        {
            MusicSlider.volume = MusicSetting.value;

            PlayerPrefs.SetFloat("MusicGame", MusicSetting.value);
        }

        public void PlayCrouchSound(int y)
        {
            GetComponent<AudioSource>().clip = sound_crouch[UnityEngine.Random.Range(0, sound_crouch.Length)];
            GetComponent<AudioSource>().Play();
        }

        public void PlayJumpSound(int c)
        {
            GetComponent<AudioSource>().clip = sound_jump[UnityEngine.Random.Range(0, sound_jump.Length)];
            GetComponent<AudioSource>().Play();
        }

        public void PlayDeadSound(int f)
        {
            GetComponent<AudioSource>().clip = sound_dead[UnityEngine.Random.Range(0, sound_dead.Length)];
            GetComponent<AudioSource>().Play();
        }
        public void PlayAliveSound(int r)
        {
            GetComponent<AudioSource>().clip = sound_alive[UnityEngine.Random.Range(0, sound_alive.Length)];
            GetComponent<AudioSource>().Play();
        }

        public void ReturnJumpAnim()
        {
            _animator.ResetTrigger("JumpMono");
        }

        //SETTINGS

        public void FpsShowOn()
        {
            FpsS = 1;
            FpsImg.SetActive(true);
        }

        public void FpsShowOff()
        {
            FpsS = 0;
            FpsImg.SetActive(false);
        }

        public void LowQuality()
        {
            QualitySettings.SetQualityLevel(0, true);

            GraphicSettigs = 0;

            PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
            PlayerPrefs.Save();
        }
        public void MediumQuality()
        {
            QualitySettings.SetQualityLevel(2, true);

            GraphicSettigs = 2;

            PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
            PlayerPrefs.Save();
        }
        public void UltraQuality()
        {
            QualitySettings.SetQualityLevel(4, true);

            GraphicSettigs = 4;

            PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
            PlayerPrefs.Save();
        }

        //AdsController

        public void OnClickGame()
        {
            AudioListener.volume = 1f;
            Pp.SetActive(false);
            Time.timeScale = 1;
            IsPaused = false;
            _canJumping = false;
            gameObject.GetComponent<CharacterController>().enabled = true;

            WayImg.SetActive(true); CardiiImg.SetActive(true); PauseBtn.SetActive(true); WayImgBroken.SetActive(false); WayImgClassic.SetActive(false);

            if (FpsS >= 1)
            {
                FpsImg.SetActive(true);
            }

            else if (FpsS <= 0)
            {
                FpsImg.SetActive(false);
            }
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            isPaused = pauseStatus;

            PlayerPrefs.SetInt("Cardii", CardiiInt);
            PlayerPrefs.SetFloat("WayVell", TotalDistance);
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
            Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });

            print("Ваш последний визит был сохранён.");
        }

        public void OnClickPause()
        {
            AudioListener.volume = 0.75f;
            Pp.SetActive(true);
            Time.timeScale = 0;
            IsPaused = true;
            _canJumping = false;
            gameObject.GetComponent<CharacterController>().enabled = false;

            WayImg.SetActive(false); CardiiImg.SetActive(false); PauseBtn.SetActive(false); JumpBtn.SetActive(false); CrouchBtn.SetActive(false); FpsImg.SetActive(false); WayImgBroken.SetActive(false); WayImgClassic.SetActive(false);
        }

        public void OnClickExit()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                    ExitBtn.interactable = false;
                    CancelExitText.SetActive(true);
                    MainTextExit.SetActive(false);

                    GetComponent<AudioSource>().PlayOneShot(CancelContinue, 1f);
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;

                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;
                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                }
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;
                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;

                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                }
            }
        }

        public void SaveZoneBtn()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                    ExitBtn.interactable = false;
                    CancelExitText.SetActive(true);
                    MainTextExit.SetActive(false);

                    GetComponent<AudioSource>().PlayOneShot(CancelContinue, 1f);
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;
                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                    SceneManager.LoadScene("Menu");
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;

                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                    SceneManager.LoadScene("Menu");
                }
            }

                
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;

                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                    SceneManager.LoadScene("Menu");
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    ExitBtn.interactable = true;
                    CancelExitText.SetActive(false);
                    MainTextExit.SetActive(true);

                    AudioListener.volume = 1f;
                    Time.timeScale = 1;

                    PlayerPrefs.SetInt("Cardii", CardiiInt);
                    PlayerPrefs.SetFloat("WayVell", TotalDistance);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    Social.ReportScore((int)TotalDistance, leaderBoard, (bool success) => { });
                    SceneManager.LoadScene("Menu");
                }
            }
        }

        public void ContinueGame()
        {
            if (CardiiInt <= 145)
            {
                CardiiBtn.interactable = false;
                GetComponent<AudioSource>().PlayOneShot(CancelContinue, 1f);
                CancelText.SetActive(true);
                MainText.SetActive(false);
            }

            else if (CardiiInt >= 150)
            {
                CardiiInt -= 150;

                CardiiBtn.interactable = true;
                CancelText.SetActive(false);
                MainText.SetActive(true);

                DeadPanel.SetActive(false);

            if (FpsS >= 1)
            {
                FpsImg.SetActive(true);
            }

            else if (FpsS <= 0)
            {
                FpsImg.SetActive(false);
            }

                CardiiImg.SetActive(true);
                WayImg.SetActive(true);
                PauseBtn.SetActive(true);
                JumpBtn.SetActive(true);
                CrouchBtn.SetActive(true);
                WayImgBroken.SetActive(false);
                WayImgClassic.SetActive(false);

                AudioListener.volume = 1f;

                gameObject.GetComponent<CharacterController>().enabled = true;

                GetComponent<AudioSource>().PlayOneShot(AliveSound, 1f);

                _animator.SetBool("DeadMono", false);
                _animator.SetBool("CrouchMono", false);

                _canJumping = false;

                Ps_Alive.SetActive(true);

                HP = 1.5f;
            }
        }


        public void AdsContinue()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                AdsBtn.interactable = false;
                CancelAdsText.SetActive(true);
                MainTextAds.SetActive(false);

                GetComponent<AudioSource>().PlayOneShot(CancelContinue, 1f);
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    AdsBtn.interactable = true;
                    CancelAdsText.SetActive(false);
                    MainTextAds.SetActive(true);

                    DeadPanel.SetActive(false);

                    if (FpsS >= 1)
                    {
                        FpsImg.SetActive(true);
                    }

                    else if (FpsS <= 0)
                    {
                        FpsImg.SetActive(false);
                    }

                    CardiiImg.SetActive(true);
                    WayImg.SetActive(true);
                    PauseBtn.SetActive(true);
                    JumpBtn.SetActive(true);
                    CrouchBtn.SetActive(true);
                    WayImgBroken.SetActive(false);
                    WayImgClassic.SetActive(false);

                    AudioListener.volume = 1f;
                    gameObject.GetComponent<CharacterController>().enabled = true;

                    GetComponent<AudioSource>().PlayOneShot(AliveSound, 1f);

                    _animator.SetBool("DeadMono", false);
                    _animator.SetBool("CrouchMono", false);

                    _canJumping = false;

                    Ps_Alive.SetActive(true);

                    HP = 1.5f;
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    AdsBtn.interactable = true;
                    CancelAdsText.SetActive(false);
                    MainTextAds.SetActive(true);

                    DeadPanel.SetActive(false);

                    if (FpsS >= 1)
                    {
                        FpsImg.SetActive(true);
                    }

                    else if (FpsS <= 0)
                    {
                        FpsImg.SetActive(false);
                    }

                    CardiiImg.SetActive(true);
                    WayImg.SetActive(true);
                    PauseBtn.SetActive(true);
                    JumpBtn.SetActive(true);
                    CrouchBtn.SetActive(true);
                    WayImgBroken.SetActive(false);
                    WayImgClassic.SetActive(false);

                    AudioListener.volume = 1f;
                    gameObject.GetComponent<CharacterController>().enabled = true;

                    GetComponent<AudioSource>().PlayOneShot(AliveSound, 1f);

                    _animator.SetBool("DeadMono", false);
                    _animator.SetBool("CrouchMono", false);

                    _canJumping = false;

                    Ps_Alive.SetActive(true);

                    HP = 1.5f;
                }
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    AdsBtn.interactable = true;
                    CancelAdsText.SetActive(false);
                    MainTextAds.SetActive(true);

                    DeadPanel.SetActive(false);

                    if (FpsS >= 1)
                    {
                        FpsImg.SetActive(true);
                    }

                    else if (FpsS <= 0)
                    {
                        FpsImg.SetActive(false);
                    }

                    CardiiImg.SetActive(true);
                    WayImg.SetActive(true);
                    PauseBtn.SetActive(true);
                    JumpBtn.SetActive(true);
                    CrouchBtn.SetActive(true);
                    WayImgBroken.SetActive(false);
                    WayImgClassic.SetActive(false);

                    AudioListener.volume = 1f;
                    gameObject.GetComponent<CharacterController>().enabled = true;

                    GetComponent<AudioSource>().PlayOneShot(AliveSound, 1f);

                    _animator.SetBool("DeadMono", false);
                    _animator.SetBool("CrouchMono", false);

                    _canJumping = false;

                    Ps_Alive.SetActive(true);

                    HP = 1.5f;
                }

                else if (!Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }

                if (FinishedIntersticial == true)
                {
                    AdsBtn.interactable = true;
                    CancelAdsText.SetActive(false);
                    MainTextAds.SetActive(true);

                    DeadPanel.SetActive(false);

                    if (FpsS >= 1)
                    {
                        FpsImg.SetActive(true);
                    }

                    else if (FpsS <= 0)
                    {
                        FpsImg.SetActive(false);
                    }

                    CardiiImg.SetActive(true);
                    WayImg.SetActive(true);
                    PauseBtn.SetActive(true);
                    JumpBtn.SetActive(true);
                    CrouchBtn.SetActive(true);
                    WayImgBroken.SetActive(false);
                    WayImgClassic.SetActive(false);

                    AudioListener.volume = 1f;
                    gameObject.GetComponent<CharacterController>().enabled = true;

                    GetComponent<AudioSource>().PlayOneShot(AliveSound, 1f);

                    _animator.SetBool("DeadMono", false);
                    _animator.SetBool("CrouchMono", false);

                    _canJumping = false;

                    Ps_Alive.SetActive(true);

                    HP = 1.5f;
                }
            }
        }

            void OnTriggerEnter(Collider col)
            {
                Ps_Cardii.SetActive(false);
                Ps_Cardii2.SetActive(false);

                if (col.tag == "CardiiBox")
                {
                    Ps_Cardii.SetActive(true);
                    CardiiInt += 5;
                    CardiiAvatar1.SetActive(true);
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().PlayOneShot(CardiiTakes, 0.5f);

                    if (CardiiInt == 10)
                    {
                        Ps_Cardii.SetActive(false);
                        Ps_Cardii2.SetActive(true);
                        CardiiAvatar2.SetActive(true);
                    }

                    if (CardiiInt >= 20)
                    {
                        Ps_Cardii.SetActive(true);
                        Ps_Cardii2.SetActive(true);
                    }

                }

            }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Dead")
            {
                if (TotalDistance >= 10000)
                {
                    HP -= Time.deltaTime / 0.25f;
                    Ps_Alive.SetActive(false);
                }

                HP -= Time.deltaTime / 0.45f;
                Ps_Alive.SetActive(false);

                if (HP <= 0)
                {
                    DeadPanel.SetActive(true);
                    WayImg.SetActive(false);
                    CardiiImg.SetActive(false);
                    PauseBtn.SetActive(false);
                    JumpBtn.SetActive(false);
                    CrouchBtn.SetActive(false);
                    FpsImg.SetActive(false);
                    WayImgBroken.SetActive(false); WayImgClassic.SetActive(false);

                    Ps_Alive.SetActive(false);

                    _animator.SetBool("DeadMono", true);
                    _animator.SetBool("CrouchMono", false);

                    if (CardiiInt >= 150)
                    {
                        CardiiBtn.interactable = true;
                        CancelText.SetActive(false);
                        MainText.SetActive(true);
                    }

                    AudioListener.volume = 0.25f;

                    gameObject.GetComponent<CharacterController>().enabled = false;
                }
            }

            if (other.tag == "Dead_under")
            {
                if (TotalDistance >= 10000)
                {
                    HP -= Time.deltaTime / 0.15f;
                    Ps_Alive.SetActive(false);
                }

                else HP -= Time.deltaTime / 0.25f;
                Ps_Alive.SetActive(false);

                if (HP <= 0)
                {
                    DeadPanel.SetActive(true);
                    WayImg.SetActive(false);
                    CardiiImg.SetActive(false);
                    PauseBtn.SetActive(false);
                    JumpBtn.SetActive(false);
                    CrouchBtn.SetActive(false);
                    FpsImg.SetActive(false);
                    WayImgClassic.SetActive(false); WayImgBroken.SetActive(false);

                    _animator.SetBool("DeadMono", true);
                    _animator.SetBool("CrouchMono", false);

                    if (CardiiInt >= 150)
                    {
                        CardiiBtn.interactable = true;
                        CancelText.SetActive(false);
                        MainText.SetActive(true);
                    }

                    AudioListener.volume = 0.25f;

                    gameObject.GetComponent<CharacterController>().enabled = false;
                }
            }
        }

        public void onNonSkippableVideoLoaded(bool isPrecache)
        {
            Precached = isPrecache;
            Precached = true;
        }

        public void onNonSkippableVideoFailedToLoad()
        {
            //none
        }

        public void onNonSkippableVideoShowFailed()
        {
            //none
        }

        public void onNonSkippableVideoShown()
        {
            //none
        }

        public void onNonSkippableVideoFinished()
        {
            //none
        }

        public void onNonSkippableVideoClosed(bool finished)
        {
            FinishedAds = finished;
            FinishedAds = true;
        }

        public void onNonSkippableVideoExpired()
        {
            //none
        }

        public void onInterstitialLoaded(bool isPrecacheInter)
        {
            PrecacheIntersticial = isPrecacheInter;
            PrecacheIntersticial = true;
        }

        public void onInterstitialFailedToLoad()
        {
            //none
        }

        public void onInterstitialShowFailed()
        {
            //none
        }

        public void onInterstitialShown()
        {
            //none
        }

        public void onInterstitialClosed()
        {
            FinishedIntersticial = true;
            
        }

        public void onInterstitialClicked()
        {
            //none
        }

        public void onInterstitialExpired()
        {
            //none
        }
    }
}
