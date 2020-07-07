using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : IHeapItem<Field>, IComparable<Field> {
    private int row;
    private int column;
    private int numOfAlg;


    private int gCost;  //distance from current Field until starting Field
    private int hCost; //distance from current Field until End Field
    private bool walkable;
    int fieldWidth;
    GameObject [] surfaceOfField;

    private int heapIndex;

    private Field parent;

    public Field Parent { get { return parent; } set { parent = value; } }

    public int Row { get { return row; } }
    public int Column { get { return column; } }

    public int GCost { get { return gCost; } set { gCost = value; } }
    public int HCost { get { return hCost; } set { hCost = value; } }
    public int FCost { get { return gCost + hCost; } }
    public bool isWalkable { get { return walkable; } set { walkable = value; colorWholeField(value ? Color.white : Color.black); } }
    public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

    public Field(int fieldWidth, int row, int column, bool walkable) {
        this.row = row;
        this.column = column;
        this.walkable = walkable;
        this.fieldWidth = fieldWidth;
        this.gCost = 0;
        this.hCost = 0;
        numOfAlg = 0;
        if (SetUpOptions.getInstanc().IsAStar)
        {
            ++numOfAlg;
        }
        if (SetUpOptions.getInstanc().IsBFS)
        {
            ++numOfAlg;
        }
        //Debug.Log(" BROJ ALGORITAM JE :" + numOfAlg);

        surfaceOfField = new GameObject[numOfAlg];
        //Utils.CreateWorldText("["+row+","+column+"]", null, getWorldPosition(row, column), 20, Color.white, TextAnchor.MiddleCenter);

        float partWidth = fieldWidth / numOfAlg;
        float leftCoord = - fieldWidth / 2 + partWidth /2;

        for (int i=0; i< numOfAlg; i++) 
        {
            surfaceOfField[i] = Utils.CreateWorldSprite("Field", Resources.Load("Sprites/Square", typeof(Sprite)) as Sprite, 
                getWorldPosition(row, column) + new Vector3(leftCoord, 0, 0), new Vector3(partWidth, fieldWidth), 30, Color.white);
            leftCoord += partWidth;
        }

        //surfaceOfField[0] = Utils.CreateWorldSprite("Field", Resources.Load("Sprites/Square",typeof(Sprite)) as Sprite, getWorldPosition(row, column)/* + new Vector3(3.5f, 0, 0)*/, new Vector3(fieldWidth, fieldWidth),30, Color.white);
        //field[1] = Utils.CreateWorldSprite("Field", Resources.Load("Sprites/Square",typeof(Sprite)) as Sprite, getWorldPosition(row, column) - new Vector3(3.5f,0,0), new Vector3(7,14),30, Color.white);

    }
    private Vector3 getWorldPosition(int row, int column)
    {
        return new Vector3(column*(fieldWidth + 1), row * (-(fieldWidth + 1)));
    }

    public Vector3 getWorldPosition()
    {
        return getWorldPosition(row, column);
    }

    public Field(Field field) //Copy constructor
    {
        this.fieldWidth = field.fieldWidth;
        this.row = field.row;
        this.column = field.column;
        this.walkable = field.walkable;
        this.gCost = field.gCost;
        this.hCost = field.hCost;
        this.heapIndex = field.heapIndex;
        this.parent = field.parent;
    }


    //odradi ovo preko delegata
    public int CompareTo(Field other) {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0)
        {
            compare = HCost.CompareTo(other.HCost);
        }
        //1 curent item is lower priority
        return -compare;
    }

    public void changeFieldColorTo(int algId, Color color)
    {
        for(int i = 0; i < surfaceOfField.Length - algId; i++)
        {
            surfaceOfField[i].GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void colorWholeField(Color color)
    {
        for (int i = 0; i < surfaceOfField.Length; i++)
        {
            surfaceOfField[i].GetComponent<SpriteRenderer>().color = color;
        }
    }
}
