using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;
using System.Collections.Generic;

namespace ThirdPersonMovement_2
{
    [RequireComponent(typeof(Animator))] //автоматическое добавление контроллера и аниматора, при закреплении скрипта к тому или иному предмету.
    public class ThirdPersonMovement : MonoBehaviour, INonSkippableVideoAdListener, IInterstitialAdListener
    {
        [Header("Main Components")]

        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _camera3;
        [SerializeField] private GameObject VellVoxel;
        [SerializeField] private MovementCharacteristics _characteristics;
        [SerializeField] private JoystickInput _joystick;
        [SerializeField] private JoystickInput2 _joystick2;
        [SerializeField] private Animator CameraAnim;
        [SerializeField] private Animator ChuckyAnim;
        [SerializeField] private Animator ChuckyAnim_1;

        [Header("RigibBody Fall platforms")]

        [SerializeField] private Rigidbody DeadPlatform1;
        [SerializeField] private Rigidbody DeadPlatform2;
        [SerializeField] private Rigidbody DeadPlatform3;
        [SerializeField] private Rigidbody MedPlatform1;
        [SerializeField] private Rigidbody MedPlatform2;
        [SerializeField] private Rigidbody MedPlatform3;
        [SerializeField] private Rigidbody MedPlatform4;
        [SerializeField] private Rigidbody MedPlatform5;
        [SerializeField] private Rigidbody DragPlatform_1;
        [SerializeField] private Rigidbody DragPlatform_2;
        [SerializeField] private Rigidbody DragPlatform_3;
        [SerializeField] private Rigidbody DragPlatform_4;
        [SerializeField] private Rigidbody DragPlatform_5;
        [SerializeField] private Rigidbody DragPlatform_6;
        [SerializeField] private Rigidbody Part_12_1_1;
        [SerializeField] private Rigidbody Part_12_1_2;
        [SerializeField] private Rigidbody Part_12_1_3;
        [SerializeField] private Rigidbody Part_12_1_4;
        [SerializeField] private Rigidbody Part_12_1_5;
        [SerializeField] private Rigidbody Part13;
        [SerializeField] private Rigidbody Part14;
        [SerializeField] private Rigidbody Part15;
        [SerializeField] private Rigidbody Part_12_;
        [SerializeField] private Rigidbody Part_11_;
        [SerializeField] private Rigidbody Part_11_1_;
        [SerializeField] private Rigidbody Part_11_2_;
        [SerializeField] private Rigidbody Part_11_3_;
        [SerializeField] private Rigidbody Part_10_;
        [SerializeField] private Rigidbody Part_10_1_;
        [SerializeField] private Rigidbody Part_10_2_;
        [SerializeField] private Rigidbody Part_9_;
        [SerializeField] private Rigidbody Part_9_3_;
        [SerializeField] private Rigidbody Part_7_;
        [SerializeField] private Rigidbody Part_6_;
        [SerializeField] private Rigidbody Part_5_;
        [SerializeField] private Rigidbody Part_4_;
        [SerializeField] private Rigidbody Part_4_1_;
        [SerializeField] private Rigidbody Part_4_2_;
        [SerializeField] private Rigidbody Part_4_3_;
        [SerializeField] private Rigidbody Part_4_4_;
        [SerializeField] private Rigidbody Part_3_;
        [SerializeField] private Rigidbody Part_1_;
        [SerializeField] private Rigidbody Part_SecretP_;
        [SerializeField] private Rigidbody Part_Platform_1;
        [SerializeField] private Rigidbody Part_Platform_2;
        [SerializeField] private Rigidbody Part_Platform_3;
        [SerializeField] private Rigidbody Part_PLatform_4;

        [Header("Time bar")]

        public float second;
        [Range(0, 30)]
        public int minute;

        [Header("Active elements")]

        [Range(0, 500)]
        public int CardiiInt = 0;
        [Range(0, 500)]
        public int ImpulseInt = 0;
        [Range(0f, 1.35f)]
        public float HP = 1f;

        [Header("Trade elements")]

        [Range(0, 500)]
        public int CardiiTrade = 0;
        [Range(0, 500)]
        public int ImpulseTrade = 0;

        [Header("Other elements")]

        [Range(0, 1)]
        public int JoystickRL = 1;
        [Range(0, 1)]
        public int Part1_End = 0;
        [Range(0, 5)]
        public int GraphicSettigs;
        [Range(0, 1)]
        public int ChuckyDestroyed = 0;
        [Range(0, 1)]
        public static int GamePart;

        [Header("RelationShip")]

        [Range(0, 20)]
        public int FriendChucky = 0;
        [Range(0, 1)]
        public int Madness = 0;
        public bool NoAds = false;

        [Header("New HP elements")]

        public int HealthL;
        public int NumberOfLives;

        [Header("New HP bar")]

        [SerializeField] public Image[] lives;
        [SerializeField] public Image UIHP;
        public Sprite FullLive, EmptyLive;

        [Header("PausePanel components")]

        private bool IsPaused = false;
        public GameObject Pp;

        [Header("Obj UIElements")]

        public GameObject C;
        public GameObject CardiiImg;
        public GameObject ImpulseImg;
        public GameObject MadnessImg;
        public GameObject Health;
        public GameObject HealthHide;
        public GameObject HealthHide2;
        public GameObject HealthHide3;
        public GameObject ZeroHP;
        public GameObject FirstPersonCamera;
        public GameObject ThirdPersonCamera;
        public GameObject EyesVell;
        public Button HP_Change;

        [Header("Text Elements")]

        [SerializeField] public Text ImpulseModule;
        [SerializeField] public Text CardiiText;
        [SerializeField] public Text CardiiTr;
        [SerializeField] public Text ImpulseTr;
        [SerializeField] public Text CardiiTr2;
        [SerializeField] public Text ImpulseTr2;
        [SerializeField] public Text MadnessText;
        [SerializeField] public Text CardiiChaky;
        [SerializeField] public Text ImpulseChaky;
        [SerializeField] public Text CardiiChaky2;
        [SerializeField] public Text ImpulseChaky2;

        [Header("Obj Elements")]

        public GameObject CardiiAvatar1;
        public GameObject CardiiAvatar2;
        public GameObject ImpulseAvatar;
        public GameObject ImpulseAvatar2;
        public GameObject CardiiBox3;
        public GameObject LastPlant;
        public GameObject FalseT;
        public GameObject FalseT_2;
        public GameObject ChuckyObj;
        public GameObject ChuckyObj_2;
        public GameObject ChuckyState_1;
        public GameObject ChuckyState_2;

        [Header("Effects elements")]

        public GameObject Ps_Imp;
        public GameObject Ps_Imp_2;
        public GameObject Ps_Cardii;
        public GameObject Ps_Cardii2;
        public GameObject Ps_1;
        public GameObject Ps_Teleport;

        [Header("Components ScanPanels")]

        public GameObject InfoCardii;
        public GameObject InfoImpulse;
        public GameObject TradeOffer;
        public GameObject TradePanel;
        public GameObject TradeInfo10;
        public GameObject MemoryArt;
        public GameObject MemoryInfo;
        public GameObject Memory_Crash;
        public GameObject AFKLoad;
        public GameObject TradeInfo;

        [Header("Logs system")]

        public GameObject StartContent;
        public GameObject WalliPs;
        public GameObject StartText;
        public GameObject LoadPart;

        [Header("System dialogs")]

        public GameObject TextManager;
        public GameObject TextManager_2;
        public GameObject TextManager_3;
        public GameObject MadnessDialog;
        public GameObject TextAM;
        public GameObject DialogAM;
        public GameObject ScanText;
        public GameObject ScanMeditation;
        public GameObject MemoryDialog;
        public GameObject MemoryT;
        public GameObject AchText;
        public GameObject AchVell;
        public GameObject MeditationDialog;
        public GameObject LastMemory;
        public GameObject InfoB_1;
        public GameObject InfoB_2;
        public GameObject InfoB_3;
        public GameObject InfoB_4;
        public GameObject InfoB_5;
        public GameObject SystemErorBase;

        [Header("Scan terrain")]

        public GameObject AnalisTerrain_1;
        public GameObject LoadTerrain_2;
        public GameObject LoadTerrain3;
        public GameObject LoadTerrain4;
        public GameObject AnalisTerrain5;
        public GameObject AnalisTerrain6;
        public GameObject InfoValerian;
        public GameObject InfoScan;
        public GameObject ScanMA;
        public GameObject ScanMB;
        public GameObject ScanMC;
        public GameObject ScanMD;
        public GameObject InfoBaseBtn_1;
        public GameObject InfoBaseBtn_2;
        public GameObject InfoBaseBtn_3;
        public GameObject InfoBaseBtn_4;
        public GameObject InfoBaseBtn_5;
        public GameObject InfoBaseBtn_6;

        [Header("Panel components")]

        public GameObject ErorCardii;
        public GameObject LoadArrow;
        public GameObject LoadArrow_2;
        public GameObject ErorCardii2;
        public GameObject LoadArrow3;
        public GameObject LoadArrow_4;
        public GameObject CardiiInfo_1;
        public GameObject CardiiInfo_2;
        public GameObject ImpInfo_1;
        public GameObject ImpInfo_2;
        public GameObject CardiiInfo_3;
        public GameObject CardiiInfo_4;
        public GameObject ImpInfo_3;
        public GameObject ImpInfo_4;
        public GameObject InfoText, InfoText10;
        public GameObject LeftText, LeftText10;
        public GameObject RightText, RightText10;

        [Header("Clouds")]

        public GameObject PartCloud;
        public GameObject PartCloud2;
        public GameObject PartCloud3;
        public GameObject PartCloud4;
        public GameObject PartCloud5;

        [Header("Other Dead panels")]

        public GameObject Dp;
        public GameObject DP_under;
        public GameObject Dp_under2;
        public GameObject Dp_platoforms;
        public GameObject Dp_Press;
        public GameObject Dp_Part9;
        public GameObject Dp_part12;

