using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAi : MonoBehaviour
{
    Priesininkas script; // Start is called before the first frame update
    public AudioClip klipas;
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Priesininkas>();
       // Debug.LogWarning("STOP2");
        GameObject[] laivai = GameObject.FindGameObjectsWithTag("Laivai");
        foreach (GameObject laiveliai in laivai)
        {
            Physics.IgnoreCollision(laiveliai.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject me = gameObject;
        AudioSource garsas;
        if (script.AiShot)
        {
            //   

            try
            {
                if (other.gameObject.GetComponent<Renderer>().material.color!=Color.yellow)
                other.gameObject.GetComponent<Renderer>().material.color = Color.black;
                GameObject explosion = script.explosion;
                Instantiate(script.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
                Instantiate(script.faieaa, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
                
            }
            catch (Exception e)
            {
                Debug.LogWarning("Bugas bullet AI " + e);
            }


        }
        else
        {
            try
            {
                if (other.gameObject.GetComponent<Renderer>().material.color != Color.yellow)
                    other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            catch
            {
               // other.gameObject.AddComponent<Renderer>().material.color = Color.blue;
            }
            garsas = GetComponent<AudioSource>();
            garsas.clip = script.splash;
            garsas.Play();




        }

        script.ejimas = true;
        Destroy(me);
    }
}
