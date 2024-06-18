using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int turns;
    public TMP_Text turnText;

    public int Turns
    {
        get { return turns; }
        set
        {
            turns = value;
            turnText?.SetText($"Turns: {Turns}");
            PlayerPrefs.SetInt("turns", Turns);
        }
    }

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("turns"))
        {
            int oldTurns = PlayerPrefs.GetInt("turns");
            Turns = IsNewDay() ? oldTurns + 3 : oldTurns;
        }
        else
        {
            // new game
            Turns = 3;
        }
    }

    private bool IsNewDay()
    {
        bool isNewDay = false;
        if (!PlayerPrefs.HasKey("time"))
        {
            isNewDay = false;
        }
        else
        {
            var oldTime = DateTime.Parse(PlayerPrefs.GetString("time"));
            var newTime = new DateTime(oldTime.Year, oldTime.Month, oldTime.Day);
            if (newTime.Subtract(oldTime).TotalDays > 0)
            {
                isNewDay = true;
            }
        }
        return isNewDay;
    }

    private void Update()
    {

    }
}
