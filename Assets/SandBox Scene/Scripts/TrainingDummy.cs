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

    public PlayerHealthBar playerHealthBar;
    TMP_Text HealthText;

    private void FixedUpdate()
    {
        poisonCurrentDuration -= Time.deltaTime;
        poisonTickTimer -= Time.deltaTime;

        if (poisonTickTimer <= 0 && poisonCurrentDuration > 0)
        {
            PoisonTick();
            poisonTickTimer = 1f;
        }
        if (poisonCurrentDuration <= 0)
        {
            poisonPercentDMG = 0;
        }
    }
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

    [SerializeField] float poisonMaxDuration, poisonCurrentDuration, poisonTickTimer = 1f;
    public float poisonPercentDMG;

    public void RefreshPoisonTimer()
    {
        poisonCurrentDuration = poisonMaxDuration;
    }
    public void PoisonTick()
    {
        PoisonDMG();
    }

    public void PoisonDMG()
    {
        BulletPoisonDamage = MissingHealth * poisonPercentDMG;
        TakeDamage(BulletPoisonDamage);
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
