using System;
using System.Collections;
using System.Collections.Generic;

namespace GladBehaviour.Tree
{
	//TODO: Do we need an actual base type or is a interface enough?
	/// <summary>
	/// Base type of all nodes in the behavior tree.
	/// </summary>
	/// <typeparam name="TContextType">The type of context the node acts on.</typeparam>
	public abstract class TreeNode<TContextType> : IContextEvaluable<TContextType>, IEnumerable<TreeNode<TContextType>>, INodeResetable
	{
		/// <summary>
		/// Indicates if the node has children.
		/// </summary>
		public abstract bool HasChildNodes { get; }

		/// <summary>
		/// Internal ctor used to hide and prevent inheritance from this node
		/// from outside assemblies.
		/// </summary>
		internal TreeNode()
		{
			
		}

		//Inheriting classes should implement this for evaluation
		/// <inheritdoc />
		protected abstract GladBehaviorTreeNodeState OnEvaluate(TContextType context);

		/// <inheritdoc />
		public GladBehaviorTreeNodeState Evaluate(TContextType context)
		{
			//We wrap the call to OnEvaluate in an exception block
			//so that on exceptions we don't leave the tree node in an invalid state
			//that could cause significant problems on future evaluations
			try
			{
				return OnEvaluate(context);
			}
			catch(Exception)
			{
				Reset();
				throw;
			}
		}

		//Subnodes should implement reseting
		//It is called by the base node in cases where exceptions are encountered.
		/// <inheritdoc />
		public abstract void Reset();

		/// <inheritdoc />
		public abstract IEnumerator<TreeNode<TContextType>> GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
