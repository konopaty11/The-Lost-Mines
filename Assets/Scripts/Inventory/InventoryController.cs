using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// логика инвентаря
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField] List<RectTransform> cellsPositions;
    [SerializeField] List<GameObject> cellPrefabs;

    [System.Serializable]
    class CellPrefabSerializable
    {
        public GameObject cellPrefab;
    }

    Cell[] cells = new Cell[11];

    int _firstIndex = -1;
    int _secondIndex = -1;

    Color _selectColor = Color.white;
    Color _unselectColor = new(0.7f, 0.7f, 0.7f);
    List<Image> _cellImages = new();


    void Start()
    {
        foreach (RectTransform cell in cellsPositions)
        {
            _cellImages.Add(cell.gameObject.GetComponent<Image>());
        }
    }

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
                if (cell != null && cell.Type == type && cell.CountItems < 64)
                {
                    cell.CountItems++;
                    return;
                }
            }
        }

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null) continue;

            foreach (GameObject cellPrefab in cellPrefabs)
            {
                Cell _cell = cellPrefab.gameObject.GetComponent<Cell>();
                if (_cell.Type == type)
                {
                    GameObject newCell = Instantiate(cellPrefab, cellsPositions[i]);
                    cells[i] = newCell.GetComponent<Cell>();
                    return;
                }
            }
        }
    }

    public void CellSelect(int _index)
    {
        foreach (Image cell in _cellImages)
        {
            cell.color = _unselectColor;
        }
        Debug.Log(_firstIndex);

        if (_firstIndex == -1)
        {
            FirstCellSelect(_index);
        }
        else
        {
            SecondCellSelect(_index);
        }
    }

    void FirstCellSelect(int _index)
    {
        _cellImages[_index].color = _selectColor;

        if (cells[_index] == null) return;

        _firstIndex = _index;
    }

    void SecondCellSelect(int _index)
    {
        if (cells[_index] != null) return;

        _secondIndex = _index;

        ChangeCell();
    }

    void ChangeCell()
    {
        RectTransform _rectTransform = cells[_firstIndex].GetComponent<RectTransform>();
        _rectTransform.position = cellsPositions[_secondIndex].position;
        _rectTransform.SetParent(cellsPositions[_secondIndex]);

        (cells[_firstIndex], cells[_secondIndex]) = (cells[_secondIndex], cells[_firstIndex]);

        _firstIndex = -1;
        _secondIndex = -1;
    }
}