        public GameObject Dp_btn_1;
        public GameObject Dp_btn_2;
        public GameObject Dp_btn_3;
        public GameObject Dp_btn_4;
        public GameObject Dp_btn_5;
        public GameObject Dp_btn_6;
        public GameObject Dp_btn_7;

        public GameObject TeleportPanel;

        [Header("Main UI elements")]

        public GameObject Sf;
        public GameObject JBd;
        public GameObject J;
        public GameObject Mp;
        public GameObject TouchBar;
        public GameObject Camera1;
        public GameObject Camera2;
        public GameObject Jump2;
        public GameObject JBd2;

        [Header("Obj flashpoints")]

        public GameObject FlashPoint5;
        public GameObject FlashPoint6;
        public GameObject FlashPoint7;
        public GameObject FlashPoint8;

        [Header("Triggers obj")]

        public GameObject CameraRig;
        public GameObject CameraRig2;
        public GameObject CameraRig3;
        public GameObject LightTrigger;
        public GameObject LightFalse;
        public GameObject LightNo;
        public GameObject PartTrigger;
        public GameObject Part_12Trigger;
        public GameObject TriggerPlatform;

        [Header("DISK ID")]
        public GameObject IconID_1;
        public GameObject IconID_2;
        public GameObject IconID_3;
        public GameObject IconID_4;
        public GameObject IconID_5;
        public GameObject IconID_6;
        public GameObject IconID_7;
        public GameObject IconID_8;
        public GameObject IconID_9;
        public GameObject IconID_10;
        public GameObject IconID_11;
        public GameObject IconID_12;
        public GameObject IconID_13;
        public GameObject IconID_14;
        public GameObject IconID_15;
        public GameObject IconID_16;
        public GameObject IconID_17;
        public GameObject IconID_18;
        public GameObject IconID_19;
        public GameObject IconID_20;

        [Header("Obj platforms")]

        public GameObject Platform1;
        public GameObject Platform2;
        public GameObject Platform3;
        public GameObject Platform4;
        public GameObject SecretPlatform;

        [Header("Obj parts location")]

        public GameObject Part1;
        public GameObject Part2;
        public GameObject Part3;
        public GameObject Part4_1;
        public GameObject Part4_2;
        public GameObject Part4_3;
        public GameObject Part4_4;
        public GameObject Part5;
        public GameObject Part6;
        public GameObject Part7;
        public GameObject Part8;
        public GameObject Part9Secret;
        public GameObject Part9;
        public GameObject Part_91;
        public GameObject Part10;
        public GameObject Part_10_1;
        public GameObject Part_10_2;
        public GameObject Part_11;
        public GameObject Part_11_1;
        public GameObject Part_11_2;
        public GameObject Part_11_3;
        public GameObject Part_12;
        public GameObject Part_12_1;
        public GameObject Part_12_1_1_1;
        public GameObject Part_12_1_2_1;
        public GameObject Part_12_2;
        public GameObject Part_12_3;
        public GameObject Part_12_4;
        public GameObject Part_12_5;
        public GameObject Part_13;
        public GameObject Part_13_1;
        public GameObject Part_13_2;
        public GameObject Part_13_3;
        public GameObject Part_13_4;
        public GameObject Part_13_5;
        public GameObject Part_13_6;
        public GameObject Part_13_7;
        public GameObject Part_13_8;
        public GameObject Part_13_9;
        public GameObject Part_13_10;
        public GameObject Part_13_11;
        public GameObject Part_13_12;
        public GameObject Part_13_13;
        public GameObject Part_13_14;
        public GameObject Part_13_15;
        public GameObject Part_13_16;
        public GameObject Part_13_17;
        public GameObject Part_13_18;
        public GameObject Part_14;
        public GameObject Part_14_1;
        public GameObject Part_14_2;
        public GameObject Part_15;

        [Header("StopCollissions")]

        public GameObject SkyBox8_Stop;
        public GameObject SkyBox9_Stop;
        public GameObject SkyBox_Stop10;
        public GameObject SkyBox_Stop9_3;
        public GameObject Collision5;
        public GameObject Collision6;
        public GameObject Collision7;
        public GameObject ColliseStop1;
        public GameObject ColliseStop2;
        public GameObject ColliseStop3;
        public GameObject ColliderBox;
        public GameObject ColliderB;
        public GameObject ColliderC;
        public GameObject CollisePart12;
        public GameObject CollisePart12_1;

        [Header("SpawnPoints")]

        public GameObject SpawnPosition;
        public GameObject SpawnPoint_2;
        public GameObject SpawnPoint_3;
        public GameObject SpawnPoint_4;
        public GameObject SpawnPoint_5;
        public GameObject SpawnPoint_6;

        [Header("AudioClips animations")]

        public AudioClip[]
        footsteps_terrain,
        sound_falling,
        sound_jump,
        sound_dead,
        sound_damage,
        sound_alive;

        [Header("AudioContent")]

        public AudioClip OnLight_1;
        public AudioClip OnCamera;
        public AudioClip Hurt_all;
        public AudioClip CardiiTakes;
        public AudioClip HealthBox;
        public AudioClip Alive_1;
        public AudioClip TeleportSound;
        public AudioClip LoadingG;
        public AudioClip FallSound;
        public AudioClip DeadSound;
        public AudioClip UpdateMech;
        public AudioClip RebornSound;
        public AudioClip ChangeChucky;
        public AudioClip DestroySound;

        [Header("AudioSource")]

        [SerializeField] public AudioSource ChuckySource_1;
        [SerializeField] public AudioSource ChuckySource_2;

        [Header("Play games Elements")]

        private const string CardiiCollect = "CgkIhqrP0qYJEAIQAg";
        private const string InfoChecker = "CgkIhqrP0qYJEAIQAw";
        private const string TakeImp = "CgkIhqrP0qYJEAIQBA";
        private const string TradeWithChucky = "CgkIhqrP0qYJEAIQBQ";
        private const string MadnessOn = "CgkIhqrP0qYJEAIQBg";
        private const string LastMemoryPlant = "CgkIhqrP0qYJEAIQBw";
        private const string Part1End = "CgkIhqrP0qYJEAIQCA";

        [Header("ADS_appodeal Controller")]

        private const string APP_KEY = "b5e53e3288cba260595d625c78ef0f20be877a365950ac5c";

        string m_ReachabilityText;

        private bool _canJumping; //может ли персонаж прыгать или нет, в зависимости от флажка.

        private float _vertical, _horizontal, _run;

        private readonly string STR_VERTICAL = "Vertical";

        private readonly string STR_HORIZONTAL = "Horizontal";

        private readonly string STR_JUMP = "Jump";

        private const float DISTANCE_OFFSET_CAMERA = 5f;

        private CharacterController _controller;

        private Animator _animator;

        private Vector3 _direction;

        private Quaternion _look; //данные, для осуществления поворотов персонажа.

        private Vector3 TargetRotate => _camera.forward * DISTANCE_OFFSET_CAMERA; //значение текущего направления движения

        private bool Idle => _horizontal == 0.0f && _vertical == 0.0f; //переменная флажок, при которой будет выключенно вращение гг в состоянии ожидания.

        public bool Precached = false;
        public bool FinishedAds = false;
        private bool PrecacheInter = false;
        private bool FinishedIntersticial = false;

        void Start()
        {
            _controller = GetComponent<CharacterController>(); //считываем и записываем контроллер
            _animator = GetComponent<Animator>();

            Cursor.visible = _characteristics.VisibleCursor; //проверка состояния курсора в зависимость от параметра в контейнере.

            if (Madness == 0 && second >= 0)
            {
                StartText.SetActive(true);
                TextManager.SetActive(true);
                gameObject.GetComponent<CharacterController>().enabled = false;
            }

            GamePart = PlayerPrefs.GetInt("GamePart", GamePart);
            _animator.SetTrigger("Alive");

            HP = 0.65f;

            Sf.SetActive(false);
            Mp.SetActive(false);
            TouchBar.SetActive(false);
            CardiiImg.SetActive(false);
            ImpulseImg.SetActive(false);
            MadnessImg.SetActive(false);
            SpawnPosition.SetActive(false);
            ChuckyState_1.SetActive(false);
            ChuckyState_2.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(RebornSound, 1f);

            if (PlayerPrefs.HasKey("UnityGraphicsQuality"))
            {
                GraphicSettigs = PlayerPrefs.GetInt("UnityGraphicsQuality");
                JoystickRL = PlayerPrefs.GetInt("Controllers");

                print("Графические компоненты успешно загружены в вашу систему!");
            }

            if (!PlayerPrefs.HasKey("UnityGraphicsQuality"))
            {
                print("Ошибка, графические компоненты не найдены!");
            }

            NoAds = (PlayerPrefs.GetInt("NoAds") != 0);

            ChangeControllers();
            LoadOtherElements();
            Initialized(true);

            HP_Change.interactable = false;
        }

