using UnityEngine;
using System.Collections;
using TMPro;
public class StatsMenu : MonoBehaviour
{
    // References
    private GameObject StatsInfo;

    private Player1 player1;
    private Player2 player2;

    // Text References
    TMP_Text PlayerXStats;

    TMP_Text MoveSpeedStat;
    TMP_Text HealthStat;
    TMP_Text DamageStat;
    TMP_Text AttackSpeedStat;
    TMP_Text AmmoStat;
    TMP_Text ReloadSpeedStat;
    TMP_Text RangeStat;
    TMP_Text BulletSpeedStat;


    TMP_Text PoisonStat;
    TMP_Text LifestealStat;
    TMP_Text BulletCountStat;
    TMP_Text ShotgunCountStat;
    TMP_Text BulletSpreadStat;
    TMP_Text BulletBouncesStat;
    TMP_Text BulletGrowthStat;
    TMP_Text ExplosionDMGStat;
    TMP_Text FireRadiusStat;

    // Bools
    public bool UpdateStatsPage = false;
    public bool ShowingPlayer1Stats = false;
    public bool ShowingPlayer2Stats = false;
    
    // Floats
    float BulletBounces = 0;

    void Start()
    {
        GetReferences();
        StatsInfo.SetActive(false);
    }
    IEnumerator UpdateStats()
    {
        while (UpdateStatsPage == true)
        {
            if (ShowingPlayer1Stats == true)
            {
                UpdateShownStatsPlayer1();
            }
            if (ShowingPlayer2Stats == true)
            {
                UpdateShownStatsPlayer2();
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void ShowStatsPlayer1()
    {
        if (ShowingPlayer1Stats == false && ShowingPlayer2Stats == false)
        {
            PlayerXStats.text = "Player 1 Stats";
            ShowingPlayer1Stats = true;
            ShowStatsInfo();
            StartCoroutine(UpdateStats());
        }
        else if (ShowingPlayer1Stats == true && ShowingPlayer2Stats == false)
        {
            ShowingPlayer1Stats = false;
            HideStatsInfo();
        }
        else if (ShowingPlayer1Stats == false && ShowingPlayer2Stats == true)
        {
            PlayerXStats.text = "Player 1 Stats";
            ShowingPlayer2Stats = false;
            ShowingPlayer1Stats = true;
        }
    }
    public void ShowStatsPlayer2()
    {
        if (ShowingPlayer1Stats == false && ShowingPlayer2Stats == false)
        {
            PlayerXStats.text = "Player 2 Stats";
            ShowingPlayer2Stats = true;
            ShowStatsInfo();
            StartCoroutine(UpdateStats());
        }
        else if (ShowingPlayer1Stats == true && ShowingPlayer2Stats == false)
        {
            PlayerXStats.text = "Player 2 Stats";
            ShowingPlayer2Stats = true;
            ShowingPlayer1Stats = false;
        }
        else if (ShowingPlayer1Stats == false && ShowingPlayer2Stats == true)
        {
            ShowingPlayer2Stats = false;
            HideStatsInfo();
        }
    }
    public void UpdateShownStatsPlayer1()
    {
        MoveSpeedStat.text = "Move Speed: " + player1.moveSpeed;
        HealthStat.text = "Health: " + player1.maxHealth;
        DamageStat.text = "Damage: " + player1.Damage;
        AttackSpeedStat.text = "Attack Speed: " + player1.AttackSpeed;
        AmmoStat.text = "Ammo: " + player1.StartingAmmo;
        ReloadSpeedStat.text = "Reload Speed: " + player1.ReloadSpeed;
        RangeStat.text = "Range: " + player1.Range;
        BulletSpeedStat.text = "Bullet Speed: " + player1.BulletSpeed;

        PoisonStat.text = "Poison: " + player1.BulletPoison;
        LifestealStat.text = "Lifesteal: " + player1.BulletLifeSteal;
        BulletCountStat.text = "Bullet Count: " + player1.BulletCount;
        ShotgunCountStat.text = "Shotgun Count: " + player1.ShotgunCount;
        BulletSpreadStat.text = "Bullet Spread: " + player1.GunInaccuracy;
        BulletBounces = player1.BulletBounces - 1;
        BulletBouncesStat.text = "Bullet Bounces: " + BulletBounces;
        BulletGrowthStat.text = "Bullet Growth: " + player1.BulletGrowth;
        ExplosionDMGStat.text = "Explosion DMG: " + player1.ExplosionDMG;
        FireRadiusStat.text = "Fire Radius: " + player1.FireRadius;
    }
    public void UpdateShownStatsPlayer2()
    {
        MoveSpeedStat.text = "Move Speed: " + player2.moveSpeed;
        HealthStat.text = "Health: " + player2.maxHealth;
        DamageStat.text = "Damage: " + player2.Damage;
        AttackSpeedStat.text = "Attack Speed: " + player2.AttackSpeed;
        AmmoStat.text = "Ammo: " + player2.StartingAmmo;
        ReloadSpeedStat.text = "Reload Speed: " + player2.ReloadSpeed;
        RangeStat.text = "Range: " + player2.Range;
        BulletSpeedStat.text = "Bullet Speed: " + player2.BulletSpeed;

        PoisonStat.text = "Poison: " + player2.BulletPoison;
        LifestealStat.text = "Lifesteal: " + player2.BulletLifeSteal;
        BulletCountStat.text = "Bullet Count: " + player2.BulletCount;
        ShotgunCountStat.text = "Shotgun Count: " + player2.ShotgunCount;
        BulletSpreadStat.text = "Bullet Spread: " + player2.GunInaccuracy;
        BulletBounces = player2.BulletBounces - 1;
        BulletBouncesStat.text = "Bullet Bounces: " + BulletBounces;
        BulletGrowthStat.text = "Bullet Growth: " + player2.BulletGrowth;
        ExplosionDMGStat.text = "Explosion DMG: " + player2.ExplosionDMG;
        FireRadiusStat.text = "Fire Radius: " + player2.FireRadius;
    }
    private void ShowStatsInfo()
    {
        StatsInfo.SetActive(true);
        UpdateStatsPage = true;
    }
    private void HideStatsInfo()
    {
        StatsInfo.SetActive(false);
        UpdateStatsPage = false;
    }
    private void GetReferences()
    {
        StatsInfo = GameObject.FindWithTag("Stats Info");

        player1 = GameObject.FindWithTag("Player").GetComponent<Player1>();
        player2 = GameObject.FindWithTag("PlayerAlt").GetComponent<Player2>();

        PlayerXStats = GameObject.FindWithTag("Player X Stats").GetComponent<TMP_Text>();

        MoveSpeedStat = GameObject.FindWithTag("Move Speed Stat").GetComponent<TMP_Text>();
        HealthStat = GameObject.FindWithTag("Health Stat").GetComponent<TMP_Text>();
        DamageStat = GameObject.FindWithTag("Damage Stat").GetComponent<TMP_Text>();
        AttackSpeedStat = GameObject.FindWithTag("Attack Speed Stat").GetComponent<TMP_Text>();
        AmmoStat = GameObject.FindWithTag("Ammo Stat").GetComponent<TMP_Text>();
        ReloadSpeedStat = GameObject.FindWithTag("Reload Speed Stat").GetComponent<TMP_Text>();
        RangeStat = GameObject.FindWithTag("Range Stat").GetComponent<TMP_Text>();
        BulletSpeedStat = GameObject.FindWithTag("Bullet Speed Stat").GetComponent<TMP_Text>();

        PoisonStat = GameObject.FindWithTag("Poison Stat").GetComponent<TMP_Text>();
        LifestealStat = GameObject.FindWithTag("Lifesteal Stat").GetComponent<TMP_Text>();
        BulletCountStat = GameObject.FindWithTag("Bullet Count Stat").GetComponent<TMP_Text>();
        ShotgunCountStat = GameObject.FindWithTag("Shotgun Count Stat").GetComponent<TMP_Text>();
        BulletSpreadStat = GameObject.FindWithTag("Bullet Spread Stat").GetComponent<TMP_Text>();
        BulletBouncesStat = GameObject.FindWithTag("Bullet Bounces Stat").GetComponent<TMP_Text>();
        BulletGrowthStat = GameObject.FindWithTag("Bullet Growth Stat").GetComponent<TMP_Text>();
        ExplosionDMGStat = GameObject.FindWithTag("Explosion DMG Stat").GetComponent<TMP_Text>();
        FireRadiusStat = GameObject.FindWithTag("Fire Radius Stat").GetComponent<TMP_Text>();
    }
}
