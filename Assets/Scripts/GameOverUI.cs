using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.OnStateChanged.AddListener(UpdateVisual);
    }

    private void UpdateVisual()
    {
        if (GameManager.gameState== GameState.Over)
        {
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
