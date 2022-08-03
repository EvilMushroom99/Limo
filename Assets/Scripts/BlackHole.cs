using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject blackHoleExplotion;
    [SerializeField] private GameObject orbit;
    private int direction;

    private void Start()
    {
        int num = Random.Range(1, 3);
        if (num == 1)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        AudioManager.Instance.PlayBlackHoleCharge();
    }

    void Update()
    {
        lifetime -= Time.deltaTime;

        orbit.transform.Rotate(new Vector3(0f, 0f, direction * velocity * Time.deltaTime));

        if (lifetime <= 0f)
        {
            Instantiate(blackHoleExplotion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
