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
	public class InverterNodeTests
	{
		[Test]
		public void Test_Inverted_Node_Doesnt_Throw_On_Ctor()
		{
			//arrange
			Assert.DoesNotThrow(() => new InverterTreeNode<int>(new TestRunningNode()));
		}

		[Test]
		public void Test_Inverter_Node_Changes_Success_To_Failure()
		{
			//arrange
			IContextEvaluable<int> node = new InverterTreeNode<int>(new TestSuccessNode());

			//assert
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, node.Evaluate(5));
		}

		[Test]
		public void Test_Inverter_Node_Changes_Failure_To_Success()
		{
			//arrange
			IContextEvaluable<int> node = new InverterTreeNode<int>(new TestFailedNode());

			//assert
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, node.Evaluate(5));
		}

		[Test]
		public void Test_Inverter_Node_DoesntChange_Running()
		{
			//arrange
			IContextEvaluable<int> node = new InverterTreeNode<int>(new TestRunningNode());

			//assert
			Assert.AreEqual(GladBehaviorTreeNodeState.Running, node.Evaluate(5));
		}
	}
}
