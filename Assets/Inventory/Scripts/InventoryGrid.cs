using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGrid : MonoBehaviour
{
    private GameObject[,] Grid;
    public int X;
    public int Y;
    public int Width;
    public int Height;
    public int Xgap;
    public int Ygap;
    public GameObject SquarePrefab;
    public GameObject SquareSlotPrefab;
    private GameObject Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = Instantiate(SquarePrefab, transform.Find("Canvas"));
        //SpriteRenderer spriteRenderer = Inventory.transform.GetComponent<SpriteRenderer>();
        //spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        //spriteRenderer.size = new Vector2(300, 200);
        //Inventory.transform.localScale = new Vector3(300, 200, 1);
        RectTransform RT = Inventory.transform.GetComponent<RectTransform>();
        int width = X * Width + (X + 1) * Xgap;
        int height = Y * Height + (Y + 1) * Ygap;
        RT.sizeDelta = new Vector2(width, height);
        Grid = new GameObject[X, Y];
        int Xcoord;
        int Ycoord;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Xcoord = i * Width + (i + 1) * Xgap;
                Ycoord = j * Height + (j + 1) * Ygap;
                Grid[i, j]= Instantiate(SquareSlotPrefab, Inventory.transform);
                RectTransform GridRT = Grid[i, j].transform.GetComponent<RectTransform>();
                GridRT.sizeDelta = new Vector2(Width, Height);
                Grid[i, j].transform.localPosition += new Vector3(Xcoord, -Ycoord,0);
                Grid[i, j].transform.GetComponent<Image>().preserveAspect = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Loot");
        for (int i = 0;i< gameObjectArray.Length; i++)
        {
            put(gameObjectArray[i]);
        }
    }
    void put(GameObject Item)
    {
        bool flag=false;
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                InventorySlot comp = Grid[i, j].GetComponent(typeof(InventorySlot)) as InventorySlot;
                if (comp.put(Item))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
                break;
        }
    }
    void delete(GameObject GridIJ)
    {
        InventorySlot comp = GridIJ.GetComponent(typeof(InventorySlot)) as InventorySlot;
        comp.delete(SquareSlotPrefab);
    }
}
