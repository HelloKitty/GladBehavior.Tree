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

		public static IEnumerable<int> NodeCountSource { get; } = Enumerable.Range(1, 20);

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_Sequence_With_SuccessNodes_Returns_Success(int numerOfSuccessNodes)
		{
			//arrange
			List<TestSuccessNode> nodes = new List<TestSuccessNode>(numerOfSuccessNodes);
			for(int i = 0; i < numerOfSuccessNodes; i++)
				nodes.Add(new TestSuccessNode());

			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = sequence.Evaluate(5);

			//assert
			Assert.False(sequence.isRunningNode);
			Assert.Null(sequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);

			foreach(TestSuccessNode node in nodes)
				Assert.AreEqual(1, node.CalledTime, $"Expected the success node to be called {1} time but was called: {node.CalledTime}s.");
		}

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_Sequence_With_FailureNodes_Returns_Failure(int numerOfNodes)
		{
			//arrange
			List<TestFailedNode> nodes = new List<TestFailedNode>(numerOfNodes);
			for(int i = 0; i < numerOfNodes; i++)
				nodes.Add(new TestFailedNode());

			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = sequence.Evaluate(5);

			//assert
			Assert.False(sequence.isRunningNode);
			Assert.Null(sequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);

			Assert.AreEqual(1, nodes.First().CalledTime);
			foreach(TestFailedNode node in nodes.Skip(1))
				Assert.AreEqual(0, node.CalledTime, $"Expected the success node to be called {1} time but was called: {node.CalledTime}s.");
		}

		[Test]
		public static void Test_Run_Sequence_With_FailureNode_Doesnt_Continue_Evaluation_When_Reached()
		{
			//arrange
			List<TreeNode<int>> nodes = new List<TreeNode<int>>(5);
			for(int i = 0; i < 5; i++)
				nodes.Add(new TestSuccessNode());

			nodes.Insert(nodes.Count / 2 + 1, new TestFailedNode());

			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = sequence.Evaluate(5);

			//assert
			Assert.False(sequence.isRunningNode);
			Assert.Null(sequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);

			Assert.AreEqual(1, ((dynamic)nodes.First()).CalledTime);
			Assert.AreEqual(0, ((dynamic)nodes.Last()).CalledTime, $"Expected the success node to be called {1} time but was called: {((dynamic)nodes.Last()).CalledTime}s.");
		}
	}
}
