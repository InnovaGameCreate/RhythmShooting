using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class notesmanager : MonoBehaviour
{
    public GameObject prefab;                                   //��������v���n�u
    public Transform[] spawnPoints;                             //0~14�̍��W��ێ�����z��
    public Canvas Canvas;                                       //�v���n�u��z�u����Canvas
    private List<SpawnData> spawnList = new List<SpawnData>();�@

    // Start is called before the first frame update
    void Start()
    {
        LoadCSV("notesManager");            //���\�b�h�Ăяo��
        StartCoroutine(SpawnPrefabs());     //�v���n�u�𐶐����鏈��
    }

    //CSV�t�@�C����ǂݍ��݁A���̓��e�Ɋ�Â��ăf�[�^����͂��AspawnList��SpawnData�I�u�W�F�N�g��ǉ����鏈��
    void LoadCSV(string fileName)   //fileName�Ƃ���������������Ƃ��Ď󂯎��A�w�肳�ꂽCSV�t�@�C����ǂݍ���
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    //Resources�t�H���_����CSV�t�@�C����ǂݍ���
        print(csvFile);
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');              //CSV�t�@�C���̓��e���s���Ƃɕ������āAlines�Ƃ���������z��Ɋi�[
            foreach (string line in lines)                          //���ׂĂ̍s�ɑ΂��ă��[�v����
            {
                string[] values = line.Split(',');                  //�e�s���J���},�ŕ������Avalues�Ƃ���������z��Ɋi�[
                if (values.Length == 2)                             //�s��2�̒l�������Ă��邩���m�F
                {
                    float time = float.Parse(values[0]);            //values[0]�̒l��float�^�ɕϊ����Atime�Ƃ����ϐ��Ɋi�[
                    int id = int.Parse(values[1]);                  //values[1]�̒l��int�^�ɕϊ����Aid�Ƃ����ϐ��Ɋi�[
                    if (id >= 0 && id < spawnPoints.Length)         //time��id�̏����g���āA�V����SpawnData�I�u�W�F�N�g���쐬���AspawnList�ɒǉ�
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

    IEnumerator SpawnPrefabs()  //spawnList���̊eSpawnData�I�u�W�F�N�g�Ɋ�Â��āA�v���n�u�𐶐�
    {
        foreach(var spawnData in spawnList)
        {
            yield return new WaitForSeconds(spawnData.spawnTime);                           //spawnData.spawnTime�b�ҋ@
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
