﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject Nerinterneto;
    // Start is called before the first frame update
    void Start()
    {
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
}
