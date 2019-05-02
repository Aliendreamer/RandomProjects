namespace CombinatorialProblems
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PermutationHolder
	{

		public PermutationHolder()
		{
			this.Result=new HashSet<string>();
		}
		private HashSet<string> Result { get; set; }

		public void IterativeCombinationsWithoutRepetition()
		{
			var arr = Console.ReadLine()
							?.Split()
							.Select(char.Parse)
							.ToArray();

			int n = int.Parse(Console.ReadLine());
			
			for (int i = 0; i < arr.Length; i++)
			{
				var current = arr[i];
				for (int k = i+1; k < arr.Length; k++)				
				{
				 var concated = current +" " + string.Join(" ", arr.Skip(k).Take(n - 1));
				 if (concated.Length < n)
				 {
					 continue;
				 }
				 Result.Add(concated);
				}
			}
			Console.WriteLine(string.Join(Environment.NewLine,this.Result));
		}


		public IEnumerable<String> CombinationsWithRepition(IEnumerable<int> input, int length)
		{
			if (length <= 0)
				yield return "";
			else
			{
				foreach (var i in input)
				foreach (var c in CombinationsWithRepition(input, length - 1))
					yield return i.ToString() + c;
			}
		}
	}
}
