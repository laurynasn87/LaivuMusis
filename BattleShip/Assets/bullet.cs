using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Priesininkas script;


    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Priesininkas>();
        //       explosion = GetComponents<AudioSource>()[1];
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        AudioSource garsas;
            if (script.ShotNotMiss)
            {
                GameObject explosion = script.explosion;
                Instantiate(script.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
                Instantiate(script.faieaa, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
                garsas = GetComponent<AudioSource>();
                garsas.clip = script.explosionaudio;
                garsas.Play();
                GameObject me = gameObject;
                other.gameObject.GetComponent<Renderer>().material.color = Color.black;
                Destroy(me);

            }
            else
            {
                garsas = GetComponent<AudioSource>();
                garsas.clip = script.splash;
                garsas.Play();
                GameObject me = gameObject;
                Destroy(me);


            }
        
    }
}
