using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _startScreen = null;
    [SerializeField] private GameObject _endScreen = null;
    [SerializeField] private TextMeshProUGUI _scoreText = null;
    [SerializeField] private GroupieSpawner _groupieSpawner = null;
    [SerializeField] private PlayerController _player = null;
    #endregion Fields
	
    #region Properties
    #endregion Properties
	
    #region Methods
    void Start()
    {
        _startScreen.SetActive(true);
        _endScreen.SetActive(false);
        _scoreText.gameObject.SetActive(false);
        _groupieSpawner.IsSpawning = false;
        _player.CanMove = false;
    }

    void Update()
    {
        _scoreText.text = $"Score : {_player.Score}\n" +
            $"Time : {_groupieSpawner.Timer:0.0}";
    }

    public void Launch()
    {
        _groupieSpawner.StartSpawn();
        _startScreen.SetActive(false);
        _endScreen.SetActive(false);
        _scoreText.gameObject.SetActive(true);
        _groupieSpawner.IsSpawning = true;
        _player.CanMove = true;
    }


    public void GameOver()
    {
        _groupieSpawner.IsSpawning = false;
        _player.CanMove = false;
        _startScreen.SetActive(false);
        _endScreen.SetActive(true);
        _scoreText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    #endregion Methods
}
