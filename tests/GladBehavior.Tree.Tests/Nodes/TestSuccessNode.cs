using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladBehaviour.Tree;

namespace GladBehavior.Tree
{
	public sealed class TestSuccessNode : LeafTreeNode<int>
	{
		public int CalledTime { get; private set; }

		/// <inheritdoc />
		protected override GladBehaviorTreeNodeState OnEvaluate(int context)
		{
			CalledTime++;
			return GladBehaviorTreeNodeState.Success;
		}

		/// <inheritdoc />
		public override void Reset()
		{

		}
	}
}
