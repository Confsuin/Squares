using UnityEngine;
using System.Collections;
using TMPro;

public class TrainingDummy : MonoBehaviour
{
    // Floats
    public float maxHealth;
    public float currentHealth;
    public float MissingHealth = 0;

    public float BulletPoisonDamage;

    // References
    public Player1 player1;
    public Player2 player2;

    //Status Effects
    public bool IsPoisoned = false;

    public PlayerHealthBar playerHealthBar;
    TMP_Text HealthText;

    public IEnumerator PoisonTimer()
    {
        Debug.Log("Player2 Poisoned");
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("Player2 Poision Damage Tick");
        PoisonDMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMG();
        IsPoisoned = false;
    }


    public void PoisonDMG()
    {
        if (IsPoisoned == true)
        {
            Debug.Log("Training Dummy Took Poison damage");
            BulletPoisonDamage = MissingHealth * player2.BulletPoison;
            TakeDamage(BulletPoisonDamage);
        }
    }
    public void StartPoisonTimer()
    {
        StartCoroutine(PoisonTimer());
    }
    public void TakeDamage(float DMG)
    {
        currentHealth -= DMG;
        MissingHealth = maxHealth - currentHealth;
        Debug.Log("Training Dummy took" + DMG + "damage. health: " + currentHealth);
        playerHealthBar.UpdateHealthBar();
    }
    private void GetRefrences()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        playerHealthBar = GameObject.FindWithTag("Player 1 Health Bar").GetComponent<PlayerHealthBar>();
    }
}
