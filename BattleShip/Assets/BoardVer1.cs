using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BoardVer1 : MonoBehaviour
{
    public static List<Laivas> Laivai = new List<Laivas>();
    public static int kelintas = 0;
    public GameObject BoardUnitPrefab;
    public GameObject mygtukasZaisti;
    //public int numSelectors = 10;
    public GameObject[,] board;
    public GameObject Sarvuotlaivis;
    public GameObject korvete;
    public GameObject minininkas;
    public GameObject frigata;
    public bool vertical = true;
    public int Length = 1;
    public GameObject Main;
    public string LaivoPavadinimas;
    public GameObject Priesininkas;
    //public GameObject klonas;
    public Canvas ZadimoCanvas;
    public bool pataike = false;
    string CreateInsert = "https://bastioned-public.000webhostapp.com/InsertStuff.php";
    string GetInfo = "https://bastioned-public.000webhostapp.com/GetStuff.php";
    string Counteris = "https://bastioned-public.000webhostapp.com/CountStuff.php?";
    string CounterData = "https://bastioned-public.000webhostapp.com/GetCounterData.php";

    bool active = true;
    public void GenerateBoard()
    {
        board = null;
        board = new GameObject[100, 100]; 
        int row = 1;
        int col = 1;
       
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int ki = i * 10;
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
    // Start is called before the first frame update
    void Start()
    {
        Main.SetActive(true);
        Main.transform.position = (new Vector3(Main.transform.position.x - 20, Main.transform.position.y, Main.transform.position.z));
        GenerateBoard();
        //Transform mainkamera = Camera.main.transform;
        //boardd = new board[10];
        //board = new GameObject[10,10];
    

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
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
    }

  public  GameObject GetObjectByCoordinates(RaycastHit hits)
    {
        GameObject[] objektai;
        GameObject Atsakymas = null;
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
        Main.transform.position = (new Vector3(45, Main.transform.position.y, Main.transform.position.z));

    }
    public List<int> index(String name)
    {
        List<int> xy = new List<int>();
        int w = board.GetLength(0); // width
        int h = board.GetLength(1); // height

        for (int x = 0; x < w; x = x + 10)
        {
            for (int y = 0; y < h; y = y + 10)
            {
                if (board[x, y].name.Equals(name))
                {
                    xy.Add(x);
                    xy.Add(y);
                    return (xy);
                }
                   

            }
        }

        return null;
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
        active = false;
        ZadimoCanvas.gameObject.SetActive(false);
        Main.transform.position = new Vector3(44.5f, 88, 45);
        Main.transform.rotation = Quaternion.Euler(90, 0, 0);
        Main.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0.6f), new Vector2(0.2f, 0.4f));
        Main.gameObject.SetActive(false);
         Priesininkas.SetActive(true);
        
     //   foreach (Laivas Lai in Laivai)
    //    {
         //  
         //   foreach (String index in Lai.Koordinates)
         //   {
               // Debug.LogWarning(index);
                // InsertStuff(index);
                //  Counter(index);
          //  }

    //    }
    }

    public void InsertStuff(string koordinates)
    {
        WWWForm form = new WWWForm();
        form.AddField("koordinates", koordinates);
        WWW www = new WWW(CreateInsert, form);
    }

 public IEnumerator GetStuff()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetInfo);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Priesininkas.GetComponent<Priesininkas>().AIKord = www.downloadHandler.text;
         
            

        }
    }

    public void Counter(string koordinates)
    {
        string url = Counteris + "koordinates='" + koordinates + "'";
        //Debug.Log(koordinates + url);
        WWW www = new WWW(url);
    }

  public  IEnumerator GetCounterData()
    {
        //WWW www = new WWW(CounterData);

        UnityWebRequest www = UnityWebRequest.Get(CounterData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Priesininkas.GetComponent<Priesininkas>().AIKord = www.downloadHandler.text;

        }
    }

    public GameObject[,] GetBoard()
    {
        return board;
    }
    public void shoot(string name)
    {
        Priesininkas pries = Priesininkas.GetComponent<Priesininkas>();
        List<int>kord = index(name);
        int x = kord[0];
        int y = kord[1];

        if (arpataike(name)) pries.ShotNotMiss = true;
GameObject kulka = Instantiate(Priesininkas.GetComponent<Priesininkas>().bullet , new Vector3(166, 184, 32), Quaternion.Euler(60, -90, 0));
        StartCoroutine(ProjectileMove(kulka.transform.position, board[x,y].transform.position, kulka));
        pries.ejimas = true;
    }
    public bool arpataike(string name)
    {
        bool pataike = false;

        foreach (Laivas laivas in Laivai)
        {
            foreach (String kord in laivas.Koordinates)
                if (kord.Equals(name))
                {
                    pataike = true;
                    break;
                }

        }

        return pataike;
    }
    IEnumerator ProjectileMove( Vector3 oldpos, Vector3 newpos, GameObject bullet)
    {

        
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 1);
            try
            {
                bullet.transform.position = Vector3.Lerp(oldpos, newpos, t);
            }
            catch (MissingReferenceException e)
            { }
            //  PriesininkoKamera.transform.rotation = Quaternion.Lerp(PriesininkoKamera.transform.rotation, new Quaternion(60, PriesininkoKamera.transform.rotation.y, PriesininkoKamera.transform.rotation.z, PriesininkoKamera.transform.rotation.w), t);
            yield return 0;

        }

    }
}

