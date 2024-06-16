using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public bool isPlaying;
    public int turns;
    public TMP_Text turnText;
    private Coroutine onBallSpawnCoroutine;

    // ball
    private float minZ = -10, maxZ = 10;
    private float minY = -1, maxY = 5;
    public int rounds;

    //goal
    public Transform[] pointsGoal;

    public GameObject ui;
    public GameObject rightHandCtrl;

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
        AudioManager.Instance.PauseAudio(AudioName.goal);
        AudioManager.Instance.PlayAudio(AudioName.music);
        ui.SetActive(false);
        rightHandCtrl.SetActive(false);
        onBallSpawnCoroutine = StartCoroutine(Spawn());
    }

    public void EndGame()
    {
        if (!isPlaying) return;
        isPlaying = false;
        AudioManager.Instance.PauseAudio(AudioName.music);
        AudioManager.Instance.PlayAudioOnceShot(AudioName.goal);
        if (onBallSpawnCoroutine != null)
        {
            StopCoroutine(onBallSpawnCoroutine);
        }
        StartCoroutine(Simple.Utilities.IEDelayCall(1, () =>
        {
            ui.SetActive(true);
            rightHandCtrl.SetActive(true);
            BallSpawner.Instance.Clear();
        }));
    }

    private IEnumerator Spawn()
    {
        int count = 0;
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (!isPlaying)
                yield break;
            count++;
            var ball = BallSpawner.Instance.Spawn();
            Vector3 randPos = new Vector3(0, UnityEngine.Random.Range(minY, maxY), UnityEngine.Random.Range(minZ, maxZ));
            ball.transform.position = randPos;
            int randIndex = UnityEngine.Random.Range(0, pointsGoal.Length);
            Vector3 randGoalPos = pointsGoal[randIndex].position;
            Vector3 direction = Vector3.Normalize(randGoalPos - ball.transform.position);
            ball.Init(direction, 10 + count);
        }
    }
}
