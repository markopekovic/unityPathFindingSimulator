using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MAIN RULE :  parent must be less then both children!

public class Heap<T> where T : IHeapItem<T>
{ //binary tree

    private T[] items;
    private int currentItemCount; // point on empty element

    public int size {get { return currentItemCount;}}

    public Heap(T[] items)
    {
        this.items = items;
        this.currentItemCount = 0;
    }

    public void add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        sortUp(item);
        ++currentItemCount;
    }

    public T removeFirst()
    {
        T firstItem = items[0];
        --currentItemCount;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        sortDown(items[0]);
        return firstItem;
    }

    public void updateItem(T item)
    {
        sortUp(item);
    }

    public bool contains(T item)
    {
        //return item.equals(items[item.getHeapIndex()]);
        return Equals(items[item.HeapIndex], item);
    }

    private void sortUp(T item)
    {
        while (true)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0 ) {
                swap(item, parentItem);
            } else {
                break;
            }
        } 
    }

    void sortDown(T item)
    {
        while(true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

    }


    void swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }


}

