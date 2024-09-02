using UnityEngine;
using System.Collections;

public class ClickableCircle : MonoBehaviour
{
    private float gracePeriod = 0.1f; // �}0.1�b�̗P�\
    private GameManager gameManager;

    void Start()
    {
        // GameManager �̎Q�Ƃ��擾
        GameObject scoreObject = GameObject.Find("GameManager");
        if (scoreObject != null)
        {
            gameManager = scoreObject.GetComponent<GameManager>();
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D fired."); // �g���K�[�����΂������Ƃ��m�F
        if (other.CompareTag("ImageNote"))
        {
            Debug.Log("Trigger with ImageNote detected.");
            StartCoroutine(HandleOverlap(other.gameObject));
        }
        else
        {
            Debug.Log("Trigger with non-ImageNote detected: " + other.gameObject.name);
        }
    }

    private IEnumerator HandleOverlap(GameObject imageNote)
    {
        float startTime = Time.time;
        float overlapStartTime = startTime + 0.1f; // �P�\�J�n���ԁi�}0.1�b�j
        float overlapEndTime = overlapStartTime + 0.1f; // �P�\�I������
        bool isSuccessful = false;

        while (Time.time < overlapEndTime)
        {
            if (Time.time >= overlapStartTime && Time.time <= overlapEndTime)
            {
                // �P�\���ԓ��ł���΃N���b�N�����o
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D collider = GetComponent<Collider2D>();

                    if (collider.OverlapPoint(mousePosition))
                    {
                        Debug.Log("Success detected.");
                        gameManager.Hit(); // �������𑝂₷
                        isSuccessful = true;
                        break; // ���������̂Ń��[�v���I��
                    }
                }
            }
            yield return null;
        }

        if (!isSuccessful)
        {
            Debug.Log("Failure detected.");
            gameManager.Miss(); // ���s���𑝂₷
        }

        // ���肪�I�������� Image Note ���폜
        Destroy(imageNote);
    }

}
