using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// логика инвентаря
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField] List<RectTransform> cellsPositions;
    [SerializeField] List<CellPrefabSerializable> cellPrefabs;

    class CellPrefabSerializable
    {
        public GameObject cellPrefab;
        public ItemType type;
    }

    Cell[] cells = new Cell[11];

    /// <summary>
    /// добавление элемента
    /// </summary>
    /// <param name="type"></param>
    public void AddItem(ItemType type)
    {
        if (type == ItemType.Iron || type == ItemType.Cupper ||
            type == ItemType.Gold || type == ItemType.Silver)
        {

            foreach (Cell cell in cells)
            {
                if (cell != null && cell.Type == type)
                {
                    cell.CountItems++;
                    return;
                }
            }
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null) continue;

            foreach (CellPrefabSerializable cell in cellPrefabs)
            {
                if (cell.type != type) continue;

                GameObject newCell = Instantiate(cell.cellPrefab, cellsPositions[i].position, Quaternion.identity);
                cells[i] = newCell.GetComponent<Cell>();
            }
        }
    }
}
