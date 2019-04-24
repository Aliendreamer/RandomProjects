namespace RecursionProblems
{
	using System;
	using System.Linq;

	class Program
	{
		static void Main()
		{
			//ReverserArray();
			var r=new Recursion<int>();
			//r.NestedLoopsRecursion(4);
			//int loopsCount = int.Parse(Console.ReadLine());
			//var loops = new int[loopsCount];
			//r.Combinations(loops,loopsCount, 0);
			var tower=new TowerOfHanoi(3);
			tower.MoveDiscs();
		}

		public static void ReverserArray()
		{
			var list = Enumerable.Range(0, 500).ToArray();

			var r = new Recursion<int>(list);
			var reversed = r.ReverseCollection();

			Console.WriteLine(string.Join(" ", reversed));
		}
	}
}
