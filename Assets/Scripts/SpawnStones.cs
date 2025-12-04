using UnityEngine;

public class SpawnStones : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;

    void Start()
    {
        for (int x = -5000; x <= 5000; x += 30)
        {
            Instantiate(stonePrefab, new(x, 12, 5000), Quaternion.identity);
        }

        for (int x = -5000; x <= 5000; x += 30)
        {
            Instantiate(stonePrefab, new(x, 12, -5000), Quaternion.identity);
        }

        for (int z = -5000; z <= 5000; z += 30)
        {
            Instantiate(stonePrefab, new(5000, 12, z), Quaternion.identity);
        }

        for (int z = -5000; z <= 5000; z += 30)
        {
            Instantiate(stonePrefab, new(-5000, 12, z), Quaternion.identity);
        }
    }
}
