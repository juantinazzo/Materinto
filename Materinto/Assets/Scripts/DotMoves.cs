using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotMoves : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private Vector2 mousePos;
    private float dX, dY;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        dX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        dY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
    }
    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x - dX, mousePos.y - dY, -4);
    }

}
