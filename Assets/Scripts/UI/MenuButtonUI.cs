using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameManager gameManager;
    private void Awake()
    {
        button.onClick.AddListener(() => 
        {
            gameManager.ChangeState(GameState.Pause);
        });
    }
}
