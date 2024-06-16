using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPoint1, shootPoint2;
    public ParticleSystem shootvfx;
    private bool isPressing;

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > .9f && !isPressing)
        {
            isPressing = true;
            AudioManager.Instance.PlayAudioOnceShot(AudioName.shoot);
            shootvfx.Play();
            Vector3 direction = Vector3.Normalize(shootPoint2.position - shootPoint1.position);
            var bullet = BulletSpawner.Instance.Spawn();
            bullet.transform.position = shootPoint1.position;
            bullet.Init(direction);
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < .2f && isPressing)
        {
            isPressing = false;
        }
    }
}
