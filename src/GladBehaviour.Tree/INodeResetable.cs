using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Contract for nodes that can be reset.
	/// </summary>
	public interface INodeResetable
	{
		/// <summary>
		/// Resets the node.
		/// </summary>
		void Reset();
	}
}
