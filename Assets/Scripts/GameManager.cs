using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> deathMessages = new List<string>();

    public Vector2 spawnPosition;
    public GameObject playerPrefab;
    public GameObject ghostPrefab;

    public float currentRunTimer = 0;

    private PlayerLagGhost ghost;
    public PlayerController player;

    private Checkpoint latestCheckpoint;

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
        currentRunTimer += Time.deltaTime;
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
        player.animator.SetBool("dead", true);
        ghost.animator.SetBool("dead", true);
        switch (cause)
        {
            case DeathCause.FallenInVoid:
                player.animator.SetBool("fall_in_void", true);
                ghost.animator.SetBool("fall_in_void", true);
                break;
            case DeathCause.Electrocuted:
                player.animator.SetBool("electrocute", true);
                ghost.animator.SetBool("electrocute", true);
                break;
        }

        StartCoroutine(Respawn(cause));
    }

    IEnumerator Respawn(DeathCause cause)
    {
        yield return new WaitForSeconds(1.5f);
        ghost.gameObject.SetActive(false);
        player.gameObject.SetActive(false);

        switch (cause)
        {
            case DeathCause.FallenInVoid:
                player.animator.SetBool("fall_in_void", false);
                ghost.animator.SetBool("fall_in_void", false);
                break;
            case DeathCause.Electrocuted:
                player.animator.SetBool("electrocute", false);
                ghost.animator.SetBool("electrocute", false);
                break;
        }
        player.animator.SetBool("dead", false);
        ghost.animator.SetBool("dead", false);
        yield return new WaitForSeconds(2);
        ghost.ClearQueue();
        if (latestCheckpoint == null)
        {
            ghost.transform.position = spawnPosition;
            player.transform.position = spawnPosition;
        }
        else
        {
            ghost.transform.position = latestCheckpoint.transform.position;
            player.transform.position = latestCheckpoint.transform.position;
        }
        player.GetComponent<PlayerCharacter>().ResetAfterAnimation();
        player.gameObject.SetActive(true);
        ghost.GetComponent<PlayerCharacter>().ResetAfterAnimation();
        ghost.gameObject.SetActive(true);

    }
    public void SnapPlayerToGhost(Vector3 position)
    {
        Debug.Log("SNAP");
        //player.GetComponent<BoxCollider2D>().enabled = false;
        player.transform.position = position;
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint == latestCheckpoint)
        {
            return;
        }
        latestCheckpoint = checkpoint;
        UIManager.Instance.ShowScreenHint("Checkpoint reached");
    }

    public float GetCurrentRunTime()
    {
        return currentRunTimer;
    }

}
