using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ��ũ���ͺ� ������Ʈ(Scriptable Object)
// ����Ƽ���� �����ϴ� �뷮�� �����͸� ������ �� �ִ� ������ �����̳��Դϴ�.
// ���� �纻�� �����Ǵ� ���� ���� �� �� �ֽ��ϴ�.

// ���� ������Ʈ�� ������Ʈ�� ������ �Ұ����ϸ�, ������Ʈ���� �������� �����

public enum ItemTYPE
{
    WEAPON, ARMOR, POTION
}

[CreateAssetMenu(fileName = "item", menuName = "SO/Item")]

public class Item : ScriptableObject
{
    [SerializeField] public ItemTYPE type;
    [SerializeField] public string description;

    public ItemTYPE Type { get => type; set => type = value; }
    public string Description { get => description; set => description = value; }

    public static Item Create()
    {
        var asset = CreateInstance<Item>();
        // CreateInstance�� ScriptableObject���� �ν��Ͻ��� �����ϴ� ���

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/item1.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static Item Load()
    {
        var item = AssetDatabase.LoadAssetAtPath("Assets/Resources/Item/item1.asset", typeof(Item)) as Item;
        // ���� �����ͺ��̽����� �ش� ��ο� �ִ� �ش� ������ ������Ʈ �ϳ��� ������ �ҷ��´�.
        return item;
    }
}
