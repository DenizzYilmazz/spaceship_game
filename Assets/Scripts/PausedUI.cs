using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button soundVolumeButton;
    [SerializeField] private TextMeshProUGUI soundVolumeTextMesh;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeTextMesh;



    private void Awake()
    {
        soundVolumeButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundVolumeTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            musicVolumeTextMesh.text = "MUSÝC " + MusicManager.Instance.GetMusicVolume();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
        });
        resumeButton.onClick.AddListener(() =>
        {
               GameManager.Instance.UnPauseGame();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGamePaused += GameManager_OnGameUnPaused;

        soundVolumeTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        musicVolumeTextMesh.text = "MUSÝC " + MusicManager.Instance.GetMusicVolume();

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
