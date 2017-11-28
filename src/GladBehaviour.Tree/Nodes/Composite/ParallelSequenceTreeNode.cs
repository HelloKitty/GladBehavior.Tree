using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	//TODO: Test
	/// <summary>
	/// Implementation of the Behavior Tree Parallel sequence node which is a <see cref="CompositeTreeNode{TContextType}"/> that
	/// runs the composed nodes in a sequential order. Only stops when it encounters a failure.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class ParallelSequenceTreeNode<TContextType> : PrioritySequenceTreeNode<TContextType>
	{
		public ParallelSequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(nodes, new SingleStopStateCompositeContinuationStrategy(GladBehaviorTreeNodeState.Failure))
		{

		}

		public ParallelSequenceTreeNode(params TreeNode<TContextType>[] nodes)
			: this((IEnumerable<TreeNode<TContextType>>)nodes)
		{

		}
	}
}
