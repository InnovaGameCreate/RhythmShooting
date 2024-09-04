using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlerulechange : MonoBehaviour
{
    public Sprite[] images;
    public SpriteRenderer spriteRenderer;
    private int roundNum = 0;
    // Start is called before the first frame update

   
    void Start()
    {
        UpdateSprite();
    }


    void UpdateSprite()
    {
        if (roundNum >= 0 && roundNum < images.Length)
        {
            spriteRenderer.sprite = images[roundNum];
        }

    }

    public void NextRound()
    {
        UpdateSprite();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextRound();
        }
    }
}
