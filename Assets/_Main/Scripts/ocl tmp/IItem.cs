using UnityEngine;

public interface IItem
{
    bool IsSelected { get; }
    void Select(Transform selecter);
    void Unselect();
}
