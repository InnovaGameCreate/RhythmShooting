using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesManager : MonoBehaviour
{
    public GameObject prefab; // 生成するプレハブ（UIエレメント）
    public Transform[] spawnPoints; // 0-14の座標を保持する配列（Canvas内のUI要素として配置）
    public Canvas canvas; // プレハブを配置するCanvas
    private List<SpawnData> spawnList = new List<SpawnData>();

    void Start()
    {
        LoadCSV("notesManager");
        StartCoroutine(SpawnPrefabs());
        
    }

    void LoadCSV(string fileName)
    {
        // ResourcesフォルダからCSVファイルを読み込む
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        print(csvFile);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                if (values.Length == 2)
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
            Debug.LogError("CSVファイルが見つかりません: " + fileName);
        }
    }

    IEnumerator SpawnPrefabs()
    {
        foreach (var spawnData in spawnList)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);

            // プレハブを生成し、Canvas内の指定された位置の子オブジェクトとして配置
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoints[spawnData.spawnID]);

            // 親オブジェクトのRectTransformに従うように設定
            spawnedPrefab.transform.localPosition = Vector3.zero;
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