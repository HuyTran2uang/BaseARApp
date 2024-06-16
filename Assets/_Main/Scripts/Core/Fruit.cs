using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, ITakeDamage
{
    public void Init()
    {
        gameObject.SetActive(true);
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}
