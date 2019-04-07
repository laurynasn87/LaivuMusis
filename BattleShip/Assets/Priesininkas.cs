using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Priesininkas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[,] board = new GameObject[100, 100];
    public GameObject MainBoard;
    public GameObject PriesininkoKamera;
    public GameObject prefab;
    public GameObject bullet;
    public GameObject explosion;
    public GameObject faieaa;
    public AudioClip splash;
    public Text ejimai;
    public Text pataikymai;
    public List<Laivas> Laivai2 = new List<Laivas>();
    public bool ShotNotMiss = true;
   public List<String> visi = new List<string>();
    public bool ejimas = true;
    public string AIKord;
    BoardVer1 Scriptas;
    void Start()
    {
         Scriptas = MainBoard.gameObject.GetComponent<BoardVer1>();
        StartCoroutine(Scriptas.GetComponent<BoardVer1>().GetCounterData());
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
                GameObject tmp = Instantiate(prefab, new Vector3(ki+1000, 0, kj), prefab.transform.rotation) as GameObject;
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
                    if (vertical == 0)
                    {
                    PlaceShip(koordin, paklaidax, 0);
                       Laivai2.Add(new Laivas(ilgis, laivas.name, koordin, false));
                    }
                    else
                    {
                    PlaceShip(koordin, 0, paklaiday);
                        Laivai2.Add(new Laivas(ilgis, laivas.name, koordin, true));
                    }
                }
                koordin.Clear();
            }

        }

    public void PlaceShip(List<String> Koordinates, int paklaidax, int paklaiday)
    {
       

        foreach (String cord in Koordinates)
        {

            GameObject Tile = GetTileByString(cord);
            if (Tile != null)
            {
                // Tile.GetComponent<Renderer>().material.color = Color.red;

            }
         
        }

    }
    // Update is called once per frame
    void Update()
    {
        
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
    public void AI()
    {// AI kord == json string
     //   Debug.LogError(AIKord);
        DataFromJson[] player = JsonHelper.FromJson<DataFromJson>(AIKord);
        for (int i = 0; i < player.Length; i++)
             Debug.LogWarning(player[i].koordinates);

        Scriptas.shoot("B1:[03,06]");// cia reikia paduoti koordinate i kuria saus 

    }
    public void atimtigyvybe(string name)
    {
        Debug.LogWarning(Laivai2.Capacity);
        foreach (Laivas laivai in Laivai2)
        {
            Debug.LogWarning(laivai.Koordinates.Count);
            if (laivai.Koordinates.Contains(name))
            {
                laivai.pamusta++;
                Debug.LogWarning("Pamustas " + laivai.pavadinimas );
            }
            if (laivai.pamusta>=laivai.ilgis)
            {
                for (int i =0; i<laivai.ilgis; i++)
                {

                    GameObject tile = GetTileByString(laivai.Koordinates[i]);
                    Instantiate(explosion, new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z), Quaternion.Euler(60, 90, 0));
                }
                
            }
        }
    }
    public void PridetiEjima()
    {
        ejimai.text = (int.Parse(ejimai.text) + 1).ToString();
    }
    public void PridetPataikyma()
    {
        pataikymai.text = (int.Parse(pataikymai.text) + 1).ToString();
    }
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
