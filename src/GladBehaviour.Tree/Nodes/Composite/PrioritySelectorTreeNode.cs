using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree selector node which is a <see cref="CompositeTreeNode{TContextType}"/> that
	/// runs the composed nodes until one is either succeeds or is running however unlike the default Selector node this node will
	/// restart at the begining of the selections on the next evaluation regardless if a node happens to be running. Meaning that the highest
	/// priority nodes, by order, will be given a chance to run instead of reentering running nodes.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class PrioritySelectorTreeNode<TContextType> : SelectorTreeNode<TContextType>
	{
		/// <inheritdoc />
		public PrioritySelectorTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(nodes)
		{

		}

		/// <inheritdoc />
		public PrioritySelectorTreeNode(params TreeNode<TContextType>[] nodes)
			: this((IEnumerable<TreeNode<TContextType>>)nodes)
		{

		}

		/// <summary>
		/// Internal ctor that allows for overriding the default continue strategy.
		/// </summary>
		/// <param name="nodes"></param>
		/// <param name="continueStrategy"></param>
		internal PrioritySelectorTreeNode(IEnumerable<TreeNode<TContextType>> nodes, ICompositeContinuationStrategy continueStrategy)
			: base(nodes, continueStrategy)
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
