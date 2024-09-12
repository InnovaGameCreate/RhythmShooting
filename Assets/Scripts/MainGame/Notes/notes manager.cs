using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public GameObject notesPrefab;                                   // ��������m�[�c�̃v���n�u
    public Transform[] spawnPoints;                                  // �m�[�c�����p�̍��W
    public Canvas canvas;                                            // �v���n�u��z�u����Canvas
    public GameObject flowerPrefab;                                  // ��������Flower�v���n�u
    public Transform flowerSpawnPosition;                            // Flower�����̊J�n�ʒu
    public Transform targetPosition;                                 // Flower���ړ�����ڕW�ʒu
    private List<SpawnData> spawnList = new List<SpawnData>();
    private float sceneStartTime;

    // Start is called before the first frame update
    void Start()
    {
        sceneStartTime = Time.time;  // �V�[���J�n���̎��Ԃ��L�^
        LoadCSV("notesManager");     // CSV�t�@�C����ǂݍ��ރ��\�b�h�Ăяo��
        StartCoroutine(SpawnPrefabs()); // �v���n�u�𐶐����鏈��
    }

    // CSV�t�@�C����ǂݍ��݁A���̓��e�Ɋ�Â��ăf�[�^����͂��AspawnList��SpawnData�I�u�W�F�N�g��ǉ����鏈��
    void LoadCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);    // Resources�t�H���_����CSV�t�@�C����ǂݍ���
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');              // CSV�t�@�C���̓��e���s���Ƃɕ������āAlines�Ƃ���������z��Ɋi�[
            foreach (string line in lines)                          // ���ׂĂ̍s�ɑ΂��ă��[�v����
            {
                string[] values = line.Split(',');                  // �e�s���J���}�ŕ������Avalues�Ƃ���������z��Ɋi�[
                if (values.Length == 2)                             // �s��2�̒l�������Ă��邩���m�F
                {
                    float time = float.Parse(values[0]);            // values[0]�̒l��float�^�ɕϊ����Atime�Ƃ����ϐ��Ɋi�[
                    int id = int.Parse(values[1]);                  // values[1]�̒l��int�^�ɕϊ����Aid�Ƃ����ϐ��Ɋi�[
                    if (id >= 0 && id < spawnPoints.Length)         // time��id�̏����g���āA�V����SpawnData�I�u�W�F�N�g���쐬���AspawnList�ɒǉ�
                    {
                        spawnList.Add(new SpawnData(time, id));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSV�t�@�C����������܂���: " + fileName);
        }
    }

    IEnumerator SpawnPrefabs()
    {
        foreach (var spawnData in spawnList)
        {
            float currentTime = Time.time - sceneStartTime; // �o�ߎ��Ԃ��v�Z
            float timeToWait = spawnData.spawnTime - currentTime; // ���ݎ��ԂƂ̍�����ҋ@���ԂƂ��Čv�Z

            // �m�[�c�̐����^�C�~���O�܂őҋ@
            if (timeToWait > 0)
            {
                yield return new WaitForSeconds(timeToWait);
            }

            // Flower���m�[�c��2�b�O�ɐ���
            StartCoroutine(SpawnFlower(spawnData.spawnTime - 2f));

            // �m�[�c�𐶐�
            GameObject spawnedPrefab = Instantiate(notesPrefab, spawnPoints[spawnData.spawnID], false);
            spawnedPrefab.transform.localPosition = Vector3.zero;  // �e�I�u�W�F�N�g��RectTransform�ɏ]���悤�ɐݒ�
            spawnedPrefab.transform.localScale = Vector3.one;
        }
    }

    IEnumerator SpawnFlower(float flowerSpawnTime)
    {
        // flowerSpawnTime���ߋ��̎��ԂłȂ����m�F
        float currentTime = Time.time - sceneStartTime;
        float timeToWait = flowerSpawnTime - currentTime;

        // ���ł�flowerSpawnTime���߂��Ă���΁A�����ɐ���
        if (timeToWait > 0)
        {
            yield return new WaitForSeconds(timeToWait); // �w�莞�ԑҋ@
        }

        // Flower�v���n�u�𐶐�
        GameObject flower = Instantiate(flowerPrefab, flowerSpawnPosition.position, Quaternion.identity);
        StartCoroutine(MoveFlower(flower));
    }

    IEnumerator MoveFlower(GameObject flower)
    {
        float elapsedTime = 0f;
        float duration = 4f; // Flower��4�b�Ԃňړ�����

        Vector3 startPosition = flower.transform.position;
        Vector3 endPosition = targetPosition.position;

        while (elapsedTime < duration)
        {
            // Flower���^�[�Q�b�g�ʒu�Ɍ����Ĉړ�
            flower.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �^�[�Q�b�g�ʒu�ɓ��B����������������A2�b��ɍ폜
        Vector3 moveDirection = (endPosition - startPosition).normalized; // Flower�̈ړ�������ێ�
        float speed = Vector3.Distance(startPosition, endPosition) / duration; // Flower�̈ړ����x���v�Z

        float moveElapsedTime = 0f;
        float moveDuration = 2f; // �^�[�Q�b�g��ʉ߂���2�b�ԓ���������

        while (moveElapsedTime < moveDuration)
        {
            // Flower�����̂܂܂̑��x�œ�����������
            flower.transform.position += moveDirection * speed * Time.deltaTime;
            moveElapsedTime += Time.deltaTime;
            yield return null;
        }

        // Flower��2�b��ɍ폜
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
