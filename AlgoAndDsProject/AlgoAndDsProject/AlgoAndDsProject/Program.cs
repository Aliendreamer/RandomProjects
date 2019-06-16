namespace AlgoAndDsProject
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Algorithms.Sorting;
	using DataStructures.Trees;

	class Program
	{
		static void Main(string[] args)
		{
		     var list = Enumerable.Range(0, 1000).ToList();
		//	 list.BubbleSortAscending(Comparer<int>.Default);
		//	 Console.WriteLine(string.Join($"{Environment.NewLine}",list));

			var tree = new RedBlackTree<int>();
			foreach (var i in list)
			{
				tree.Insert(i);
			}

			Console.WriteLine(tree.DrawTree());
		}
	}
}
