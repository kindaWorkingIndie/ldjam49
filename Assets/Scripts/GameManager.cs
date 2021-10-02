using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Vector2 spawnPosition;
    public GameObject playerPrefab;
    public GameObject ghostPrefab;


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
        GameObject player = (GameObject)Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        GameObject ghost = (GameObject)Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);


        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.ghost = ghost.GetComponent<PlayerLagGhost>();
        Camera.main.GetComponent<CameraController>().SetTarget(player.transform);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnPosition, 0.5f);
    }
}
