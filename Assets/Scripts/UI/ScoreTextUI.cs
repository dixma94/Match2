using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTextUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Score: 0";
        gameManager.levelSystem.MovesCountChanged += UpdateText;
    }

    private void UpdateText()
    {
        text.text = "Score: " + gameManager.levelSystem.movesCount.ToString();
    }

}
