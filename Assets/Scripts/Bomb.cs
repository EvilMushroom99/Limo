using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int attack;

    private float deathTime = 1f;

    private void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HitBox"))
        {
            collision.gameObject.GetComponentInParent<Building>().TakeDamage(attack);
        }
    }
}
