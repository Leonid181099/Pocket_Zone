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
    private void Awake()
    {
        EventManager.OnTakeDamage.AddListener(TakeDamage);
    }
    private void TakeDamage(GameObject enemy, float damage)
    {
        if (gameObject == enemy)
        {
            HP = Math.Max(0f, HP - damage);
        }
    }
    public void FixedUpdate()
    {
        if (HP <= 0)
        {
            Death();
        }
        PickUpLoot();
        transform.Translate(Vector2.up * Time.fixedDeltaTime * speed * fixedJoystick.Vertical+Vector2.right * Time.fixedDeltaTime * speed * fixedJoystick.Horizontal);
    }
    void Start()
    {
        //gun= new Makarov(gameObject);
        HP = MaxHP;
        gun= Instantiate(Makarov,transform);
        gun.tag = "Weapon";
    }
    public void Shoot()
    {
        IGun comp = gun.GetComponent(typeof(IGun)) as IGun;
        comp.Shoot();
    }
    void Death()
    {
        GameObject camera = gameObject.transform.Find("Main Camera").gameObject;
        camera.transform.SetParent(null);
        Destroy(gameObject);
    }
    void PickUpLoot()
    {

    }
}
