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
			r.NestedLoopsRecursion(4);

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
