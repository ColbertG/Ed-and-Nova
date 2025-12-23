using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class Dialog 
{
    public Sprite Myimage;
    public List<string> Dialogs;
}
[Serializable]
class DialogOrder
{
    public int DialogOrderNumber;
    public int ImageNumber;

}
public class ControllerDialog : MonoBehaviour
{
    [SerializeField]
    List<Dialog> CharacterDialog;
    [SerializeField]
    List<DialogOrder> DialogOrders;
    [SerializeField]
    GameObject DialogPanle;
    [SerializeField]
    Image ActiveImage;
    [SerializeField]
    Text ActiveDialogs;
    int DialogCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowDialog()
    {
        ActiveImage.sprite = CharacterDialog[DialogOrders[DialogCount].ImageNumber].Myimage;
        ActiveDialogs.text = CharacterDialog[DialogOrders[DialogCount].ImageNumber].Dialogs[DialogOrders[DialogCount].DialogOrderNumber];
    }
    public void ShowNextDialog() 
    {
        DialogCount++;
    }
}
