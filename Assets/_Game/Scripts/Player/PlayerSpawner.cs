using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public GameObject SpawnPlayer()
    {
        return Instantiate(
            playerPrefab,
            transform.position,
            transform.rotation
        );
    }
}