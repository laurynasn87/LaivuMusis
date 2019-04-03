using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using System.Linq;
using System;
using UnityEngine.UI;

public class BoardVer1 : MonoBehaviour
{
    public static List<Laivas> Laivai = new List<Laivas>();
    public static int kelintas = 0;
    public GameObject BoardUnitPrefab;
    public GameObject mygtukasZaisti;
    //public int numSelectors = 10;
    public GameObject[,] board = new GameObject[100, 100];
    public GameObject Sarvuotlaivis;
    public GameObject korvete;
    public GameObject minininkas;
    public GameObject frigata;
    public bool vertical = true;
    public int Length = 1;
    public Text koordinates;
    public GameObject Main;
    public string LaivoPavadinimas;
    public GameObject Priesininkas;
    //public GameObject klonas;

    string CreateInsert = "https://bastioned-public.000webhostapp.com/InsertStuff.php";
    string GetInfo = "https://bastioned-public.000webhostapp.com/GetStuff.php";
    string Counteris = "https://bastioned-public.000webhostapp.com/CountStuff.php?";
    string CounterData = "https://bastioned-public.000webhostapp.com/GetCounterData.php";

    // Start is called before the first frame update
    void Start()
    {
        Main.SetActive(true);
        Main.transform.position =(new Vector3(Main.transform.position.x - 20, Main.transform.position.y, Main.transform.position.z));
   
        //Transform mainkamera = Camera.main.transform;
        //boardd = new board[10];
        //board = new GameObject[10,10];
        int row = 1;
        int col = 1;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int ki=i*10;
                int kj = j * 10;
                //for (int k = 0; k < numSelectors; k++)
                //{
                GameObject tmp = Instantiate(this.BoardUnitPrefab, new Vector3(ki, 0, kj), this.BoardUnitPrefab.transform.rotation) as GameObject;
                //GameObject go = tmp;
                BoardUIVer1 tmpUI = tmp.GetComponent<BoardUIVer1>();
                //Debug.Log(tmpUI);
                string name = string.Format("B1:[{0:00},{1:00}]", row, col);
                tmpUI.lblBoardPosition.text = name;
                tmpUI.COL = kj;
                tmpUI.ROW = ki;
                //Debug.Log(name);


                //tmp.transform.localScale = Vector3.one;
                board[ki, kj] = tmp;
                tmp.name = name;

                col++;
                //}
            }
            col = 1;
            row++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
      //  if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
     //       OnMouseEnter(hit);

        if (Input.GetMouseButtonDown(0))
        {
             
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                

            //    String index = GetObjectByCoordinates(hit).name;
             //   koordinates.text = indexs(index);
               
            }

        }

    }

    GameObject GetObjectByCoordinates(RaycastHit hits)
    {
        GameObject[] objektai;
        GameObject Atsakymas= null;
        objektai = GameObject.FindGameObjectsWithTag("board");
        foreach (GameObject taskas in objektai)
        {
            Collider myCollider;
            myCollider = taskas.GetComponent<Collider>();
            if (myCollider.bounds.Contains(hits.point))
            {
                int index = System.Array.IndexOf(objektai, taskas);
                Atsakymas = taskas;
            }
        }



        return Atsakymas;
    }

   public void SetEnabled()
    {
        enabled = true;
    }

    public void Zaistimygtukas()
    {
        mygtukasZaisti.SetActive(true);
            Main.transform.position = (new Vector3(Main.transform.position.x + 20, Main.transform.position.y, Main.transform.position.z));
    
    }
    public String indexs(String name)
    {
        int w = board.GetLength(0); // width
        int h = board.GetLength(1); // height
        
        for (int x = 0; x < w; x=x+10)
        {
            for (int y = 0; y < h; y=y+10)
            {
                if (board[x, y].name.Equals(name))
                    return (x + " " + y);
                    
            }
        }

        return "Nerasta";
    }
    public void LengthtoPav()
    {
        switch (Length)
        {
            case 1:
                LaivoPavadinimas = "Frigata";
                break;
            case 2:
                LaivoPavadinimas = "Minininkas";
                break;
            case 3:
                LaivoPavadinimas = "Kreiseris";
                break;
            case 4:
                LaivoPavadinimas = "Sarvuotlaivis";
                break;
        }
    }
    public void printStuff()
    {
        Priesininkas.SetActive(true);

        foreach (Laivas Lai in Laivai)
        {
            foreach (String index in Lai.Koordinates)
            {
                Debug.LogWarning(index);
                InsertStuff(index);
                Counter(index);
            }
                
        }
    }

    public void InsertStuff(string koordinates)
    {
        WWWForm form = new WWWForm();
        form.AddField("koordinates", koordinates);
        WWW www = new WWW(CreateInsert, form);
    }

    public void GetStuff()
    {
        WWW www = new WWW(GetInfo);
    }

    public void Counter (string koordinates)
    {
        string url = Counteris + "koordinates='" + koordinates + "'";
        //Debug.Log(koordinates + url);
        WWW www = new WWW(url);
    }

    public void GetCounterData()
    {
        WWW www = new WWW(CounterData);
    }

}

