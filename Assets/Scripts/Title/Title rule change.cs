using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Titlerulechange : MonoBehaviour
{
    public Sprite[] images;
    public Image image;
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
            image.sprite = images[roundNum];
        }

    }

    public void NextRound()
    {
        roundNum++;
        if (roundNum>=images.Length)
        {
            roundNum = 0;
            this.gameObject.SetActive(false);
        }

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
