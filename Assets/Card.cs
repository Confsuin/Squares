using UnityEngine;

public class Card : MonoBehaviour
{
    // Intergers. Numbers in Decimal form.
    public int Health = 1;
    public int Damage = 1;
    
    public int BulletSpeed = 1;
    public int BulletSize = 1;
    public int BulletSlow = 1;

    
    public int AttackSpeed = 1;

    public int Range = 1;
    
    // Intergers. Numbers in flat amount form.
    public int BulletPoison;
    public int BulletBounces;
    public int BulletCount;

    // Reload Speed is in 0.25 second intervals.
    public int ReloadSpeed;
    public int Ammo;
    
    public int BlockCoolDown;
    public int BlockCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
