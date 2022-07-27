using UnityEngine;
using UnityEngine.UI;

public class HouseController : MonoBehaviour
{
    private int food;
    private int totalpop;

    [SerializeField] private Text popText;
    [SerializeField] private GameObject canvas;

    void Start()
    {
        food = 0;
        totalpop = 1;
        popText.text = "+" + food;
    }

    public void GetResources()
    {
        GameManager.Instance.IncreasePopulation(totalpop + food);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            food += 1;
            popText.text = "+" + food;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            food -= 1;
            popText.text = "+" + food;
        }
    }

    private void OnMouseEnter()
    {
        if (!GameManager.Instance.warTime)
        {
            canvas.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.Instance.warTime)
        {
            canvas.SetActive(false);
        }
    }
}
