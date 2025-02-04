using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Makarov : MonoBehaviour, IGun
{
    public GameObject Player;
    public int Ammo;
    public float Damage;
    public float RateOfFire;
    public bool allowFire;
    void Start()
    {
        Player = transform.parent.gameObject;
    }
    public void Shoot()
    {
        float dist = 6f;
        if (Ammo > 0)
        {
            var ememyDist = FindClosestEnemy(Player, dist);
            if (ememyDist.Item1 != null)
            {
                if (allowFire == true)
                {
                    StartCoroutine(Shooting(ememyDist.Item1));
                }
                
                //Debug.Log($"Pew!{Ammo}");
            }
        }
        
    }
    IEnumerator Shooting(GameObject enemy)
    {
        allowFire = false;
        Ammo--;
        MakeDamage(enemy, Damage);
        yield return new WaitForSeconds(1/ RateOfFire);
        allowFire = true;
    }
    private void MakeDamage(GameObject enemy, float damage)
    {
        EventManager.SendTakeDamage(enemy,damage);
    }
    private (GameObject, float) FindClosestEnemy(GameObject Player, float dist)
    {
        float distanceMin = dist + 1f;
        GameObject closestEnemy = null;
        for (int i = 0; i < 360; i++)
        {
            Vector2 vector = new Vector2((float)Math.Cos(i * (float)Math.PI / 180f) * dist, (float)Math.Sin(i * (float)Math.PI / 180f) * dist);
            RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, vector);
            if (hit)
            {
                float distance = (float)Math.Sqrt(Math.Pow(hit.point.y - Player.transform.position.y, 2) + Math.Pow(hit.point.x - Player.transform.position.x, 2));
                if (distanceMin > distance)
                {
                    distanceMin = distance;
                    closestEnemy = hit.transform.gameObject;
                }
            }
        }
        //Debug.Log(distanceMin);
        return (closestEnemy,distanceMin);

    }
}
