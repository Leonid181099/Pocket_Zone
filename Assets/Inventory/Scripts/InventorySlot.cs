using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot:MonoBehaviour
{
    //GameObject item;
    public string itemId="";
    public int amount=0;
    public GameObject Pic=null;
    public Sprite slotSprite=null;
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
            slotSprite=image.sprite;
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
            slotSprite = SquareSlotPrefab.GetComponent<Image>().sprite;
            image.sprite = slotSprite;
            itemId = "";
            transform.Find("Button(Clone)").gameObject.SetActive(false);
            transform.Find("Button(Clone)").Find("Button 1(Clone)").gameObject.SetActive(false);
        }
    }
    public void update(GameObject SquareSlotPrefab)
    {
        if (amount == 0)
        {
            Image image = gameObject.GetComponent<Image>();
            slotSprite = SquareSlotPrefab.GetComponent<Image>().sprite;
            image.sprite = slotSprite;
            itemId = "";
            transform.Find("Button(Clone)").gameObject.SetActive(false);
            transform.Find("Button(Clone)").Find("Button 1(Clone)").gameObject.SetActive(false);
        }
        if (amount == 1)
        {
            Image image = gameObject.GetComponent<Image>();
            image.sprite = slotSprite;
            transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().text = "";
            transform.Find("Button(Clone)").gameObject.SetActive(true);
            transform.Find("Button(Clone)").Find("Button 1(Clone)").gameObject.SetActive(false);
        }
        if (amount > 1)
        {
            Image image = gameObject.GetComponent<Image>();
            image.sprite = slotSprite;
            transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().text = amount.ToString();
            transform.Find("Button(Clone)").gameObject.SetActive(true);
            transform.Find("Button(Clone)").Find("Button 1(Clone)").gameObject.SetActive(false);
        }
    }
}
