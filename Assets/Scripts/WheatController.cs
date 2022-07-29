using UnityEngine;
using UnityEngine.UI;

public class WheatController : MonoBehaviour
{
    private int food;
    private int growState;

    [SerializeField] private Text woodText;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject canvas;

    private SpriteRenderer render2D;

    void Start()
    {
        render2D = GetComponent<SpriteRenderer>();
        food = 0;
        growState = 0;
        woodText.text = "+" + food;
    }

    public void GetResources()
    {
        if (growState == 3)
        {
            GameManager.Instance.IncreaseFood(food);
        }

        GrowUp();
    }

    public void GrowUp()
    {
        growState += 1;
        food += 2;
        if (growState == 4)
        {
            growState = 0;
            food = 0;
        }
        render2D.sprite = sprites[growState];
        woodText.text = "+" + food;
    }

    private void OnMouseOver()
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
