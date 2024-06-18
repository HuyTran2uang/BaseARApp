using System;
using UnityEngine;

public class BaseHand : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right
    }
    public HandType Hand;
    private bool _isHandPressing;
    private IItem _item;
    public Action OnUpdate;
    public float Radius = .05f;

    private void Start()
    {
        OnUpdate += Hand == HandType.Left ? LeftHand : RightHand;
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void LeftHand()
    {
        float primaryHandTrigger = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);

        if (primaryHandTrigger > .9f)
        {
            if (!_isHandPressing)
            {
                _isHandPressing = true;
                PickUp();
            }
        }

        if (primaryHandTrigger < .1f)
        {
            _isHandPressing = false;
            _item?.Unselect();
            _item = null;
        }
    }

    private void RightHand()
    {
        float primaryHandTrigger = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);

        if (primaryHandTrigger > .9f)
        {
            if (!_isHandPressing)
            {
                _isHandPressing = true;
                PickUp();
            }
        }

        if (primaryHandTrigger < .1f)
        {
            _isHandPressing = false;
            _item?.Unselect();
            _item = null;
        }
    }

    private void PickUp()
    {
        var hits = Physics.OverlapSphere(transform.position, Radius);
        foreach (var hit in hits)
        {
            //check item
            _item = hit.GetComponent<IItem>();

            if (_item == null) continue;
            if (_item.IsSelected)
            {
                _item = null;
                continue;
            }
            else
            {
                _item?.Select(transform);
            }
            
            break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
