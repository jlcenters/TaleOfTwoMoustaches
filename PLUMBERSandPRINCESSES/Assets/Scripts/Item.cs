using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    public void PickUp()
    {
        FindObjectOfType<Player>().AddToInventory(itemName);
        Destroy(gameObject);
    }
}
