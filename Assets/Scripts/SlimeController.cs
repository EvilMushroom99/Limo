using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float velocity;
    [SerializeField] private float appearTime;

    [SerializeField] private GameObject deathParticle;

    private Animator anim;
    private Rigidbody2D rb;

    private bool targetFound;
    private bool attacking;
    private GameObject currentTarget;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (appearTime <= 0f)
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
                    targetFound = false;
                }

            }
            else if (Vector2.Distance(transform.position, currentTarget.transform.position) < 0.35f)
            {
                if (!attacking)
                {
                    attacking = true;

                    if (currentTarget.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                    }
                }
            }
            else
            {
                if (attacking)
                {
                    attacking = false;
                    currentTarget = ObjectManager.Instance.GetClosestBuilding(transform.position);
                    if (currentTarget != null)
                    {
                        targetFound = true;
                    }
                    else
                    {
                        targetFound = false;
                    }
                }
            }

            if (currentTarget)
            {
                if (!currentTarget.activeInHierarchy)
                {
                    targetFound = false;
                    currentTarget = null;
                    attacking = false;
                }
            }
        }
        else
        {
            appearTime -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (appearTime <= 0f)
        {
            if (targetFound)
            {
                if (attacking)
                {
                    anim.Play("attack");
                }
                else
                {
                    anim.Play("walk");
                }
            }
            else
            {
                anim.Play("idle");
            }
        }
    }

    public void MoveSlime()
    {
        if (targetFound)
        {
            Vector3 direction = currentTarget.transform.position - transform.localPosition;
            rb.AddForce(direction.normalized * velocity, ForceMode2D.Impulse);
        }
    }

    public void StopSlime()
    {
        rb.velocity = Vector2.zero;
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            GameManager.Instance.totalSlimes -= 1;
            GameObject particle = Instantiate(deathParticle);
            particle.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    public void MakeDamage()
    {
        if (currentTarget)
        {
            if (currentTarget.activeInHierarchy)
            {
                currentTarget.GetComponentInParent<Building>().TakeDamage(attack);
            }
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
