using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zaisti : MonoBehaviour
{
    public Canvas PradziosCanvas;
    public GameObject ZaidimoCanvas;

    public GameObject Board;
    // Start is called before the first frame update
    public void Camerazaisti()
    {
        GameObject Meniu = GameObject.FindGameObjectWithTag("Menu");
        Meniu.SetActive(false);
        Debug.Log("Isjungiu Meniu kamera ");
        PradziosCanvas.enabled = false;
         BoardVer1 script =  Board.GetComponent<BoardVer1>();
        script.enabled = true;
        ZaidimoCanvas.SetActive(true);

        

    }
    private void Start()
    {
      //  GameObject Meniu = GameObject.FindGameObjectWithTag("MainCamera");
     //   Meniu.SetActive(false);
    }


    // Update is called once per frame
}
