using System.Collections;
using System.Collections.Generic;
using System.IO; // File, Directory ����� ���� using
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{

    public static InfoManager instance;

/*    public static InfoManager GetInstance()
    {
        if (instance == null)
        {
            instance = new InfoManager();
        }
        return instance;
    }
*/

    [SerializeField] Info player_info;
    public Text ID_Text;
    public Text Point_Text;
    public Text Gold_Text;

    private void Awake()
    {
        player_info = new Info();
        instance = this;

        var loadedJson = Resources.Load<TextAsset>("info");
        // ���ҽ� ������ �ִ� info(Text Asset)�� �ε� �ϰڽ��ϴ�.

        player_info = JsonUtility.FromJson<Info>(loadedJson.text);
        // JsonUtility.FromJson<T>(string json);
        // json ���Ϸκ��� �о�� ������ �������� �����͸� �����ϴ� �ڵ�

        LoadData2();
    }
    


    private void SetText()
    {
        ID_Text.text = "ID : " + player_info.name;
        Point_Text.text = player_info.point.ToString();
        Gold_Text.text = player_info.gold.ToString();
    }


    /// <summary>
    /// ����Ʈ�� ����ؼ� ���� �����ϴ� �ڵ� (100 ����Ʈ -> 10000 ���)
    /// </summary>
    public void GoldPlus()
    {
        if (player_info.point >= 100)
        {
            player_info.point -= 100;
            player_info.gold += 10000;

            // JsonUtility.ToJson(Object obj);
            // ��ü�� ������ Json ���Ϸ� ������ ���
            // �÷��̾� ������ json ���Ͽ� ����
            SaveData(player_info);
            SetText();
        }
        else
        {
            Debug.Log("��ȯ�� ����Ʈ�� �����մϴ�.");
        }
    }

    private string ResourcePath => Application.dataPath + "/Resources/";

    private string SavePath => Application.persistentDataPath;
    // ���� ������ ������ ��ġ, Ư�� �ü������ ���� ����� �� �ֵ��� ����ϴ� ���
    // C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]

    private string DataPath => Application.dataPath;
    // �������� ���� ���(�б�����)���� ������Ʈ ���� ����(Asset)�� �ǹ���

    private string StreamingPath => Application.streamingAssetsPath;
    // Application.dataPath + StreamingAssets = Application.streamingAssetsPath

    public void SaveData(Info info)
    {
        // ������ ���� ��쿡�� ������ ����
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(info); // 1. json ������ ������ string ���·� ����

        var FilePath = ResourcePath + "info.json";

        File.WriteAllText(FilePath, sJson);
    }

    public void SaveData2()
    {
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(player_info); // 1. json ������ ������ string ���·� ����

        var FilePath = Path.Combine(DataPath, "info.json");

        File.WriteAllText(FilePath, sJson);
    }

    public Info LoadData(string path)
    {
        player_info = null; // Ŭ���� ��ü ���� (���ص� ����� ����)

        if (File.Exists(path)) // ������ ������ �н��� ������ ���
        {
            var json = File.ReadAllText(path); // �ش� ��ηκ��� ������ �о�ɴϴ�.
            player_info = JsonUtility.FromJson<Info>(json); // �о�� ������ ���� Info�� ���� ����
        }

        return player_info; // �ϼ��� ��ü ����
    }

    public void LoadData2()
    {
        var data = File.ReadAllText(ResourcePath + "info.json");
        player_info = JsonUtility.FromJson<Info>(data);

        SetText();

    }

    public void PlusPoint(int point)
    {
        player_info.point += point;
        SaveData(player_info);
        SetText();
    }

    public int GetGold()
    {
        return player_info.gold;
    }

    public void UseGold(int gold)
    {
        player_info.gold -= gold;
        SaveData(player_info);
        SetText();
    }

}
