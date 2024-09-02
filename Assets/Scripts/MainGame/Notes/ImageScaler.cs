using UnityEngine;
using System.Collections;

public class ImageScaler : MonoBehaviour
{
    public GameObject imagePrefab; // Image Notes �� Prefab
    public Canvas canvas; // �V�[������ Canvas ���A�T�C��

    [Header("Image Notes �̐����ʒu")]
    public Vector3[] positions = new Vector3[15]; // Inspector �Ŏw�肷��ʒu

    private GameObject currentImageInstance; // ���ݑ��݂��� Image Notes �̃C���X�^���X

    private void Start()
    {
        StartCoroutine(ManageImageNotes());
    }

    private IEnumerator ManageImageNotes()
    {
        while (true)
        {
            // ������ Image Notes ���폜
            if (currentImageInstance != null)
            {
                Destroy(currentImageInstance);
            }

            // 15�̈ʒu���烉���_����1��I�����Đ���
            int index = Random.Range(0, positions.Length);
            Vector3 selectedPosition = positions[index];

            currentImageInstance = Instantiate(imagePrefab);
            currentImageInstance.transform.SetParent(canvas.transform, false); // Canvas �̎q�Ƃ��Ĕz�u
            currentImageInstance.transform.localPosition = selectedPosition; // �w�肳�ꂽ�ʒu�Ɉړ�

            StartCoroutine(ScaleImage(currentImageInstance));

            // 4�b�ԑҋ@
            yield return new WaitForSeconds(4.0f);
        }
    }

    private IEnumerator ScaleImage(GameObject imageNote)
    {
        Vector3 initialScale = imageNote.transform.localScale;
        Vector3 targetScale = new Vector3(1.1f, 1.1f, 1f);
        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (imageNote == null)
            {
                yield break; // �����I�u�W�F�N�g���j������Ă����珈�����I������
            }

            imageNote.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (imageNote != null)
        {
            imageNote.transform.localScale = targetScale; // �ŏI�I�Ƀ^�[�Q�b�g�X�P�[���ɐݒ�
        }
    }
}
