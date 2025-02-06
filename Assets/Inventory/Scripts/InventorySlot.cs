using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot:MonoBehaviour
{
    //GameObject item;
    string itemId="";
    int amount=0;
    GameObject Pic=null;
    public bool put(GameObject Item)
    {
        Id comp = Item.GetComponent(typeof(Id)) as Id;
        string ItemId = comp.id;
        if (amount == 0)
        {
            itemId = ItemId;
            amount = 1;
            Pic = Item.transform.Find("Pic").gameObject;
            //PicObject=Instantiate(Pic,transform);
            Image image=gameObject.GetComponent<Image>();
            image.sprite = Pic.GetComponent<SpriteRenderer>().sprite;
            Destroy(Item);
            return true;
        }
        else
        {
            if (itemId == ItemId)
            {
                amount ++;
                Destroy(Item);
                return true;
            }
        }
        return false;
    }
    public void delete(GameObject SquareSlotPrefab)
    {
        amount--;
        if (amount == 0)
        {
            Image image = gameObject.GetComponent<Image>();
            image.sprite = SquareSlotPrefab.GetComponent<Image>().sprite;
            itemId = "";
        }
    }
}
