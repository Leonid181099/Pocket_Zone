using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Zombie : MonoBehaviour,IHP
{
    public float maxHP;
    public float MaxHP
    {
        set { maxHP = value; }
        get { return maxHP; }
    }
    public float HP {  get; set; }
    public float Speed;
    public float ATK;
    public float viewingRange;
    public float attackRange;
    public float attackSpeed;
    private GameObject Player;
    private void Awake()
    {
        EventManager.OnTakeDamage.AddListener(TakeDamage);
    }
    private void TakeDamage(GameObject enemy, float damage)
    {
        if (gameObject == enemy)
        {
            HP=Math.Max(0f,HP-damage);
        }
    }
    void Start()
    {
        HP = MaxHP;
    }
    void FixedUpdate()
    {
        if (HP <= 0)
        {
            Death();
        }
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjectArray.Length>0)
        {
            Player =FindClosestGameObjectFromArray(gameObjectArray);
        }
        else
        {
            Player = null;
        }
        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= viewingRange)
        {
            Chase(Player);
        }
    }
    void Chase(GameObject smth)
    {
        transform.position = Vector2.MoveTowards(transform.position, smth.transform.position, Speed* Time.fixedDeltaTime);
    }
    GameObject FindClosestGameObjectFromArray(GameObject[] array)
    {
        float minDist= DistFromMe(array[0]);
        int it = 0;
        float dist = 0;
        for (int i = 1; i < array.Length; i++)
        {
            dist = DistFromMe(array[i]);
            if (dist < minDist)
            {
                minDist = dist;
                it = i;
            }
        }
        return array[it];
    }
    float DistFromMe(GameObject smth)
    {
        return Vector2.Distance(gameObject.transform.position, smth.transform.position);
        //(float)Math.Sqrt(Math.Pow(gameObject.transform.position.y - smth.transform.position.y, 2) + Math.Pow(gameObject.transform.position.x - smth.transform.position.x, 2));
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
