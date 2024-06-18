using UnityEngine;

public class Ball : MonoBehaviourSingleton<Ball>, IItem
{
    public Rigidbody rb;
    public int checkPoint;
    private Vector3 oldPos, newPos;

    public bool IsSelected { get; private set; }

    public void Select(Transform selecter)
    {
        if (IsSelected) return;
        IsSelected = true;
        rb.isKinematic = true;
        oldPos = transform.position;
        newPos = oldPos;
    }

    public void Unselect()
    {
        if (!IsSelected) return;
        IsSelected = false;
        rb.isKinematic = false;
        checkPoint = 0;
    }

    private void FixedUpdate()
    {
        if (!IsSelected) return;
        newPos = transform.position;
        Vector3 direction = Vector3.Normalize(newPos - oldPos);
        float force = (newPos - oldPos).magnitude / Time.deltaTime;

        rb.AddForce(direction * force, ForceMode.Impulse);

        oldPos = newPos;
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
