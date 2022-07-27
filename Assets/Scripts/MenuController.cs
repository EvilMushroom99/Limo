using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Animator anim;
    private bool Pressed;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetAnimation(string state)
    {
        if (!Pressed)
        {
            anim.Play(state);
        }
    }

    public void PressButton(string state)
    {
        anim.Play(state);
        Pressed = true;

        switch (state)
        {
            case "Menu1P":
                SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
                break;
            case "Menu2P":
                break;
            case "Menu3P":
                break;
            case "Menu4P":
                break;
        }
    }

    public void Unpressed()
    {
        Pressed = true;
    }
}
