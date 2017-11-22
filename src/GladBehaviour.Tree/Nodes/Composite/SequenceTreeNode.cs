using System;
using System.Collections.Generic;
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
		public bool isRunningNode => RunningNode != null;

		/// <summary>
		/// Cache of the currently running sequence.
		/// The enumerator use facilitates the async/running potential nature of child nodes.
		/// </summary>
		private IEnumerator<IContextEvaluable<TContextType>> AsyncNodeEnumerator { get; }

		/// <inheritdoc />
		public IContextEvaluable<TContextType> RunningNode => AsyncNodeEnumerator.Current;

		public SequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(nodes)
		{
			AsyncNodeEnumerator = CompositionNodes.GetEnumerator();
		}

		/// <inheritdoc />
		public override GladBehaviorTreeNodeState Evaluate(TContextType context)
		{
			//The idea here is if isRunningNode is false we're either 1 element before the start
			//or its at the end but since we know Enumerator.Reset() was called then it means it is at the start
			//If it's not at the start then isRunningNode will be true and we'll be in the middle running and shouldn't call MoveNext to start
			//because we need to re-enter the running node
			if(!isRunningNode)
				AsyncNodeEnumerator.MoveNext();

			//If there are nodes this will occur and we should treat it as a success
			if(!isRunningNode)
				return GladBehaviorTreeNodeState.Success;

			GladBehaviorTreeNodeState state = EvaluateEnumerator(context);
			OnFinishedState(state);

			return state;
		}

		private void OnFinishedState(GladBehaviorTreeNodeState state)
		{
			//If we weren't running then we should reset the iterator
			switch(state)
			{
				case GladBehaviorTreeNodeState.Success:
				case GladBehaviorTreeNodeState.Failure:
					AsyncNodeEnumerator.Reset();
					break;
			}
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
