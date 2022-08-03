using UnityEngine;

public class FireBallBomb : MonoBehaviour
{
    [SerializeField] private int damage;

    private float deathTime = 0.25f;

    private void Start()
    {
        AudioManager.Instance.PlayFireBallExplotion();
    }

    private void Update()
    {
        deathTime -= Time.deltaTime;
        if (deathTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public int MakeDamage()
    {
        return damage;
    }
}
