using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Mediator between the enumerator for a collection of evaluables and a composite.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	internal sealed class EvaluationEnumeratorMediator<TContextType> : IRunningEvaluatable<TContextType>, IContextEvaluable<TContextType>, IEnumerable<IContextEvaluable<TContextType>>
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

		/// <summary>
		/// Subscribable event that fires when the enumerator is reset.
		/// </summary>
		public Action OnReset { get; }

		/// <summary>
		/// Strategy that determines if the enumerator should continue based
		/// on the current state.
		/// </summary>
		private ICompositeContinuationStrategy ContinuationStrategy { get; }

		public EvaluationEnumeratorMediator(IEnumerator<IContextEvaluable<TContextType>> enumerator, ICompositeContinuationStrategy continuationStrategy)
		{
			if(enumerator == null) throw new ArgumentNullException(nameof(enumerator));
			if(continuationStrategy == null) throw new ArgumentNullException(nameof(continuationStrategy));

			AsyncNodeEnumerator = enumerator;
			ContinuationStrategy = continuationStrategy;

			//Start the enumerator
			AsyncNodeEnumerator.MoveNext();

			//Add the internal reset to the OnReset delegate
			OnReset += Reset;
		}

		private void OnFinishedState(GladBehaviorTreeNodeState state)
		{
			//If we weren't running then we should reset the iterator
			switch(state)
			{
				case GladBehaviorTreeNodeState.Success:
				case GladBehaviorTreeNodeState.Failure:
					OnReset?.Invoke();
					break;
				case GladBehaviorTreeNodeState.Running:
					break;
			}

			isRunningNode = state == GladBehaviorTreeNodeState.Running;
		}

		/// <inheritdoc />
		public void Reset()
		{
			AsyncNodeEnumerator.Reset();
			AsyncNodeEnumerator.MoveNext();
		}

		public GladBehaviorTreeNodeState Evaluate(TContextType context)
		{
			//This should only happen if we have no children
			//otherwise the collection should have been reset
			if(AsyncNodeEnumerator.Current == null)
				return GladBehaviorTreeNodeState.Success;

			GladBehaviorTreeNodeState state;
			do
			{
				state = AsyncNodeEnumerator.Current.Evaluate(context);

				//We should return if we encounter anything but the designated continue state
				if(!ContinuationStrategy.ShouldContinue(state))
					break;

			} while(AsyncNodeEnumerator.MoveNext());

			//The the finished state method
			OnFinishedState(state);

			//It's either success or failure
			return state;
		}

		/// <inheritdoc />
		public IEnumerator<IContextEvaluable<TContextType>> GetEnumerator()
		{
			return AsyncNodeEnumerator;
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return AsyncNodeEnumerator;
		}
	}
}
