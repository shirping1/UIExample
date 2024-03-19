using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items
{
    public ItemData Data {  get; private set; }
    public Items(ItemData data)
    {
        Data = data;
    }
}
