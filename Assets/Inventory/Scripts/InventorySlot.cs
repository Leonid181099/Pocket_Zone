using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            transform.Find("Button(Clone)").gameObject.SetActive(true);
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
                transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().text=amount.ToString();
                Destroy(Item);
                return true;
            }
        }
        return false;
    }
    public void delete(GameObject SquareSlotPrefab)
    {
        amount--;
        if (amount > 1)
            transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().text = amount.ToString();
        if (amount<=1)
            transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().text = "";
        if (amount == 0)
        {
            Image image = gameObject.GetComponent<Image>();
            image.sprite = SquareSlotPrefab.GetComponent<Image>().sprite;
            itemId = "";
            transform.Find("Button(Clone)").gameObject.SetActive(false);
            transform.Find("Button(Clone)").Find("Button 1(Clone)").gameObject.SetActive(false);
        }
    }
}
