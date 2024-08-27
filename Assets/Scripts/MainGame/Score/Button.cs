using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    private GameObject scoreObject;
    private GameManager scoreScript;
    void Start()
    {
        scoreObject = GameObject.Find("GameManager");
        scoreScript = scoreObject.GetComponent<GameManager>();

    }
    public void i()
    {
        scoreScript.Hit();
        scoreScript.Miss();
    }
}
