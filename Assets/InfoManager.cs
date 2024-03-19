using System.Collections;
using System.Collections.Generic;
using System.IO; // File, Directory 사용을 위한 using
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
        // 리소스 폴더에 있는 info(Text Asset)를 로드 하겠습니다.

        player_info = JsonUtility.FromJson<Info>(loadedJson.text);
        // JsonUtility.FromJson<T>(string json);
        // json 파일로부터 읽어온 파일을 기준으로 데이터를 적용하는 코드

        LoadData2();
    }
    


    private void SetText()
    {
        ID_Text.text = "ID : " + player_info.name;
        Point_Text.text = player_info.point.ToString();
        Gold_Text.text = player_info.gold.ToString();
    }


    /// <summary>
    /// 포인트를 사용해서 골드로 변경하는 코드 (100 포인트 -> 10000 골드)
    /// </summary>
    public void GoldPlus()
    {
        if (player_info.point >= 100)
        {
            player_info.point -= 100;
            player_info.gold += 10000;

            // JsonUtility.ToJson(Object obj);
            // 객체의 정보를 Json 파일로 보내는 기능
            // 플레이어 정보를 json 파일에 전달
            SaveData(player_info);
            SetText();
        }
        else
        {
            Debug.Log("교환할 포인트가 부족합니다.");
        }
    }

    private string ResourcePath => Application.dataPath + "/Resources/";

    private string SavePath => Application.persistentDataPath;
    // 쓰기 가능한 폴더의 위치, 특정 운영체제에서 앱이 사용할 수 있도록 허용하는 경로
    // C:\Users\[user name]\AppData\LocalLow\[company name]\[product name]

    private string DataPath => Application.dataPath;
    // 데이터의 저장 경로(읽기전용)으로 프로젝트 폴더 내부(Asset)을 의미함

    private string StreamingPath => Application.streamingAssetsPath;
    // Application.dataPath + StreamingAssets = Application.streamingAssetsPath

    public void SaveData(Info info)
    {
        // 폴더가 없을 경우에는 폴더를 생성
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(info); // 1. json 파일의 정보를 string 형태로 저장

        var FilePath = ResourcePath + "info.json";

        File.WriteAllText(FilePath, sJson);
    }

    public void SaveData2()
    {
        if (!Directory.Exists(ResourcePath))
        {
            Directory.CreateDirectory(ResourcePath);
        }
        var sJson = JsonUtility.ToJson(player_info); // 1. json 파일의 정보를 string 형태로 저장

        var FilePath = Path.Combine(DataPath, "info.json");

        File.WriteAllText(FilePath, sJson);
    }

    public Info LoadData(string path)
    {
        player_info = null; // 클래스 객체 비우기 (안해도 상관은 없음)

        if (File.Exists(path)) // 파일이 전달한 패스에 존재할 경우
        {
            var json = File.ReadAllText(path); // 해당 경로로부터 파일을 읽어옵니다.
            player_info = JsonUtility.FromJson<Info>(json); // 읽어온 내용을 통해 Info에 값을 전달
        }

        return player_info; // 완성된 객체 리턴
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
