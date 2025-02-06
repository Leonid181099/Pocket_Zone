using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpenClose : MonoBehaviour
{
    public GameObject Inventory;
    public void OpenClose()
    {
        Inventory.SetActive(!Inventory.activeSelf);
    }
}