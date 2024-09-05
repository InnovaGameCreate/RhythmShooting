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

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Debug.Log("GameManager found and assigned.");
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }

        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component not found on this GameObject!");
        }

        creationTime = Time.time;
        StartCoroutine(CheckClickTiming());
    }

    void Update()
    {
        float elapsedTime = Time.time - creationTime;

        // 3.0�b����3.1�b�̊ԂɐԂ����点��
        if (elapsedTime >= 3.0f && elapsedTime <= 3.1f)
        {
            imageComponent.color = Color.red;
            isClickable = true;
        }
        else if (elapsedTime > 3.1f)
        {
            imageComponent.color = Color.white;
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
