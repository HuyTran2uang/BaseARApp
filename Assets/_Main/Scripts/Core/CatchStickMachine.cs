using System.Collections.Generic;
using UnityEngine;
using System;

public class CatchStickMachine : MonoBehaviourSingleton<CatchStickMachine>
{
    public Transform[] Points;
    public List<Stick> Sticks = new List<Stick>();
    public bool IsPlaying;
    public Coroutine OnFallCoroutine, OnLedFlickerCoroutine;
    [SerializeField] private int playTurn;
    public MeshRenderer[] leds;
    public Material greenLed, redLed;
    public MeshRenderer playButton;

    public int PlayTurn
    {
        get { return playTurn; }
        set
        {
            playTurn = value;
            if (playTurn > 0)
            {
                playButton.material = greenLed;
                GreenLed(true);
            }
            else
            {
                playButton.material = redLed;
                RedLed(true);
            }
            PlayerPrefs.SetInt("PlayTurn", PlayTurn);
            UIController.Instance.mainPage.SetTurnText(PlayTurn);
        }
    }

    private void Start()
    {
        PlayTurn = PlayerPrefs.GetInt("PlayTurn");
        CheckNewDay();
        UIController.Instance.mainPage.SetTurnText(PlayTurn);
    }

    private void CheckNewDay()
    {
        DateTime newTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        string oldTimeString = PlayerPrefs.HasKey("Time") ? PlayerPrefs.GetString("Time") : "";
        if(!string.IsNullOrEmpty(oldTimeString))
        {
            DateTime oldTime = DateTime.Parse(oldTimeString);
            if (newTime.Subtract(oldTime).Days > 1)
            {
                NewDay();
            }
        }
        else
        {
            NewDay();
        }
        PlayerPrefs.SetString("Time", newTime.ToString());
    }

    private void NewDay()
    {
        PlayTurn += 3;
    }

    public void IncreaseTurn(int amount)
    {
        PlayTurn += amount;
    }

    private void OverTurn()
    {
        playButton.material = redLed;
        RedLed(true);
    }

    public void Play()
    {
        if (PlayTurn <= 0)
        {
            Debug.Log("Over Turn");
            OverTurn();
            return;
        }
        AudioManager.Instance.PlayAudio(AudioName.Background, 1);
        RedLed();
        PlayTurn--;
        StickSpawner.Instance.Clear();
        SpawnStick();
        IsPlaying = true;
        float waitTime = 5; //seconds
        StartCoroutine(Utilities.IEDelayCall(waitTime, StartFall));
    }

    private void StartFall()
    {
        if(Sticks.Count <= 0)
        {
            Win();
            return;
        }
        float nextTime = UnityEngine.Random.Range(2f, 3f);
        int randIndex = UnityEngine.Random.Range(0, Sticks.Count);
        Stick stick = Sticks[randIndex];
        Sticks.RemoveAt(randIndex);
        stick.Fall();
        OnFallCoroutine = StartCoroutine(Utilities.IEDelayCall(nextTime, StartFall));
    }

    public void SpawnStick()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            var stick = StickSpawner.Instance.Spawn();
            Sticks.Add(stick);
            stick.transform.position = Points[i].position;
            stick.transform.eulerAngles = Vector3.zero;
            stick.Init();
        }
    }

    public void Win()
    {
        AudioManager.Instance.PauseAudio(AudioName.Background);
        AudioManager.Instance.PlayAudio(AudioName.Win);
        Debug.Log("Win");
        IsPlaying = false;
        if (OnFallCoroutine != null)
        {
            StopCoroutine(OnFallCoroutine);
        }
        if (OnLedFlickerCoroutine != null)
        {
            StopCoroutine(OnLedFlickerCoroutine);
            GreenLed(true);
        }
    }

    public void Lose()
    {
        AudioManager.Instance.PauseAudio(AudioName.Background);
        AudioManager.Instance.PlayAudio(AudioName.Lose);
        Debug.Log("Lose");
        IsPlaying = false;
        if(OnFallCoroutine != null)
        {
            StopCoroutine(OnFallCoroutine);
        }
        if (OnLedFlickerCoroutine != null)
        {
            StopCoroutine(OnLedFlickerCoroutine);
            RedLed(true);
        }
    }

    private void GreenLed(bool isStop = false)
    {
        foreach (var led in leds)
        {
            led.material = greenLed;
        }
        if (isStop) return;
        OnLedFlickerCoroutine = StartCoroutine(Utilities.IEDelayCall(.1f, () => RedLed()));
    }

    private void RedLed(bool isStop = false)
    {
        foreach (var led in leds)
        {
            led.material = redLed;
        }
        if (isStop) return;
        OnLedFlickerCoroutine = StartCoroutine(Utilities.IEDelayCall(.1f, () => GreenLed()));
    }

#if UNITY_EDITOR
    private GameObject holdingItem;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                var clickable = hit.collider.GetComponent<IClickable>();
                clickable?.OnClick();
                var item = hit.collider.GetComponent<IItem>();
                if(item != null)
                {
                    holdingItem = hit.collider.gameObject;
                    item?.Select(transform);
                }
            }
        }

        if(Input.GetMouseButtonUp(0) && holdingItem != null)
        {
            var item = holdingItem.GetComponent<IItem>();
            item?.Unselect();
        }
    }
#endif
}
