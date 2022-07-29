using UnityEngine;

public class Wheater : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    private float time;

    void Start()
    {
        time = 5f;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            CreateCloud();
            time = Random.Range(5f, 20f);
        }
    }

    private void CreateCloud()
    {
        Instantiate(clouds[Random.Range(0, 12)]);
    }
}
