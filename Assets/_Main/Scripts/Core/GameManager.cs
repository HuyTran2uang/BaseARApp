using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int turns;
    public TMP_Text turnText;
    public TargetCtrl[] targets;
    public bool isPlaying;
    private int rounds;
    private List<TargetCtrl> targetsShowing = new List<TargetCtrl>();
    public GameObject rightController;
    [Header("UI")]
    public GameObject ui;
    private Coroutine onNextRoundCoroutine;

    public int Turns
    {
        get { return turns; }
        set
        {
            turns = value;
            turnText.text = $"Turns: {Turns}";
            PlayerPrefs.SetInt("turns", Turns);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("turns"))
        {
            int oldTurns = PlayerPrefs.GetInt("turns");
            Turns = IsNewDay() ? oldTurns + 3 : oldTurns;
        }
        else
        {
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

    public void Play()
    {
        if (isPlaying) return;
        if (Turns <= 0) return;
        Turns--;
        isPlaying = true;
        rightController.SetActive(false);
        AudioManager.Instance.PlayAudioOnceShot(AudioName.start);
        targetsShowing.ForEach(i => i.SetHide());
        rounds = 0;
        ui.SetActive(false);
        NextRound();
    }

    public void EndGame()
    {
        if(!isPlaying) return;
        isPlaying = false;
        if (onNextRoundCoroutine != null)
        {
            StopCoroutine(onNextRoundCoroutine);
        }
        rightController.SetActive(true);
        AudioManager.Instance.PlayAudioOnceShot(AudioName.gameover);
        ui.SetActive(true);
    }

    public void HideTarget(TargetCtrl item)
    {
        targetsShowing.Remove(item);
        if(targetsShowing.Count <= 0)
        {
            onNextRoundCoroutine = StartCoroutine(Simple.Utilities.IEDelayCall(2f, NextRound));
        }
    }

    public void Show()
    {
        List<TargetCtrl> items = targets.ToList().Where(i => i.isShowing == false).ToList();
        targetsShowing = new List<TargetCtrl>();
        for (int i = 0; i < rounds; i++)
        {
            if (items.Count <= 0) break;
            int randIndex = UnityEngine.Random.Range(0, items.Count);
            var item = items[randIndex];
            items.Remove(item);
            targetsShowing.Add(item);
        }
        targetsShowing.ForEach(i => i.Init());
    }

    private void NextRound()
    {
        rounds++;
        Show();
    }
}
