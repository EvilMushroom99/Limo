using UnityEngine;

public class DisableSound : MonoBehaviour
{
    private float timer;

    private void Start()
    {
        timer = 0.2f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.5f;
            gameObject.SetActive(false);
        }
    }
}
