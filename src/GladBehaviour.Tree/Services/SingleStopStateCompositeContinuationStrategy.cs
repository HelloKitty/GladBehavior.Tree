using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// <see cref="ICompositeContinuationStrategy"/> implementation that stops only if
	/// a specified state is encountered.
	/// </summary>
	internal sealed class SingleStopStateCompositeContinuationStrategy : ICompositeContinuationStrategy
	{
		/// <summary>
		/// The only state the composite should continue with.
		/// </summary>
		private GladBehaviorTreeNodeState StopState { get; }

		public SingleStopStateCompositeContinuationStrategy(GladBehaviorTreeNodeState stopState)
		{
			if(!Enum.IsDefined(typeof(GladBehaviorTreeNodeState), stopState)) throw new ArgumentOutOfRangeException(nameof(stopState), "Value should be defined in the GladBehaviorTreeNodeState enum.");

			StopState = stopState;
		}

		/// <inheritdoc />
		public bool ShouldContinue(GladBehaviorTreeNodeState state)
		{
			return StopState != state;
		}
	}
}
