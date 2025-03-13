using UnityEngine;

public class PointCounter : MonoBehaviour
{
    public int CurrentPoints = 0;
    public int MaxPoints = 2;

    public PointCounter pointCounter;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //DamagePlayer(10);
        }
    }

    /*public void DamagePlayer(int damage)
    {
        curHealth -= damage;

        healthBar.SetHealth(curHealth);
    }*/
}
