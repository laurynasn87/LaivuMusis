using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public GameObject ranking;
    GameObject[] reitingai;
    public GameObject Nerinterneto;
    // Start is called before the first frame update
    void Start()
    {
        reitingai = GameObject.FindGameObjectsWithTag("Ranking");
        ranking.SetActive(false);
        if (!IsConnected()) Nerinterneto.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 5f);
    }
    public void check()
    {
        if (IsConnected()) Nerinterneto.SetActive(false);
    }

    public bool IsConnected(string hostedURL = "http://www.google.com")
    {
        try
        {
            string HtmlText = GetHtmlFromUri(hostedURL);
            if (HtmlText == "")
                return false;
            else
                return true;
        }
        catch 
        {
            return false;
        }
    }
    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }
    public void GeriausiuLentele()
    {
        ranking.SetActive(!ranking.active);
        if (ranking.active == true) StartCoroutine(makelist());
        foreach (GameObject reitingas in reitingai)
        {
            reitingas.SetActive(!reitingas.active);
        }
    }
    public IEnumerator makelist()
    {
    
        //WWW www = new WWW(CounterData);

        UnityWebRequest www = UnityWebRequest.Get("https://bastioned-public.000webhostapp.com/GetScore.php?fbclid=IwAR3gWa6551RsQNc-wvbc4rpyM31kNX7nXodpiKSi086dnIsooD0YR_iELoo");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string uzimtaVieta = www.downloadHandler.text;
           List<String> parsed =  Parser(uzimtaVieta);
            for (int ix=0; ix<parsed.Count; ix++)
            {
                Text tekstukas = reitingai[ix].GetComponent<Text>();
                tekstukas.text = parsed[ix];
                reitingai[ix].SetActive(true);
            }
        }
    }
    public List<String> Parser(String stringas)
    {
        // Debug.LogWarning(stringas);
        List<String> data = new List<String>();
        String[] parse = stringas.Split(';');

        for (int i = 0; i < parse.Length - 1; i++)
        {
            String[] parsemore = parse[i].Split('|');

            data.Add(parsemore[0]);
            data.Add(parsemore[1]);
            data.Add(parsemore[2]);
        }
        return data;
    }
    public void exit()
    {
        Application.Quit();

    }
    public void mainmeniu()
    {
        Application.LoadLevel(Application.loadedLevel);

    }
}
