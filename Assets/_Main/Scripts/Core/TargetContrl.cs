using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetContrl : MonoBehaviour
{
    public Transform center;
    public float radius10, radius9, radius8, radius7;

    public void CheckScore(Transform tran)
    {
        var pos = tran.position;
        pos.x = center.position.x;
        float dis = Vector3.Distance(pos, center.position);
        if (dis <= radius10)
        {
            ScoreManager.Instance.Score += 10;
        }
        else if (dis > radius10 && dis <= radius9)
        {
            ScoreManager.Instance.Score += 9;
        }
        else if (dis > radius9 && dis <= radius8)
        {
            ScoreManager.Instance.Score += 8;
        }
        else if (dis > radius8 && dis <= radius7)
        {
            ScoreManager.Instance.Score += 7;
        }
        else
        {
            ScoreManager.Instance.Score += 0;
        }
    }

    public bool isShowGizmos;
    private void OnDrawGizmos()
    {
        if (!isShowGizmos) return;
        //10
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawSphere(center.position, radius10);
        //9
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawSphere(center.position, radius9);
        //8
        Gizmos.color = new Color(0, 0, 1, .5f);
        Gizmos.DrawSphere(center.position, radius8);
        //7
        Gizmos.color = new Color(1, 1, 0, .5f);
        Gizmos.DrawSphere(center.position, radius7);
    }
}
