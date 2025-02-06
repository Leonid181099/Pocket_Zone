using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button1 : MonoBehaviour
{
    public void Click()
    {
        GameObject Slot = transform.parent.parent.gameObject;
        GameObject Grid = Slot.transform.parent.parent.parent.gameObject;
        InventoryGrid comp = Grid.GetComponent(typeof(InventoryGrid)) as InventoryGrid;
        comp.delete(Slot);
    }
}
