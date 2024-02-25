using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] 
    private UnityEngine.UI.Text _scoreField;
    [SerializeField] 
    private UnityEngine.UI.Text _bestScoreField;

    private int Score = 0;
    private int BestScore = 0;

    private void Start()
    {
        BestScore = PlayerPrefs.GetInt("Best", 0);
        _bestScoreField.text = BestScore.ToString();

        Ship.OnShipArriveToPlanet += GetRevard;
        Ship.OnShipDisabled += HandleResult;
    }

    private void HandleResult()
    {
        if (BestScore < Score)
        {
            PlayerPrefs.SetInt("Best", Score);
            BestScore = Score;
            _bestScoreField.text = BestScore.ToString();
        }

        Score = 0;
        _scoreField.text = Score.ToString();
    }

    private void GetRevard(SpaceObject spaceObject)
    {
        switch (spaceObject)
        {
            case Planet planet:
                Score += Bonuses.ScoreEnabled ? 10 : 5;
                break;
            case BlackHole blackHole:
                Score += Bonuses.ScoreEnabled ? 30 : 15;
                break;
        }

        _scoreField.text = Score.ToString();
    }

    private void OnApplicationQuit()
    {
        HandleResult();
    }
}
