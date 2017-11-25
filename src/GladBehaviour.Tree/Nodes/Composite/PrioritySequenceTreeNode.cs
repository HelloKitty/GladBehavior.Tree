using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree sequence node which is a <see cref="CompositeTreeNode{TContextType}"/> that
	/// runs the composed nodes in a sequential order but unlike the default sequence node this sequence node will
	/// restart the sequence even if a Running state was encountered last evaluation. Meaning that nodes with the highest priority, based on order, will be run
	/// instead of directly re-entering a running node.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class PrioritySequenceTreeNode<TContextType> : SequenceTreeNode<TContextType>
	{
		/// <inheritdoc />
		public PrioritySequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(nodes)
		{

		}

		/// <inheritdoc />
		public PrioritySequenceTreeNode(params TreeNode<TContextType>[] nodes)
			: this((IEnumerable<TreeNode<TContextType>>)nodes)
		{

		}

		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(TContextType context)
		{
			//Reset the sequence before evaluating so that the highest priority nodes
			//get to run instead of rentering into Running node
			Reset();
			return base.OnEvaluate(context);
		}
	}
}
