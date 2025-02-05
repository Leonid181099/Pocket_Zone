using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    GameObject Smbd;
    [SerializeField] private Image _healthBarFilling;
    void Start()
    {
        Smbd= transform.parent.gameObject.transform.parent.gameObject;
    }
    void Update()
    {
        IHP comp = Smbd.GetComponent(typeof(IHP)) as IHP;
        _healthBarFilling.fillAmount=comp.HP/comp.MaxHP;
    }
}
