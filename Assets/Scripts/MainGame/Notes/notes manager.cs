using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class notesmanager : MonoBehaviour
{
    public GameObject prefab;                                   //生成するプレハブ
    public Transform[] spawnPoints;                             //0~14の座標を保持する配列
    public Canvas Canvas;                                       //プレハブを配置するCanvas
    private List<SpawnData> spawnList = new List<SpawnData>();　

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV("notesManager");            //メソッド呼び出し
        StartCoroutine(SpawnPrefabs());     //プレハブを生成する処理
    }

    //CSVファイルを読み込み、その内容に基づいてデータを解析し、spawnListにSpawnDataオブジェクトを追加する処理
    void LoadCSV(string fileName)   //fileNameという文字列を引数として受け取り、指定されたCSVファイルを読み込み
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    //ResourcesフォルダからCSVファイルを読み込む
        print(csvFile);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');              //CSVファイルの内容を行ごとに分割して、linesという文字列配列に格納
            foreach (string line in lines)                          //すべての行に対してループ処理
            {
                string[] values = line.Split(',');                  //各行をカンマ,で分割し、valuesという文字列配列に格納
                if (values.Length == 2)                             //行が2つの値を持っているかを確認
                {
                    float time = float.Parse(values[0]);            //values[0]の値をfloat型に変換し、timeという変数に格納
                    int id = int.Parse(values[1]);                  //values[1]の値をint型に変換し、idという変数に格納
                    if (id >= 0 && id < spawnPoints.Length)         //timeとidの情報を使って、新しいSpawnDataオブジェクトを作成し、spawnListに追加
                    {
                        spawnList.Add(new SpawnData(time, id));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが見つかりません;" + fileName);
        }
    }

    IEnumerator SpawnPrefabs()  //spawnList内の各SpawnDataオブジェクトに基づいて、プレハブを生成
    {
        foreach(var spawnData in spawnList)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);                           //spawnData.spawnTime秒待機
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoints[spawnData.spawnID]); //プレハブを生成し、Canvas内の指定された位置の子オブジェクトとして配置
            spawnedPrefab.transform.localPosition = Vector3.zero;                           //親オブジェクトのRectTransformに従うように設定
            spawnedPrefab.transform.localScale = Vector3.one;

        }
    }
    [System.Serializable]
    public struct SpawnData
    {
        public float spawnTime;
        public int spawnID;
        public SpawnData(float time, int id)
        {
            spawnTime = time;
            spawnID = id;
        }
    }
}
