using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Contract for node's that support long running evaluation.
	/// </summary>
	/// <typeparam name="TContextType">The context type to be evaluated.</typeparam>
	public interface IRunningEvaluatable<in TContextType>
	{
		/// <summary>
		/// Indicates if a node is currently running.
		/// Meaning that the behavior of this evaluation will be circumvenveted
		/// by a longer-term running node.
		/// </summary>
		bool isRunningNode { get; }

		/// <summary>
		/// The currently running evaluation.
		/// It's possible that evaluation could take multiple evaluation calls
		/// to complete. This will be null if <see cref="isRunningNode"/> is false.
		/// </summary>
		IContextEvaluable<TContextType> RunningNode { get; }
	}
}
