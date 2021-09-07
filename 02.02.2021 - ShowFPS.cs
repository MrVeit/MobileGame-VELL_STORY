using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowFPS : MonoBehaviour
{
    [SerializeField] private Text _fpsInfo;
    private float _fps;

    private void Update()
    {
        float Fps = 1.0f / _fps;
        _fps += (Time.deltaTime - _fps) * 0.1f;
        _fpsInfo.text = Mathf.Ceil(Fps).ToString();

        switch (Fps)
        {
            case <=20:
                _fpsInfo.color = Color.red;
                break;
            case <=30:
                _fpsInfo.color = Color.yellow;
                break;
            case <=40:
                _fpsInfo.color = Color.white;
                break;
            case <=60:
                _fpsInfo.color = Color.magenta;
                break;
        }
    }
}