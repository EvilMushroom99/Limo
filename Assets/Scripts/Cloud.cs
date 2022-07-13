using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private float velocity;
    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //float x = Random.Range(-1f, 2f);
        //float y = Random.Range(-1f, 2f);

        //direction = new Vector2(x, y);

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * velocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lifetime == 0)
        {
            Destroy(gameObject);
        }
    }
}
