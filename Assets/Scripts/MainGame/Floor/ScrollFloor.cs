using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollFloor : MonoBehaviour
{
    private float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
        if (transform.position.x <= 236f)
        {
            transform.position = new Vector3(550, 110, -230);
        }
    }
}
