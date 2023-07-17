using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maximumScoreText;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;

  


    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        resumeButton.onClick.AddListener(() =>
        {
            gameManager.stateSystem.ChangeState(GameState.Playing);
        });
        gameManager.stateSystem.OnStateChanged += UpdateVisual;
    }

    private void UpdateVisual()
    {
        if (gameManager.stateSystem.gameState == GameState.Pause)
        {
            scoreText.text = "Score: " + gameManager.levelSystem.movesCount.ToString();
            maximumScoreText.text = "Maximum Score: "+ gameManager.gameSaveLoad.maximumScore.ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);

    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
