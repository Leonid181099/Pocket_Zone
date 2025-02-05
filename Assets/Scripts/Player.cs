using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour,IHP
{
    public float speed;
    public float maxHP;
    public float MaxHP
    {
        set {maxHP = value;}
        get { return maxHP; }
    }
    public float HP { get; set; }
    public FixedJoystick fixedJoystick;
    public GameObject Makarov;
    private GameObject gun;
    public void FixedUpdate()
    {
        transform.Translate(Vector2.up * Time.fixedDeltaTime * speed * fixedJoystick.Vertical+Vector2.right * Time.fixedDeltaTime * speed * fixedJoystick.Horizontal);
    }
    void Start()
    {
        //gun= new Makarov(gameObject);
        HP = MaxHP;
        gun= Instantiate(Makarov,transform);
    }
    public void Shoot()
    {
        Makarov comp = gun.GetComponent(typeof(Makarov)) as Makarov;
        comp.Shoot();
    }
}
