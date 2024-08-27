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
    void Update() //テキストをスコアに変換
    {
        int a = scoreScript.hit; //aにGameManagerのhitを代入
        int b = scoreScript.miss;
        myText = GetComponent<Text>();
        scoreScript.GetHit();
        scoreScript.GetMiss();
        myText.text = "成功数：" + a.ToString() + "回\n" + "失敗数：" + b.ToString() + "回";
    }
}
