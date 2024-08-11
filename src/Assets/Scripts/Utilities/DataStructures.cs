using System.Collections.Generic;
using UnityEngine;

public static class DataStructures
{
    // 1. Grid Structure
    public class Grid<T>
    {
        private T[,] gridArray;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            gridArray = new T[width, height];
        }

        public void SetValue(int x, int y, T value)
        {
            if (IsInBounds(x, y))
            {
                gridArray[x, y] = value;
            }
        }

        public T GetValue(int x, int y)
        {
            if (IsInBounds(x, y))
            {
                return gridArray[x, y];
            }
            return default(T);
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public void ClearGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    gridArray[x, y] = default(T);
                }
            }
        }
    }

    // 2. Node for a Binary Tree Structure
    public class TreeNode<T>
    {
        public T Value;
        public TreeNode<T> Left;
        public TreeNode<T> Right;

        public TreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    // 3. Binary Tree Structure
    public class BinaryTree<T>
    {
        public TreeNode<T> Root;

        public BinaryTree(T rootValue)
        {
            Root = new TreeNode<T>(rootValue);
        }

        public void Insert(T value)
        {
            InsertRecursive(Root, value);
        }

        private void InsertRecursive(TreeNode<T> node, T value)
        {
            if (Comparer<T>.Default.Compare(value, node.Value) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(value);
                }
                else
                {
                    InsertRecursive(node.Left, value);
                }
            }
            else
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode<T>(value);
                }
                else
                {
                    InsertRecursive(node.Right, value);
                }
            }
        }

        public bool Contains(T value)
        {
            return ContainsRecursive(Root, value);
        }

        private bool ContainsRecursive(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                return false;
            }

            if (Comparer<T>.Default.Compare(value, node.Value) == 0)
            {
                return true;
            }
            else if (Comparer<T>.Default.Compare(value, node.Value) < 0)
            {
                return ContainsRecursive(node.Left, value);
            }
            else
            {
                return ContainsRecursive(node.Right, value);
            }
        }
    }

    // 4. Node for a Linked List Structure
    public class LinkedListNode<T>
    {
        public T Value;
        public LinkedListNode<T> Next;

        public LinkedListNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    // 5. Linked List Structure
    public class LinkedList<T>
    {
        public LinkedListNode<T> Head;

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new LinkedListNode<T>(value);
            }
            else
            {
                LinkedListNode<T> current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new LinkedListNode<T>(value);
            }
        }

        public bool Contains(T value)
        {
            LinkedListNode<T> current = Head;
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void Remove(T value)
        {
            if (Head == null)
            {
                return;
            }

            if (EqualityComparer<T>.Default.Equals(Head.Value, value))
            {
                Head = Head.Next;
                return;
            }

            LinkedListNode<T> current = Head;
            while (current.Next != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Next.Value, value))
                {
                    current.Next = current.Next.Next;
                    return;
                }
                current = current.Next;
            }
        }
    }

    // 6. Stack Structure
    public class Stack<T>
    {
        private List<T> elements = new List<T>();

        public void Push(T item)
        {
            elements.Add(item);
        }

        public T Pop()
        {
            if (elements.Count == 0)
            {
                throw new System.InvalidOperationException("Stack is empty.");
            }

            T item = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            return item;
        }

        public T Peek()
        {
            if (elements.Count == 0)
            {
                throw new System.InvalidOperationException("Stack is empty.");
            }

            return elements[elements.Count - 1];
        }

        public int Count()
        {
            return elements.Count;
        }
    }

    // 7. Queue Structure
    public class Queue<T>
    {
        private List<T> elements = new List<T>();

        public void Enqueue(T item)
        {
            elements.Add(item);
        }

        public T Dequeue()
        {
            if (elements.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            T item = elements[0];
            elements.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (elements.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            return elements[0];
        }

        public int Count()
        {
            return elements.Count;
        }
    }
}