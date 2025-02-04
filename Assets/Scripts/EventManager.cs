using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<GameObject, float> OnTakeDamage=new UnityEvent<GameObject, float>();
    public static void SendTakeDamage(GameObject enemy, float damage)
    {
        OnTakeDamage.Invoke(enemy,damage);
    }
}
