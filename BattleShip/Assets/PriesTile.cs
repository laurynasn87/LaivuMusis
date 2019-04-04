using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriesTile : MonoBehaviour
{
    public int row;
    public int col;
    // Start is called before the first frame update
    Priesininkas script;
    public GameObject bullet;
    void Start()
    {
        script = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Priesininkas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        Debug.LogWarning(gameObject.name);
        GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    private void OnMouseDown()
    {
        if (script.ejimas)
        {
            string name;
            name = gameObject.name;
            if (aryrakazkas(name))
            {
                shoot(name);
            }
            else miss(name);
           // script.ejimas = false;
        }
    }
    bool aryrakazkas(string name)
    {
        return true;
    }
    void shoot (string name)
    {
       bullet = Instantiate(script.bullet, new Vector3(953, 184, 32), Quaternion.Euler(60,90,0));
        StartCoroutine(ProjectileMove());
    }
    void miss(string name)
    {

    }
    IEnumerator ProjectileMove()
    {

        Vector3 oldpos = bullet.transform.position;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 1);

            bullet.transform.position = Vector3.Lerp(oldpos, new Vector3(transform.position.x, transform.position.y, transform.position.z), t);

            //  PriesininkoKamera.transform.rotation = Quaternion.Lerp(PriesininkoKamera.transform.rotation, new Quaternion(60, PriesininkoKamera.transform.rotation.y, PriesininkoKamera.transform.rotation.z, PriesininkoKamera.transform.rotation.w), t);
            yield return 0;

        }

    }
}
