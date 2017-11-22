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
	}
}
