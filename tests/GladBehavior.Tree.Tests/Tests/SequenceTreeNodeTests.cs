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
	public class SequenceTreeNodeTests
	{
		[Test]
		public static void Test_Can_Construct_SequenceNode()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			Assert.DoesNotThrow(() => new SequenceTreeNode<int>(new TreeNode<int>[1] { node }));
		}

		[Test]
		public static void Test_Unrun_Sequence_Indicates_NotRunning()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(new TreeNode<int>[1] { node });

			//assert
			Assert.False(sequence.isRunningNode);
			Assert.Null(sequence.RunningNode);
		}

		public static IEnumerable<int> NodeCountSource { get; } = Enumerable.Range(1, 1000);

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_Sequence_With_SuccessNodes_Returns_Success(int numerOfSuccessNodes)
		{
			//arrange
			IEnumerable<TestSuccessNode> nodes = Enumerable.Repeat(new TestSuccessNode(), numerOfSuccessNodes);
			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(nodes.ToList());

			//act
			GladBehaviorTreeNodeState state = sequence.Evaluate(5);

			//assert
			Assert.False(sequence.isRunningNode);
			Assert.Null(sequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);
		}
	}
}
