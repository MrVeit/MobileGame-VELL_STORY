using UnityEngine;
using UnityEngine.UI;

public class PauseSetting : MonoBehaviour
{
	[Header("COMPONENTS PAUSEP")]

	[SerializeField] public Slider AudioSlider;
	[SerializeField] public Slider MusicSetting;

	[Range(0f, 1f)]
	public float MusicFloat = 0.25f;
	[Range(0f, 1f)]
	public float SoundFloat = 0.25f;
	[Range(0, 5)]
	public int GraphicSettigs;
	[Range(0, 1)]
	public int FShow = 1;

	[Header("OBJ PANELS")]

	public GameObject Pp;
	public GameObject Ps;
	public GameObject ShowF;

	[Header("AUDIO CONTROLLER")]

	[SerializeField] public AudioSource MusicSlider;
	[SerializeField] public AudioSource VellSource;
	[SerializeField] public AudioSource PlatformSource;
	[SerializeField] public AudioSource PlatformSource2;
	[SerializeField] public AudioSource PlatformSource3;
	[SerializeField] public AudioSource PlatformSource4;
	[SerializeField] public AudioSource PlatformSource5;
	[SerializeField] public AudioSource PlatformSource6;
	[SerializeField] public AudioSource PlatformSource7;
	[SerializeField] public AudioSource PlatformSource8;
	[SerializeField] public AudioSource PlatformSource9;
	[SerializeField] public AudioSource PlatformSource10;
	[SerializeField] public AudioSource PlatformSource11;
	[SerializeField] public AudioSource PlatformSource12;
	[SerializeField] public AudioSource ChuckySource;
	[SerializeField] public AudioSource FlashLightSource4;
	[SerializeField] public AudioSource FlashLightSource5;
	[SerializeField] public AudioSource FlashLightSource6;
	[SerializeField] public AudioSource FlashLightSource7;
	[SerializeField] public AudioSource VeiterioState;
	[SerializeField] public AudioSource ChuckySource_2;
	[SerializeField] public AudioSource Part_11_Sound;
	[SerializeField] public AudioSource Part_12_Sound;

	private void Start()
    {
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

    public void Volume()
	{
		VellSource.volume = AudioSlider.value;
		VeiterioState.volume = AudioSlider.value;
		ChuckySource.volume = AudioSlider.value;
		ChuckySource_2.volume = AudioSlider.value;

		PlatformSource.volume = AudioSlider.value;
		PlatformSource2.volume = AudioSlider.value;
		PlatformSource3.volume = AudioSlider.value;
		PlatformSource4.volume = AudioSlider.value;
		PlatformSource5.volume = AudioSlider.value;
		PlatformSource6.volume = AudioSlider.value;
		PlatformSource7.volume = AudioSlider.value;
		PlatformSource8.volume = AudioSlider.value;
		PlatformSource9.volume = AudioSlider.value;
		PlatformSource10.volume = AudioSlider.value;
		PlatformSource11.volume = AudioSlider.value;
		PlatformSource12.volume = AudioSlider.value;

		FlashLightSource4.volume = AudioSlider.value;
		FlashLightSource5.volume = AudioSlider.value;
		FlashLightSource6.volume = AudioSlider.value;
		FlashLightSource7.volume = AudioSlider.value;

		Part_11_Sound.volume = AudioSlider.value;
		Part_12_Sound.volume = AudioSlider.value;

		PlayerPrefs.SetFloat("SoundGame", AudioSlider.value);
	}

	public void Music()
    {
		MusicSlider.volume = MusicSetting.value;

		PlayerPrefs.SetFloat("MusicGame", MusicSetting.value);
	}

	public void FpsShowOn()
    {
		FShow = 1;
		ShowF.SetActive(true);
    }

	public void FpsShowOff()
    {
		FShow = 0;
		ShowF.SetActive(false);
    }

	public void ChangeFpsFrame()
    {
		if (FShow >= 1)
        {
			ShowF.SetActive(true);
        }

		else if (FShow <= 0)
		{
			ShowF.SetActive(false);
		}
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

	public void OnClickGame()
	{
		Ps.SetActive(false);
		Pp.SetActive(true);

		PlayerPrefs.SetFloat("SoundGame", AudioSlider.value);
		PlayerPrefs.SetInt("UnityGraphicsQuality", GraphicSettigs);
		PlayerPrefs.SetFloat("MusicGame", MusicSetting.value);
	}
}
