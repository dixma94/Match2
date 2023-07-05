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
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        exitButton.onClick.AddListener(() =>
        {

        });
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        resumeButton.onClick.AddListener(() => 
        {
            gameManager.ChangeState(GameState.Playing);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager.OnStateChanged.AddListener(UpdateVisual);
    }

    private void UpdateVisual()
    {
        if (GameManager.gameState == GameState.Pause)
        {
            scoreText.text = "Score: " + GameManager.movesCount.ToString();
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
