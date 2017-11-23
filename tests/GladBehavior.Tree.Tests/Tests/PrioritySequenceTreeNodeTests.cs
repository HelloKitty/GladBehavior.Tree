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
	public class PrioritySequenceTreeNodeTests
	{
		[Test]
		public static void Test_Can_Construct_PrioritySequenceNode()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			Assert.DoesNotThrow(() => new PrioritySequenceTreeNode<int>(new TreeNode<int>[1] { node }));
		}

		[Test]
		public static void Test_Unrun_PrioritySequence_Indicates_NotRunning()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			PrioritySequenceTreeNode<int> PrioritySequence = new PrioritySequenceTreeNode<int>(new TreeNode<int>[1] { node });

			//assert
			Assert.False(PrioritySequence.isRunningNode);
			Assert.Null(PrioritySequence.RunningNode);
		}

		public static IEnumerable<int> NodeCountSource { get; } = Enumerable.Range(1, 20);

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_PrioritySequence_With_SuccessNodes_Returns_Success(int numerOfSuccessNodes)
		{
			//arrange
			List<TestSuccessNode> nodes = new List<TestSuccessNode>(numerOfSuccessNodes);
			for(int i = 0; i < numerOfSuccessNodes; i++)
				nodes.Add(new TestSuccessNode());

			PrioritySequenceTreeNode<int> PrioritySequence = new PrioritySequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = PrioritySequence.Evaluate(5);

			//assert
			Assert.False(PrioritySequence.isRunningNode);
			Assert.Null(PrioritySequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);

			foreach(TestSuccessNode node in nodes)
				Assert.AreEqual(1, node.CalledTime, $"Expected the success node to be called {1} time but was called: {node.CalledTime}s.");
		}

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_PrioritySequence_With_FailureNodes_Returns_Failure(int numerOfNodes)
		{
			//arrange
			List<TestFailedNode> nodes = new List<TestFailedNode>(numerOfNodes);
			for(int i = 0; i < numerOfNodes; i++)
				nodes.Add(new TestFailedNode());

			PrioritySequenceTreeNode<int> PrioritySequence = new PrioritySequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = PrioritySequence.Evaluate(5);

			//assert
			Assert.False(PrioritySequence.isRunningNode);
			Assert.Null(PrioritySequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);

			Assert.AreEqual(1, nodes.First().CalledTime);
			foreach(TestFailedNode node in nodes.Skip(1))
				Assert.AreEqual(0, node.CalledTime, $"Expected the success node to be called {1} time but was called: {node.CalledTime}s.");
		}

		[Test]
		public static void Test_Run_PrioritySequence_With_FailureNode_Doesnt_Continue_Evaluation_When_Reached()
		{
			//arrange
			List<TreeNode<int>> nodes = new List<TreeNode<int>>(5);
			for(int i = 0; i < 5; i++)
				nodes.Add(new TestSuccessNode());

			nodes.Insert(nodes.Count / 2 + 1, new TestFailedNode());

			PrioritySequenceTreeNode<int> PrioritySequence = new PrioritySequenceTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = PrioritySequence.Evaluate(5);

			//assert
			Assert.False(PrioritySequence.isRunningNode);
			Assert.Null(PrioritySequence.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);

			Assert.AreEqual(1, ((dynamic)nodes.First()).CalledTime);
			Assert.AreEqual(0, ((dynamic)nodes.Last()).CalledTime, $"Expected the success node to be called {1} time but was called: {((dynamic)nodes.Last()).CalledTime}s.");
		}

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public void Test_Run_PrioritySequence_With_RunningNode_DoesntReenter_Only_Running_Node(int count)
		{
			//arrange
			List<TreeNode<int>> nodes = new List<TreeNode<int>>(5);
			for(int i = 0; i < 5; i++)
				nodes.Add(new TestSuccessNode());

			TestRunningNode runningNode = new TestRunningNode();

			nodes.Add(runningNode);

			PrioritySequenceTreeNode<int> PrioritySequence = new PrioritySequenceTreeNode<int>(nodes);

			//act
			for(int i = 0; i < count; i++)
				PrioritySequence.Evaluate(5);

			//assert
			Assert.True(PrioritySequence.isRunningNode, $"The PrioritySequence should indicate that it is running.");
			Assert.NotNull(PrioritySequence.RunningNode, $"The PrioritySequence should have a non-null running node.");
			Assert.AreEqual(runningNode, PrioritySequence.RunningNode, $"The running node should be the same as the running node from construction.");

			foreach(var node in nodes)
				Assert.AreEqual(count, ((dynamic)node).CalledTime, $"Should have called all the nodes {count} many times.");
		}
	}
}
