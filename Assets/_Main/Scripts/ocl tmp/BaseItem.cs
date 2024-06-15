using System;
using UnityEngine;
using UnityEngine.Events;

public class BaseItem : MonoBehaviour, IItem
{
    public Action OnSelectedEvent;
    public Action OnUnSelectEvent;
    [HideInInspector] public Transform HoldingObject;
    public bool IsSelected { get; private set; }

    public virtual void Select(Transform selecter)
    {
        if (IsSelected) return;
        IsSelected = true;
        transform.SetParent(selecter);
        HoldingObject = selecter;
        OnSelectedEvent?.Invoke();
    }

    public virtual void Unselect()
    {
        IsSelected = false;
        transform.SetParent(null);
        HoldingObject = null;
        OnUnSelectEvent?.Invoke();
    }
}
