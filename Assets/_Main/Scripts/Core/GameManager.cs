using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    [SerializeField] private int _turns;
    [SerializeField] private TMP_Text turnText;
    public GameObject righthandCtrl;
    private bool isPlaying;
    public Button playBtn;
    public TargetContrl[] _targetCtrls;
    private int _round;
    private int _targetCount;

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
            PlayerPrefs.SetInt("shootpractice_turns", Turns);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("shootpractice_turns"))
        {
            int oldTurns = PlayerPrefs.GetInt("shootpractice_turns");
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
        if (Turns <= 0) return;
        if (isPlaying) return;
        isPlaying = true;
        Turns--;
        AudioManager.Instance.PlayAudioOnceShot(AudioName.start);
        righthandCtrl.SetActive(false);
        playBtn.gameObject.SetActive(false);
        ScoreManager.Instance.Init();
        _round = 0;
        StartCoroutine(Simple.Utilities.IEDelayCall(2, () =>
        {
            //ScoreTable.Instance.Init();
            StartTarget();
        }));
    }

    private void StartTarget()
    {
        _targetCount = 5;
        float dur = 5 - _round * .5f;
        dur = dur >= .5f ? dur : .5f;
        _targetCtrls.ToList().ForEach(i => i.Init(dur));
    }

    public void NextRound()
    {
        _round++;
        StartCoroutine(Simple.Utilities.IEDelayCall(2f, () =>
        {
            StartTarget();
        }));
    }

    public void End()
    {
        if(!isPlaying) return;
        isPlaying = false;
        playBtn.gameObject.SetActive(true);
        righthandCtrl.SetActive(true);
    }

    public void Target()
    {
        _targetCount--;
        if (_targetCount <= 0)
        {
            NextRound();
        }
    }
}
