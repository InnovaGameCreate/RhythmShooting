using UnityEngine;
using System.Collections;

public class CircleSpawner : MonoBehaviour
{
    public GameObject imageNotesPrefab; // Image Notes �� Prefab ��ݒ�
    public Vector3[] spawnPositions; // 15�ӏ��̐����ʒu�̍��W��ݒ�
    private GameObject currentNote;
    public float spawnDelay = 1f; // ���̃m�[�g�𐶐�����܂ł̒x������

    void Start()
    {
        SpawnNote();
    }

    void SpawnNote()
    {
        // �����̃m�[�g���폜
        if (currentNote != null)
        {
            Destroy(currentNote);
        }

        // �����_���Ȑ����ʒu��I��
        if (spawnPositions != null && spawnPositions.Length == 15)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Vector3 spawnPosition = spawnPositions[randomIndex];

            // Image Notes �𐶐����A���݂̃m�[�g�Ƃ��ĕۑ�
            currentNote = Instantiate(imageNotesPrefab, spawnPosition, Quaternion.identity);

            // Canvas���擾���āAImage Notes��Canvas�̎q�I�u�W�F�N�g�ɐݒ�
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                currentNote.transform.SetParent(canvas.transform, false);
            }
            else
            {
                Debug.LogError("Canvas not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Please ensure that 15 spawn positions are assigned in the inspector.");
        }

        // 4�b��Ɏ��̃m�[�g�𐶐����邽�߂ɃR���[�`�����J�n
        StartCoroutine(SpawnNextNote());
    }

    IEnumerator SpawnNextNote()
    {
        // �w�肳�ꂽ�x�����Ԃ����ҋ@
        yield return new WaitForSeconds(spawnDelay);

        // �Ăуm�[�g�𐶐�
        SpawnNote();
    }
}
