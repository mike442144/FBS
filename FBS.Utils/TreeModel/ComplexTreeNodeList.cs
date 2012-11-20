using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Utils.TreeModel
{
    public class ComplexTreeNodeList<T>:List<ComplexTreeNode<T>> where T:ComplexTreeNode<T>
    {
        public T Parent;

        public ComplexTreeNodeList(ComplexTreeNode<T> Parent)
        {
            this.Parent = (T)Parent;
        }

        public T Add(T Node)
        {
            base.Add(Node);
            Node.Parent = Parent;
            return Node;
        }

        public override string ToString()
        {
            return "Count=" + Count.ToString();
        }
    }
}
