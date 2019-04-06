using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFromJson
{
    public string id;
    public string koordinates;
    public string counter;

    public DataFromJson(string id, string koordinates, string counter)
    {
        this.id = id;
        this.koordinates = koordinates;
        this.counter = counter;
       
    }
}
