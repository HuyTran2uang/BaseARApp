using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ColorType color;
    public GameObject redModel, blueModel;
    private bool isTracked;

    public void Init()
    {
        isTracked = false;
        color = Random.Range(0, 2) == 0 ? ColorType.red : ColorType.blue;
        redModel.SetActive(color == ColorType.red);
        blueModel.SetActive(color == ColorType.blue);
        gameObject.SetActive(true);
    }

    public void TakeDamage(ColorType color)
    {
        if (!GameManager.Instance.isPlaying) return;
        if (isTracked) return;
        isTracked = true;
        AudioManager.Instance.PlayAudioOnceShot(AudioName.Hit);
        if(this.color != color)
        {
            GameManager.Instance.End();
        }
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPlaying) return;
        transform.position += Vector3.back * GameManager.Instance.speed * Time.deltaTime;
    }
}
