using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class rotate : MonoBehaviour

{

    private bool[] dots= {false,false,false,false};
    private string[] dotNames = { "Dot1", "Dot2", "Dot3", "Dot4" };


    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        
    }
     public void OnMouseDown()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f));
        Text texto = transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        texto.transform.eulerAngles = new Vector3(0f, 0f, 45f);
        int numero = Convert.ToInt32(transform.name.Substring(4));
        GameObject XMLManager = GameObject.Find("XMLManager");
        int cuadrante= XMLManager.GetComponent<XMLManager>().Cuadrantes[numero-1];
        cuadrante++;
        if (cuadrante > 4) cuadrante = 1;
        XMLManager.GetComponent<XMLManager>().Cuadrantes[numero-1]=cuadrante;
        for (int i=0;i<4;i++) if(dots[i]) GameObject.Find(dotNames[i]).transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90f);
        // Debug.Log("Clicked");
        //if()
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        switch (collision.gameObject.name){
            case "Dot1":
                dots[0] = true;
                break;
            case "Dot2":
                dots[1] = true;
                break;
            case "Dot3":
                dots[2] = true;
                break;
            case "Dot4":
                dots[3] = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Dot1":
                dots[0] = false;
                break;
            case "Dot2":
                dots[1] = false;
                break;
            case "Dot3":
                dots[2] = false;
                break;
            case "Dot4":
                dots[3] = false;
                break;
        }
    }
}
