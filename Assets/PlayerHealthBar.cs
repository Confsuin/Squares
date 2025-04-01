using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // References
    private Player1 player1;
    private Player2 player2;

    private Slider PlayerHealthBarSlider;

    void Start()
    {
        GetReferences();
    }
    private void UpdateHealthBar()
    {
        if (player1 != null)
        {

        }
        if (player2 != null)
        {

        }
    }
    private void GetReferences()
    {
        if (this.transform.parent.tag == "Player")
        {
            player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
            //GameObject.this.GetComponenet<PlayerHealthBar>();
        }
        if (this.transform.parent.tag == "PlayerAlt")
        {
            player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        }
    }
}
