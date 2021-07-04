using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int score;
    [SerializeField] private Text _textScore;

    private void Update()
    {
        score++;
        _textScore.text = "" + score;
    }
}
