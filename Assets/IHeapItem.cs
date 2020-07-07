using System;
using System.Collections.Generic;
using UnityEngine;


public interface IHeapItem<T> : IComparable<T>
{

    int HeapIndex { get; set; }
}