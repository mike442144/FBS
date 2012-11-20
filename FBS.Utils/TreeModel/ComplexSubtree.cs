using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Utils.TreeModel
{
    public class ComplexSubtree<T>:ComplexTreeNode<T> where T:ComplexTreeNode<T>
    {
        public ComplexSubtree() { }
    }
}
