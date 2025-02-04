using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed;
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
        gun= Instantiate(Makarov,transform);
    }
    public void Shoot()
    {
        Makarov comp = gun.GetComponent(typeof(Makarov)) as Makarov;
        comp.Shoot();
    }
}
