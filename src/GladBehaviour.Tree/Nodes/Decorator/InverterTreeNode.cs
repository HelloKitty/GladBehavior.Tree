using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree inverter node which is a <see cref="DecoratorTreeNode{TContextType}"/> that
	/// runs the decorated nodes and inverts the response. Changing <see cref="GladBehaviorTreeNodeState"/> Success to Failure and Failure to Success. 
	/// Leaves Running the same.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class InverterTreeNode<TContextType> : DecoratorTreeNode<TContextType>
	{
		/// <inheritdoc />
		public InverterTreeNode(TreeNode<TContextType> node) 
			: base(node)
		{

		}

		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(TContextType context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			GladBehaviorTreeNodeState state = DecoratedNode.Evaluate(context);

			if(state == GladBehaviorTreeNodeState.Failure)
				return GladBehaviorTreeNodeState.Success;
			else if(state == GladBehaviorTreeNodeState.Success)
				return GladBehaviorTreeNodeState.Failure;

			return state;
		}

		/// <inheritdoc />
		public override void Reset()
		{
			//Just dispatch it to the decorated node.
			DecoratedNode.Reset();
		}
	}
}
