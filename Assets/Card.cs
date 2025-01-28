using UnityEngine;

public class Card : MonoBehaviour
{
    // Floats. Numbers in Decimal form.
    public float MovementSpeed = 1;
    public float Health = 1;
    public float Damage = 1;
    
    public float BulletSpeed = 1;
    public float BulletSize = 1;
    public float BulletSlow = 1;

    
    public float AttackSpeed = 1;

    public float Range = 1;
    
    // Floats. Numbers in flat amount form.
    public float BulletPoison;
    public float BulletBounces;
    public float BulletCount;

    public float FireRadius;
    public float ExplosiveRadius;

    // Reload Speed is in 0.25 second intervals.
    public float ReloadSpeed;
    public float Ammo;
    
    public float BlockCoolDown;
    public float BlockCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoSelectCard()
    {
        
        Debug.Log("Card Clicked");
    }
}
