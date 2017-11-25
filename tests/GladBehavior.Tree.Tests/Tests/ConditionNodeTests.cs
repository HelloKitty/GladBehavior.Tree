using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladBehaviour.Tree;
using NUnit.Framework;

namespace GladBehavior.Tree.Tests
{
	[TestFixture]
	public class ConditionNodeTests
	{
		[Test]
		public void Test_Can_Call_ConditionNode_Ctor()
		{
			//arrange
			Assert.DoesNotThrow(() => new TestFalseConditionNode());
			Assert.DoesNotThrow(() => new TestTrueConditionNode());
		}

		[Test]
		public void Test_True_Condition_Returns_Success()
		{
			//arrange
			TestTrueConditionNode node = new TestTrueConditionNode();

			//act
			GladBehaviorTreeNodeState state = node.Evaluate(5);

			//assert
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);
		}

		[Test]
		public void Test_True_Condition_Returns_Failure()
		{
			//arrange
			TestFalseConditionNode node = new TestFalseConditionNode();

			//act
			GladBehaviorTreeNodeState state = node.Evaluate(5);

			//assert
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);
		}
	}
}
