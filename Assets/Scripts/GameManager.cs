using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Vector2 spawnPosition;
    public GameObject playerPrefab;
    public GameObject ghostPrefab;


    private PlayerLagGhost ghost;
    public PlayerController player;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {

    }

    void Initialize()
    {
        GameObject playerObject = (GameObject)Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        GameObject ghostObject = (GameObject)Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);

        player = playerObject.GetComponent<PlayerController>();
        player.ghost = ghostObject.GetComponent<PlayerLagGhost>();
        ghost = ghostObject.GetComponent<PlayerLagGhost>();
        Camera.main.GetComponent<CameraController>().SetTarget(playerObject.transform);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPosition, 0.5f);
    }


    public void Respawn()
    {
        ghost.transform.position = spawnPosition;
        player.transform.position = spawnPosition;
    }

}
