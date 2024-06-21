using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int bullets;
    [SerializeField] private int _turns;
    [SerializeField] private TMP_Text turnText;
    public GameObject righthandCtrl;
    private bool isPlaying;
    public Button playBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() =>
        {
            Play();
        });
    }

    public int Turns
    {
        get { return _turns; }
        set
        {
            _turns = value;
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
        isPlaying = true;
        AudioManager.Instance.PlayAudioOnceShot(AudioName.start);
        righthandCtrl.SetActive(false);
        playBtn.gameObject.SetActive(false);
        StartCoroutine(Simple.Utilities.IEDelayCall(2, () =>
        {
            ScoreTable.Instance.Init();
            bullets = 10;
        }));
    }

    public void End()
    {
        isPlaying = false;
        playBtn.gameObject.SetActive(true);
        righthandCtrl.SetActive(true);
    }
}
