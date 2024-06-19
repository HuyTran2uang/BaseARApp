using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    private Coroutine onDelayHideCoroutine;

    public void Init(Vector3 direction)
    {
        rb.velocity = direction * 50;
        gameObject.SetActive(true);
        onDelayHideCoroutine = StartCoroutine(Simple.Utilities.IEDelayCall(5, () =>
        {
            gameObject.SetActive(false);
        }));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "target")
        {
            AudioManager.Instance.PlayAudioOnceShot(AudioName.hittarget);
            Debug.Log("tracked target");
            if(onDelayHideCoroutine != null)
            {
                StopCoroutine(onDelayHideCoroutine);
            }
            other.GetComponent<TargetContrl>().CheckScore(this.transform);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            Debug.Log("tracked target");
            if (onDelayHideCoroutine != null)
            {
                StopCoroutine(onDelayHideCoroutine);
            }
            collision.gameObject.GetComponent<TargetContrl>().CheckScore(this.transform);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        BulletSpawner.Instance.AddToPool(this);
    }
}
