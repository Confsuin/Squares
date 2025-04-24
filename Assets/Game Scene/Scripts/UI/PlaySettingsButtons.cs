using UnityEngine;

public class PlaySettingsButtons : MonoBehaviour
{
    public GameObject PlaySettings;

    public void StartGame()
    {
        Destroy(PlaySettings);
    }
}
