using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int damage;
    private float deathTime;

    private void Start()
    {
        deathTime = 4f;
    }

    private void Update()
    {
        deathTime -= Time.deltaTime;
        if(deathTime <= 0f)
        {
            DestroyArrow();
        }
    }

    public void GetDamage(int towerAttack)
    {
        damage = towerAttack;
    }

    public int MakeDamage()
    {
        return damage;
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
