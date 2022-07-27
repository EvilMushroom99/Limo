using UnityEngine;

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
    private SpriteRenderer render;

    private bool targetFound;
    private bool attacking;

    private bool hit;
    private float hitTime;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material hitMaterial;


    private GameObject currentTarget;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (appearTime <= 0f)
        {
            if (hit)
            {
                hitTime -= Time.deltaTime;
                if (hitTime <= 0f)
                {
                    hit = false;
                }
            }
            else
            {
                hitTime = 0.25f;
            }

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
            else if (Vector2.Distance(transform.position, currentTarget.transform.position) < 0.5f)
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

            if (hit)
            {
                render.material = hitMaterial;
            }
            else
            {
                render.material = baseMaterial;
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
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Proyectile":
                Arrow arrow = collision.GetComponent<Arrow>();

                if (arrow.arrowType == Arrow.ArrowType.MultipleTarget)
                {
                    arrow.Expand();
                    arrow.DestroyArrow();
                }
                else
                {
                    TakeDamage(arrow.MakeDamage());
                    hit = true;
                    hitTime = 0.25f;
                    arrow.DestroyArrow();
                }
                break;

            case "Bomb":
                FireBallBomb bomb = collision.GetComponent<FireBallBomb>();
                TakeDamage(bomb.MakeDamage());
                hit = true;
                hitTime = 0.25f;
                break;

            case "Trap":
                SpikeTrapActive trap = collision.GetComponent<SpikeTrapActive>();
                TakeDamage(trap.MakeDamage());
                break;
        }
    }
}
