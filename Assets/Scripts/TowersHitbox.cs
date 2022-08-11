using UnityEngine;

public class TowersHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProyectile"))
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
            if (gameObject.activeInHierarchy)
            {
                GetComponentInParent<Building>().TakeDamage(arrow.MakeDamage());
                arrow.DestroyArrow();
            }
        }
    }
}
