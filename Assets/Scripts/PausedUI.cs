using System;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
               GameManager.Instance.UnPauseGame();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGamePaused += GameManager_OnGameUnPaused;

        Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }
    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        resumeButton.Select();

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
