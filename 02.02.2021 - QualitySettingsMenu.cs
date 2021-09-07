using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QualitySettingsMenu : MonoBehaviour
{
	[Header("MAIN COMPONENTS")]

	[SerializeField] public Slider AudioSlider;
	[SerializeField] public Slider MusicSetting;

	[Header("AUDIO CONTROLLER")]

	[SerializeField] public AudioSource GrSound;
	[SerializeField] public AudioSource ClickSound;
	[SerializeField] public AudioSource AudioSource;
	[SerializeField] public AudioSource ChuckySource;
	[SerializeField] public AudioSource PortSource;
	[SerializeField] public AudioSource ModeSource;

	[Header("MUSIC CONTROLLER")]

	[SerializeField] public AudioSource MusicSound;

	[Range(0f, 1f)]
	public float MusicFloat = 0.25f;
	[Range(0f, 1f)]
	public float SoundFloat = 0.25f;
	[Range(0, 5)]
	public int GraphicSettigs;
	[Range(0, 3)]
	public int GameMode;

	private void Start()
	{
		if (PlayerPrefs.HasKey("UnityGraphicsQuality"))
		{
			GraphicSettigs = PlayerPrefs.GetInt("UnityGraphicsQuality");
			MusicSetting.value = PlayerPrefs.GetFloat("MusicMenu");
			AudioSlider.value = PlayerPrefs.GetFloat("SoundMenu");

			print("Игровые данные были успешно загружены в вашу систему!");
		}

		else if (!PlayerPrefs.HasKey("UnityGraphicsQuality"))
		{
			print("Ошибка в получении игровых настроек!");
		}
	}


    public void SoundV()
    {
		GrSound.volume = AudioSlider.value;
		ClickSound.volume = AudioSlider.value;
		AudioSource.volume = AudioSlider.value;
		ChuckySource.volume = AudioSlider.value;
		PortSource.volume = AudioSlider.value;
		ModeSource.volume = AudioSlider.value;

		PlayerPrefs.SetFloat("SoundMenu", AudioSlider.value);
	}

	public void MusicV()
	{
		MusicSound.volume = MusicSetting.value;

		PlayerPrefs.SetFloat("MusicMenu", MusicSetting.value);
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
	public void OnClickSave()
	{
		PlayerPrefs.SetFloat("SoundMenu", AudioSlider.value);
		PlayerPrefs.SetFloat("MusicMenu", MusicSetting.value);
		PlayerPrefs.Save();
	}

	public void OnClickMenu()
    {
		SceneManager.LoadScene("Menu");

		GameMode = 3;
		PlayerPrefs.SetInt("GameMode", GameMode);
	}		
}