using Oculus.Interaction;
using UnityEngine;

public class Ball : BaseItem
{
    public Rigidbody body;
    public bool isMoving;
    private Coroutine onDelayHideCoroutine;
    public bool isCatched;

    public void Init(Vector3 direction, float force)
    {
        body.isKinematic = false;
        body.useGravity = false;
        isCatched = false;
        gameObject.SetActive(true);
        onDelayHideCoroutine = StartCoroutine(Simple.Utilities.IEDelayCall(4, () =>
        {
            gameObject.SetActive(false);
        }));
        body.velocity = direction * force;
    }

    private void OnDisable()
    {
        BallSpawner.Instance.AddToPool(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6 && !isCatched && GameManager.Instance.isPlaying)
        {
            Debug.Log("66666666666666666666");
            GameManager.Instance.EndGame();
        }

        if(collision.gameObject.layer == 7 && !isCatched && GameManager.Instance.isPlaying)
        {
            AudioManager.Instance.PauseAudio(AudioName.catched);
            isCatched = true;
            body.velocity = Vector3.zero;
            body.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !isCatched && GameManager.Instance.isPlaying)
        {
            Debug.Log("66666666666666666666");
            GameManager.Instance.EndGame();
        }

        if (other.gameObject.layer == 7 && !isCatched && GameManager.Instance.isPlaying)
        {
            AudioManager.Instance.PauseAudio(AudioName.catched);
            isCatched = true;
            body.velocity = Vector3.zero;
            body.useGravity = true;
        }
    }
}
