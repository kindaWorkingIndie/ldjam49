using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool _isPaused = false;
    public bool isPaused { get { return _isPaused; } }
    public GameObject visuals;

    public Text titleText;

    private static PauseMenu _instance;
    public static PauseMenu Instance
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
        visuals.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        _isPaused = true;
        visuals.SetActive(_isPaused);
        Time.timeScale = 0;
        titleText.text = "PAUSE";
    }

    public void EndGame()
    {
        _isPaused = true;
        visuals.SetActive(_isPaused);
        Time.timeScale = 0;
        titleText.text = "CONGRATULATIONS";
    }

    public void UnpauseGame()
    {
        _isPaused = false;
        visuals.SetActive(_isPaused);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        UnpauseGame();
    }

    public void QuitGame()
    {
        UnpauseGame();
        Application.Quit();
    }
}
