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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = script.explosion;
        Instantiate(script.explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
        Instantiate(script.faieaa, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(60, 90, 0));
        GameObject me = gameObject;
        Destroy(me);
    }
}
