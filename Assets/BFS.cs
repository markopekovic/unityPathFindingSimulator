using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : PathFindingAlg
{
    public BFS() :base()
    {
        name = "BFS";
        Debug.Log("BFS ID = " + id);
    }

    public override IEnumerator calculate(Grid grid)
    {
        Field startField = grid.getStartField();
        Field endField = grid.getEndField();

        Queue<Field> openQueue = new Queue<Field>(); // For DFS use Stack
        HashSet<Field> exploredFields = new HashSet<Field>();

        openQueue.Enqueue(startField);
        float startTime = Time.time;
        while (openQueue.Count > 0)
        {
            Field current = openQueue.Dequeue();
            exploredFields.Add(current);
            //current.changeFieldColorTo(id, Color.cyan);
            enterField(current, Color.cyan);

            yield return moveToNext(current.getWorldPosition());
            if (current == endField)
            {
                retracePath(startField, endField);
                onPathFind(Time.time - startTime, exploredFields.Count);
                break;
            }

            foreach (Field neighbour in grid.getNeighbors(current))
            {
                if (!neighbour.isWalkable || exploredFields.Contains(neighbour))
                {
                    continue;
                }
                else
                {
                    //mark this node as explored
                    exploredFields.Add(neighbour);

                    neighbour.Parent = current;
                    //add this to the queue of fields to examine
                    openQueue.Enqueue(neighbour);
                    //neighbour.changeFieldColorTo(id, Color.gray);
                    enterField(neighbour, Color.gray);
                    yield return new WaitForSeconds(0.25f);
                }
            }

        }
    }
}
