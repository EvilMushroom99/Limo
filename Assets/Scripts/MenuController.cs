using UnityEngine;

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
    }
}
