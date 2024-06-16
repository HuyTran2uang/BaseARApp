using UnityEngine;

public class Stick : BaseItem
{
    public Collider col;
    public Rigidbody rb;
    public Vector3 velocity;
    public bool isCatched;
    public float gravity = -9.81f;

    private void Start()
    {
        OnSelectedEvent += Catched;
    }

    private void OnDisable()
    {
        StickSpawner.Instance?.AddToPool(this);
    }

    public void Init()
    {
        rb.velocity = Vector3.zero;
        col.enabled = false;
        rb.useGravity = false;
        velocity = Vector3.zero;
        isCatched = false;
        gameObject.SetActive(true);
    }

    public void Fall()
    {
        col.enabled = true;
        rb.useGravity = true;
    }

    public void Catched()
    {
        isCatched = true;
        col.enabled = false;
        rb.useGravity = false;
    }

    public void UnCatch()
    {
        if (CatchStickMachine.Instance.IsPlaying && !isCatched)
        {
            CatchStickMachine.Instance.Lose();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            UnCatch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            UnCatch();
        }
    }
}
