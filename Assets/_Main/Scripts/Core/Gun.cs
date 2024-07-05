using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPoint1, shootPoint2;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (GameManager.Instance.bullets > 0)
            {
                AudioManager.Instance.PlayAudioOnceShot(AudioName.shoot);
                GameManager.Instance.bullets--;
                var direction = Vector3.Normalize(shootPoint2.position - shootPoint1.position);
                var bullet = BulletSpawner.Instance.Spawn();
                bullet.transform.position = shootPoint1.position;
                bullet.Init(direction);
                if (GameManager.Instance.bullets <= 0)
                {
                    StartCoroutine(Simple.Utilities.IEDelayCall(1, () =>
                    {
                        GameManager.Instance.End();
                    }));
                }
            }
        }
    }
}
