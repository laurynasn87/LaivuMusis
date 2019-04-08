using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Priesininkas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[,] board = new GameObject[100, 100];
    public GameObject MainBoard;
    public GameObject PriesininkoKamera;
    public GameObject prefab;
    public GameObject bullet;
    public GameObject AIbullet;
    public GameObject explosion;
    public GameObject faieaa;
    public AudioClip splash;
    public AudioClip explosionaudio;
    public Slider lygis;
    public Text ejimai;
    public GameObject databaseconn;
    public Text pataikymai;
    bool nuskaityta = false;
    public List<Laivas> Laivai2 = new List<Laivas>();
    public bool ShotNotMiss = true;
    public bool AiShot = false;
    public bool AIeile = false;
    public List<String> visi = new List<string>();
    public List<String> DazniausiaiPataikomi = new List<String>();
    public bool ejimas = true;
    public string AIKord = "";
    BoardVer1 Scriptas;
    GameObject wins;
    public GameObject kubas;
    void Start()
    {
        Scriptas = MainBoard.gameObject.GetComponent<BoardVer1>();
        wins = GameObject.FindGameObjectWithTag("Finish");
        wins.SetActive(false);
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
                GameObject tmp = Instantiate(prefab, new Vector3(ki + 1000, 0, kj), prefab.transform.rotation) as GameObject;
                //GameObject go = tmp;
                PriesTile tmpUI = tmp.GetComponent<PriesTile>();
                //Debug.Log(tmpUI);
                string name = string.Format("B1:[{0:00},{1:00}]", row, col);

                tmpUI.col = kj;
                tmpUI.row = ki;
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
        StartCoroutine(MoveKamera());

        RandomDidesniLaivai(1, Scriptas.frigata, 0, 0, 4);
        RandomDidesniLaivai(2, Scriptas.minininkas, 0, 0, 3);
        RandomDidesniLaivai(3, Scriptas.korvete, 0, 0, 2);
        RandomDidesniLaivai(4, Scriptas.Sarvuotlaivis, 0, 0, 1);

        if (AIKord.Equals("")) databaseconn.SetActive(true);

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
                    if (koordin.Contains("B1:[" + xstring + "," + ystring + "]")) uzimta = true;
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
                    if (koordin.Contains("B1:[" + xstring + "," + ystring + "]")) uzimta = true;
                    koordin.Add("B1:[" + xstring + "," + ystring + "]");
                    if (visi.Contains("B1:[" + xstring + "," + ystring + "]")) uzimta = true;

                }

            if (uzimta)
            {
                koordin.Clear();
                i--;
                continue;

            }
            else
            {

                koordin.ForEach(item => visi.Add(item));
                String[] array = koordin.ToArray();
                // cia zjb kording
                if (vertical == 0)
                {

                    Laivai2.Add(new Laivas(ilgis, laivas.name, array, false));

                }
                else
                {
                    Laivai2.Add(new Laivas(ilgis, laivas.name, array, true));
                }
            }
            koordin.Clear();
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (!AIKord.Equals("")) databaseconn.SetActive(false);

    }
    IEnumerator MoveKamera()
    {
        BoardVer1 Scriptas = MainBoard.gameObject.GetComponent<BoardVer1>();
        Vector3 oldpos = PriesininkoKamera.transform.position;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 1.3f);

            PriesininkoKamera.transform.position = Vector3.Lerp(oldpos, new Vector3(1028, 98, -27), t);

            //  PriesininkoKamera.transform.rotation = Quaternion.Lerp(PriesininkoKamera.transform.rotation, new Quaternion(60, PriesininkoKamera.transform.rotation.y, PriesininkoKamera.transform.rotation.z, PriesininkoKamera.transform.rotation.w), t);
            yield return 0;

        }
        PriesininkoKamera.transform.rotation = Quaternion.Euler(60, 0, 0);
        Camera Kamera = PriesininkoKamera.GetComponent<Camera>();
        Kamera.rect = new Rect(new Vector2(0.2f, 0), new Vector2(0.8f, 1));
        Scriptas.Main.SetActive(true);


    }
    public GameObject GetTileByString(string name)
    {
        GameObject[] plteles = GameObject.FindGameObjectsWithTag("board2");
        foreach (GameObject tile in plteles)
        {
            if (tile.name.Equals(name)) return tile;
        }

        return null;
    }
    public List<String> Parser(String stringas)
    {
        Debug.LogWarning(stringas);
        List<String> data = new List<String>();
        String[] parse = stringas.Split(';');

        for (int i = 0; i < parse.Length - 1; i++)
        {
            String[] parsemore = parse[i].Split('|');

            data.Add(parsemore[1]);

        }
        return data;
    }
    int lop = 0;
    public void AI()
    {
        if (nuskaityta == false)
        {
            DazniausiaiPataikomi = Parser(AIKord);
            nuskaityta = true;
        }

          try
           {

      
            Scriptas.shootAI(DazniausiaiPataikomi[0]);

            DazniausiaiPataikomi.RemoveAt(0);


       }
        catch (Exception e)
        {
            Debug.LogWarning("Prisijungti Nepavyko prie duomenu bazes");
            Debug.LogWarning(e);
            if (lop < 20)
            {
                Debug.LogWarning("Bandoma vel");
              lop++;
                AI();
                return;
           }
        }
    

    }


    public void atimtigyvybe(string name)
    {
        if (Laivai2.Count == 0) win();
        Debug.LogWarning(Laivai2.Count);
        if (Laivai2.Count == 0) win();
        foreach (Laivas laivelis in Laivai2)
        {
            int index = Array.IndexOf(laivelis.Koordinates, name);
            if (index >= 0) laivelis.pamusta++;

            if (laivelis.pamusta >= laivelis.ilgis)
            {
                foreach (String kord in laivelis.Koordinates)
                {
                    GetTileByString(kord).GetComponent<Renderer>().material.color = Color.yellow;
                }
            }

        }




        /*
        for (int i = 0; i < Laivai2.Count; i++)
        {
            if (Array.IndexOf(Laivai2[i].Koordinates, name) > -1)
                Laivai2[i].pamusta++;
            Debug.LogWarning(Laivai2[i].pavadinimas + " " + Laivai2[i].pamusta);
            if (Laivai2[i].pamusta >= Laivai2[i].ilgis)
            {
                GameObject tile = GetTileByString(name);
                for (int k = 0; k < Laivai2[i].Koordinates.Length; k++)
                    shotDown(tile);

                Laivai2.Remove(Laivai2[i]);
            }

        }*/

    }
    public void PridetiEjima()
    {
        ejimai.text = (int.Parse(ejimai.text) + 1).ToString();
    }
    public void PridetPataikyma()
    {
        pataikymai.text = (int.Parse(pataikymai.text) + 1).ToString();
    }
    GameObject getlaivasbyilgis(int id)
    {
        switch (id)
        {
            case 1:
                return Scriptas.frigata;
                break;
            case 2:
                return Scriptas.korvete;
                break;
            case 3:
                return Scriptas.minininkas;
            case 4:
                return Scriptas.Sarvuotlaivis;

        }
        return null;

    }

    public void win()
    {
        wins.SetActive(true);
    }
    public void loose()
    {
        wins.SetActive(true);
    }
    IEnumerator shotDown(GameObject tile)
    {

        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 2);
            tile.GetComponent<Renderer>().material.color = Color.yellow;
            yield return 0;
            tile.GetComponent<Renderer>().material.color = Color.red;
        }
        tile.GetComponent<Renderer>().material.color = Color.black;
    }
    public void exit()
    {
        SceneManager.LoadScene("SampleScene");
        /*
                PriesininkoKamera.SetActive(false);
                wipe(GameObject.FindGameObjectsWithTag("Laivai"));
                Scriptas.Main.SetActive(false);
                wipe(Scriptas.board);
                wipe(board);
                kubas.SetActive(true);
                Scriptas.oldcanvas.GetComponent<Canvas>().enabled = true;
                Scriptas.oldcanvas.SetActive(true);
                MainBoard.SetActive(false);

                this.gameObject.SetActive(false);
                */
    }
    public void wipe(GameObject[,] arejus)
    {
        int w = arejus.GetLength(0); // width
        int h = arejus.GetLength(1); // height

        for (int x = 0; x < w; x = x + 10)
        {
            for (int y = 0; y < h; y = y + 10)
            {
                Destroy(arejus[x, y]);


            }
        }
    }
    public void wipe(GameObject[] arejus)
    {
        int w = arejus.Length; // width

        for (int x = 0; x < w; x = x + 10)
        {

            Destroy(arejus[x]);



        }
    }
}
