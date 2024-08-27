using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIPresenter : MonoBehaviour
{
    private GameObject scoreObject;
    private GameManager scoreScript;
    private Text myText;

    void Start()
    {
        scoreObject = GameObject.Find("GameManager");
        scoreScript = scoreObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() //�e�L�X�g���X�R�A�ɕϊ�
    {
        int a = scoreScript.hit; //a��GameManager��hit����
        int b = scoreScript.miss;
        myText = GetComponent<Text>();
        scoreScript.GetHit();
        scoreScript.GetMiss();
        myText.text = "�������F" + a.ToString() + "��\n" + "���s���F" + b.ToString() + "��";
    }
}
