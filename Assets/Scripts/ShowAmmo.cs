using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAmmo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Ammo;
    GameObject Smbd;
    void Start()
    {
        Smbd = GameObject.FindGameObjectWithTag("Weapon");
    }
    void Update()
    {
        IAmmo comp = Smbd.GetComponent(typeof(IAmmo)) as IAmmo;
        _Ammo.text=(comp.Ammo).ToString();
    }
}
