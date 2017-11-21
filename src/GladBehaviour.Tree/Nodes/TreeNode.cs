using System;

namespace GladBehaviour.Tree
{
	//TODO: Do we need an actual base type or is a interface enough?
	/// <summary>
	/// Base type of all nodes in the behavior tree.
	/// </summary>
	/// <typeparam name="TContextType">The type of context the node acts on.</typeparam>
	public abstract class TreeNode<TContextType> : IContextEvaluable<TContextType>
	{
		/// <inheritdoc />
		public abstract GladBehaviorTreeNodeState Evaluate(TContextType context);

		/// <summary>
		/// Internal ctor used to hide and prevent inheritance from this node
		/// from outside assemblies.
		/// </summary>
		internal TreeNode()
		{
			
		}
	}
}
