using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickableCircle : MonoBehaviour
{
    private GameManager gameManager;
    private Image imageComponent;
    private bool isClickable = false;
    private bool hasClicked = false;
    private float creationTime;
    [SerializeField]
    private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.0f);
    [SerializeField]
    private Vector3 initialScale = new Vector3(0.1f, 0.1f, 1.0f);
    [SerializeField]
    private float duration = 3.3f;
    private float startTime;

    void Start()
    {
        // GameManager�̎擾
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }

        // Image�R���|�[�l���g�̎擾
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on this GameObject!");
        }

        creationTime = Time.time;
        startTime = Time.time; // �X�P�[���p�̊J�n���Ԃ��ݒ�
        transform.localScale = initialScale; // �X�P�[���̏�����

        StartCoroutine(CheckClickTiming());
    }

    void Update()
    {
        float elapsedTime = Time.time - creationTime;

        // �X�P�[������: 3.3�b�ŃT�C�Y��ω�������
        float t = Mathf.Clamp01(elapsedTime / duration);
        transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

        // 3.0�b����3.1�b�̊Ԃɗ΂Ɍ��点��
        if (elapsedTime >= 3.0f && elapsedTime <= 3.1f)
        {
            imageComponent.color = Color.green;
            isClickable = true;
        }
        else if (elapsedTime > 3.1f)
        {
            imageComponent.color = Color.red;
            isClickable = false;
        }

        // 3.3�b��ɃI�u�W�F�N�g��j������
        if (elapsedTime > 3.3f && !hasClicked)
        {
            gameManager.Miss();
            Destroy(gameObject);
        }
    }

    IEnumerator CheckClickTiming()
    {
        yield return new WaitForSeconds(3.0f);

        while (Time.time - creationTime <= 3.1f)
        {
            yield return null;
            if (isClickable && Input.GetMouseButtonDown(0))
            {
                if (!hasClicked)
                {
                    hasClicked = true;
                    gameManager.Hit();
                    Destroy(gameObject);
                }
            }
        }

        // �N���b�N����Ȃ������ꍇ�A�F�����ɖ߂�
        if (!hasClicked)
        {
            imageComponent.color = Color.white;
        }
    }
}
