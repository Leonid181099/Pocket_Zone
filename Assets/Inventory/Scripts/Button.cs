using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void OpenClose()
    {
        GameObject Button1 = transform.Find("Button 1(Clone)").gameObject;
        Button1.SetActive(!Button1.activeSelf);
    }
}
