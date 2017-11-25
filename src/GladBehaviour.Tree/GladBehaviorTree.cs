using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GladBehaviour.Tree.Enumerators;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Behavior Tree (BT) collection Type. Implements <see cref="IContextEvaluable{TContextType}"/> which will handle
	/// tree traveseral. Exposes the nodes as a flat collection if needed.
	/// </summary>
	/// <typeparam name="TContextType">The Type of agent/context.</typeparam>
	public class GladBehaviorTree<TContextType> : IEnumerable<TreeNode<TContextType>>, IContextEvaluable<TContextType>
	{
		/// <summary>
		/// The root node of the tree.
		/// </summary>
		protected TreeNode<TContextType> RootNode { get; }

		/// <inheritdoc />
		public GladBehaviorTree(TreeNode<TContextType> rootNode)
		{
			if(rootNode == null) throw new ArgumentNullException(nameof(rootNode));

			RootNode = rootNode;
		}

		/// <inheritdoc />
		public GladBehaviorTreeNodeState Evaluate(TContextType context)
		{
			//TODO: Should we catch and reset?
			return RootNode.Evaluate(context);
		}

		/// <inheritdoc />
		public IEnumerator<TreeNode<TContextType>> GetEnumerator()
		{
			return new TreeEnumerator<TContextType>(RootNode).GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
