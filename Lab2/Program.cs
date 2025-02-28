﻿using System;

namespace Lab2
{

    class TreeNode
    {
        public int Value;
        public int Height;
        public TreeNode Left, Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = Right = null;
            Height = 1;
        }
    }

    class AVLTree
    {
        private TreeNode root; 
        private Random random = new Random();

        private int Height(TreeNode node) => node?.Height ?? 0;
        private int BalanceFactor(TreeNode node) => node == null ? 0 : Height(node.Left) - Height(node.Right);
        private TreeNode RotateRight(TreeNode y)
        {
            TreeNode x = y.Left;
            TreeNode T = x.Right;
            x.Right = y;
            y.Left = T;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            return x;
        }

        private TreeNode RotateLeft(TreeNode x)
        {
            TreeNode y = x.Right;
            TreeNode T = y.Left;
            y.Left = x;
            x.Right = T;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            return y;
        }

        private TreeNode Insert(TreeNode node, int value)
        {
            if (node == null) return new TreeNode(value);

            if (value < node.Value)
                node.Left = Insert(node.Left, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = BalanceFactor(node);

            if (balance > 1 && value < node.Left.Value)
                return RotateRight(node);
            if (balance < -1 && value > node.Right.Value)
                return RotateLeft(node);
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public void Add(int value)
        {
            root = Insert(root, value);
        }

        public void GenerateRandomNumbers(int count, int min = 1, int max = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Add(random.Next(min, max));
            }
        }

        public void InOrderTraversal(TreeNode node)
        {
            if (node == null) return;
            InOrderTraversal(node.Left);
            Console.Write(node.Value + " ");
            InOrderTraversal(node.Right);
        }

        public void PrintSorted()
        {
            Console.WriteLine("AVL tree generated by random numbers:");
            InOrderTraversal(root);
            Console.WriteLine();
        }

        public bool Search(int value)
        {
            return SearchNode(root, value) != null;
        }

        private TreeNode SearchNode(TreeNode node, int value)
        {
            if (node == null || node.Value == value)
                return node;
            if (value < node.Value)
                return SearchNode(node.Left, value);
            else
                return SearchNode(node.Right, value);
        }

    }
    public class Program
    {
        static void Main(string[] args)
        {
            AVLTree tree = new AVLTree();
            tree.GenerateRandomNumbers(10, 1, 50);
            tree.PrintSorted();
            Console.WriteLine("Searching for 44: " + (tree.Search(44) ? "Found" : "Not Found"));

        }
    }
}
