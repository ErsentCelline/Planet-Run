using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses
{
    public const string SCORE_X = "SCORE_X";
    public const string SPEED_X = "SPEED_X";

    public bool CanBuyScoreBonus { get; private set; } = true;
    public bool CanBuySpeedBonus { get; private set; } = true;

    public static bool ScoreEnabled { get; private set; } = false;
    public static bool SpeedEnabled { get; private set; } = false;

    private DateTime? _lastClaimTimeScore
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimTimeSCORE_X", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimTimeSCORE_X", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimTimeSCORE_X");
        }
    }

    private DateTime? _lastClaimTimeSpeed
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimTimeSPEED_X", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimTimeSPEED_X", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimTimeSPEED_X");
        }
    }

    public System.Action<string> Buy;
    public System.Action<string> Disable;

    public Bonuses()
    {
        Ship.OnShipDisabled += delegate
        {
            DisableBonus(SCORE_X);
            DisableBonus(SPEED_X);
        };

        CheckIsAvailable(SCORE_X);
        CheckIsAvailable(SPEED_X);

        ScoreEnabled = CheckIsEnabled(SCORE_X);
        SpeedEnabled = CheckIsEnabled(SPEED_X);
    }

    public void OnBonusBuy(string name)
    {
        switch (name)
        {
            case SCORE_X:
                ScoreEnabled = true;
                _lastClaimTimeScore = DateTime.UtcNow;

                break;
            case SPEED_X:
                SpeedEnabled = true;
                _lastClaimTimeSpeed = DateTime.UtcNow;

                break;
        }
        PlayerPrefs.SetInt(name + "enabled", 1);
        CheckIsAvailable(name);
        Buy?.Invoke(name);
        SaveData();
    }


    public void DisableBonus(string name)
    {
        switch (name)
        {
            case SCORE_X:
                ScoreEnabled = false;
                _lastClaimTimeScore = null;
                break;
            case SPEED_X:
                SpeedEnabled = false;
                _lastClaimTimeSpeed = null;
                break;
        }
        PlayerPrefs.SetInt(name + "enabled", 0);
        Disable?.Invoke(name);

        SaveData();
    }

    public void CheckIsAvailable(string name)
    {
        switch (name)
        {
            case SCORE_X:
                CanBuyScoreBonus = true;

                if (_lastClaimTimeScore.HasValue)
                {
                    var timeSpan = DateTime.UtcNow - _lastClaimTimeScore.Value;

                    if (timeSpan.TotalHours < 24)
                    {
                        CanBuyScoreBonus = false;
                    }
                }
                break;
            case SPEED_X:
                CanBuySpeedBonus = true;

                if (_lastClaimTimeSpeed.HasValue)
                {
                    var timeSpan = DateTime.UtcNow - _lastClaimTimeSpeed.Value;

                    if (timeSpan.TotalHours < 24)
                    {
                        CanBuySpeedBonus = false;
                    }
                }

                break;
        }
    }

    public void SaveData()
    {
        int value = CanBuyScoreBonus ? 1 : 0;
        PlayerPrefs.SetInt(SCORE_X, value);

        value = CanBuySpeedBonus ? 1 : 0;
        PlayerPrefs.SetInt(SPEED_X, value);

        value = ScoreEnabled ? 1 : 0;
        PlayerPrefs.SetInt(SCORE_X + "enabled", value);

        value = SpeedEnabled ? 1 : 0;
        PlayerPrefs.SetInt(SPEED_X + "enabled", value);
    }

    private bool CheckIsEnabled(string name)
    {
        return PlayerPrefs.GetInt(name + "enabled") == 1;
    }
}
