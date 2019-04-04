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
    List<String> visi = new List<string>();
    void Start()
    {
        this.OCCUPIED = false;
        Lenta = GameObject.FindGameObjectWithTag("LentaMano");
        Script = Lenta.GetComponent<BoardVer1>();
        board2 = Script.GetBoard();
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
       if(ArNeraLaivo(gameObject.name, visi))return;
        foreach (GameObject ibjektas in Dabartiniai)
        {
            ibjektas.GetComponent<Renderer>().material.SetColor("_Color", Color.red); // Bug : Kai norimas padet laivas overlapina su padetu, nes kur nori pakeist spalva nera prefabo
            visi.Add(ibjektas.name);                                                                   // Possible fix: Jei overlapina neduoti padeti
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
               // else Debug.LogWarning(Script.board[x,y].name);

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
        refresh();
        RandomDidesniLaivai(1, Script.frigata, 0, 0, 4);
     //   RandomDidesniLaivai(3, Script.korvete, 10, 10, 2);
       // RandomDidesniLaivai(4, Script.Sarvuotlaivis, 15, 15, 1);
        RandomDidesniLaivai(2, Script.minininkas, 5, 5, 3);
    }
    public bool ArNeraLaivo(String cord, List<string> koordas)
    {
      
        bool ats = false;
      

        foreach (String koord in koordas)
        {

           
            if (koord.Equals(cord)) ats = true;

        }

        return ats;
    }
    public void LaivoPridejimas(List<String> plytName, GameObject Laivas, Quaternion Laipsniai, int paklaidax, int paklaiday)
    {
        bool Arpadetas = false;
        GameObject[] plteles = GameObject.FindGameObjectsWithTag("board");
        foreach (String koordinate in plytName)
        {
            foreach (GameObject plytele in plteles)
            {

                if (koordinate.Equals(plytele.name))
                {
                    plytele.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    if (!Arpadetas)
                    {
                        if (paklaidax != 0)
                            Instantiate(Laivas, new Vector3(plytele.transform.position.x + paklaidax, 2.85f, plytele.transform.position.z), Laipsniai);
                        else Instantiate(Laivas, new Vector3(plytele.transform.position.x, 2.85f, plytele.transform.position.z + paklaiday), Laipsniai);
                        Arpadetas = true;
                    }

                }
            }
        }
    }
    public void PlaceShip(List<String> Koordinates, GameObject Laivas, Quaternion Laipsniai, int paklaidax, int paklaiday)
    {
        bool Placed = false;
        
        foreach (String cord in Koordinates)
        {

            GameObject Tile = GetTileByString(cord);
            if (Tile!=null)
            {
                Tile.GetComponent<Renderer>().material.color = Color.red;
            }
            if (!Placed)
            {
                if (paklaidax != 0)
                    Instantiate(Laivas, new Vector3(Tile.transform.position.x + paklaidax, 2.85f, Tile.transform.position.z), Laipsniai);
                else Instantiate(Laivas, new Vector3(Tile.transform.position.x, 2.85f, Tile.transform.position.z + paklaiday), Laipsniai);
                Placed = true;
            }
        }

    }
    void RandomDidesniLaivai(int ilgis, GameObject laivas, int paklaidax, int paklaiday, int kiekis)
    {
        System.Random rnd = new System.Random();
        List<String> koordin = new List<string>();
        for (int i = 0; i < kiekis; i++)
        {
            bool uzimta = false;
            string xstring;
            string ystring;

            int x = rnd.Next(1, 11);
            int y = rnd.Next(1, 11);
            int vertical = rnd.Next(0, 2);
     
            if (vertical == 0)
                while (x > 10 - ilgis)
                    x--;
            else
                while (y > 10 - ilgis)
                    y--;
            if (vertical == 0)                                                   
                for (int e = x; e < x + ilgis; e++)                         
                {
                    if (e < 10) xstring = "0" + e;
                    else xstring = e.ToString();
                    if (y < 10) ystring = "0" + y;
                    else ystring = y.ToString();
                    koordin.Add("B1:[" + xstring + "," + ystring + "]");
                    if (visi.Contains("B1:[" + xstring + "," + ystring + "]")) uzimta = true;
                }
            else
                for (int e = y; e < y + ilgis; e++)
                {
                    if (x < 10) xstring = "0" + x;
                    else xstring = x.ToString();
                    if (e < 10) ystring = "0" + e;
                    else ystring = e.ToString();
                    koordin.Add("B1:[" + xstring + "," + ystring + "]");
                    if (visi.Contains("B1:[" + xstring + "," + ystring + "]")) uzimta = true;
                }
       
            if (uzimta)
            {
                i--;
                continue;
            }
            else
            {
                
                koordin.ForEach(item => visi.Add(item));
                koordin.ForEach(item => Debug.LogWarning(item));
                if (vertical == 0)
                {
                    LaivoPridejimas(koordin, laivas, Quaternion.Euler(0, 90, 0), paklaidax, 0);
                    BoardVer1.Laivai.Add(new Laivas(ilgis, laivas.name, koordin, false));
                }
                else
                {
                    LaivoPridejimas(koordin, laivas, Quaternion.identity, 0, paklaiday);
                    BoardVer1.Laivai.Add(new Laivas(ilgis, laivas.name, koordin, true));
                }
            }
            koordin.Clear();
        }
        pasibaige(ilgis.ToString());
        Script.printStuff();
    }
    void refresh()
    {
        BoardVer1.Laivai.Clear();
        visi.Clear();
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("board");
        for (int i = 0; i < tiles.Length; i++)
            tiles[i].GetComponent<Renderer>().material.color = Color.white;
        tiles = GameObject.FindGameObjectsWithTag("Laivai");
        for (int i = 0; i < tiles.Length; i++)
            GameObject.Destroy(tiles[i]);
        //board2 = new GameObject[100, 100];
       
        board2 = Script.board;
       
    }
    public void Reset()
    {

        refresh();
        Text kiekis;


        for (int i = 0; i < 4; i++)
            Script.ZadimoCanvas.transform.GetChild(3 + i).gameObject.SetActive(true);

         kiekis = GameObject.FindGameObjectWithTag("Frigata").GetComponent<Text>();
         kiekis.text = ("4").ToString() ;
         kiekis = GameObject.FindGameObjectWithTag("Minininkas").GetComponent<Text>();
         kiekis.text = ("3").ToString();
         kiekis = GameObject.FindGameObjectWithTag("Kreiseris").GetComponent<Text>();
         kiekis.text = ("2").ToString(); 
         kiekis = GameObject.FindGameObjectWithTag("Sarvuotlaivis").GetComponent<Text>();
         kiekis.text = ("1").ToString();
        length = 1;

    }
    public GameObject GetTileByString(string name)
    {
        GameObject[] plteles = GameObject.FindGameObjectsWithTag("board");
        foreach (GameObject tile in plteles)
        {
            if (tile.name.Equals(name)) return tile;
        }

        return null;
    }
}
