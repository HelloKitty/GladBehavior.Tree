using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree sequence node which is a <see cref="CompositeTreeNode{TContextType}"/> that
	/// runs the composed nodes in a sequential order.
	/// </summary>
	/// <typeparam name="TContextType"></typeparam>
	public sealed class SequenceTreeNode<TContextType> : CompositeTreeNode<TContextType>, IRunningEvaluatable<TContextType>
	{
		/// <inheritdoc />
		public bool isRunningNode { get; private set; }

		/// <summary>
		/// Cache of the currently running sequence.
		/// The enumerator use facilitates the async/running potential nature of child nodes.
		/// </summary>
		private IEnumerator<IContextEvaluable<TContextType>> AsyncNodeEnumerator { get; }

		/// <inheritdoc />
		public IContextEvaluable<TContextType> RunningNode => isRunningNode ? AsyncNodeEnumerator.Current : null;

		public SequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(!nodes.GetType().IsArray || !(nodes.GetType() is IList) ? nodes.ToArray() : nodes) //if we don't call ToArray then Enumerator.Reset will throw 
		{
			AsyncNodeEnumerator = CompositionNodes.GetEnumerator();
			AsyncNodeEnumerator.MoveNext(); //start the enumerator
		}

		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(TContextType context)
		{
			//Exceptions are propgated up and handled
			//This should only happen if we have no children
			//otherwise the collection should have been reset
			if(AsyncNodeEnumerator.Current == null)
				return GladBehaviorTreeNodeState.Success;

			GladBehaviorTreeNodeState state = EvaluateEnumerator(context);
			OnFinishedState(state);
			return state;
		}

		/// <inheritdoc />
		public override void Reset()
		{
			AsyncNodeEnumerator.Reset();
			AsyncNodeEnumerator.MoveNext();
		}

		private void OnFinishedState(GladBehaviorTreeNodeState state)
		{
			//If we weren't running then we should reset the iterator
			switch(state)
			{
				case GladBehaviorTreeNodeState.Success:
				case GladBehaviorTreeNodeState.Failure:
					Reset();
					break;
				case GladBehaviorTreeNodeState.Running:
					break;
			}

			isRunningNode = state == GladBehaviorTreeNodeState.Running;
		}

		private GladBehaviorTreeNodeState EvaluateEnumerator(TContextType context)
		{
			GladBehaviorTreeNodeState state;
			do
			{
				state = AsyncNodeEnumerator.Current.Evaluate(context);

				//We should return if we encounter a running or a failure
				//Success for sequences means we should continue the sequence
				if(state != GladBehaviorTreeNodeState.Success)
					break;

			} while(AsyncNodeEnumerator.MoveNext());

			//It's either success or failure
			return state;
		}
	}
}