        void Update()
        {
            Movement();
            Rotate();

            ImpulseModule.text = ImpulseInt.ToString();
            CardiiText.text = CardiiInt.ToString();

            ImpulseTr.text = ImpulseInt.ToString();
            CardiiTr.text = CardiiInt.ToString();

            ImpulseTr2.text = ImpulseInt.ToString();
            CardiiTr2.text = CardiiInt.ToString();

            ImpulseChaky2.text = ImpulseTrade.ToString();
            CardiiChaky2.text = CardiiTrade.ToString();

            ImpulseChaky.text = ImpulseTrade.ToString();
            CardiiChaky.text = CardiiTrade.ToString();

            MadnessText.text = Madness.ToString();

            UIHP.fillAmount = HP;

            if (CardiiInt >= 80)
            {
                GetAchivement(CardiiCollect);
            }

            if (ImpulseInt >= 8)
            {
                GetAchivement(TakeImp);
            }

            if (FriendChucky >= 20)
            {
                GetAchivement(TradeWithChucky);
            }

            if (Part1_End == 1)
            {
                GetAchivement(Part1End);
            }

            for (int i = 0; i < lives.Length; i++) //в планах.
            {
                if (i < HealthL)
                {
                    lives[i].sprite = FullLive;
                }
                else
                {
                    lives[i].sprite = EmptyLive;
                }

                if (i < NumberOfLives)
                {
                    lives[i].enabled = true;
                }
                else
                {
                    lives[i].enabled = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger(STR_JUMP);
                _direction.y += _characteristics.JumpForce;
                _canJumping = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
            {
                AudioListener.volume = 0.75f;
                Pp.SetActive(true);
                Time.timeScale = 0;
                IsPaused = true;
                _canJumping = false;
                gameObject.GetComponent<CharacterController>().enabled = false;

                ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false); Jump2.SetActive(false); JBd2.SetActive(false);
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused)
            {
                AudioListener.volume = 1f;
                Pp.SetActive(false);
                Time.timeScale = 1;
                IsPaused = false;
                _canJumping = false;
                gameObject.GetComponent<CharacterController>().enabled = true;

                _joystick2._handlePos = Vector2.zero;
                _joystick2._input = Vector2.zero;
                _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                _joystick._handlePos = Vector2.zero;
                _joystick._input = Vector2.zero;
                _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                if (JoystickRL == 1)
                {
                    Jump2.SetActive(false);
                    JBd2.SetActive(false);
                    J.SetActive(true);
                    JBd.SetActive(true);
                }

                else if (JoystickRL == 0)
                {
                    Jump2.SetActive(true);
                    JBd2.SetActive(true);
                    J.SetActive(false);
                    JBd.SetActive(false);
                }

                ZeroHP.SetActive(true); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
            }

            if (Madness <= 0 && minute == 4)
            {
                _animator.SetBool("Dead", true);
            }

            Timer();

            if (second == -3000)
            {
                AchText.SetActive(false);
                AFKLoad.SetActive(false);
                StartContent.SetActive(false);
                StartText.SetActive(false);
                TextManager.SetActive(false);
                MadnessDialog.SetActive(false);
                StartText.SetActive(false);
            }
        }

        //ADS
        public void Initialized(bool isTesting)
        {
            Appodeal.setTesting(isTesting);
            Appodeal.setNonSkippableVideoCallbacks(this);
            Appodeal.setInterstitialCallbacks(this);
            Appodeal.muteVideosIfCallsMuted(true);
            Appodeal.initialize(APP_KEY, Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.INTERSTITIAL);
        }

        public void GetAchivement(string id)
        {
            Social.ReportProgress(id, 100.0f, (bool success) => { });
        }

        public void SaveUIElements()
        {
            PlayerPrefs.SetInt("Cardii", CardiiInt); // Cохранение.
            PlayerPrefs.SetInt("Impulse", ImpulseInt);
            PlayerPrefs.SetInt("Madness", Madness);
            PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
            PlayerPrefs.SetInt("Controllers", JoystickRL);
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        public void LoadGameSettings()
        {
            PlayerPrefs.SetInt("Controllers", JoystickRL);
            PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
            PlayerPrefs.Save();
        }

        public void LoadOtherElements()
        {
            Madness = PlayerPrefs.GetInt("Madness");
            FriendChucky = PlayerPrefs.GetInt("FriendShipChucky");

            if (PlayerPrefs.HasKey("Controllers"))
            {
                JoystickRL = PlayerPrefs.GetInt("Controllers");
                print("Система контроллеров теперь работает исправно!");
            }

            if (!PlayerPrefs.HasKey("Controllers"))
            {
                print("Интеграция системы контроллеров завершилась ошибкой!");
            }
        }

        public void EnabledJumping(bool isActive) => _canJumping = isActive; //смена состояния прыжка.

        private void Movement()
        {
            if (_controller.isGrounded) //если контроллер на земле, то.
            {
                PlayAnimation();

                if (JoystickRL == 1)
                {
                    _horizontal = _joystick.Horizontal; //записывание значений джойстика по горизонтали и вертикали.
                    _vertical = _joystick.Vertical;
                }

                else if (JoystickRL == 0)
                {
                    _horizontal = _joystick2.Horizontal;
                    _vertical = _joystick2.Vertical;
                }

                _direction = transform.TransformDirection(_horizontal, 0, _vertical).normalized; //считывание вектора направления движения по камере гг..

                Jump();
            }

            _direction.y -= _characteristics.Gravity * Time.deltaTime; //отнимаем у вектора движения "у" значения гравитации, для притягивания гг к игровой поверхности.

            float speed = _run * _characteristics.RunSpeed + _characteristics.MovementSpeed;   //вычисление скорости перемещения персонажа в зависимости от стандартных клавиш.
            Vector3 dir = _direction * speed * Time.deltaTime; //умножаем наше направление на стандартную скорость перемещения.

            dir.y = _direction.y; //записывание старого значение по оси у.

            _controller.Move(dir); //перемещение контроллера.
        }

        private void Rotate()
        {
            if (Idle) return;

            Vector3 target = TargetRotate; //точка, в которую должен повернуться персонаж
            target.y = 0; //обнуление вектора по оси у, чтобы персонаж вращался правильно, а не как долбоёб.

            _look = Quaternion.LookRotation(target); //вычисление скорости поворота и поворачивание самого гг.

            float speed = _characteristics.AngularSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _look, speed);
        }

        private void Jump()
        {
            if (_canJumping)
            {
                _animator.SetTrigger(STR_JUMP);
                _direction.y += _characteristics.JumpForce; //прибавление силы прыжка в значении направления по оси у.
                _canJumping = false;
                Input.GetAxis(STR_JUMP);
            }
        }

        private void PlayAnimation() //метод проигрывания анимации движения.
        {
            float horizontal = _run * _horizontal + _horizontal;
            float vertical = _run * _vertical + _vertical;

            _animator.SetFloat(STR_VERTICAL, _vertical);
            _animator.SetFloat(STR_HORIZONTAL, _horizontal);
            //вычисление скорости перемещения, для проигрывания анимации.
        }

        public void PlayFootSteps(int k)
        {
            GetComponent<AudioSource>().clip = footsteps_terrain[UnityEngine.Random.Range(0, footsteps_terrain.Length)];
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

        public void PlayDamageSound(int d)
        {
            GetComponent<AudioSource>().clip = sound_damage[UnityEngine.Random.Range(0, sound_damage.Length)];
            GetComponent<AudioSource>().Play();
        }

        public void PlayAliveSound(int r)
        {
            GetComponent<AudioSource>().clip = sound_alive[UnityEngine.Random.Range(0, sound_alive.Length)];
            GetComponent<AudioSource>().Play();
        }

        public void Timer()
        {
            second += 1 * Time.deltaTime;

            if (Madness >= 1)
            {
                StartText.SetActive(false);
                TextManager.SetActive(false);
                StartContent.SetActive(false);
                AchText.SetActive(false);
                MadnessDialog.SetActive(true);

                gameObject.GetComponent<CharacterController>().enabled = true;
            }

            if (Madness <= 0)
            {
                if (second >= 3)
                {
                    StartText.SetActive(false);
                    TextManager.SetActive(false);
                    StartContent.SetActive(true);
                    GetComponent<AudioSource>().volume = 0.3f;

                    gameObject.GetComponent<CharacterController>().enabled = true;
                }

                if (minute == 2)
                {
                    StartText.SetActive(false);
                    TextManager.SetActive(false);
                    StartContent.SetActive(false);
                    AchText.SetActive(true);
                    Ps_Cardii.SetActive(true);
                    CardiiAvatar1.SetActive(true);

                    CardiiInt = 5;
                }

                if (minute == 4)
                {
                    AchText.SetActive(false);
                    StartText.SetActive(false);
                    TextManager.SetActive(false);
                    StartContent.SetActive(false);
                    AFKLoad.SetActive(true);
                }

                if (second >= 60)
                {
                    minute += 1;
                    second = 0;
                }
            }
        }

        public void RightControl()
        {
            JoystickRL = 0;

            PlayerPrefs.SetInt("Controllers", JoystickRL);
            PlayerPrefs.Save();
        }

        public void LeftControl()
        {
            JoystickRL = 1;

            PlayerPrefs.SetInt("Controllers", JoystickRL);
            PlayerPrefs.Save();
        }

        public void ChangeControllers()
        {
            if (JoystickRL == 1)
            {
                Jump2.SetActive(false);
                JBd2.SetActive(false);
                J.SetActive(true);
                JBd.SetActive(true);
            }

            else if (JoystickRL == 0)
            {
                Jump2.SetActive(true);
                JBd2.SetActive(true);
                J.SetActive(false);
                JBd.SetActive(false);
            }
        }

        public IEnumerator PartFall_1(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(Part1.gameObject);
        }

        public IEnumerator PartFall_2(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(Part2.gameObject);
            Destroy(Part3.gameObject);
            Destroy(Part4_1.gameObject);
            Destroy(Part4_2.gameObject);
            Destroy(Part4_3.gameObject);
            Destroy(Part4_4.gameObject);
        }

        public IEnumerator PartFall_3(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(Part5.gameObject);
            Destroy(Part6.gameObject);
            Destroy(Part7.gameObject);
        }

        public IEnumerator PartFall_4(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            Destroy(Part_10_1.gameObject);
            Destroy(Part_10_2.gameObject);
            Destroy(Part_11.gameObject);
            Destroy(Part_11_1.gameObject);
            Destroy(Part_11_2.gameObject);
            Destroy(Part_11_3.gameObject);
        }

        public IEnumerator DeletePrefabVox(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            GameObject[] game_object = GameObject.FindGameObjectsWithTag("VoxelModel");

            foreach (GameObject element in game_object)
            {
                Destroy(element);
            }

            GetComponent<AudioSource>().PlayOneShot(DestroySound, 0.5f);
        }


        public void OnTriggerStay(Collider other)
        {
            if (other.tag == "InteractB")
            {
                AnalisTerrain_1.SetActive(true);
                TextManager_2.SetActive(false);
                LoadPart.SetActive(false);

                IconID_1.SetActive(false);
            }

            if (other.tag == "InteractA")
            {
                LoadTerrain_2.SetActive(true);
                TextManager_2.SetActive(false);
                LoadPart.SetActive(false);

                IconID_2.SetActive(false);
            }

            if (other.tag == "InteractC")
            {
                LoadTerrain3.SetActive(true);
                TextManager_2.SetActive(false);
                LoadPart.SetActive(false);

                IconID_3.SetActive(false);
            }

            if (other.tag == "InteractD")
            {
                LoadTerrain4.SetActive(true);
                TextAM.SetActive(false);
                ScanText.SetActive(true);

                IconID_4.SetActive(false);
            }

            if (other.tag == "InteractE")
            {
                IconID_5.SetActive(false);

                if (CardiiInt < 15)
                {
                    AnalisTerrain5.SetActive(true);
                }
            }

            if (other.tag == "InteractF")
            {
                IconID_6.SetActive(false);

                AnalisTerrain6.SetActive(true);
            }

            if (other.tag == "InfoPart")
            {
                InfoValerian.SetActive(true);
                ScanText.SetActive(false);
                WalliPs.SetActive(false);

                IconID_7.SetActive(false);
            }

            if (other.tag == "MemoryPlantA")
            {
                ScanMA.SetActive(true);
                FlashPoint8.SetActive(true);

                IconID_8.SetActive(false);
            }

            if (other.tag == "MemoryPlantB")
            {
                FlashPoint6.SetActive(true);
                ScanMB.SetActive(true);

                IconID_9.SetActive(false);
            }

            if (other.tag == "MemoryPlantC")
            {
                ScanMC.SetActive(true);
                FlashPoint7.SetActive(true);

                IconID_10.SetActive(false);
            }

            if (other.tag == "MemoryPlantD")
            {
                ScanMD.SetActive(true);
                FlashPoint5.SetActive(true);

                IconID_11.SetActive(false);
            }

            if (other.tag == "InfoBase_1")
            {
                InfoBaseBtn_1.SetActive(true);

                IconID_12.SetActive(false);
            }

            if (other.tag == "InfoBase_2")
            {
                InfoBaseBtn_2.SetActive(true);

                IconID_13.SetActive(false);
            }

            if (other.tag == "InfoBase_3")
            {
                InfoBaseBtn_3.SetActive(true);

                IconID_16.SetActive(false);
            }

            if (other.tag == "InfoBase_4")
            {
                InfoBaseBtn_4.SetActive(true);

                IconID_17.SetActive(false);
            }

            if (other.tag == "InfoBase_5")
            {
                InfoBaseBtn_5.SetActive(true);

                IconID_18.SetActive(false);
            }

            if (other.tag == "SystemEror")
            {
                InfoBaseBtn_6.SetActive(true);

                IconID_19.SetActive(false);
            }

            if (other.tag == "LastMemory")
            {
                LastMemory.SetActive(true);
            }

            if (other.tag == "MeditationState")
            {
                IconID_15.SetActive(false);

                ScanMeditation.SetActive(true);
                _animator.SetBool("Dead", true);
            }

            if (other.tag == "TradePart")
            {
                CardiiImg.SetActive(true);

                if (FriendChucky >= 10)
                {
                    TradeInfo10.SetActive(true);
                    TradeInfo.SetActive(false);
                    IconID_20.SetActive(false);
                }

                if (FriendChucky <= 0)
                {
                    TradeInfo.SetActive(true);
                    TradeInfo10.SetActive(false);
                    IconID_14.SetActive(false);
                }
            }

            if (other.tag == "Dead")
            {
                HP -= Time.deltaTime / 15.0f;
                _animator.SetBool("GetDamage", true);

                if (ImpulseInt > 0)
                {
                    ImpulseInt -= 1;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar2.SetActive(true);
                    HP += 0.35f;

                    GetComponent<AudioSource>().PlayOneShot(Alive_1, 1f);
                }

                if (HP <= 0)
                {
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 0)
                    {
                        Dp.SetActive(true);
                        Dp_btn_1.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp.SetActive(true);
                        Dp_btn_1.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    Jump2.SetActive(false); JBd2.SetActive(false);

                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);

                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;

                    CardiiInt = 0;

                }
            }

            if (other.tag == "Dead_under")
            {
                HP -= Time.deltaTime / 3f;

                if (HP <= 0)
                {
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false);
                    SpawnPosition.SetActive(true);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        DP_under.SetActive(true);
                        Dp_btn_2.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        DP_under.SetActive(true);
                        Dp_btn_2.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    Jump2.SetActive(false); JBd2.SetActive(false);

                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);

                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;

                    CardiiInt = 0;
                }
            }

