using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;
public class UIFrameTest : MonoBehaviour
{
    public PopupWindow popupWindow1;
    public PopupWindow popupWindow2;
    public PopupWindow popupWindow3;
    public PopupWindow popupWindow4;
    public PopupWindow popupWindow5;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            popupWindow1.Open();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            popupWindow2.Open();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            popupWindow3.Open();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            popupWindow4.Open();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            popupWindow5.Open();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.A))
        {
            popupWindow1.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.S))
        {
            popupWindow2.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.D))
        {
            popupWindow3.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.F))
        {
            popupWindow4.Close();
        }
        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.G))
        {
            popupWindow5.Close();
        }
    }
}
