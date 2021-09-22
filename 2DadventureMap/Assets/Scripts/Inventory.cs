using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallBack;

    public List<Item> items = new List<Item>();

    public int space = 9;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More Inventory");
            return;
        }
        instance = this;    
    }
    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            return false;
        }
        items.Add(item);
        if (OnItemChangedCallBack != null)
        {
            OnItemChangedCallBack.Invoke();
        }
        
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (OnItemChangedCallBack != null)
        {
            OnItemChangedCallBack.Invoke();
        }
    }
}
