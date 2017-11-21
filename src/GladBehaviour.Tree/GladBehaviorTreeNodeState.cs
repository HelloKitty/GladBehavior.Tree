using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Enumeration of all behavior tree node states.
	/// </summary>
	public enum GladBehaviorTreeNodeState : int
	{
		/// <summary>
		/// Indicates that a node is successfully evaluated.
		/// </summary>
		Success = 0,
		
		/// <summary>
		/// Indicates that a node is running and will require
		/// more time for evaluation.
		/// </summary>
		Running = 1,

		/// <summary>
		/// Indicates that a node failed to evaluate.
		/// </summary>
		Failure = 2
	}
}
