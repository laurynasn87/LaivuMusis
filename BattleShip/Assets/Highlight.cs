using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class Highlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
   
    public Image img;
    Color oldcollor;
    public bool isOver = false;
 
    Image yellow;
    public BoardVer1 ver1;
    public void Start()
    {
        oldcollor = img.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        img.color = Color.red;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (img.color == Color.red)
        img.color = oldcollor;

    }
    public void OnPointerClick(PointerEventData eventData) // 3
    {
        int foo = int.Parse(img.name);
        ver1.Length = foo;
        ver1.LengthtoPav();
        if (img.color == Color.yellow)
        {
            ver1.vertical = !ver1.vertical;
        }
        if (yellow!=null)
        {
            yellow.color = oldcollor;
            Debug.LogWarning(yellow.color + " " + oldcollor);
        }
        img.color = Color.yellow;
      
        yellow = img;
    }

}
