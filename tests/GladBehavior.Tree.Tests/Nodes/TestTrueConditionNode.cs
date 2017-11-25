using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladBehaviour.Tree;

namespace GladBehavior.Tree.Tests
{
	public sealed class TestTrueConditionNode : ConditionTreeNode<int>
	{
		/// <inheritdoc />
		protected override bool CheckCondition(int context)
		{
			return true;
		}
	}
}
