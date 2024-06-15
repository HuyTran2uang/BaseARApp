using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public Transform CenterEye;
    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        var direction = CenterEye.right * input.x + CenterEye.forward * input.y;
        direction.y = 0;
        transform.position += direction * 2 * Time.deltaTime;
    }

    private void Rotate()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        transform.eulerAngles += Vector3.up * 30 * input.x * Time.deltaTime;
    }
}
