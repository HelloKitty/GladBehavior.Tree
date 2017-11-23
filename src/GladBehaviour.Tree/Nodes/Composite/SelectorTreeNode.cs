using System;
using System.Collections.Generic;
using System.Text;

namespace GladBehaviour.Tree
{
	/// <summary>
	/// Implementation of the Behavior Tree selector node which is a <see cref="CompositeTreeNode{TContextType}"/> that
	/// runs the composed nodes until one is either succeeds or is running.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public class SelectorTreeNode<TContextType> : CompositeTreeNode<TContextType>
	{
		/// <inheritdoc />
		public bool isRunningNode => EvaluationEnumerator.isRunningNode;

		/// <inheritdoc />
		public IContextEvaluable<TContextType> RunningNode => EvaluationEnumerator.RunningNode;

		/// <summary>
		/// The enumeration mediator for the composite evaluables
		/// </summary>
		private EvaluationEnumeratorMediator<TContextType> EvaluationEnumerator { get; }

		public SelectorTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: base(nodes)
		{
			//Unlike the sequence node we need to continue if we encounter Failure, not success.
			EvaluationEnumerator = new EvaluationEnumeratorMediator<TContextType>(CompositionNodes.GetEnumerator(), new SingleContinueStateCompositeContinuationStrategy(GladBehaviorTreeNodeState.Failure));
		}

		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(TContextType context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			return EvaluationEnumerator.Evaluate(context);
		}

		/// <inheritdoc />
		public override void Reset()
		{
			//Propagate the reset to the enumerator
			EvaluationEnumerator.OnReset?.Invoke();
		}
	}
}
