using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [SerializeField] private int _id; // 아이템 식별 번호
    [SerializeField] private string _name; // 아이템 이름
    [SerializeField] private string _description; // 아이템 설명
    [SerializeField] private Sprite _icon; // 아이템 이미지(아이콘)
    [SerializeField] private GameObject _prefab; // 아이템 드롭 시 표현할 게임 오브젝트

    public abstract Items Create();
}
