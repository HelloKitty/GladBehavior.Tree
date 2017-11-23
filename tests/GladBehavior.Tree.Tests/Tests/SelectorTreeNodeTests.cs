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
	public class SelectorTreeNodeTests
	{
		[Test]
		public static void Test_Can_Construct_SelectorNode()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			Assert.DoesNotThrow(() => new SelectorTreeNode<int>(new TreeNode<int>[1] { node }));
		}

		[Test]
		public static void Test_Unrun_Selector_Indicates_NotRunning()
		{
			//arrange
			TestSuccessNode node = new TestSuccessNode();
			SelectorTreeNode<int> Selector = new SelectorTreeNode<int>(new TreeNode<int>[1] { node });

			//assert
			Assert.False(Selector.isRunningNode);
			Assert.Null(Selector.RunningNode);
		}

		public static IEnumerable<int> NodeCountSource { get; } = Enumerable.Range(1, 20);

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_Selector_With_SuccessNodes_Returns_Success(int numerOfSuccessNodes)
		{
			//arrange
			List<TestSuccessNode> nodes = new List<TestSuccessNode>(numerOfSuccessNodes);
			for(int i = 0; i < numerOfSuccessNodes; i++)
				nodes.Add(new TestSuccessNode());

			SelectorTreeNode<int> Selector = new SelectorTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = Selector.Evaluate(5);

			//assert
			Assert.False(Selector.isRunningNode);
			Assert.Null(Selector.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);

			Assert.AreEqual(1, nodes.First().CalledTime, $"Expected the success node to be called {1} time but was called: {nodes.First().CalledTime}s.");
		}

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public static void Test_Run_Selector_With_FailureNodes_Returns_Failure(int numerOfNodes)
		{
			//arrange
			List<TestFailedNode> nodes = new List<TestFailedNode>(numerOfNodes);
			for(int i = 0; i < numerOfNodes; i++)
				nodes.Add(new TestFailedNode());

			SelectorTreeNode<int> Selector = new SelectorTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = Selector.Evaluate(5);

			//assert
			Assert.False(Selector.isRunningNode);
			Assert.Null(Selector.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Failure, state);

			foreach(TestFailedNode node in nodes)
				Assert.AreEqual(1, node.CalledTime, $"Expected the failure node to be called {1} time each but was called: {node.CalledTime}s.");
		}

		[Test]
		public static void Test_Run_Selector_With_FailureNode_Doesnt_Continue_Evaluation_When_Reached_Success_Returns_Sucess()
		{
			//arrange
			List<TreeNode<int>> nodes = new List<TreeNode<int>>(5);
			for(int i = 0; i < 5; i++)
				nodes.Add(new TestFailedNode());

			nodes.Insert(nodes.Count / 2 + 1, new TestSuccessNode());

			SelectorTreeNode<int> Selector = new SelectorTreeNode<int>(nodes);

			//act
			GladBehaviorTreeNodeState state = Selector.Evaluate(5);

			//assert
			Assert.False(Selector.isRunningNode);
			Assert.Null(Selector.RunningNode);
			Assert.AreEqual(GladBehaviorTreeNodeState.Success, state);

			Assert.AreEqual(1, ((dynamic)nodes.First()).CalledTime);
			Assert.AreEqual(0, ((dynamic)nodes.Last()).CalledTime, $"Expected the success node to be called {1} time but was called: {((dynamic)nodes.Last()).CalledTime}s.");
		}

		[Test]
		[TestCaseSource(nameof(NodeCountSource))]
		public void Test_Run_Selector_With_RunningNode_Reenter_Only_Running_Node(int count)
		{
			//arrange
			List<TreeNode<int>> nodes = new List<TreeNode<int>>(5);
			for(int i = 0; i < 5; i++)
				nodes.Add(new TestFailedNode());

			TestRunningNode runningNode = new TestRunningNode();

			nodes.Add(runningNode);

			SelectorTreeNode<int> sequence = new SelectorTreeNode<int>(nodes);

			//act
			for(int i = 0; i < count; i++)
				sequence.Evaluate(5);

			//assert
			Assert.True(sequence.isRunningNode, $"The sequence should indicate that it is running.");
			Assert.NotNull(sequence.RunningNode, $"The sequence should have a non-null running node.");
			Assert.AreEqual(runningNode, sequence.RunningNode, $"The running node should be the same as the running node from construction.");

			foreach(var node in nodes.AsEnumerable().Reverse().Skip(1).Reverse())
				Assert.AreEqual(1, ((dynamic)node).CalledTime, $"Should only have called the non-running nodes once.");

			Assert.AreEqual(count, ((dynamic)nodes.Last()).CalledTime, $"The last node (running node) should have been called every time the evaluation occured.");
		}
	}
}
