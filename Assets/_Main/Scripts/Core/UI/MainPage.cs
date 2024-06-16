using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    public TMP_Text turnText;
    public Button storeButton;

    private void Awake()
    {
        storeButton.onClick.AddListener(GotoStore);
    }

    public void SetTurnText(int turn)
    {
        turnText.text = turn.ToString();
    }

    private void GotoStore()
    {
        AudioManager.Instance.PlayAudio(AudioName.Click);
        UIController.Instance.storePage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
