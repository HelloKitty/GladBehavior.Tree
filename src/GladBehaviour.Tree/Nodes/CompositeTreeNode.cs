using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Base for all composite nodes. Composite nodes contain multiple node references
	/// and can be treated like a collection of the nodes.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context to evaluate.</typeparam>
	public abstract class CompositeTreeNode<TContextType> : TreeNode<TContextType>
	{
		/// <summary>
		/// Collection of the nodes involved in the composition.
		/// </summary>
		protected virtual IEnumerable<TreeNode<TContextType>> CompositionNodes { get; }

		/// <inheritdoc />
		public override bool HasChildNodes { get; } = true;

		//TODO: Should we throw if it's empty?
		protected CompositeTreeNode(IEnumerable<TreeNode<TContextType>> compositionNodes)
		{
			if(compositionNodes == null) throw new ArgumentNullException(nameof(compositionNodes));

			//if we don't call ToArray then Enumerator.Reset will throw if it's an IEnumerable as many won't support Reset on their IEnumerator
			//Assuming IEnumerator is being called at some point and used directly. Iterating an array is also faster anyway, this is the best thing to do.
			CompositionNodes = !compositionNodes.GetType().IsArray || !(compositionNodes.GetType() is IList) ? compositionNodes.ToArray() : compositionNodes;
		}

		/// <summary>
		/// Only call this ctor if you are overriding the <see cref="CompositionNodes"/> property.
		/// Reserved only for internal library use.
		/// </summary>
		internal CompositeTreeNode()
		{
			
		}

		/// <inheritdoc />
		public override IEnumerator<TreeNode<TContextType>> GetEnumerator()
		{
			return CompositionNodes.GetEnumerator();
		}
	}
}
