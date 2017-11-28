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
			: this(nodes, new SingleContinueStateCompositeContinuationStrategy(GladBehaviorTreeNodeState.Failure))
		{

		}

		public SelectorTreeNode(params TreeNode<TContextType>[] nodes)
			: this((IEnumerable<TreeNode<TContextType>>)nodes)
		{

		}

		/// <summary>
		/// Internal ctor that allows for overriding the default continue strategy.
		/// </summary>
		/// <param name="nodes"></param>
		/// <param name="continueStrategy"></param>
		internal SelectorTreeNode(IEnumerable<TreeNode<TContextType>> nodes, ICompositeContinuationStrategy continueStrategy)
			: base(nodes)
		{
			if(continueStrategy == null) throw new ArgumentNullException(nameof(continueStrategy));

			EvaluationEnumerator = new EvaluationEnumeratorMediator<TContextType>(CompositionNodes.GetEnumerator(), continueStrategy);
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
