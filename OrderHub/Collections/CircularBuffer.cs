using System;
using System.Collections;
using System.Collections.Generic;

namespace OrderHub.Collections;

public class CircularBuffer<T> : IEnumerable<T>
{
    private readonly T[] buffer;
    private int next = 0; 
    private int size = 0; 
    private readonly int capacity;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Ёмкость должна быть больше нуля.");

        this.capacity = capacity;
        buffer = new T[capacity];
    }

    public int Count => size;
    public bool IsFull => size == capacity;

    public void Add(T item)
    {
        buffer[next] = item;               
        next = (next + 1) % capacity;     

        if (size < capacity)
            size++;                      
    }

    public IEnumerator<T> GetEnumerator()
    {
        int start = (next - size + capacity) % capacity;

        for (int i = 0; i < size; i++)
            yield return buffer[(start + i) % capacity];
    }

    public IEnumerable<T> Reverse()
    {
        int index = next;

        for (int i = 0; i < size; i++)
        {
            index = (index - 1 + capacity) % capacity;
            yield return buffer[index];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}