using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class BackGroundSetting
{
    public List<Sprite> BackGrounds;
}

public class ControllerBackGroundPic : MonoBehaviour
{
    [SerializeField]
    List<BackGroundSetting> backGroundSettings;
    [SerializeField]
    List<GameObject> BackGroundObjects;
    [SerializeField]
    Transform MoveTarget;
    float Speed = 0;

    // Update is called once per frame
    void Update()
    {
        if (MoveTarget != null) Camera.main.transform.position = Vector2.MoveTowards(Camera.main.transform.position, MoveTarget.position, Speed * Time.deltaTime);
    }
    public void SetBackGround(int level) 
    {
        for (int i = 0; i < BackGroundObjects.Count; i++) 
        {
            if(i == 0 || i == 3 || i == 6)
                BackGroundObjects[i].GetComponent<SpriteRenderer>().sprite = backGroundSettings[level].BackGrounds[0];
            if (i == 1 || i == 4 || i == 7)
                BackGroundObjects[i].GetComponent<SpriteRenderer>().sprite = backGroundSettings[level].BackGrounds[1];
            if (i == 2 || i == 5 || i == 8)
                BackGroundObjects[i].GetComponent<SpriteRenderer>().sprite = backGroundSettings[level].BackGrounds[2];
        }
    }
    public void MoveToTarget(int dir, float speed)
    {
        Speed = speed;

        if (dir == 1) MoveTarget.position = Camera.main.transform.position + new Vector3(10, 0, 0);

        if (dir == 2) MoveTarget.position = Camera.main.transform.position + new Vector3(-10, 0, 0);
    }
}
