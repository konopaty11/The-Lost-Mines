using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject researchBtn;
    [SerializeField] Transform spawnPoint;

    [SerializeField] List<Transform> deposites;
    [SerializeField] GameObject ore;
    [SerializeField] int countDeposites;

    void Start()
    {
        for (int i = 0; i < countDeposites; i++)
        {
            Instantiate(ore, deposites[i]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        researchBtn.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        researchBtn.SetActive(false);
    }

    public void TransitionInside()
    {
        player.Player.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}
