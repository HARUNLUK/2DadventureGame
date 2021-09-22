using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interact
{
    public Item item;
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
    }
    void PickUp()
    {
        if (Inventory.instance.Add(item))
        {
            Debug.Log(item.name +" picked");
            Destroy(gameObject);
        }
    }
    public override void Interacted()
    {
        base.Interacted();
        PickUp();
    }
}
