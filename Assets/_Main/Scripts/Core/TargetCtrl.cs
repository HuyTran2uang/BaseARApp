using UnityEngine;
using DG.Tweening;
using System.Linq;

public class TargetCtrl : MonoBehaviour, ITakeDamage
{
    public bool isDead, isShowing, isBomb;
    public Transform container;
    private Coroutine onHideDelay;
    public GameObject[] fruits;
    public GameObject bomb;
    public Collider col;

    public void Init()
    {
        fruits.ToList().ForEach(i => i.SetActive(false));
        bomb.SetActive(false);
        isBomb = Random.Range(0, 5) == 1 ? true : false;
        if(isBomb)
        {
            bomb.SetActive(true);
        }
        else
        {
            int randIndex = Random.Range(0, fruits.Length);
            fruits[randIndex].SetActive(true);
        }
        isDead = false;
        isShowing = true;
        container.gameObject.SetActive(true);
        transform.DORotate(Vector3.zero, 1).OnComplete(() =>
        {
            onHideDelay = StartCoroutine(Simple.Utilities.IEDelayCall(2, Hide));
        });
        col.enabled = true;
    }

    public void TakeDamage()
    {
        if (isDead) return;
        isDead = true;
        if (onHideDelay != null)
        {
            StopCoroutine(onHideDelay);
        }
        if (isBomb)
        {
            AudioManager.Instance.PlayAudioOnceShot(AudioName.bomb);
            GameManager.Instance.EndGame();
            return;
        }
        else
        {
            AudioManager.Instance.PlayAudioOnceShot(AudioName.hitfruit);
            container.gameObject.SetActive(false);
            transform.DORotate(Vector3.right * 90, .5f).OnComplete(() =>
            {
                isShowing = false;
                col.enabled = false;
                GameManager.Instance.HideTarget(this);
            });
        }
    }

    public void Hide()
    {
        transform.DORotate(Vector3.right * 90, .5f).OnComplete(() =>
        {
            isShowing = false;
            container.gameObject.SetActive(false);
            if (isBomb)
            {
                GameManager.Instance.HideTarget(this);
                return;
            }
            if (isDead) return;
            col.enabled = false;
            GameManager.Instance.EndGame();
        });
    }

    public void SetHide()
    {
        if(onHideDelay != null)
        {
            StopCoroutine(onHideDelay);
        }
        isShowing = false;
        container.gameObject.SetActive(false);
        col.enabled = false;
        transform.eulerAngles = Vector3.right * 90;
    }
}
