using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> deathMessages = new List<string>();

    public Vector2 spawnPosition;
    public GameObject playerPrefab;
    public GameObject ghostPrefab;


    private PlayerLagGhost ghost;
    public PlayerController player;

    private Transform latestCheckpoint;

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

    public void Die(DeathCause cause)
    {
        UIManager.Instance.ShowScreenHint(deathMessages[Random.Range(0, deathMessages.Count)]);
        StartCoroutine(Respawn());

    }

    IEnumerator Respawn()
    {
        ghost.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        ghost.ClearQueue();
        if (latestCheckpoint == null)
        {
            ghost.transform.position = spawnPosition;
            player.transform.position = spawnPosition;
        }
        else
        {
            ghost.transform.position = latestCheckpoint.position;
            player.transform.position = latestCheckpoint.position;
        }
        ghost.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }
    public void SnapPlayerToGhost(Vector3 position)
    {
        Debug.Log("SNAP");
        //player.GetComponent<BoxCollider2D>().enabled = false;
        player.transform.position = position;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        if (checkpoint == latestCheckpoint)
        {
            return;
        }
        latestCheckpoint = checkpoint;
        UIManager.Instance.ShowScreenHint("Checkpoint reached");
    }

}
