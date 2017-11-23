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
	public sealed class SequenceTreeNode<TContextType> : CompositeTreeNode<TContextType>, IRunningEvaluatable<TContextType>
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
			: base(nodes)
		{
			EvaluationEnumerator = new EvaluationEnumeratorMediator<TContextType>(CompositionNodes.GetEnumerator(), GladBehaviorTreeNodeState.Success);
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
