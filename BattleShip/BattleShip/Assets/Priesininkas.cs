using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priesininkas : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[,] board = new GameObject[100, 100];
    public GameObject MainBoard;

    void Start()
    {
        BoardVer1 Scriptas = MainBoard.gameObject.GetComponent<BoardVer1>();
        int row = 1;
        int col = 1;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int ki = i * 10;
                int kj = j * 10;
                //for (int k = 0; k < numSelectors; k++)
                //{
                GameObject tmp = Instantiate(Scriptas.BoardUnitPrefab, new Vector3(ki+1000, 0, kj), Scriptas.BoardUnitPrefab.transform.rotation) as GameObject;
                //GameObject go = tmp;
                BoardUIVer1 tmpUI = tmp.GetComponent<BoardUIVer1>();
                //Debug.Log(tmpUI);
                string name = string.Format("B1:[{0:00},{1:00}]", row, col);
                tmpUI.lblBoardPosition.text = name;
                tmpUI.COL = kj;
                tmpUI.ROW = ki;
                //Debug.Log(name);


                //tmp.transform.localScale = Vector3.one;
                board[ki, kj] = tmp;
                tmp.name = name;

                col++;
                //}
            }
            col = 1;
            row++;
        }
        StartCoroutine(MoveKamera());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveKamera()
    {
        BoardVer1 Scriptas = MainBoard.gameObject.GetComponent<BoardVer1>();
        Vector3 oldpos = Scriptas.Main.transform.position;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 2);

            Scriptas.Main.transform.position = Vector3.Lerp(oldpos, new Vector3(oldpos.x + 1000, oldpos.y, oldpos.z), t);
        //    Scriptas.Main.transform.rotation = Quaternion.Lerp(Scriptas.Main.transform.rotation, new Quaternion(Scriptas.Main.transform.rotation.x, 360, Scriptas.Main.transform.rotation.z, Scriptas.Main.transform.rotation.w), t);
            yield return 0;
            
        }

    }
}
