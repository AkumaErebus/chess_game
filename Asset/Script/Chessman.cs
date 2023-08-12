using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // Refernce
    public GameObject Controller;
    public GameObject movePlate;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable to track of "Black" player or "White" player
    private string player;

    // Refernce for all the sprites that the chesspeiece can be
    public Sprite black_queen, black_king, black_rook, black_knight, black_bishop, black_pawn;
    public Sprite white_queen, white_king, white_rook, white_knight, white_bishop, white_pawn;

    public void Activate()
    {
        Controller = GameObject.FindGameObjectWithTag("GameController");

        //take the instantiated location  and adjust the tranform
        SetCoords();

       switch(this.name) 
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
        }

    }
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.4f;
        y += -2.4f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    { 
        return xBoard; 
    }
     public int GetYBoard() 
    { 
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard= x;
    }
    public void SetYBoard(int y)
    {
        yBoard= y;
    }
    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i =0; i< movePlates.Length; i++) 
        {
            Destroy(movePlates[i]);
        }
    }
    public void InitiateMovePlates()
    {
        switch(this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0,1);
                LineMovePlate(-1,1);
                LineMovePlate(-1, 0);
                LineMovePlate(0,-1);
                LineMovePlate(-1,-1);
                LineMovePlate(-1, 1);
                LineMovePlate(1,-1);
                break;

            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;

            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;

            case "black_king":
            case "white_knig":
                SurroundMovePlate();
                break;

            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard -1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard +1);
                break;

        }
    }
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = Controller.GetComponent<Game>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBorad(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if(sc.PositionOnBorad(x,y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player) 
        { 

            MovePlateAttackSpawn(x,y);
        }
    }
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }
    public void PointMovePlate(int x, int y)
    {
        Game sc = Controller.GetComponent<Game>();
        if (sc.PositionOnBorad(x,y))
        {
            GameObject cp = sc.GetPosition(x,y);
            if(cp == null)
            {
                MovePlateSpawn(x,y);
            }
            else if(cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }
    public void PawnMovePlate(int x, int y)
    {
        Game sc = Controller.GetComponent<Game>();
        if(sc.PositionOnBorad(x,y))
        {
            if(sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
            }
            if(sc.PositionOnBorad(x + 1, y) && sc.GetPosition(x+1, y) !=null && 
                sc.GetPosition(x+1,y).GetComponent<Chessman>().player !=player )
            {
                MovePlateSpawn(x+1,y);
            }
            if (sc.PositionOnBorad(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateSpawn(x - 1, y);
            }
        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY ,  bool isAttack = false) 
    { 
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.4f;
        y += -2.4f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity); 
            
        MovePlate mpScript= mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

    }
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.4f;
        y += -2.4f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

    }
}
