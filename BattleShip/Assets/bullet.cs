using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Priesininkas script;
    public AudioClip audio;
    AudioSource garsas;
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
        if (script.ShotNotMiss)
        {
            GameObject explosion = script.explosion;
            Instantiate(script.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
            Instantiate(script.faieaa, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
            AudioSource.PlayClipAtPoint(audio, transform.position, 1);
        GameObject me = gameObject;
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;
            Destroy(me);
            
        }
        else
        {
            GameObject me = gameObject;
            AudioSource.PlayClipAtPoint(script.splash, transform.position, 1);
            
            Destroy(me);


        }
    }
}
