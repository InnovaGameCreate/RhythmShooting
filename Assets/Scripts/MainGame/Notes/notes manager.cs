using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public GameObject notesPrefab;                                   // 生成するノーツのプレハブ
    public Transform[] spawnPoints;                                  // ノーツ生成用の座標
    public Canvas canvas;                                            // プレハブを配置するCanvas
    public GameObject flowerPrefab;                                  // 生成するFlowerプレハブ
    public Transform flowerSpawnPosition;                            // Flower生成の開始位置
    public Transform targetPosition;                                 // Flowerが移動する目標位置
    private List<SpawnData> spawnList = new List<SpawnData>();
    private float sceneStartTime;

    // Start is called before the first frame update
    void Start()
    {
        sceneStartTime = Time.time;  // シーン開始時の時間を記録
        LoadCSV("notesManager");     // CSVファイルを読み込むメソッド呼び出し
        StartCoroutine(SpawnPrefabs()); // プレハブを生成する処理
    }

    // CSVファイルを読み込み、その内容に基づいてデータを解析し、spawnListにSpawnDataオブジェクトを追加する処理
    void LoadCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    // ResourcesフォルダからCSVファイルを読み込む
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');              // CSVファイルの内容を行ごとに分割して、linesという文字列配列に格納
            foreach (string line in lines)                          // すべての行に対してループ処理
            {
                string[] values = line.Split(',');                  // 各行をカンマで分割し、valuesという文字列配列に格納
                if (values.Length == 2)                             // 行が2つの値を持っているかを確認
                {
                    float time = float.Parse(values[0]);            // values[0]の値をfloat型に変換し、timeという変数に格納
                    int id = int.Parse(values[1]);                  // values[1]の値をint型に変換し、idという変数に格納
                    if (id >= 0 && id < spawnPoints.Length)         // timeとidの情報を使って、新しいSpawnDataオブジェクトを作成し、spawnListに追加
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
            float currentTime = Time.time - sceneStartTime; // 経過時間を計算
            float timeToWait = spawnData.spawnTime - currentTime; // 現在時間との差分を待機時間として計算

            // ノーツの生成タイミングまで待機
            if (timeToWait > 0)
            {
                yield return new WaitForSeconds(timeToWait);
            }

            // Flowerをノーツの2秒前に生成
            StartCoroutine(SpawnFlower(spawnData.spawnTime - 2f));

            // ノーツを生成
            GameObject spawnedPrefab = Instantiate(notesPrefab, spawnPoints[spawnData.spawnID], false);
            spawnedPrefab.transform.localPosition = Vector3.zero;  // 親オブジェクトのRectTransformに従うように設定
            spawnedPrefab.transform.localScale = Vector3.one;
        }
    }

    IEnumerator SpawnFlower(float flowerSpawnTime)
    {
        // flowerSpawnTimeが過去の時間でないか確認
        float currentTime = Time.time - sceneStartTime;
        float timeToWait = flowerSpawnTime - currentTime;

        // すでにflowerSpawnTimeが過ぎていれば、すぐに生成
        if (timeToWait > 0)
        {
            yield return new WaitForSeconds(timeToWait); // 指定時間待機
        }

        // Flowerプレハブを生成
        GameObject flower = Instantiate(flowerPrefab, flowerSpawnPosition.position, Quaternion.identity);
        StartCoroutine(MoveFlower(flower));
    }

    IEnumerator MoveFlower(GameObject flower)
    {
        float elapsedTime = 0f;
        float duration = 4f; // Flowerが4秒間で移動する

        Vector3 startPosition = flower.transform.position;
        Vector3 endPosition = targetPosition.position;

        while (elapsedTime < duration)
        {
            // Flowerをターゲット位置に向けて移動
            flower.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ターゲット位置に到達した後も動き続け、2秒後に削除
        Vector3 moveDirection = (endPosition - startPosition).normalized; // Flowerの移動方向を保持
        float speed = Vector3.Distance(startPosition, endPosition) / duration; // Flowerの移動速度を計算

        float moveElapsedTime = 0f;
        float moveDuration = 2f; // ターゲットを通過して2秒間動き続ける

        while (moveElapsedTime < moveDuration)
        {
            // Flowerをそのままの速度で動かし続ける
            flower.transform.position += moveDirection * speed * Time.deltaTime;
            moveElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Flowerを2秒後に削除
        Destroy(flower);
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
