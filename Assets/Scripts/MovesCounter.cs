using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovesCounter : MonoBehaviour
{
    static Text text;
    public static int _CountMoves = 0;
    // Start is called before the first frame update
    void Start()
    {
       text =  GetComponent<Text>();
        text.text = "���-�� �����: " +_CountMoves.ToString();
    }

    public static int CountMoves
    {
        get
        {
            return _CountMoves;
        }
        set 
        {
            text.text = "���-�� �����: " + value.ToString();
            _CountMoves = value;
        }
    }


}
