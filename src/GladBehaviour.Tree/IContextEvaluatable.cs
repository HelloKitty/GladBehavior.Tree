using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Contract for Types that can evaluate a context.
	/// </summary>
	public interface IContextEvaluable<in TContextType> //it is important that allows variance
	{
		/// <summary>
		/// Evaluates a context and produces an evaluation state.
		/// </summary>
		/// <param name="context">The context to evaluate.</param>
		/// <returns>Success if the evaluation was successful or failure if it wasn't.
		/// If the evaluation requires more time to compute then it will return Running.</returns>
		GladBehaviorTreeNodeState Evaluate(TContextType context);
	}
}
