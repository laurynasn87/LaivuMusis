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
    string laimejimas = "https://bastioned-public.000webhostapp.com/Score.php?";
    string vieta = "https://bastioned-public.000webhostapp.com/GetUserScore.php?username='";
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
       // Debug.LogWarning(stringas);
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
            if (lop == 21)
            {
                AIKord = "88|B1:[09,08]|33;37|B1:[04,07]|31;68|B1:[07,08]|30;62|B1:[07,02]|29;74|B1:[08,04]|28;25|B1:[03,05]|28;69|B1:[07,09]|28;48|B1:[05,08]|26;89|B1:[09,09]|26;35|B1:[04,05]|26;49|B1:[05,09]|25;57|B1:[06,07]|25;75|B1:[08,05]|24;64|B1:[07,04]|24;67|B1:[07,07]|24;78|B1:[08,08]|23;9|B1:[01,09]|23;47|B1:[05,07]|23;36|B1:[04,06]|23;39|B1:[04,09]|22;7|B1:[01,07]|22;27|B1:[03,07]|21;38|B1:[04,08]|21;72|B1:[08,02]|21;79|B1:[08,09]|21;80|B1:[08,10]|21;84|B1:[09,04]|21;77|B1:[08,07]|20;76|B1:[08,06]|20;85|B1:[09,05]|20;42|B1:[05,02]|20;55|B1:[06,05]|20;29|B1:[03,09]|20;28|B1:[03,08]|20;23|B1:[03,03]|20;45|B1:[05,05]|19;90|B1:[09,10]|19;59|B1:[06,09]|19;65|B1:[07,05]|19;52|B1:[06,02]|19;56|B1:[06,06]|19;63|B1:[07,03]|19;26|B1:[03,06]|19;15|B1:[02,05]|19;17|B1:[02,07]|19;18|B1:[02,08]|19;58|B1:[06,08]|18;87|B1:[09,07]|18;86|B1:[09,06]|18;8|B1:[01,08]|18;19|B1:[02,09]|18;34|B1:[04,04]|18;98|B1:[10,08]|18;73|B1:[08,03]|18;24|B1:[03,04]|18;43|B1:[05,03]|17;33|B1:[04,03]|17;54|B1:[06,04]|17;31|B1:[04,01]|17;12|B1:[02,02]|16;53|B1:[06,03]|15;16|B1:[02,06]|15;41|B1:[05,01]|15;5|B1:[01,05]|14;82|B1:[09,02]|14;46|B1:[05,06]|14;4|B1:[01,04]|14;66|B1:[07,06]|14;13|B1:[02,03]|14;61|B1:[07,01]|14;95|B1:[10,05]|14;97|B1:[10,07]|13;2|B1:[01,02]|13;96|B1:[10,06]|13;99|B1:[10,09]|13;22|B1:[03,02]|13;1|B1:[01,01]|13;32|B1:[04,02]|12;94|B1:[10,04]|12;81|B1:[09,01]|12;14|B1:[02,04]|11;21|B1:[03,01]|11;71|B1:[08,01]|11;70|B1:[07,10]|10;83|B1:[09,03]|9;44|B1:[05,04]|9;92|B1:[10,02]|9;60|B1:[06,10]|9;6|B1:[01,06]|9;40|B1:[04,10]|8;51|B1:[06,01]|8;93|B1:[10,03]|8;11|B1:[02,01]|8;3|B1:[01,03]|7;30|B1:[03,10]|7;50|B1:[05,10]|6;10|B1:[01,10]|6;20|B1:[02,10]|6;91|B1:[10,01]|4;100|B1:[10,10]|0;";
                lop++;
                AI();
                return;
            }
            }
    

    }


    public void atimtigyvybe(string name)
    {
        int kelinta = -1;
        
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
                    GameObject tiles= GetTileByString(kord);
                    //Instantiate(explosion, tiles.transform.position, Quaternion.Euler(60, 90, 0));
                }
                kelinta = Laivai2.IndexOf(laivelis);
              //  Laivai2.Remove(laivelis);
            }
        }
       if (kelinta>-1) Laivai2.RemoveAt(kelinta);
        if (Laivai2.Count == 0) win();





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
                
            case 2:
                return Scriptas.korvete;
               
            case 3:
                return Scriptas.minininkas;
            case 4:
                return Scriptas.Sarvuotlaivis;

        }
        return null;

    }

    public void win()
    {
        int ejimukiekis = int.Parse(ejimai.text);
        int suvis =  int.Parse(pataikymai.text);
        String name = Scriptas.Vardas.text;
        if (name.Equals("")) name = "Žaidėjas1";
        wins.SetActive(true);
        //Cia paduoti i serva kiek suviu prireike i duombaze kaip score ir name
        //

        string url = laimejimas + "username='" + name + "'&score='" + suvis + "'";
        Debug.Log(url);
        WWW www = new WWW(url);

        Text[] baiges = wins.GetComponentsInChildren<Text>();
        baiges[1].text = "Jums Prireikė: " + ejimai.text + " ėjimų";
        baiges[2].text = "Jums Prireikė: " + pataikymai.text + " šuvių";
        baiges[3].text = "Jus užemat: " + getvieta(name) + " vietą";
    }
    public void loose()
    {
        wins.SetActive(true);
    }
    /*public IEnumerator getvieta(string name)
    {
        //WWW www = new WWW(CounterData);

        UnityWebRequest www = UnityWebRequest.Get(vieta+name+"'");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string uzimtaVieta = www.downloadHandler.text;
            Debug.Log(uzimtaVieta);
            Debug.Log(www);
        }
    }*/

    IEnumerator getvieta(string vardas)
    {
        string url = vieta + vardas + "'";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data); // this log is returning the requested data. 
            string a = www.downloadHandler.text;
            Debug.Log(www.downloadHandler.text);
        }
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
