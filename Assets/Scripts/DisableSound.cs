using UnityEngine;

public class DisableSound : MonoBehaviour
{
    [SerializeField] private float clipTime;
    private float timer;

    private void Start()
    {
        timer = clipTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = clipTime;
            gameObject.SetActive(false);
        }
    }
}
