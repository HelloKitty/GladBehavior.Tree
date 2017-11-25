using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree condition node which is a <see cref="LeafTreeNode{TContextType}"/> that
	/// checks the condition against the agent/context provided. Yielding true of false meaning Success and Failure.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public abstract class ConditionTreeNode<TContextType> : LeafTreeNode<TContextType>
	{
		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(TContextType context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			bool result = CheckCondition(context);

			return result ? GladBehaviorTreeNodeState.Success : GladBehaviorTreeNodeState.Failure;
		}

		/// <summary>
		/// Verified the condition based on the provided <see cref="context"/>.
		/// Returning false means that Failure will be returned from the node.
		/// Returning true means that Success will be returned from the node.
		/// </summary>
		/// <param name="context">The context to use for checking the condition.</param>
		/// <returns>True if the condition is met. False if not.</returns>
		protected abstract bool CheckCondition(TContextType context);
	}
}
