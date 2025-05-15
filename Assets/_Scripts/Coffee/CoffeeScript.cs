using UnityEngine;

public class CoffeeScript : MonoBehaviour
{
    public string url = "https://buymeacoffee.com/tekamuttmedia";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
