using System;

namespace CombinatorialProblems
{
	using System.Collections.Generic;
	using System.Linq;

	class Program
	{
		static void Main()
		{
			//var holder=new AlgoHolder();
			//holder.Iterations();
			//var h=new PermutationHolder();
			//h.IterativeCombinationsWithoutRepetition();
			//var nums = Console.ReadLine()?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
			//Console.WriteLine(string.Join(" ",h.CombinationsWithRepition(nums,3)));
			//   var snakeObj=new Snake();
			//snakeObj.n = int.Parse(Console.ReadLine());
			//Stack<Snake.Cell> snake = new Stack<Snake.Cell>();
			//List<char> directions = new List<char>();

			//snakeObj.GenerateSnake(new Snake.Cell { Row = 0, Column = 0 }, 'S', snake, directions);

			//Console.WriteLine("Snakes count = {0}",snakeObj.snakesCount);

				
				int[] sticks = Console.ReadLine().Split().Select(int.Parse).ToArray();
				int count =CubeProblem.NumberOfCubes(sticks);
				Console.WriteLine(count);
			
		}
	}
}
