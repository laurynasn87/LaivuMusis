using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Priesininkas script;
    AudioSource[] explosioon;
    public AudioClip klipas;
    public AudioClip splash;
    // Start is called before the first frame update
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Priesininkas>();
        //       explosion = GetComponents<AudioSource>()[1];
        explosioon = GetComponents<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
     
            if (script.ShotNotMiss)
            {
                GameObject explosion = script.explosion;
                Instantiate(script.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
                Instantiate(script.faieaa, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
               
            
           AudioSource sound = other.gameObject.AddComponent<AudioSource>();
            sound.clip = klipas;
            sound.enabled = true;
            sound.Play();
                GameObject me = gameObject;

            if (other.gameObject.GetComponent<Renderer>().material.color != Color.yellow)
                other.gameObject.GetComponent<Renderer>().material.color = Color.black;
            Destroy(me);

            }
            else
            {
            AudioSource sound = other.gameObject.AddComponent<AudioSource>();
            sound.clip = splash;
            sound.enabled = true;
            sound.Play();
            GameObject me = gameObject;
                Destroy(me);


            }
        
    }
}
