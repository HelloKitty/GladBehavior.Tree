using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Base for all leaf/action/custom user-defined nodes.
	/// </summary>
	/// <typeparam name="TContextType">The type of context to evaluate.</typeparam>
	public abstract class LeafTreeNode<TContextType> : TreeNode<TContextType>
	{
		//We don't really need anything here, at least not yet. But consumers of the library
		//will be confused if they need to implement TreeNode instead of Leaf like seen
		//in most definitions of a Behavior Tree. Also, some implementation stuff may be done here
		//Though I'm unsnure what it could be. Maybe a stateful system similar to AsyncLocal stuff.

		//Inheritors can override the reset method if they have reset semantics
		/// <inheritdoc />
		public override void Reset()
		{

		}
	}
}
