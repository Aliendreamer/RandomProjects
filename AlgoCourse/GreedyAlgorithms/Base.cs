using System;
using System.Collections.Generic;
using System.Text;

namespace GreedyAlgorithms
{
	using System.Data;
	using System.Linq;

	public class Base
	{
		public static void FractionalKnapsackProblem()
		{
			//read input

			int capacity = int.Parse(Console.ReadLine()?.Split(':', StringSplitOptions.RemoveEmptyEntries).ToArray()[1]);
			int items = int.Parse(Console.ReadLine()?.Split(':', StringSplitOptions.RemoveEmptyEntries).ToArray()[1]);
			var itemCollection = new Item[items];

			for (int i = 0; i < items; i++)
			{
				var tokens = Console.ReadLine()?.Split("->", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				var item = new Item
				{
					// ReSharper disable once PossibleNullReferenceException
					Price = tokens[0],
					Weight = tokens[1]
				};
				itemCollection[i] = item;
			}

			var totalSum = CalculateMaxSum(itemCollection.OrderByDescending(i => i.Price / i.Weight).ToArray(), capacity);
			Console.WriteLine(totalSum.ToString("f2"));
		}

		private static double CalculateMaxSum(Item[] itemCollection, int capacity)
		{
			double price = 0;
			int takenCapacity = 0;

			foreach (var item in itemCollection)
			{
				if (item.Weight + takenCapacity <= capacity)
				{
					takenCapacity += item.Weight;
					price += item.Price;
				}
				else
				{
					double avaiblespace = capacity - takenCapacity;
					var persentageTotake = Math.Round(avaiblespace / item.Weight, 2) * 100;
					double currentWeight = (item.Weight * persentageTotake) / 100;
					double currentPrice = (item.Price * persentageTotake) / 100;
					price += currentPrice;
					takenCapacity += (int)currentWeight;
				}

				if (takenCapacity == capacity)
				{
					break;
				}
			}

			return price;
		}

		public static void ProccessScheduling()
		{
			int tasksCount = int.Parse(Console.ReadLine()?.Split(':', StringSplitOptions.RemoveEmptyEntries).ToArray()[1]);
			var tasks = new List<ScheduleTask>();
			var chosenTasks = new List<ScheduleTask>();
			var indexes = new List<int>();
			for (int i = 0; i < tasksCount; i++)
			{
				var tokens = Console.ReadLine()?.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				var task = new ScheduleTask
				{
					// ReSharper disable once PossibleNullReferenceException
					Value = tokens[0],
					Step = tokens[1]
				};
				tasks.Add(task);
			}

			var maxSteps = tasks.Select(x => x.Step).Max();
			var minStep = tasks.Select(x => x.Step).Min();
			while (minStep <= maxSteps)
			{
				var nextStep = minStep + 1;
				var currentTask = tasks.Where(x => x.Step == minStep && chosenTasks.All(z => z != x))
									  .OrderByDescending(x => x.Value).FirstOrDefault()
								  ?? tasks.Where(x =>
										  x.Step == nextStep && chosenTasks.All(z => z != x))
									  .OrderByDescending(x => x.Value).FirstOrDefault();

				var nextBestTask = minStep + 1 <= maxSteps
					? tasks.Where(x => x.Step == minStep + 1).OrderByDescending(x => x.Value).First()
					: null;
				if (nextBestTask?.Value > currentTask?.Value)
				{
					var changeTask = currentTask.Value + nextBestTask.Value <
									nextBestTask.Value + tasks.Where(x => x.Step > minStep && x != nextBestTask).OrderByDescending(x => x.Value).First().Value
									? nextBestTask
									: currentTask;

					currentTask = changeTask;
				}

				var index = tasks.IndexOf(currentTask) + 1;
				chosenTasks.Add(currentTask);
				indexes.Add(index);
				minStep++;
			}

			Console.WriteLine(string.Join("->", indexes));
			Console.WriteLine($"Total value:{chosenTasks.Sum(x => x.Value)}");
		}

		public static void KnightProblem()
		{
			var size = int.Parse(Console.ReadLine());

			var board = new int[size, size];

			//fill matrix
			for (int row = 0; row < board.GetLength(0); row++)
			{
				for (int col = 0; col < board.GetLength(1); col++)
				{
					if (col == 0 && row == 0)
					{
						board[row, col] = 1;
					}
					board[row, col] = 0;
				}
			}
		}
	}

	public class ScheduleTask
	{
		public int Value { get; set; }

		public int Step { get; set; }
	}

	public class Item
	{
		public int Weight { get; set; }

		public int Price { get; set; }
	}
}