using UnityEngine;

public class SpikeTrapActive : MonoBehaviour
{
    [SerializeField] private int damage;

    public int MakeDamage()
    {
        return damage;
    }
}
