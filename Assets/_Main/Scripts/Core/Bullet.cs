using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    private Coroutine onHideDelayCoroutine;
    private bool isExploded;

    public void Init(Vector3 direcion)
    {
        isExploded = false;
        rb.velocity = direcion * 50;
        transform.rotation = Quaternion.LookRotation(direcion);
        gameObject.SetActive(true);
        onHideDelayCoroutine = StartCoroutine(Simple.Utilities.IEDelayCall(2, (() =>
        {
            gameObject.SetActive(false);
        })));
    }

    private void OnDisable()
    {
        BulletSpawner.Instance.AddToPool(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "takedamagable" && isExploded == false)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
            return;
        }

        if (other.gameObject.layer == 6)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "takedamagable" && isExploded == false)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
            return;
        }

        if (other.gameObject.layer == 6)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "takedamagable" && isExploded == false)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
            return;
        }

        if (other.gameObject.layer == 6)
        {
            if (isExploded) return;
            isExploded = true;
            other.GetComponent<ITakeDamage>()?.TakeDamage();
            if (onHideDelayCoroutine != null)
            {
                StopCoroutine(onHideDelayCoroutine);
            }
            gameObject.SetActive(false);
        }
    }
}
