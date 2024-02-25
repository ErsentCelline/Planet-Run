using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button _scoreBonus;
    [SerializeField] private Button _speedBonus;

    [SerializeField] private Text[] _activeBonuses;

    private Bonuses Bonuses;

    private System.Action<string> OnBonusBuy;

    public void OnOpen()
    {
        Bonuses.CheckIsAvailable("SCORE_X");
        Bonuses.CheckIsAvailable("SPEED_X");
    }

    private void Awake()
    {
        Bonuses = new Bonuses();

        Bonuses.Buy += SetButtonInteractable;
        Bonuses.Disable += (n) =>
        {
            switch (n)
            {
                case "SCORE_X":
                    _activeBonuses[0].color = Color.gray;
                    break;
                case "SPEED_X":
                    _activeBonuses[1].color = Color.gray;
                    break;
            }
        };

        _scoreBonus.onClick.AddListener(() =>
        {
            Bonuses.OnBonusBuy("SCORE_X");
            _activeBonuses[0].color = Color.white;
        });

        _speedBonus.onClick.AddListener(() =>
        {
            Bonuses.OnBonusBuy("SPEED_X");
            _activeBonuses[1].color = Color.white;
        });

        SetButtonInteractable("SCORE_X");
        SetButtonInteractable("SPEED_X");

        if (Bonuses.ScoreEnabled)
            _activeBonuses[0].color = Color.white;
        if (Bonuses.SpeedEnabled)
            _activeBonuses[1].color = Color.white;
    }

    private void SetButtonInteractable(string bonusName)
    {
        switch (bonusName)
        {
            case "SCORE_X":
                _scoreBonus.interactable = Bonuses.CanBuyScoreBonus;
                break;
            case "SPEED_X":
                _speedBonus.interactable = Bonuses.CanBuySpeedBonus;
                break;
        }
    }

    private void OnDestroy()
    {
        Bonuses.SaveData();
    }
}
