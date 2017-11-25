using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree condition node which is a <see cref="LeafTreeNode{TContextType}"/> that
	/// checks provided delegate the condition against the agent/context provided. Yielding true of false meaning Success and Failure.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class LambdaConditionTreeNode<TContextType> : ConditionTreeNode<TContextType>
	{
		/// <summary>
		/// The delegate func for the condition.
		/// </summary>
		private Func<TContextType, bool> ConditionLambda { get; }

		/// <summary>
		/// Creates a new <see cref="LambdaConditionTreeNode{TContextType}"/> that will uses the provided <see cref="ConditionLambda"/>
		/// as the condition to check.
		/// </summary>
		/// <param name="conditionLambda">The delegate to use as the condition.</param>
		public LambdaConditionTreeNode(Func<TContextType, bool> conditionLambda)
		{
			if(conditionLambda == null) throw new ArgumentNullException(nameof(conditionLambda));

			ConditionLambda = conditionLambda;
		}

		/// <inheritdoc />
		protected override bool CheckCondition(TContextType context)
		{
			//We just call the lambda which will produce the result
			return ConditionLambda.Invoke(context);
		}
	}
}
