using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "SO/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> iList;

    /// <summary>
    /// 아이템 생성 함수
    /// </summary>
    /// <returns></returns>
    public static ItemList Create()
    {
        var asset = CreateInstance<ItemList>();
        // CreateInstance는 ScriptableObject에서 인스턴스를 생성하는 기능

        AssetDatabase.CreateAsset(asset, "Assets/Resources/Item/itemExample01.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    public static ItemList Load()
    {
        var itemlist = AssetDatabase.LoadAssetAtPath("Assets/Resources/Item/itemExample01.asset", typeof(ItemList)) as ItemList;
        // 에셋 데이터베이스에서 해당 경로에 있는 해당 유형의 오브젝트 하나를 가져와 불러온다.
        return itemlist;
    }
}

