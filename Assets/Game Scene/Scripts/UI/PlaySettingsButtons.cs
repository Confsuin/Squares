using UnityEngine;

public class PlaySettingsButtons : MonoBehaviour
{
    public GameObject PlaySettings;
    public GameObject Player1;
    public GameObject Player2;

    void Start()
    {
        
    }

    public void StartGame()
    {
        Time.timeScale = 1;

        Destroy(PlaySettings);

        Player1.SetActive(true);
        Player2.SetActive(true);
    }
}
