using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Base for all composite nodes. Composite nodes contain multiple node references
	/// and can be treated like a collection of the nodes.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context to evaluate.</typeparam>
	public abstract class CompositeTreeNode<TContextType> : TreeNode<TContextType>, IEnumerable<IContextEvaluable<TContextType>>
	{
		/// <summary>
		/// Collection of the nodes involved in the composition.
		/// </summary>
		private IEnumerable<TreeNode<TContextType>> CompositionNodes { get; }

		//TODO: Should we throw if it's empty?
		protected CompositeTreeNode(IEnumerable<TreeNode<TContextType>> compositionNodes)
		{
			if(compositionNodes == null) throw new ArgumentNullException(nameof(compositionNodes));

			CompositionNodes = compositionNodes;
		}

		/// <inheritdoc />
		IEnumerator<IContextEvaluable<TContextType>> IEnumerable<IContextEvaluable<TContextType>>.GetEnumerator()
		{
			return CompositionNodes.GetEnumerator();
		}

		/// <inheritdoc />
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)CompositionNodes).GetEnumerator();
		}
	}
}
