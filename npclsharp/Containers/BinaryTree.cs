using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using npclsharp.Annotations;

namespace npclsharp.Containers
{
    public interface INode<T>
    {
        //TODO should this not be readonly?
        T Value { get; set; }
        INode<T> RightNode { get; set; }
        Boolean IsLeaf { get; }
    }

    public interface IParentedNode<T>
    {
        INode<T> ParentNode { get; set; }
    }
    public interface IBinaryNode<T> : INode<T>, IParentedNode<T> where T:IComparable<T>
    {
        IBinaryNode<T> LeftNode { get; set; }
        new IBinaryNode<T> RightNode { get; set; }
        new IBinaryNode<T> ParentNode { get; set; }
    }

    public class Node<T> : INode<T>
    {
        public T Value { get; set; }
        public INode<T> RightNode { get; set; }
        public virtual Boolean IsLeaf => RightNode == null;
    }

    public class ParentedNode<T> : Node<T>, IParentedNode<T>
    {
        public INode<T> ParentNode { get; set; }
    }
    public class BinaryNode<T> : ParentedNode<T>, IBinaryNode<T> where T:IComparable<T>
    {
        public BinaryNode(T value,IBinaryNode<T> parent )
        {
            Value = value;
            ParentNode = parent;
        }
        public BinaryNode(T value, IBinaryNode<T> parent, IBinaryNode<T> left, IBinaryNode<T> right  )
        {
            Value = value;
            ParentNode = parent;
            LeftNode = left;
            RightNode = RightNode;
        }
        public IBinaryNode<T> LeftNode { get; set; }
        public new IBinaryNode<T> RightNode { get; set; }
        public new IBinaryNode<T> ParentNode { get; set; }
        public override Boolean IsLeaf => RightNode == null && LeftNode == null;

    }

    public interface IBinaryTree<T> where T:IComparable<T>
    {

        IBinaryNode<T> Root { get; set; }
        void PreOrder(Action<T> action);
        void InOrder(Action<T> action);
        void PostOrder(Action<T> action);
        void Insert(T value);
        Int32 Count { get;  }
        Boolean IsEmpty { get; }
    }

    public class BinaryTree<T> : IBinaryTree<T> where T : IComparable<T>
    {
        public BinaryTree(T value)
        {
            Root = new BinaryNode<T>(value,null);
        }

        public BinaryTree()
        {
        }

        [CanBeNull]
        public IBinaryNode<T> Root { get; set; }


        public virtual void PreOrder(Action<T> action)
        {
            Pre(action, Root);
        }

        protected virtual void Pre(Action<T> action, IBinaryNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                Pre(action, node.LeftNode);
                Pre(action, node.RightNode);
            }
        }

        public virtual void InOrder(Action<T> action)
        {
            In(action, Root);
        }

        protected virtual void In(Action<T> action, IBinaryNode<T> node)
        {
            if (node != null)
            {
                In(action, node.LeftNode);
                action(node.Value);
                In(action, node.RightNode);
            }
        }

        public virtual void PostOrder(Action<T> action)
        {
            Post(action, Root);
        }

        protected virtual void Post(Action<T> action, IBinaryNode<T> node)
        {
            if (node != null)
            {
                Post(action, node.LeftNode);
                Post(action, node.RightNode);
                action(node.Value);
            }
        }

        public virtual void Insert(T value)
        {
            if (IsEmpty)
            {
                Root = new BinaryNode<T>(value,null);
                return;
            }
            IBinaryNode<T> cur = Root;
            while (true)
            {
                if (value.CompareTo(cur.Value) < 0)
                {
                    if (cur.LeftNode == null)
                    {
                        IBinaryNode<T> binaryNode = new BinaryNode<T>(value,cur.LeftNode);
                        cur.LeftNode = binaryNode;
                        break;
                    }
                    else
                    {
                        cur = cur.LeftNode;
                    }
                }
                else
                {
                    if (cur.RightNode == null)
                    {
                        IBinaryNode<T> binaryNode = new BinaryNode<T>(value,cur.RightNode);
                        cur.RightNode = binaryNode;
                        break;
                    }
                    else
                    {
                        cur = cur.RightNode;
                    }
                }
            }
        }

        public Int32 Count { get; set; }
        public Boolean IsEmpty => Root == null;
    }
}

