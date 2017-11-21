using System;
using System.Collections.Generic;
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
		protected override IEnumerable<TreeNode<TContextType>> CompositionNodes => DecoratedElement;

		/// <summary>
		/// The decorated node. It is stored in a single element array to
		/// allow for efficient enumeration in the event of <see cref="CompositeTreeNode{TContextType}"/>'s
		/// enumeration.
		/// </summary>
		private TreeNode<TContextType>[] DecoratedElement { get; }

		/// <inheritdoc />
		protected DecoratorTreeNode(TreeNode<TContextType> node) 
			: base()
		{
			if(node == null) throw new ArgumentNullException(nameof(node));

			DecoratedElement = new TreeNode<TContextType>[1] { node };
		}
	}
}
