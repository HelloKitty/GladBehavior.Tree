using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Strategy for indicating if a composite should continue
	/// evaluating based on the current <see cref="GladBehaviorTreeNodeState"/>.
	/// </summary>
	public interface ICompositeContinuationStrategy
	{
		/// <summary>
		/// Indicates if a composite should continue evaluating based on the provided
		/// <see cref="state"/>.
		/// </summary>
		/// <param name="state">The current evaluated state.</param>
		/// <returns>True if the composite should continue evaluating.</returns>
		bool ShouldContinue(GladBehaviorTreeNodeState state);
	}
}
