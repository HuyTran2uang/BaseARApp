using UnityEngine;

public class Ball : MonoBehaviourSingleton<Ball>, IItem
{
    public Rigidbody rb;
    public int checkPoint;
    private Vector3 oldPos;
    private float timer;
    public bool IsSelected { get; private set; }
    private float force;
    private Vector3 direction;

    public void Select(Transform selecter)
    {
        if (IsSelected) return;
        IsSelected = true;
        rb.isKinematic = true;
        oldPos = transform.position;
    }

    public void Unselect()
    {
        if (!IsSelected) return;
        IsSelected = false;
        rb.isKinematic = false;
        checkPoint = 0;
        rb.AddForce(direction * 2 * force / Time.deltaTime, ForceMode.Impulse);
    }

    private void CachePos()
    {
        force = Vector3.Distance(oldPos, transform.position);
        direction = Vector3.Normalize(transform.position - oldPos);
        oldPos = transform.position;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!IsSelected) return;
        CachePos();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("checkpoint1") && checkPoint == 0)
        {
            checkPoint++;
        }

        if (collision.gameObject.CompareTag("checkpoint2") && checkPoint == 1)
        {
            checkPoint++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint1") && checkPoint == 0)
        {
            checkPoint++;
        }

        if (other.CompareTag("checkpoint2") && checkPoint == 1)
        {
            checkPoint++;
        }
    }
}
