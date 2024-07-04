using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
{
    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreText;

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _scoreText.text = Score.ToString();
        }
    }

    public void Init()
    {
        Score = 0;
    }
}
