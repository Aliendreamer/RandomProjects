namespace SearchAndSortProblems
{
	using System;
	using System.Linq;

	internal class Program
	{
		private static void Main()
		{
			var sequence = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
			var num = int.Parse(Console.ReadLine());
			var baseS = new SortBase();
			var index = baseS.GetElementWithBinarySearch(sequence, num);
			Console.WriteLine(index);
			//baseS.SortedSequenceWithBubleSort(sequence);
			//baseS.PrintCollection();
		}
	}
}