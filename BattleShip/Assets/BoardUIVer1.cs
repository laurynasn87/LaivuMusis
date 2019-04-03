using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUIVer1 : MonoBehaviour
{

    public Text lblBoardPosition;

    public int ROW;
    public int COL;

    public bool OCCUPIED;
    public int length = 1;
    GameObject Lenta;
    BoardVer1 Script;
    GameObject[,] board2 = new GameObject[100, 100];
    List<GameObject> Dabartiniai = new List<GameObject>();

    void Start()
    {
        this.OCCUPIED = false;
        Lenta = GameObject.FindGameObjectWithTag("LentaMano");
        Script = Lenta.GetComponent<BoardVer1>();
        board2 = Script.board;
        //lblBoardPosition.text = "B1[00:00]";
        //lblBoardPosition.name = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        List<String> Koordinates = new List<string>();
        if (Script.Length == 0) return;

        foreach (GameObject ibjektas in Dabartiniai)
        {
            ibjektas.GetComponent<Renderer>().material.SetColor("_Color", Color.red); // Bug : Kai norimas padet laivas overlapina su padetu, nes kur nori pakeist spalva nera prefabo
                                                                                      // Possible fix: Jei overlapina neduoti padeti
            Koordinates.Add(ibjektas.name);
            removetile(ibjektas);
        }
        BoardVer1.Laivai.Add(new Laivas(Script.Length, Script.LaivoPavadinimas, Koordinates, Script.vertical));
        Text kiekis;
        switch (Script.Length)
        {
            case 1:
                kiekis = GameObject.FindGameObjectWithTag("Frigata").GetComponent<Text>();
                if (Script.vertical)
                    Instantiate(Script.frigata, new Vector3(transform.position.x, 2.85f, transform.position.z), Quaternion.identity);
                else
                {
                    Instantiate(Script.frigata, new Vector3(transform.position.x, 2.85f, transform.position.z), Quaternion.Euler(0, 90, 0));
                }
                kiekis.text = (int.Parse(kiekis.text) - 1).ToString();

                if (kiekis.text.Equals("0")) pasibaige(Script.Length.ToString());
                break;
            case 2:
                kiekis = GameObject.FindGameObjectWithTag("Minininkas").GetComponent<Text>();
                kiekis.text = (int.Parse(kiekis.text) - 1).ToString();
                if (Script.vertical)
                    Instantiate(Script.minininkas, new Vector3(transform.position.x, 2.85f, transform.position.z + 5), Quaternion.identity);
                else
                {
                    Instantiate(Script.minininkas, new Vector3(transform.position.x + 5, 2.85f, transform.position.z), Quaternion.Euler(0, 90, 0));
                }
                if (kiekis.text.Equals("0")) pasibaige(Script.Length.ToString());

                break;
            case 3:
                kiekis = GameObject.FindGameObjectWithTag("Kreiseris").GetComponent<Text>();
                kiekis.text = (int.Parse(kiekis.text) - 1).ToString();
                if (Script.vertical)
                    Instantiate(Script.korvete, new Vector3(transform.position.x, 2.85f, transform.position.z + 10), Quaternion.identity);
                else
                {
                    Instantiate(Script.korvete, new Vector3(transform.position.x + 10, 2.85f, transform.position.z), Quaternion.Euler(0, 90, 0));
                }
                if (kiekis.text.Equals("0")) pasibaige(Script.Length.ToString());
                break;
            case 4:
                kiekis = GameObject.FindGameObjectWithTag("Sarvuotlaivis").GetComponent<Text>();
                kiekis.text = (int.Parse(kiekis.text) - 1).ToString();
                if (Script.vertical)
                    Instantiate(Script.Sarvuotlaivis, new Vector3(transform.position.x, 2.85f, transform.position.z + 15), Quaternion.identity);
                else
                {
                    Instantiate(Script.Sarvuotlaivis, new Vector3(transform.position.x + 15, 2.85f, transform.position.z), Quaternion.Euler(0, 90, 0));
                }
                if (kiekis.text.Equals("0")) pasibaige(Script.Length.ToString());
                break;
        }

    }
    private void OnMouseEnter()
    {
        if (Script.Length == 0) return;
        if (GetComponent<Renderer>().material.color == Color.red) return;
        int length = Script.Length;
        String skaiciusy = name.Substring(name.IndexOf("["));
        String skaiciusx = skaiciusy.Substring(1, 2);
        skaiciusy = skaiciusy.Substring(4, 2);
        int x = int.Parse(skaiciusx);
        int y = int.Parse(skaiciusy);

        if (Script.vertical)
        {

            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            Dabartiniai.Add(indexs(name)); // Ten kur paspaudziau
            length--;
            double ycord = y;
            while (length != 0)
            {

                ycord = ycord + 1;
                string xstring = x.ToString();
                if (x != 10) xstring = '0' + xstring;
                if (ycord > 10) ycord = ycord - Script.Length;
                string ystring = Math.Floor(ycord).ToString();

                if (ycord != 10) ystring = '0' + ystring;
                GameObject tile = indexs("B1:[" + xstring + "," + ystring + "]");
                if (tile != null)
                {
                    tile.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    Dabartiniai.Add(indexs("B1:[" + xstring + "," + ystring + "]"));

                }
                length--;
                //   GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }


        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            Dabartiniai.Add(indexs(name));
            length--;
            double xcord = x;
            String xs;
            String ystring = "";
            while (length != 0)
            {

                xcord = xcord + 1;
                if (xcord > 10) xcord = xcord - Script.Length;
                if (xcord < 10) xs = '0' + xcord.ToString();
                else xs = xcord.ToString();

                if (y != 10) ystring = '0' + y.ToString();

                GameObject tile = indexs("B1:[" + xs + "," + ystring + "]");
                if (tile != null)
                    tile.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                Dabartiniai.Add(indexs("B1:[" + xs + "," + ystring + "]"));
                length--;
                //   GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }



        }


    }
    private void OnMouseExit()
    {

        if (Script.Length == 0) return;
        int w = board2.GetLength(0); // width
        int h = board2.GetLength(1); // height

        for (int x = 0; x < w; x = x + 10)
        {
            for (int y = 0; y < h; y = y + 10)
            {
                if (board2[x, y] != null)
                    board2[x, y].GetComponent<Renderer>().material.SetColor("_Color", Color.white);

            }
        }

        Dabartiniai.Clear();

    }
    public GameObject indexs(String name)
    {
        int w = board2.GetLength(0); // width
        int h = board2.GetLength(1); // height

        for (int x = 0; x < w; x = x + 10)
        {
            for (int y = 0; y < h; y = y + 10)
            {
                if (board2[x, y] != null)
                    if (board2[x, y].name.Equals(name))
                        return (board2[x, y]);

            }
        }

        return null;
    }
    public void removetile(GameObject name)
    {
        int w = board2.GetLength(0); // width
        int h = board2.GetLength(1); // height

        for (int x = 0; x < w; x = x + 10)
        {
            for (int y = 0; y < h; y = y + 10)
            {
                if (board2[x, y] != null)
                    if (board2[x, y].name.Equals(name.name))
                        board2[x, y] = null;

            }
        }


    }
    void pasibaige(string name)
    {

        GameObject[] paneles = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < paneles.Length; i++)
            if (paneles[i].name == name)
            {
                paneles[i].SetActive(false);

            }



        Script.Length = 0;
        Debug.LogWarning(kiekisjungtu());
        if (kiekisjungtu() == 0) Script.Zaistimygtukas();
    }
    int kiekisjungtu()
    {
        int kiek = 0;
        GameObject[] paneles = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < paneles.Length; i++)
            if (paneles[i].activeSelf)
            {

                kiek++;
            }
        return kiek;
    }
    public void randomPlacement()
    {
        BoardVer1.Laivai.Clear();
        Text kiekis;
        string xstring;
        string ystring;
        kiekis = GameObject.FindGameObjectWithTag("Frigata").GetComponent<Text>();
        List<String> koordin = new List<string>();
        bool vertical= false;
        List<String> visi = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            System.Random rnd = new System.Random();
            //
            int boolean = rnd.Next(0, 1);
           
            if (boolean == 0) vertical = false;
            else vertical = true;
            //
            int x = rnd.Next(1, 11);
            int y = rnd.Next(1, 11);
            if (x < 10) xstring = "0" + x;
            else xstring =x.ToString();
            if (y < 10) ystring = "0" + y;
            else ystring = y.ToString();
            if (ArNeraLaivo("B1:[" + xstring + "," + ystring + "]", visi))
            {
                i--;
                continue;
            }
            koordin.Add("B1:[" + xstring + "," + ystring + "]");
            visi.Add("B1:[" + xstring + "," + ystring + "]");
          
            BoardVer1.Laivai.Add(new Laivas(1, "Frigata", koordin, vertical));
            kiekis.text = (int.Parse(kiekis.text) - 1).ToString();
            koordin.ForEach(Debug.LogWarning);
            koordin.Clear();
        }
        GameObject[] plteles = GameObject.FindGameObjectsWithTag("board");
        foreach(GameObject plytele in plteles)
        {
            foreach(String pav in visi)
            {
                if (pav.Equals(plytele.name))
                {
                    plytele.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                   Instantiate(Script.frigata, new Vector3(plytele.transform.position.x, 2.85f, plytele.transform.position.z), Quaternion.identity);
                    
                }
            }



        }

        pasibaige("1");
    }
    public bool ArNeraLaivo(String cord, List<string> koordas)
    {
   //     Debug.LogWarning("atejau  " + BoardVer1.Laivai.Count);
        bool ats = false;
   //         Debug.LogWarning(index.Koordinates.Count);

            foreach (String koord in koordas)
            {
                
              //  Debug.LogWarning(koord + cord);
                if (koord.Equals(cord)) ats = true;

            }
   
        return ats;
    }
}
