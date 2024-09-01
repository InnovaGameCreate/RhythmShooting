using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class notesmanager : MonoBehaviour
{
    public GameObject prefab;                                   //生成するプレハブ
    public Transform[] spawnPoints;                             //0~14の座標を保持する配列
    public Canvas canvas;                                       //プレハブを配置するCanvas
    private List<SpawnData> spawnList = new List<SpawnData>();　

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV("notesManager");
        StartCoroutine(SpawnPrefabs());
    }

    void LoadCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    //ResourcesフォルダからCSVファイルを読み込む
        print(csvFile);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if(values.Length == 2 )
                {
                    float time = float.Parse(values[0]);
                    int id = int.Parse(values[1]);
                    if (id >= 0 && id < spawnPoints.Length)
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

    IEnumerator SpawnPrefabs()
    {
        foreach(var spawnData in spawnList)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);
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
