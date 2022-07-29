using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float velocity;
    private Vector2 direction;

    private Rigidbody2D rb;

    void Start()
    {
        float x = Random.Range(-1f, 1.01f);

        velocity = Random.Range(1f, 1.2f);

        direction = new Vector2(x, 0f);

        if (x >= 0f)
        {
            transform.position = new Vector3(-25f, Random.Range(-14f, 23f));
        }
        else
        {
            transform.position = new Vector3(25f, Random.Range(-14f, 23f));
        }

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * velocity, ForceMode2D.Impulse);
    }
}
