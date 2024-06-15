using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    Dictionary<AudioName, AudioData> _audioDict = new Dictionary<AudioName, AudioData>();

    private void Awake()
    {
        _audioDict = new Dictionary<AudioName, AudioData>();
        foreach (var audio in AudioStorage.Instance.Audios)
        {
            GameObject obj = new GameObject($"{audio.name}");
            obj.transform.SetParent(transform);
            _audioDict[audio.name] = new AudioData(audio, obj.AddComponent<AudioSource>());
        }
    }

    public void PlayAudio(AudioName name, float volume = 1)
    {
        AudioData audioData = _audioDict[name];
        audioData.Play(false, volume);
    }

    public void PlayAudioOnceShot(AudioName name, float volume = 1)
    {
        AudioData audioData = _audioDict[name];
        audioData.PlayOnceShot(false, volume);
    }

    public void PauseAudio(AudioName name)
    {
        _audioDict[name].Pause();
    }
}
