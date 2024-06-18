using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour
{
    public ColorType color;   

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.isPlaying) return;
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Ball>().TakeDamage(color);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.isPlaying) return;
        if (other.gameObject.tag == "Ball")
        {
            other.gameObject.GetComponent<Ball>().TakeDamage(color);
        }
    }
}

public enum ColorType
{
    red,
    blue
}