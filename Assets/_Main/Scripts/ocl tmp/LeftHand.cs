using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public Gun _gun;
    public Transform _gunHandle;
    public Transform _rightHand;

    private void Update()
    {
        //if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        //{
        //    var cols = Physics.OverlapSphere(transform.position, .5f);
        //    foreach (var col in cols)
        //    {
        //        if(col.tag == "Handle")
        //        {
        //            _gunHandle = col.transform;
        //        }
        //    }
        //}
        //if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        //{
        //    if(_gunHandle != null)
        //    {
        //        Vector3 direction = Vector3.Normalize(_gunHandle.position - _rightHand.position);
        //        _gun.transform.eulerAngles = Quaternion.LookRotation(direction).eulerAngles;
        //    }
        //}
        //if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        //{
        //    _gunHandle = null;
        //}
    }
}
