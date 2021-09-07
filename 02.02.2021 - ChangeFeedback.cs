using UnityEngine;

public class ChangeFeedback : MonoBehaviour
{
    private string s = "https://play.google.com/store/apps/details?id=com.Veiteriogames.vellstory";

    public void ClickServer()
    {
        Application.OpenURL(s);
    }
}
