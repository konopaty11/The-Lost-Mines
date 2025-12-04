using UnityEngine;

public class Mineral : MonoBehaviour
{
    [SerializeField] ItemType type;

    public ItemType Type => type;
}
