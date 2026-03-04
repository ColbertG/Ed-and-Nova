using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBackGround : MonoBehaviour
{
    [SerializeField]
    GameObject Cam;
    [SerializeField]
    float ParallaxEffect;
    float length;
    float startPosX;
    float height;
    float startPosY;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        //startPosY = transform.position.y;
        //height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempX = (Cam.transform.position.x * (1 - ParallaxEffect));
        float distX = (Cam.transform.position.x * ParallaxEffect);

        //float tempY = (Cam.transform.position.y * (1 - ParallaxEffect));
        //float distY = (Cam.transform.position.y * ParallaxEffect);

        transform.position = new Vector3(startPosX + tempX, transform.position.y, transform.position.z);

        if (distX > startPosX + length) startPosX += length;
        else if (distX < startPosX - length) startPosX -= length;


        //transform.position = new Vector3(transform.position.x, startPosY + distY, transform.position.z);

        //if (tempY > startPosY + height) startPosY += height;
        //else if (tempY < startPosY - height) startPosY -= height;

    }
}