            if (other.tag == "Dead_under2")
            {
                HP -= Time.deltaTime / 3f;
                SpawnPoint_2.SetActive(true);

                if (HP <= 0)
                {
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false); TextManager_3.SetActive(false); ScanText.SetActive(false); MemoryInfo.SetActive(false); MemoryDialog.SetActive(false); MemoryT.SetActive(false);

                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        Dp_under2.SetActive(true);
                        Dp_btn_3.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp_under2.SetActive(true);
                        Dp_btn_3.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    Jump2.SetActive(false);
                    JBd2.SetActive(false);

                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);
                    CardiiInt = 0;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;
                }
            }

            if (other.tag == "DeadPlatform")
            {
                HP -= Time.deltaTime / 2f;

                if (HP <= 0)
                {
                    TextManager_3.SetActive(false);
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false); MemoryInfo.SetActive(false); MemoryDialog.SetActive(false); MemoryT.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        Dp_platoforms.SetActive(true);
                        Dp_btn_4.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp_platoforms.SetActive(true);
                        Dp_btn_4.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    Jump2.SetActive(false);
                    JBd2.SetActive(false);
                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);
                    CardiiInt = 0;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;
                }
            }

            if (other.tag == "DeadPress")
            {
                HP -= Time.deltaTime / 0.01f;

                if (HP <= 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(DestroySound, 1f);
                    TextManager_3.SetActive(false);
                    gameObject.SetActive(false);
                    Instantiate(VellVoxel, transform.position, transform.rotation);
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false); MemoryInfo.SetActive(false); MemoryDialog.SetActive(false); MemoryT.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        Dp_Press.SetActive(true);
                        Dp_btn_5.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp_Press.SetActive(true);
                        Dp_btn_5.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    Jump2.SetActive(false); JBd2.SetActive(false);
                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);
                    CardiiInt = 0;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;
                }
            }

            if (other.tag == "DeadPart9")
            {
                HP -= Time.deltaTime / 3f;

                if (HP <= 0)
                {
                    TextManager_3.SetActive(false); MeditationDialog.SetActive(false);
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false); MemoryInfo.SetActive(false); MemoryDialog.SetActive(false); MemoryT.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        Dp_Part9.SetActive(true);
                        Dp_btn_6.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp_Part9.SetActive(true);
                        Dp_btn_6.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);
                    CardiiInt = 0;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;
                }
            }

            if (other.tag == "DeadPart12")
            {
                HP -= Time.deltaTime / 3f;

                if (HP <= 0)
                {
                    TextManager_3.SetActive(false); MeditationDialog.SetActive(false);
                    TextManager.SetActive(false); TextManager_2.SetActive(false); TextAM.SetActive(false); ScanText.SetActive(false); MemoryInfo.SetActive(false); MemoryDialog.SetActive(false); MemoryT.SetActive(false);
                    SpawnPoint_5.SetActive(true);
                    gameObject.GetComponent<CharacterController>().enabled = false;

                    if (ImpulseInt >= 1)
                    {
                        Dp_part12.SetActive(true);
                        Dp_btn_7.SetActive(true);
                    }

                    else if (ImpulseInt <= 0)
                    {
                        Dp_part12.SetActive(true);
                        Dp_btn_7.SetActive(false);
                    }

                    ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false); Jump2.SetActive(false); JBd2.SetActive(false);
                    _animator.SetBool("GetDamage", false);
                    _animator.SetBool("Dead", true);
                    CardiiInt = 0;
                    ImpulseAvatar.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);

                    AudioListener.volume = 0.25f;
                }
            }

            if (other.tag == "FastTeleport")
            {
                HP -= Time.deltaTime / 600f;
                _animator.SetBool("GetDamage", true);

                if (HP <= 0)
                {
                    Dp.SetActive(false);
                    TeleportPanel.SetActive(true);
                    GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
                    gameObject.GetComponent<CharacterController>().enabled = false;
                    TextAM.SetActive(false); ScanText.SetActive(false); TextManager.SetActive(false); TextManager_2.SetActive(false); ZeroHP.SetActive(false); TouchBar.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd2.SetActive(false);
                    Jump2.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); Mp.SetActive(false); C.SetActive(false);
                    AudioListener.volume = 0.25f;
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "InteractA")
            {
                LoadTerrain_2.SetActive(false);

                IconID_2.SetActive(false);
            }

            if (other.tag == "InteractB")
            {
                AnalisTerrain_1.SetActive(false);

                IconID_1.SetActive(false);
            }

            if (other.tag == "InteractC")
            {
                LoadTerrain3.SetActive(false);

                IconID_3.SetActive(false);
            }

            if (other.tag == "InteractD")
            {
                LoadTerrain4.SetActive(false);

                IconID_4.SetActive(false);
            }

            if (other.tag == "InteractE")
            {
                AnalisTerrain5.SetActive(false);

                IconID_5.SetActive(false);
            }

            if (other.tag == "InteractF")
            {
                AnalisTerrain6.SetActive(false);

                IconID_6.SetActive(false);
            }

            if (other.tag == "InfoPart")
            {
                InfoValerian.SetActive(false);

                IconID_7.SetActive(false);
            }

            if (other.tag == "MemoryPlantA")
            {
                ScanMA.SetActive(false);
                FlashPoint8.SetActive(false);

                IconID_8.SetActive(false);
            }

            if (other.tag == "MemoryPlantB")
            {
                ScanMB.SetActive(false);
                FlashPoint6.SetActive(false);

                IconID_9.SetActive(false);
            }

            if (other.tag == "MemoryPlantC")
            {
                ScanMC.SetActive(false);
                FlashPoint7.SetActive(false);

                IconID_10.SetActive(false);
            }

            if (other.tag == "MemoryPlantD")
            {
                ScanMD.SetActive(false);
                FlashPoint5.SetActive(false);

                IconID_11.SetActive(false);
            }

            if (other.tag == "MeditationState")
            {
                ScanMeditation.SetActive(false);
                _animator.SetBool("Dead", false);

                IconID_15.SetActive(false);
            }

            if (other.tag == "TradePart")
            {
                TradeInfo.SetActive(false);
                CardiiImg.SetActive(false);
                TradeInfo10.SetActive(false);
                ImpulseImg.SetActive(false);

                IconID_14.SetActive(false);
                IconID_20.SetActive(false);
            }

            if (other.tag == "Dead")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "Dead_under")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "Dead_under2")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "DeadPlatform")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "DeadPress")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "FastTeleport")
            {
                _animator.SetBool("GetDamage", false);
            }

            if (other.tag == "InfoBase_1")
            {
                InfoBaseBtn_1.SetActive(false);

                IconID_12.SetActive(false);
            }

            if (other.tag == "InfoBase_2")
            {
                InfoBaseBtn_2.SetActive(false);

                IconID_13.SetActive(false);
            }

            if (other.tag == "InfoBase_3")
            {
                InfoBaseBtn_3.SetActive(false);

                IconID_16.SetActive(false);
            }

            if (other.tag == "InfoBase_4")
            {
                InfoBaseBtn_4.SetActive(false);

                IconID_17.SetActive(false);
            }

            if (other.tag == "InfoBase_5")
            {
                InfoBaseBtn_5.SetActive(false);

                IconID_18.SetActive(false);
            }

            if (other.tag == "SystemEror")

            {
                InfoBaseBtn_6.SetActive(false);

                IconID_19.SetActive(false);
            }

            if (other.tag == "LastMemory")
            {
                LastMemory.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider col)
        {
            Ps_Cardii.SetActive(false);
            Ps_Cardii2.SetActive(false);
            Ps_Imp.SetActive(false);

            if (col.tag == "CardiiBox")
            {
                CardiiImg.SetActive(true);
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
            }

            if (col.tag == "CardiiTrigger")
            {
                CardiiInt += 5;

                Destroy(col.gameObject);

                Ps_Cardii.SetActive(true);
                InfoCardii.SetActive(true);
                Sf.SetActive(false);
                J.SetActive(false);
                JBd.SetActive(false);
                Jump2.SetActive(false);
                JBd2.SetActive(false);
                Mp.SetActive(false);
                ZeroHP.SetActive(false);
                HealthHide.SetActive(false);
                CardiiImg.SetActive(true);
                ImpulseImg.SetActive(false);
                TouchBar.SetActive(false);
                TextAM.SetActive(false);

                gameObject.GetComponent<CharacterController>().enabled = false;
                GetComponent<AudioSource>().PlayOneShot(CardiiTakes, 0.5f);

                _animator.SetBool("Idle2", true);
                CameraAnim.SetBool("CameraStaticAnim", true);
                CameraAnim.SetBool("Static", false);

                _joystick._handlePos = Vector2.zero;
                _joystick._input = Vector2.zero;
                _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero; //обнуляем значения джойстика

                if (CardiiInt == 10)
                {
                    CardiiAvatar2.SetActive(true);
                }

                if (CardiiInt == 5)
                {
                    CardiiAvatar1.SetActive(true);
                    CardiiAvatar2.SetActive(false);
                }
            }

            if (col.tag == "CardiiPlant")
            {
                if (CardiiInt >= 15)
                {
                    CardiiInt += 10;
                    Ps_Cardii.SetActive(true);
                    CardiiAvatar1.SetActive(true);
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().PlayOneShot(CardiiTakes, 0.5f);
                    LastPlant.SetActive(true);
                    WalliPs.SetActive(true);
                    SecretPlatform.SetActive(true);
                    ScanText.SetActive(false);
                    InfoValerian.SetActive(false);
                    GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.3f);

                    GetAchivement(LastMemoryPlant);
                }
            }

            if (col.tag == "Medicine")
            {
                if (HP < 1)
                {
                    Ps_Imp.SetActive(true);
                    HP += 0.35f;
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().PlayOneShot(HealthBox, 0.75f);
                }

                else if (HP >= 1)
                {
                    ImpulseImg.SetActive(true);
                    ImpulseInt += 1;
                    Destroy(col.gameObject);
                    GetComponent<AudioSource>().PlayOneShot(HealthBox, 0.75f);

                    if (ImpulseInt == 1)
                    {
                        Ps_Imp.SetActive(true);
                        ImpulseAvatar.SetActive(true);
                        CardiiAvatar2.SetActive(false);
                    }
                    else if (ImpulseInt >= 2)
                    {
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp_2.SetActive(true);
                        ImpulseAvatar.SetActive(true);
                        Ps_Imp.SetActive(true);
                        CardiiAvatar1.SetActive(false);
                        CardiiAvatar2.SetActive(false);
                    }
                }
            }

            if (col.tag == "ImpulseTrigger")
            {
                if (HP < 1)
                {
                    HP += 0.35f;
                    Destroy(col.gameObject);

                    ImpulseAvatar.SetActive(false);
                    Ps_Imp.SetActive(true);
                    InfoImpulse.SetActive(true);
                    Sf.SetActive(false);
                    J.SetActive(false);
                    JBd.SetActive(false);
                    JBd2.SetActive(false);
                    Jump2.SetActive(false);
                    Mp.SetActive(false);
                    ZeroHP.SetActive(false);
                    HealthHide.SetActive(false);
                    CardiiImg.SetActive(false);
                    ImpulseImg.SetActive(false);
                    TouchBar.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;
                    GetComponent<AudioSource>().PlayOneShot(HealthBox, 0.75f);
                    TextAM.SetActive(false);
                    _animator.SetBool("Idle2", true);
                    CameraAnim.SetBool("CameraStaticAnim", true);
                    CameraAnim.SetBool("Static", false);

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;
                }

                else if (HP >= 1)
                {
                    ImpulseInt += 1;
                    Destroy(col.gameObject);

                    InfoImpulse.SetActive(true);
                    Sf.SetActive(false);
                    J.SetActive(false);
                    JBd.SetActive(false);
                    JBd2.SetActive(false);
                    Jump2.SetActive(false);
                    Mp.SetActive(false);
                    ZeroHP.SetActive(false);
                    HealthHide.SetActive(false);
                    CardiiImg.SetActive(false);
                    ImpulseImg.SetActive(true);
                    TouchBar.SetActive(false);
                    gameObject.GetComponent<CharacterController>().enabled = false;
                    GetComponent<AudioSource>().PlayOneShot(HealthBox, 0.75f);
                    TextAM.SetActive(false);
                    _animator.SetBool("Idle2", true);
                    CameraAnim.SetBool("CameraStaticAnim", true);
                    CameraAnim.SetBool("Static", false);

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero; //обнуляем значения джойстика
                }
                if (CardiiInt >= 5 && ImpulseInt == 1)
                {
                    Ps_Imp.SetActive(true);
                    ImpulseAvatar.SetActive(true);
                    CardiiAvatar2.SetActive(false);
                    ImpulseAvatar2.SetActive(false);
                    CardiiAvatar1.SetActive(true);
                }

                if (CardiiInt <= 0 && ImpulseInt >= 2)
                {
                    Ps_Imp.SetActive(true);
                    Ps_Imp_2.SetActive(true);
                    ImpulseAvatar.SetActive(true);
                    CardiiAvatar2.SetActive(false);
                    ImpulseAvatar2.SetActive(true);
                    CardiiAvatar1.SetActive(false);
                }
            }

            if (col.tag == "FalseTouch")
            {
                TouchBar.SetActive(false);
            }

            if (col.tag == "CameraTrigger")
            {
                GetComponent<AudioSource>().PlayOneShot(OnCamera, 1);
                GetComponent<AudioSource>().volume = 1f;
                HP_Change.interactable = true;
                FriendChucky = FriendChucky * 0;

                second = -3000; minute = 0; AchText.SetActive(false); Sf.SetActive(true); AFKLoad.SetActive(false); TouchBar.SetActive(true); Mp.SetActive(true); TextManager.SetActive(false); TextManager_2.SetActive(true); LoadPart.SetActive(true); StartText.SetActive(false); Camera1.SetActive(true); Camera2.SetActive(false); CameraRig.SetActive(false); CameraRig2.SetActive(false); CameraRig3.SetActive(false);

                if (PlayerPrefs.HasKey("Part1End"))
                {
                    Part1_End = PlayerPrefs.GetInt("Part1End");

                    FirstPersonCamera.SetActive(true);
                    ThirdPersonCamera.SetActive(true);

                    Madness = 0;
                    MadnessDialog.SetActive(false);

                    CameraAnim.SetBool("CameraStaticAnim", true);
                }

                if (!PlayerPrefs.HasKey("Part1End"))
                {
                    Part1_End = PlayerPrefs.GetInt("Part1End");

                    FirstPersonCamera.SetActive(false);
                    ThirdPersonCamera.SetActive(true);

                    Madness = 0;
                    MadnessDialog.SetActive(false);

                    CameraAnim.SetBool("CameraStaticAnim", false);
                }

                if (Madness >= 1)
                {
                    Madness = 0;
                    MadnessDialog.SetActive(false);
                }

                if (Madness <= 0)
                {
                    AchText.SetActive(false);
                    AFKLoad.SetActive(false);
                    StartContent.SetActive(false);
                    StartText.SetActive(false);
                    TextManager.SetActive(false);
                    MadnessDialog.SetActive(false);
                    StartText.SetActive(false);
                }
            }

            if (col.tag == "Light")
            {
                TextManager_2.SetActive(false); TextManager.SetActive(false); StartText.SetActive(false); LoadPart.SetActive(false); Platform1.SetActive(true); Platform2.SetActive(true); Platform3.SetActive(true); Platform4.SetActive(true); LightTrigger.SetActive(false);
            }

            if (col.tag == "LightOff")
            {
                GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.3f);
                TextAM.SetActive(true); PartCloud2.SetActive(true); Part2.SetActive(true); CardiiBox3.SetActive(true); LightFalse.SetActive(false);
            }

            if (col.tag == "PartTrigger")
            {
                Part3.SetActive(true);
                Part4_1.SetActive(true);
                Part4_2.SetActive(true);
                Part4_3.SetActive(true);
                Part4_4.SetActive(true);
            }

            if (col.tag == "LightFalse")
            {
                Part_1_.isKinematic = false;

                Time.timeScale = 1;

                Destroy(Platform1.gameObject);
                Destroy(Platform2.gameObject);
                Destroy(Platform3.gameObject);
                Destroy(Platform4.gameObject);

                StartCoroutine(PartFall_1(5f));

                PartTrigger.SetActive(false); PartCloud.SetActive(false); SkyBox9_Stop.SetActive(true); SkyBox8_Stop.SetActive(true); LightNo.SetActive(false);
                GetComponent<AudioSource>().PlayOneShot(FallSound, 1f);
            }

            if (col.tag == "TextOff")
            {
                WalliPs.SetActive(false);
                ScanText.SetActive(false);
                TextAM.SetActive(false);
                DialogAM.SetActive(false);
                PartCloud3.SetActive(true);
                Part5.SetActive(true);
                Part6.SetActive(true);
                Part7.SetActive(true);
            }

            if (col.tag == "DeadWay")
            {
                Part_3_.isKinematic = false;
                Part_4_.isKinematic = false;
                Part_4_1_.isKinematic = false;
                Part_4_2_.isKinematic = false;
                Part_4_3_.isKinematic = false;
                Part_4_4_.isKinematic = false;

                Collision5.SetActive(true);
                Collision6.SetActive(true);
                Collision7.SetActive(true);

                Time.timeScale = 1;

                StartCoroutine(PartFall_2(5f));

                Destroy(SecretPlatform.gameObject);

                TriggerPlatform.SetActive(false);

                Part8.SetActive(true);
                Part9Secret.SetActive(true);
                Part9.SetActive(true);
                Part_91.SetActive(true);
                Part10.SetActive(true);
                Part_10_1.SetActive(true);
                Part_10_2.SetActive(true);
                Part_11_1.SetActive(true);
                Part_11_2.SetActive(true);
                Part_11_3.SetActive(true);
                Part_12.SetActive(true);
                Part_12_1.SetActive(true);
                Part_12.SetActive(true);
                Part_12_1.SetActive(true);
                Part_12_2.SetActive(true);
                Part_12_3.SetActive(true);
                Part_12_4.SetActive(true);
                Part_12_5.SetActive(true);
                Part_13.SetActive(true);

                PartCloud2.SetActive(false);

                GetComponent<AudioSource>().PlayOneShot(FallSound, 1f);
            }

            if (col.tag == "Falling")
            {
                DeadPlatform1.isKinematic = false;
                DeadPlatform1.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.75f);
            }

            if (col.tag == "FallPart2")
            {
                DeadPlatform2.isKinematic = false;
                DeadPlatform2.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.75f);
            }

            if (col.tag == "MeditationEnd")
            {
                MedPlatform1.isKinematic = false;
                MedPlatform1.useGravity = true;
                MedPlatform2.isKinematic = false;
                MedPlatform2.useGravity = true;
                MedPlatform3.isKinematic = false;
                MedPlatform3.useGravity = true;
                MedPlatform4.isKinematic = false;
                MedPlatform4.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.6f);
            }

            if (col.tag == "MeditationClose")
            {
                MedPlatform5.isKinematic = false;
                MedPlatform5.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.6f);
            }

            if (col.tag == "FallPart3")
            {
                DeadPlatform3.isKinematic = false;
                DeadPlatform3.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.4f);
            }

            if (col.tag == "Bye")
            {
                DragPlatform_1.isKinematic = false;
                DragPlatform_1.useGravity = true;
            }

            if (col.tag == "Bye_1")
            {
                DragPlatform_2.isKinematic = false;
                DragPlatform_2.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.7f);
            }

            if (col.tag == "Bye_2")
            {
                DragPlatform_3.isKinematic = false;
                DragPlatform_3.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.7f);
            }

            if (col.tag == "Bye_3")
            {
                DragPlatform_4.isKinematic = false;
                DragPlatform_4.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.7f);
            }

            if (col.tag == "Bye_4")
            {
                DragPlatform_5.isKinematic = false;
                DragPlatform_5.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.7f);
            }

            if (col.tag == "Bye_5")
            {
                DragPlatform_6.isKinematic = false;
                DragPlatform_6.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 0.7f);
            }

            if (col.tag == "PartEnd")
            {
                Part13.isKinematic = false;
                Part13.useGravity = true;
                Part14.isKinematic = false;
                Part14.useGravity = true;
                Part15.isKinematic = false;
                Part15.useGravity = true;

                J.SetActive(false);
                JBd.SetActive(false);
                JBd2.SetActive(false);
                Jump2.SetActive(false);
                HealthHide.SetActive(false);
                ZeroHP.SetActive(false);
                TouchBar.SetActive(false);
                Mp.SetActive(false);
                Sf.SetActive(false);

                FirstPersonCamera.SetActive(true);
                ThirdPersonCamera.SetActive(true);
                EyesVell.SetActive(true);

                GetComponent<AudioSource>().PlayOneShot(DeadSound, 0.75f);

                Part1_End += 1;
            }

            if (col.tag == "EndingPart_1")
            {
                PlayerPrefs.SetInt("Cardii", CardiiInt);
                PlayerPrefs.SetInt("Impulse", ImpulseInt);
                PlayerPrefs.SetInt("Madness", Madness);
                PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                PlayerPrefs.SetInt("Part1End", Part1_End);
                PlayerPrefs.SetInt("Controllers", JoystickRL);
                PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
                PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                SceneManager.LoadScene("LoadMenu");
                PlayerPrefs.Save();
                GetAchivement(Part1End);

                print("Первая глава успешно завершена!");
            }

            if (col.tag == "UpperWay")
            {
                CardiiAvatar1.SetActive(true);
                Ps_Cardii.SetActive(true);
                CardiiImg.SetActive(true);
                CardiiInt += 5;
                Destroy(col.gameObject);
                GetComponent<AudioSource>().PlayOneShot(CardiiTakes, 0.5f);

                if (ImpulseInt >= 3)
                {
                    ColliderBox.SetActive(true);
                    ColliderB.SetActive(true);
                    ColliderC.SetActive(true);
                    AchVell.SetActive(true);
                }
            }

            if (col.tag == "Part10Stopers")
            {

                SpawnPoint_4.SetActive(true);

                PartCloud4.SetActive(true);
                PartCloud3.SetActive(false);

                StartCoroutine(PartFall_3(5f));

                ColliseStop1.SetActive(true);
                ColliseStop2.SetActive(true);
                ColliseStop3.SetActive(true);

                Part_5_.isKinematic = false;
                Part_6_.isKinematic = false;
                Part_7_.isKinematic = false;

                Part_10_1.SetActive(true);
                Part_10_2.SetActive(true);
                Part_11.SetActive(true);
                Part_11_1.SetActive(true);
                Part_11_2.SetActive(true);
                Part_11_3.SetActive(true);
                Part_12.SetActive(true);
                Part_12_1_1_1.SetActive(true);
                Part_12_1_2_1.SetActive(true);

                GetComponent<AudioSource>().PlayOneShot(FallSound, 1f);

                ChuckySource_1.volume = 0.35f;

                SkyBox_Stop9_3.SetActive(false);
            }

            if (col.tag == "ChuckySoure_1")
            {
                ChuckySource_1.volume = 0f;
            }

            if (col.tag == "FinallyWay")
            {
                StartCoroutine(PartFall_4(5f));

                Part_9_.isKinematic = false;
                Part_9_3_.isKinematic = false;
                Part_10_.isKinematic = false;
                Part_10_1_.isKinematic = false;
                Part_10_2_.isKinematic = false;
                Part_11_.isKinematic = false;
                Part_11_1_.isKinematic = false;
                Part_11_2_.isKinematic = false;
                Part_11_3_.isKinematic = false;

                Part_12.SetActive(true);
                Part_12_1.SetActive(true);
                Part_12_2.SetActive(true);
                Part_12_3.SetActive(true);
                Part_12_4.SetActive(true);
                Part_12_5.SetActive(true);
                Part_13.SetActive(true);
                Part_13_1.SetActive(true);
                Part_13_2.SetActive(true);
                Part_13_3.SetActive(true);
                Part_13_4.SetActive(true);
                Part_13_5.SetActive(true);
                Part_13_6.SetActive(true);
                Part_13_7.SetActive(true);
                Part_13_8.SetActive(true);
                Part_13_9.SetActive(true);
                Part_13_10.SetActive(true);
                Part_13_11.SetActive(true);
                Part_13_12.SetActive(true);
                Part_13_13.SetActive(true);
                Part_13_14.SetActive(true);
                Part_13_15.SetActive(true);
                Part_13_16.SetActive(true);
                Part_13_17.SetActive(true);
                Part_13_18.SetActive(true);
                Part_14.SetActive(true);
                Part_14_1.SetActive(true);
                Part_14_2.SetActive(true);
                Part_15.SetActive(true);

                PartCloud4.SetActive(false);
                PartCloud5.SetActive(true);

                SpawnPoint_5.SetActive(true);

                CollisePart12.SetActive(true);
                CollisePart12_1.SetActive(true);

                Part_12Trigger.SetActive(false);

                GetComponent<AudioSource>().PlayOneShot(FallSound, 1f);

                ChuckySource_1.volume = 0f;
                ChuckySource_2.volume = 0.35f;
            }

            if (col.tag == "ByeVell")
            {
                Part_12_1_1.isKinematic = false;
                Part_12_1_1.useGravity = true;
                Part_12_1_2.isKinematic = false;
                Part_12_1_2.useGravity = true;
                Part_12_1_3.isKinematic = false;
                Part_12_1_3.useGravity = true;
                Part_12_1_4.isKinematic = false;
                Part_12_1_4.useGravity = true;
                Part_12_1_5.isKinematic = false;
                Part_12_1_5.useGravity = true;

                GetComponent<AudioSource>().PlayOneShot(FallSound, 1f);
            }
        }

        public void ChangeCameras()
        {
            if (Part1_End >= 1)
            {
                FirstPersonCamera.SetActive(false);
                ThirdPersonCamera.SetActive(true);
            }

            else if (Part1_End <= 0)
            {
                print("Дополнительное действие не обнаружено!");

            }
        }

        public void ADSContinue()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    _canJumping = false;
                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        HP += 0.3f;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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

                    HP = 1;
                    _canJumping = false;
                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        HP += 0.3f;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }
        }


        public void Continue()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 0)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPosition.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); DP_under.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 0)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPosition.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); DP_under.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }
        }

        public void ContinueUnder()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {

                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_2.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_under2.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_2.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_under2.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }

        }

        public void ContinuePlatforms()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_3.transform.position;
                        StartCoroutine(StopVellAlive(3f));

                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_platoforms.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_3.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_platoforms.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }
        }

        public void ContinuePressF()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        gameObject.SetActive(true);
                        VellVoxel.SetActive(false);
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_3.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_Press.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }

                    if (HP >= 1)
                    {
                        gameObject.SetActive(true);
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_3.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_Press.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }
        }

        public void ContinuePart9()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {

                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_4.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_Part9.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_4.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(false);
                        Ps_Imp_2.SetActive(true);
                        ZeroHP.SetActive(true); Dp_Part9.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);
                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }
                    }
                }
            }
        }

        public void ContinuePart12()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_5.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(true);
                        Ps_Imp_2.SetActive(false);
                        ZeroHP.SetActive(true); Dp_part12.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }

                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;
                    }
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
                    HP = 1;
                    Ps_1.SetActive(false);
                    _canJumping = false;

                    _joystick._handlePos = Vector2.zero;
                    _joystick._input = Vector2.zero;
                    _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    _joystick2._handlePos = Vector2.zero;
                    _joystick2._input = Vector2.zero;
                    _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                    if (ImpulseInt >= 1)
                    {
                        ImpulseInt -= 1;
                    }


                    if (HP >= 1)
                    {
                        Ps_1.SetActive(true);
                        gameObject.transform.position = SpawnPoint_5.transform.position;
                        StartCoroutine(StopVellAlive(3f));
                        ImpulseAvatar.SetActive(false);
                        ImpulseAvatar2.SetActive(true);
                        Ps_Imp.SetActive(true);
                        Ps_Imp_2.SetActive(false);
                        ZeroHP.SetActive(true); Dp_part12.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); Sf.SetActive(true); Mp.SetActive(true);

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }

                        _animator.SetBool("Dead", false);
                        _animator.SetTrigger("Alive");
                        GetComponent<AudioSource>().PlayOneShot(Alive_1, 0.5f);
                        AudioListener.volume = 1;
                    }
                }
            }
        }


        public void TeleportSettungs()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    HP = 1;
                    _canJumping = false;

                    if (HP >= 1)
                    {
                        Ps_Teleport.SetActive(true);

                        ImpulseAvatar.SetActive(false);
                        CardiiAvatar1.SetActive(false);
                        CardiiAvatar2.SetActive(false);

                        ImpulseInt = 0;
                        CardiiInt = 0;

                        _animator.SetBool("GetDamage", false);

                        gameObject.transform.position = SpawnPoint_6.transform.position;
                        TeleportPanel.SetActive(false); ZeroHP.SetActive(true); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true); Sf.SetActive(true); JBd.SetActive(true); J.SetActive(true); Mp.SetActive(true);
                        StartCoroutine(StopVellAlive(3f));

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }

                        PartCloud5.SetActive(true);
                        PartCloud4.SetActive(false);
                        PartCloud.SetActive(false);
                        PartCloud2.SetActive(false);
                        PartCloud3.SetActive(false);

                        CollisePart12.SetActive(true);
                        CollisePart12_1.SetActive(true);
                        Part_12Trigger.SetActive(false);

                        Part_12.SetActive(true);
                        Part_12_1.SetActive(true);
                        Part_12_1_1_1.SetActive(true);
                        Part_12_1_2_1.SetActive(true);
                        Part_12_2.SetActive(true);
                        Part_12_3.SetActive(true);
                        Part_12_4.SetActive(true);
                        Part_12_5.SetActive(true);
                        Part_13.SetActive(true);
                        Part_13_1.SetActive(true);
                        Part_13_2.SetActive(true);
                        Part_13_3.SetActive(true);
                        Part_13_4.SetActive(true);
                        Part_13_5.SetActive(true);
                        Part_13_6.SetActive(true);
                        Part_13_7.SetActive(true);
                        Part_13_8.SetActive(true);
                        Part_13_9.SetActive(true);
                        Part_13_10.SetActive(true);
                        Part_13_11.SetActive(true);
                        Part_13_12.SetActive(true);
                        Part_13_13.SetActive(true);
                        Part_13_14.SetActive(true);
                        Part_13_15.SetActive(true);
                        Part_13_16.SetActive(true);
                        Part_13_17.SetActive(true);
                        Part_13_18.SetActive(true);
                        Part_14.SetActive(true);
                        Part_14_1.SetActive(true);
                        Part_14_2.SetActive(true);
                        Part_15.SetActive(true);

                        GetComponent<AudioSource>().PlayOneShot(TeleportSound, 0.5f);

                        _animator.SetTrigger("Alive");

                        _joystick._handlePos = Vector2.zero;
                        _joystick._input = Vector2.zero;
                        _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                        AudioListener.volume = 1f;
                    }
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
                    HP = 1;
                    _canJumping = false;

                    if (HP >= 1)
                    {
                        Ps_Teleport.SetActive(true);

                        ImpulseAvatar.SetActive(false);
                        CardiiAvatar1.SetActive(false);
                        CardiiAvatar2.SetActive(false);

                        ImpulseInt = 0;
                        CardiiInt = 0;

                        _animator.SetBool("GetDamage", false);
                        gameObject.transform.position = SpawnPoint_6.transform.position;
                        TeleportPanel.SetActive(false); ZeroHP.SetActive(true); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(true); HealthHide3.SetActive(true); Sf.SetActive(true); Mp.SetActive(true); Sf.SetActive(true); JBd.SetActive(true); J.SetActive(true); Mp.SetActive(true);
                        StartCoroutine(StopVellAlive(3f));

                        if (JoystickRL == 1)
                        {
                            Jump2.SetActive(false);
                            JBd2.SetActive(false);
                            J.SetActive(true);
                            JBd.SetActive(true);
                        }

                        else if (JoystickRL == 0)
                        {
                            Jump2.SetActive(true);
                            JBd2.SetActive(true);
                            J.SetActive(false);
                            JBd.SetActive(false);
                        }

                        PartCloud.SetActive(false);
                        PartCloud2.SetActive(false);
                        PartCloud3.SetActive(false);
                        PartCloud4.SetActive(false);
                        PartCloud5.SetActive(true);

                        Part_12.SetActive(true);
                        Part_12_1.SetActive(true);
                        Part_12_1_1_1.SetActive(true);
                        Part_12_1_2_1.SetActive(true);
                        Part_12_2.SetActive(true);
                        Part_12_3.SetActive(true);
                        Part_12_4.SetActive(true);
                        Part_12_5.SetActive(true);
                        Part_13.SetActive(true);
                        Part_13_1.SetActive(true);
                        Part_13_2.SetActive(true);
                        Part_13_3.SetActive(true);
                        Part_13_4.SetActive(true);
                        Part_13_5.SetActive(true);
                        Part_13_6.SetActive(true);
                        Part_13_7.SetActive(true);
                        Part_13_8.SetActive(true);
                        Part_13_9.SetActive(true);
                        Part_13_10.SetActive(true);
                        Part_13_11.SetActive(true);
                        Part_13_12.SetActive(true);
                        Part_13_13.SetActive(true);
                        Part_13_14.SetActive(true);
                        Part_13_15.SetActive(true);
                        Part_13_16.SetActive(true);
                        Part_13_17.SetActive(true);
                        Part_13_18.SetActive(true);
                        Part_14.SetActive(true);
                        Part_14_1.SetActive(true);
                        Part_14_2.SetActive(true);
                        Part_15.SetActive(true);

                        CollisePart12.SetActive(true);
                        CollisePart12_1.SetActive(true);
                        Part_12Trigger.SetActive(false);

                        GetComponent<AudioSource>().PlayOneShot(TeleportSound, 0.5f);

                        _animator.SetTrigger("Alive");

                        _joystick._handlePos = Vector2.zero;
                        _joystick._input = Vector2.zero;
                        _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

                        AudioListener.volume = 1f;
                    }
                }

            }
        }

        public void ExitGame()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();

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
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();
                }
            }
        }

        public void MeditationStop()
        {
            _animator.SetBool("Dead", false);
        }

        public void ExitPanel()
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
            _animator.SetBool("GetDamage", false);
            HP = 1;
        }

        public void ExitInfobase()
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
            _animator.SetBool("Idle2", false);
            CameraAnim.SetBool("CameraStaticAnim", false);
            CameraAnim.SetBool("Static", true);
            TouchBar.SetActive(true);

            _joystick._handlePos = Vector2.zero;
            _joystick._input = Vector2.zero;
            _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

            _joystick2._handlePos = Vector2.zero;
            _joystick2._input = Vector2.zero;
            _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

        }

        public void ExitInfobaseVell()
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
            DialogAM.SetActive(true);
        }

        public void ExitMemoryScan()
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
            MemoryDialog.SetActive(true);
        }

        public void SelectScan()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoScan.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void SelectMemory()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            MemoryArt.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }
        public void SelectM()
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
            MemoryT.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void SelectInfoM()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            MemoryInfo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void CrashSystem()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            Memory_Crash.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void InfoBase_1()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoB_1.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void InfoBase_2()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoB_2.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void InfoBase_3()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoB_3.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void InfoBase_4()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoB_4.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void InfoBase_5()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            InfoB_5.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
        }

        public void SystemEror()
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
            SystemErorBase.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(LoadingG, 0.5f);
            GetAchivement(InfoChecker);
        }

        public void RestartSystem()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    gameObject.GetComponent<CharacterController>().enabled = true;
                    SceneManager.LoadScene("CrossBar");
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.Save();
                    GetAchivement(MadnessOn);
                    Madness = 1;
                }
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                if (Precached == true)
                {
                    if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                        Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    gameObject.GetComponent<CharacterController>().enabled = true;
                    SceneManager.LoadScene("CrossBar");
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.Save();
                    GetAchivement(MadnessOn);
                    Madness = 1;
                }
            }
        }

        public void SystemMadness()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {

                    Madness = 1;

                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.Save();
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
                    Madness = 1;

                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.Save();
                }
            }
        }

        public void PauseOn()
        {
            AudioListener.volume = 0.75f;
            Pp.SetActive(true);
            Time.timeScale = 0;
            IsPaused = true;
            gameObject.GetComponent<CharacterController>().enabled = false;

            ZeroHP.SetActive(false); TouchBar.SetActive(false); HealthHide.SetActive(false); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Health.SetActive(false); Sf.SetActive(false); JBd.SetActive(false); J.SetActive(false); JBd2.SetActive(false); Jump2.SetActive(false); Mp.SetActive(false); C.SetActive(false);
        }

        public void ContinuePp()
        {
            AudioListener.volume = 1f;
            Pp.SetActive(false);
            Time.timeScale = 1;
            IsPaused = false;
            gameObject.GetComponent<CharacterController>().enabled = true;

            _joystick2._handlePos = Vector2.zero;
            _joystick2._input = Vector2.zero;
            _joystick2._stickImage.rectTransform.anchoredPosition = Vector2.zero;

            _joystick._handlePos = Vector2.zero;
            _joystick._input = Vector2.zero;
            _joystick._stickImage.rectTransform.anchoredPosition = Vector2.zero;

            if (JoystickRL == 1)
            {
                Jump2.SetActive(false);
                JBd2.SetActive(false);
                J.SetActive(true);
                JBd.SetActive(true);
            }

            else if (JoystickRL == 0)
            {
                Jump2.SetActive(true);
                JBd2.SetActive(true);
                J.SetActive(false);
                JBd.SetActive(false);
            }

            ZeroHP.SetActive(true); Dp_under2.SetActive(false); TouchBar.SetActive(true); HealthHide.SetActive(true); HealthHide2.SetActive(false); HealthHide3.SetActive(false); Sf.SetActive(true); Mp.SetActive(true);
        }

        public void GoToMenu()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();

                    Time.timeScale = 1;
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
                    Time.timeScale = 1;
                    ;
                    SceneManager.LoadScene("Menu");
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();

                }
            }
        }

        public void M_madness()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                print("Ошибка интернет соединения, пожалуйста повторите попытку позже!");
            }

            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
                {
                    Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
                }

                if (FinishedAds == true)
                {
                    Time.timeScale = 1;

                    SceneManager.LoadScene("Menu");
                    GetAchivement(MadnessOn);
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();
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

                    Time.timeScale = 1;

                    SceneManager.LoadScene("Menu");
                    GetAchivement(MadnessOn);
                    PlayerPrefs.SetInt("Madness", Madness);
                    PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
                    PlayerPrefs.SetInt("Controllers", JoystickRL);
                    PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
                    PlayerPrefs.Save();
                }
            }
        }

        public void Setting()
        {
            AudioListener.volume = 0.75f;
            Time.timeScale = 1;
        }

        public void ChangeCardii()
        {
            if (CardiiInt >= 25)
            {
                CardiiTrade += 25;
                CardiiInt -= 25;
                Ps_Cardii.SetActive(true);
                Ps_Cardii2.SetActive(true);
                LoadArrow.SetActive(true);
                ErorCardii.SetActive(false);
                CardiiInfo_1.SetActive(true);
                CardiiInfo_2.SetActive(true);
            }

            else if (CardiiInt <= 20)
            {
                ErorCardii.SetActive(true);
                LoadArrow.SetActive(false);
                CardiiInfo_1.SetActive(false);
                CardiiInfo_2.SetActive(false);
                ImpInfo_1.SetActive(false);
                ImpInfo_2.SetActive(false);
            }
        }

        public void ChangeCardii10()
        {
            if (CardiiInt >= 20)
            {
                CardiiTrade += 20;
                CardiiInt -= 20;
                Ps_Cardii.SetActive(true);
                Ps_Cardii2.SetActive(true);
                LoadArrow3.SetActive(true);
                ErorCardii2.SetActive(false);
                CardiiInfo_3.SetActive(true);
                CardiiInfo_4.SetActive(true);
            }

            else if (CardiiInt <= 15)
            {
                ErorCardii2.SetActive(true);
                LoadArrow3.SetActive(false);
                CardiiInfo_3.SetActive(false);
                CardiiInfo_4.SetActive(false);
                ImpInfo_3.SetActive(false);
                ImpInfo_4.SetActive(false);
            }
        }

        public void ChangeImp()
        {
            if (CardiiTrade >= 25)
            {
                ImpulseTrade += 1;
                CardiiTrade -= 25;
                Ps_Cardii.SetActive(true);
                Ps_Cardii2.SetActive(true);
                ErorCardii.SetActive(false);
                ImpInfo_1.SetActive(true);
                ImpInfo_2.SetActive(true);
                LoadArrow_2.SetActive(true);
            }

            else if (ImpulseTrade == 0)
            {
                ErorCardii.SetActive(true);
                LoadArrow.SetActive(false);
                LoadArrow_2.SetActive(false);
                Ps_Imp.SetActive(false);
                ImpInfo_1.SetActive(false);
                ImpInfo_2.SetActive(false);
            }
        }

        public void ChangeImp10()
        {
            if (CardiiTrade >= 20)
            {
                ImpulseTrade += 1;
                CardiiTrade -= 20;
                Ps_Cardii.SetActive(true);
                Ps_Cardii2.SetActive(true);
                ErorCardii2.SetActive(false);
                ImpInfo_3.SetActive(true);
                ImpInfo_4.SetActive(true);
                LoadArrow_4.SetActive(true);
            }

            else if (ImpulseTrade == 0)
            {
                ErorCardii2.SetActive(true);
                LoadArrow3.SetActive(false);
                LoadArrow_4.SetActive(false);
                Ps_Imp.SetActive(false);
                ImpInfo_3.SetActive(false);
                ImpInfo_4.SetActive(false);
            }
        }

        public IEnumerator StopVellAlive(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.GetComponent<CharacterController>().enabled = true;

            StartCoroutine(DeletePrefabVox(3f));
        }

        public IEnumerator StopChucky(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            ChuckySource_1.volume = 0f;
            Destroy(ChuckyObj.gameObject);
            ChuckyAnim.SetBool("ByeAnim", false);
            ChuckyDestroyed = 1;
        }

        public IEnumerator StopChucky_1(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            ChuckySource_2.volume = 0f;
            Destroy(ChuckyObj_2.gameObject);
            ChuckyAnim_1.SetBool("ByeAnim", false);
        }

        public void TakeImpulse()
        {
            if (ChuckyDestroyed >= 1)
            {
                StartCoroutine(StopChucky_1(15f));
            }
            else StartCoroutine(StopChucky(15f));

            if (ImpulseTrade >= 1)
            {
                ImpulseInt += ImpulseTrade;
                ImpulseTrade = 0;
                ImpulseImg.SetActive(true);

                if (CardiiInt <= 0 && ImpulseInt >= 2)
                {
                    ImpulseAvatar2.SetActive(true);
                    Ps_Imp_2.SetActive(true);
                    ImpulseAvatar.SetActive(true);
                    Ps_Imp.SetActive(true);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);
                }

                if (CardiiInt <= 0 && ImpulseInt <= 1)
                {
                    ImpulseAvatar.SetActive(false);
                    ImpulseAvatar2.SetActive(true);
                    Ps_Imp_2.SetActive(true);
                    Ps_Imp.SetActive(false);
                    CardiiAvatar1.SetActive(false);
                    CardiiAvatar2.SetActive(false);
                }

                else if (CardiiInt == 5 && ImpulseInt == 1)
                {
                    ImpulseAvatar2.SetActive(false);
                    ImpulseAvatar.SetActive(true);
                    Ps_Imp.SetActive(true);
                    Ps_Imp_2.SetActive(false);
                    CardiiAvatar1.SetActive(true);
                    Ps_Cardii.SetActive(true);
                    CardiiAvatar2.SetActive(false);
                }
            }
        }

        public void BruhTrader()
        {
            FriendChucky += 10;

            PlayerPrefs.SetInt("FriendShipChucky", FriendChucky);
            PlayerPrefs.Save();

            if (FriendChucky <= 10)
            {
                ChuckyState_1.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(ChangeChucky, 1f);
                ChuckyAnim.SetBool("ByeAnim", true);
            }

            else if (FriendChucky >= 20)
            {
                ChuckyState_2.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(ChangeChucky, 1f);
                ChuckyAnim_1.SetBool("ByeAnim", true);

            }
        }

        public void FriendShip10()
        {
            if (FriendChucky >= 10 && CardiiInt >= 20)
            {
                TradePanel.SetActive(true);
                TradeOffer.SetActive(false);
                InfoText10.SetActive(true);
                LeftText10.SetActive(true);
                RightText10.SetActive(true);
            }
        }

        public void FriendShip0()
        {
            if (FriendChucky <= 10)
            {
                TradeOffer.SetActive(true);
                TradePanel.SetActive(false);
                InfoText.SetActive(true);
                LeftText.SetActive(true);
                RightText.SetActive(true);
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
            PrecacheInter = isPrecacheInter;
            PrecacheInter = true;
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
