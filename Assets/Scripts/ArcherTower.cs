using UnityEngine;
using System.Collections.Generic;

public class ArcherTower : MonoBehaviour
{
    [SerializeField] private int attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Transform arrowPosition;
    [SerializeField] private GameObject Arrow;

    private GameObject target;

    private float timeColdDown;

    private List<GameObject> targets = new List<GameObject>();

    private void FixedUpdate()
    {
        if (timeColdDown > 0f)
        {
            timeColdDown -= Time.fixedDeltaTime;
        }

        if (target)
        {
            if (timeColdDown <= 0f)
            {
                SetArrow();
                timeColdDown = attackSpeed;
            }
        }
        else if(targets.Count > 0)
        {
            target = targets[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);
            target = null;
        }
    }

    private void SetArrow()
    {
        AudioManager.Instance.PlayArcherTowerAttack();

        GameObject arrow = Instantiate(Arrow, arrowPosition.position, Quaternion.identity);

        Vector3 direction = target.transform.position - arrow.transform.localPosition;

        arrow.transform.localRotation = Quaternion.Euler(0f, 0f, GetAngleFromEnemy(direction));

        arrow.GetComponent<Rigidbody2D>().AddForce(arrow.transform.right * 2f, ForceMode2D.Impulse);
        arrow.GetComponent<Arrow>().GetDamage(attack);
    }

    private float GetAngleFromEnemy(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360f;
        }
        return angle;
    }
}
