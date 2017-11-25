using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladBehaviour.Tree.Enumerators
{
	/// <summary>
	/// Enumerable that can enumerate from a <see cref="EnumerationRoot"/> node through all child nodes.
	/// This does NOT follow tree traversal semantics but instead yields a enumerable of all nodes.
	/// </summary>
	/// <typeparam name="TContextType">The type of the context.</typeparam>
	public sealed class TreeEnumerator<TContextType> : IEnumerable<TreeNode<TContextType>>
	{
		/// <summary>
		/// The root node to start the enumeration at.
		/// </summary>
		private TreeNode<TContextType> EnumerationRoot { get; }

		/// <inheritdoc />
		public TreeEnumerator(TreeNode<TContextType> enumerationRoot)
		{
			if(enumerationRoot == null) throw new ArgumentNullException(nameof(enumerationRoot));

			EnumerationRoot = enumerationRoot;
		}
		
		//TODO: Can we improve this performance?
		/// <summary>
		/// Recursive tree node enumerator. Will NOT follow the node semantics for traversal but instead
		/// will yield each node in a collection for iteration.
		/// </summary>
		/// <param name="nodes">The node to recursively enumerate.</param>
		/// <returns>Enumerator for all the nodes that are a child of the root node including the root node.</returns>
		private IEnumerator<TreeNode<TContextType>> TreeEnumeration(IEnumerable<TreeNode<TContextType>> nodes)
		{
			foreach(var n in nodes)
			{
				//Yield the node
				yield return n;

				if(n.HasChildNodes)
					//This handles the case even if the root node has no children. Because n2 will be n and will not continue
					//We should traverse subnodes if the subnode isn't equal to the actual node (base case that means it is childless)
					foreach(var n2 in n)
					{
						//Yield the node, if it's composite this will be the composite node
						yield return n2;

						//Recur if the first node isn't itself
						if(n2.HasChildNodes)
						{
							//Recursively enumerate on each subnode with children
							//We will yield each one and if they have children it will recursively yield them too
							IEnumerator<TreeNode<TContextType>> recursiveEnumerator = TreeEnumeration(n2);

							while(recursiveEnumerator.MoveNext())
								yield return recursiveEnumerator.Current;
						}
					}
					
			}
		}

		//We have to have some enumeration logic in here
		//So that we also return the root node.
		/// <inheritdoc />
		public IEnumerator<TreeNode<TContextType>> GetEnumerator()
		{
			//Return the root node
			yield return EnumerationRoot;

			//This checks to see if it has children or if it's just an end node
			if(EnumerationRoot.HasChildNodes)
			{
				IEnumerator<TreeNode<TContextType>> enumerator = TreeEnumeration(EnumerationRoot);

				//We should only use the complex recursive enumerator if the enumeration root has children
				while(enumerator.MoveNext())
					yield return enumerator.Current;
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
