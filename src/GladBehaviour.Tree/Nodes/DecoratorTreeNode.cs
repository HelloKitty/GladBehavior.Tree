using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Base for all decorator nodes. Decorators are a special case of
	/// composite nodes that only have 1 element.
	/// </summary>
	/// <typeparam name="TContextType"></typeparam>
	public abstract class DecoratorTreeNode<TContextType> : CompositeTreeNode<TContextType>
	{
		/// <inheritdoc />
		protected override IEnumerable<TreeNode<TContextType>> CompositionNodes => _CompositionNodes;

		/// <summary>
		/// The decorated node. It is stored in a single element array to
		/// allow for efficient enumeration in the event of <see cref="CompositeTreeNode{TContextType}"/>'s
		/// enumeration.
		/// </summary>
		private TreeNode<TContextType>[] _CompositionNodes { get; }

		/// <summary>
		/// The decorated node.
		/// </summary>
		protected TreeNode<TContextType> DecoratedNode => _CompositionNodes[0];

		/// <inheritdoc />
		protected DecoratorTreeNode(TreeNode<TContextType> node) 
			: base()
		{
			if(node == null) throw new ArgumentNullException(nameof(node));

			_CompositionNodes = new TreeNode<TContextType>[1] { node };
		}

		/// <inheritdoc />
		public override IEnumerator<TreeNode<TContextType>> GetEnumerator()
		{
			//TODO: Is there a performance cost associated with AsEnumerable? I assume no?
			return _CompositionNodes.AsEnumerable().GetEnumerator();
		}
	}
}
