using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float velocity;

    private Animator anim;
    private bool targetFound;
    private bool targetReached;
    private bool attacking;
    private bool jumping;
    private GameObject currentTarget;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!targetFound)
        {
            currentTarget = ObjectManager.Instance.GetClosestBuilding(transform.position);
            if (currentTarget != null)
            {
                targetFound = true;
            }
            else
            {
                targetReached = false;
                targetFound = false;
            }
            
        }
        else if(!targetReached && jumping)
        {
            if(Vector2.Distance(transform.position, currentTarget.transform.position) < 0.5f)
            {
                targetReached = true;
                if (!attacking)
                {
                    attacking = true;
                    StartCoroutine(MakeDamage());
                }
            }
            else
            {
                if (attacking)
                {
                    attacking = false;
                    StopAllCoroutines();
                }

                transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, velocity * Time.deltaTime);
            }

        }

        if (currentTarget)
        {
            if (!currentTarget.activeInHierarchy)
            {
                targetReached = false;
                targetFound = false;
                currentTarget = null;
                attacking = false;
                StopAllCoroutines();
            }
        }
    }

    private void LateUpdate()
    {
        if (targetFound)
        {
            anim.Play("walk");
        }
        else 
        {
            anim.Play("idle");
        }
    }

    public void MoveSlime()
    {
        jumping = true;
    }

    public void StopSlime()
    {
        jumping = false;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            GameManager.Instance.totalSlimes -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator MakeDamage()
    {
        while (currentTarget)
        {
            currentTarget.GetComponentInParent<Building>().TakeDamage(attack);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Arrow"))
        {
            Arrow arrow = collision.GetComponent<Arrow>();
            TakeDamage(arrow.MakeDamage());
            arrow.DestroyArrow();
        }
    }
}
