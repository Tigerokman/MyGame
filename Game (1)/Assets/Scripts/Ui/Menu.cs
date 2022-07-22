using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _levelUp;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private PlayerLevel _playerLevel;

    private void OnEnable()
    {
        _playerLevel.GotLevelUp += LevelUp;
        _player.Dying += OpenDeathPanel;
    }

    private void OnDisable()
    {
        _playerLevel.GotLevelUp -= LevelUp;
        _player.Dying -= OpenDeathPanel;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void LevelUp()
    {
        _levelUp.SetActive(true);
        Time.timeScale = 0;
    }

    private void OpenDeathPanel()
    {
        _deathPanel.SetActive(true);
    }
}
