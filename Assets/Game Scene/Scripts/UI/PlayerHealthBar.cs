using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // References
    public Player1 player1;
    public Player2 player2;
    public TrainingDummy trainingDummy;

    public Slider PlayerHealthBarSlider;
    void Start()
    {
        GetReferences();
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        if (player1 != null)
        {
            PlayerHealthBarSlider.maxValue = player1.maxHealth;
            PlayerHealthBarSlider.value = player1.currentHealth;
        }
        if (player2 != null)
        {
            PlayerHealthBarSlider.maxValue = player2.maxHealth;
            PlayerHealthBarSlider.value = player2.currentHealth;
        }
        if (trainingDummy != null)
        {
            PlayerHealthBarSlider.maxValue = trainingDummy.maxHealth;
            PlayerHealthBarSlider.value = trainingDummy.currentHealth;
        }
    }
    private void GetReferences()
    {
        if (this.transform.parent.parent.tag == "Player")
        {
            player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        }
        if (this.transform.parent.parent.tag == "PlayerAlt")
        {
            player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        }
        if (this.transform.parent.parent.tag == "Training Dummy")
        {
            trainingDummy = GameObject.FindWithTag("Training Dummy").GetComponent<TrainingDummy>();
        }
        PlayerHealthBarSlider = this.GetComponent<Slider>();
    }
}
