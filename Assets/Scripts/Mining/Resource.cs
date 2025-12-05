using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] ItemType type;

    public ItemType Type => type;
}
