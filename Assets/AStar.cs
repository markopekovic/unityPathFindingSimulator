using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : PathFindingAlg
{
    public AStar() : base()
    {
        name = "A*";
        Debug.Log("Astar ID = " + id);
    }

    public override IEnumerator calculate(Grid grid)
    {
        float startTime = Time.time;

        Field startField = grid.getStartField();
        Field endField = grid.getEndField();
        Field[] heapArray = new Field[grid.Row * grid.Column];

        Heap<Field> openSet = new Heap<Field>(heapArray);
        HashSet<Field> closedSet = new HashSet<Field>();

        openSet.add(startField);

        while (openSet.size > 0)
        {
            Field current = openSet.removeFirst();
            closedSet.Add(current);

            enterField(current, Color.magenta);
           
            yield return moveToNext(current.getWorldPosition());

            if (current == endField)
            {
                //Debug.Log("A* Matrica: " + grid.gridToString());
                if(simulate) retracePath(startField, endField);
                onPathFind(Time.time - startTime, closedSet.Count);
                break;
               
            }

            foreach ( Field neighbour in grid.getNeighbors(current)) {
                
                if (!neighbour.isWalkable || closedSet.Contains(neighbour)) {
                    continue;
                } else {
                    int newMovementCostToNeighbor = current.GCost + getDistance(current, neighbour);
                    if( newMovementCostToNeighbor < neighbour.GCost || !openSet.contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbor;
                        neighbour.HCost = getDistance(neighbour, endField);
                        neighbour.Parent = current;

                        if(!openSet.contains(neighbour))
                        {
                            openSet.add(neighbour);
                        } else
                        {
                            openSet.updateItem(neighbour);
                        }
                        
                        enterField(neighbour, Color.yellow);
                        if (simulate) yield return new WaitForSeconds(0.25f);
                    }

                }
            }
        }
       if (!simulate) onPathFind(0, -1);

    }

   
}
