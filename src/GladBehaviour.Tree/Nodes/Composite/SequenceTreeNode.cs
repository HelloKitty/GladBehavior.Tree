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
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public class SequenceTreeNode<TContextType> : CompositeTreeNode<TContextType>, IRunningEvaluatable<TContextType>
	{
		/// <inheritdoc />
		public bool isRunningNode => EvaluationEnumerator.isRunningNode;

		/// <inheritdoc />
		public IContextEvaluable<TContextType> RunningNode => EvaluationEnumerator.RunningNode;

		/// <summary>
		/// The enumeration mediator for the composite evaluables
		/// </summary>
		private EvaluationEnumeratorMediator<TContextType> EvaluationEnumerator { get; }

		public SequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes)
			: this(nodes, new SingleContinueStateCompositeContinuationStrategy(GladBehaviorTreeNodeState.Success))
		{

		}

		public SequenceTreeNode(params TreeNode<TContextType>[] nodes)
			: this((IEnumerable<TreeNode<TContextType>>)nodes)
		{

		}

		/// <summary>
		/// Internal ctor that allows for overriding the default continue strategy.
		/// </summary>
		/// <param name="nodes"></param>
		/// <param name="continueStrategy"></param>
		internal SequenceTreeNode(IEnumerable<TreeNode<TContextType>> nodes, ICompositeContinuationStrategy continueStrategy)
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
