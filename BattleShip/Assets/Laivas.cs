﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laivas
{
   public int ilgis;
    public string pavadinimas;
    public bool vertical;
    public int pamusta = 0;
    public String[] Koordinates;

    public Laivas(int ilgis, string pavadinimas, String[] Koordinates, bool vertical)
    {
        this.ilgis = ilgis;
        this.pavadinimas = pavadinimas;
        this.Koordinates = Koordinates;
        this.vertical = vertical;
    }
    public int Getilgis()
    {
        return this.ilgis;
    }
    public String Getpavadinimas()
    {
        return this.pavadinimas;
 
    }
    public String[] Koordinatess()
    {
        return this.Koordinates;

    }


}
