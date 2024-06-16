using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePage : MonoBehaviour
{
    public Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(GotoMainPage);
    }

    public void GotoMainPage()
    {
        AudioManager.Instance.PlayAudio(AudioName.Click);
        UIController.Instance.mainPage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
