using UnityEngine;

public class ChangeServer : MonoBehaviour
{
    private string s = "https://vk.com/vell_story";

    public void ClickServer()
    {
        Application.OpenURL(s);
    }
}
