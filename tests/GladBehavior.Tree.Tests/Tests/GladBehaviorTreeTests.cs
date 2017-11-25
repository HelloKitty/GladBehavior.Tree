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
	public class GladBehaviorTreeTests
	{
		[Test]
		public void Test_Can_Call_GladBehaviorTree_Ctor()
		{
			Assert.DoesNotThrow(() => new GladBehaviorTree<int>(new TestTrueConditionNode()));
		}

		[Test]
		public void Test_Enumerator_Enumerates_All_Nodes()
		{
			//arrange
			List<TreeNode<int>> nodeList = new List<TreeNode<int>>(100);
			SequenceTreeNode<int> sequence = new SequenceTreeNode<int>(new TestSuccessNode(), new TestSuccessNode(), new TestSuccessNode(), new TestSuccessNode());
			SequenceTreeNode<int> nestedSequence = new SequenceTreeNode<int>(new LambdaConditionTreeNode<int>(c => true), new TestSuccessNode());
			SelectorTreeNode<int> selector = new SelectorTreeNode<int>(new TestFailedNode(), nestedSequence, new TestFailedNode());

			PrioritySelectorTreeNode<int> rootNode = new PrioritySelectorTreeNode<int>(selector, sequence);

			nodeList.AddRange(selector);
			nodeList.AddRange(sequence);
			nodeList.AddRange(nestedSequence);
			nodeList.Add(rootNode);
			nodeList.Add(sequence);
			nodeList.Add(selector);

			GladBehaviorTree<int> tree = new GladBehaviorTree<int>(rootNode);

			//act
			IEnumerable<TreeNode<int>> nodes = tree;

			Assert.AreEqual(nodeList.Count, nodes.Count(), $"Node lists didn't match in length.");

			//assert
			foreach(var n in nodeList)
				Assert.True(nodes.Any(tn => Object.ReferenceEquals(n, tn)), $"Encountered Node: {n} not in original node list.");
		}
	}
}
