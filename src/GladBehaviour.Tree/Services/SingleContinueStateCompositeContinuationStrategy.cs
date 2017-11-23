using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// <see cref="ICompositeContinuationStrategy"/> implementation that continues only if
	/// a specified state is encountered.
	/// </summary>
	public sealed class SingleContinueStateCompositeContinuationStrategy : ICompositeContinuationStrategy
	{
		/// <summary>
		/// The only state the composite should continue with.
		/// </summary>
		private GladBehaviorTreeNodeState ContinueState { get; }

		public SingleContinueStateCompositeContinuationStrategy(GladBehaviorTreeNodeState continueState)
		{
			if(!Enum.IsDefined(typeof(GladBehaviorTreeNodeState), continueState)) throw new ArgumentOutOfRangeException(nameof(continueState), "Value should be defined in the GladBehaviorTreeNodeState enum.");

			ContinueState = continueState;
		}

		/// <inheritdoc />
		public bool ShouldContinue(GladBehaviorTreeNodeState state)
		{
			return ContinueState == state;
		}
	}
}
