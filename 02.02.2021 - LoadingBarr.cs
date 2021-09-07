using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingBarr : MonoBehaviour
{
    [Header("Загружаемая сцена")]

    public int sceneID;

    [Header("Остальные объекты")]

    public GameObject SliderLoad;
    public Slider Slider;

    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01 (operation.progress / .9f);
            Slider.value = progress;
            yield return null;
        }
    }
}
