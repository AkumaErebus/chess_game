using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject Controller;

    GameObject reference = null;

    //Board position, not world positions
    int matrixX;
    int matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if(attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f, 1.0f, 1.0f);
        }
    }
    public void OnMouseUp()
    {
        Controller = GameObject.FindGameObjectWithTag("GameController");

        if(attack)
        {
            GameObject cp = Controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }
        Controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), 
            reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        Controller.GetComponent<Game>().SetPosition(reference);
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj) 
    {
        reference = obj;
    }
    public GameObject GetReference() 
    { 
        return reference;
    }
}
