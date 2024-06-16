using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    Dictionary<AudioName, AudioData> _audioDict = new Dictionary<AudioName, AudioData>();
    bool _isOpenMusic = true, _isOpenSound = true;

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
        bool mute = (audioData.Audio.type == AudioType.Music) ? !_isOpenMusic : !_isOpenSound;
        audioData.Play(mute, volume);
    }

    public void PlayAudioOnceShot(AudioName name, float volume = 1)
    {
        AudioData audioData = _audioDict[name];
        bool mute = (audioData.Audio.type == AudioType.Music) ? !_isOpenMusic : !_isOpenSound;
        audioData.PlayOnceShot(mute, volume);
    }

    public void PauseAudio(AudioName name)
    {
        _audioDict[name].Pause();
    }

    #region public function audio
    public void PlaySoundClickButton()
    {
        PlayAudioOnceShot(AudioName.Click);
    }
    #endregion
}
