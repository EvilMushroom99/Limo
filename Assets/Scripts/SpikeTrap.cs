using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject hitbox;
    private bool activate;

    private void Update()
    {
        if (!GameManager.Instance.warTime)
        {
            trap.SetActive(true);
            activate = false;
        }
        else if (!activate)
        {
            trap.SetActive(false);
            hitbox.SetActive(false);
        }
        else
        {
            hitbox.SetActive(true);
            trap.SetActive(true);
        }
    }

    public bool IsActive()
    {
        if (activate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!trap.activeInHierarchy)
            {

                if (collision.gameObject.transform.position.y >= transform.position.y)
                {
                    activate = true;
                }
            }
        }
    }
}
