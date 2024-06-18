using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int turns;
    public TMP_Text turnText;
    public bool isPlaying;
    public float speed;
    public GameObject rightHand;

    public int Turns
    {
        get { return turns; }
        set
        {
            turns = value;
            turnText?.SetText($"{Turns}");
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
            Turns = IsNewDay() ? oldTurns + 10 : oldTurns;
        }
        else
        {
            // new game
            Turns = 10;
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

    public void Play()
    {
        if (Turns > 0)
        {
            Turns--;
            AudioManager.Instance.PlayAudio(AudioName.Music);
            speed = 2;
            BallManager.Instance.StartSpawn();
            isPlaying = true;
            UIController.Instance.main.SetActive(false);
            UIController.Instance.shop.SetActive(false);
            rightHand.SetActive(false);
        }
    }

    public void End()
    {
        AudioManager.Instance.PauseAudio(AudioName.Music);
        AudioManager.Instance.PlayAudioOnceShot(AudioName.End);
        isPlaying = false;
        BallManager.Instance.Clear();
        UIController.Instance.main.SetActive(true);
        UIController.Instance.shop.SetActive(true);
        rightHand.SetActive(true);
    }
}