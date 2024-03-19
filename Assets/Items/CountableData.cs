using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableData : ItemData
{
    [SerializeField][Range(20, 99)] private int _maxAmount = 99;

}
