using UnityEngine;

/// <summary>
/// €чейка
/// </summary>
public class Cell : MonoBehaviour
{
    [SerializeField] ItemType type;
    [SerializeField] bool isStack;

    public ItemType Type => type;
    public int CountItems { get; set; }
    public bool IsStack => IsStack;

}
