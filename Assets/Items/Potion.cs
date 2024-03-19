using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Countable, Usable
{
    public bool Use()
    {
        // 포션의 개수가 충반할 경우 포션의 개수를 1 감소합니다

        return true;
    }
}
