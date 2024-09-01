using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class notesmanager : MonoBehaviour
{
    public GameObject prefab;                                   //��������v���n�u
    public Transform[] spawnPoints;                             //0~14�̍��W��ێ�����z��
    public Canvas canvas;                                       //�v���n�u��z�u����Canvas
    private List<SpawnData> spawnList = new List<SpawnData>();�@

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV("notesManager");
        StartCoroutine(SpawnPrefabs());
    }

    void LoadCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    //Resources�t�H���_����CSV�t�@�C����ǂݍ���
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
            Debug.LogError("CSV�t�@�C����������܂���;" + fileName);
        }
    }

    IEnumerator SpawnPrefabs()
    {
        foreach(var spawnData in spawnList)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);
            GameObject spawnedPrefab = Instantiate(prefab, spawnPoints[spawnData.spawnID]); //�v���n�u�𐶐����ACanvas���̎w�肳�ꂽ�ʒu�̎q�I�u�W�F�N�g�Ƃ��Ĕz�u
            spawnedPrefab.transform.localPosition = Vector3.zero;                           //�e�I�u�W�F�N�g��RectTransform�ɏ]���悤�ɐݒ�
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
