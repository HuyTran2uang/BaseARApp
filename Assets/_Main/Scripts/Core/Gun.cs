using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPoint1, shootPoint2;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            AudioManager.Instance.PlayAudioOnceShot(AudioName.shoot);
            var direction = Vector3.Normalize(shootPoint2.position - shootPoint1.position);
            var bullet = BulletSpawner.Instance.Spawn();
            bullet.transform.position = shootPoint1.position;
            bullet.Init(direction);
        }
    }
}
