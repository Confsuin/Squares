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

    private GameObject hitIndicator;

    //Status Effects
    public bool IsInFireZone = false;

    public PlayerHealthBar playerHealthBar;
    TMP_Text HealthText;

    void Start()
    {
        currentHealth = maxHealth;
        GetRefrences();
        hitIndicator.SetActive(false);
    }
    private void UpdateHealthText()
    {
        HealthText.text = "" + currentHealth;
    }

    public IEnumerator PoisonTimerP1DMG()
    {
        Debug.Log("Player1 Poisoned");
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP1DMG();
    }
    public IEnumerator PoisonTimerP2DMG()
    {
        Debug.Log("Player2 Poisoned");
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
        yield return new WaitForSecondsRealtime(1f);
        PoisonDMGP2DMG();
    }


    public void PoisonDMGP1DMG()
    {
        Debug.Log("Player1 Took Poison damage");
        BulletPoisonDamage = MissingHealth * player1.BulletPoison;
        TakeDamage(BulletPoisonDamage);
    }
    public void PoisonDMGP2DMG()
    {
        Debug.Log("Player1 Took Poison damage");
        BulletPoisonDamage = MissingHealth * player2.BulletPoison;
        TakeDamage(BulletPoisonDamage);
    }
    public void StartPoisonTimerP1DMG()
    {
        StartCoroutine(PoisonTimerP1DMG());
    }
    public void StartPoisonTimerP2DMG()
    {
        StartCoroutine(PoisonTimerP2DMG());
    }
    IEnumerator HitIndicator()
    {
        hitIndicator.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        hitIndicator.SetActive(false);
    }
    public void TakeDamage(float DMG)
    {
        StartCoroutine(HitIndicator());
        currentHealth -= DMG;
        MissingHealth = maxHealth - currentHealth;
        Debug.Log("Training Dummy took" + DMG + "damage. health: " + currentHealth);
        playerHealthBar.UpdateHealthBar();
        UpdateHealthText();
    }
    private void GetRefrences()
    {
        hitIndicator = GameObject.FindWithTag("Training Dummy Hit Indicator");
        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();
        playerHealthBar = GameObject.FindWithTag("Training Dummy Health Bar").GetComponent<PlayerHealthBar>();
        HealthText = GameObject.FindWithTag("Training Dummy Health Text").GetComponent<TMP_Text>();
    }
}
