namespace GreedyAlgorithms
{
	using System;
	using System.Collections.Generic;
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
			int count = int.Parse(Console.ReadLine());
			List<Cell> board = new List<Cell>();

			for (byte i = 1; i <= count; i++)
			{
				for (byte j = 1; j <= count; j++)
				{
					board.Add(new Cell { Row = i, Col = j });
				}
			}
			int counter = 1;
			var currentCell = board[0];
			currentCell.IsVisited = true;
			currentCell.TurnVisited = counter++;

			while (board.Any(c => !c.IsVisited))
			{
				currentCell = SelectNextCell(currentCell, board);
				currentCell.IsVisited = true;
				currentCell.TurnVisited = counter++;
			}

			PrintBoard(count, board);
		}

		public static void EgyptianFractions()
		{
			// ReSharper disable once PossibleNullReferenceException
			int[] tokens = Console.ReadLine().Split('/').Select(int.Parse).ToArray();
			int p = tokens[0];
			int q = tokens[1];

			if (p >= q)
			{
				Console.WriteLine("Error (fraction is equal to or greater than 1)");
				return;
			}

			Console.Write($"{p}/{q} = ");
			if (q % p == 0)
			{
				q = q / p;
				p = 1;
				Console.WriteLine($"1/{q}");
				return;
			}

			while (true)
			{
				int divider = (p + q) / p;
				Console.Write($"1/{divider} + ");

				p = (p * divider) - q;
				q = q * divider;

				if (q % p != 0) continue;
				q = q / p;
				p = 1;
				Console.WriteLine($"1/{q}");
				break;
			}
		}

		private static void PrintBoard(int count, List<Cell> board)
		{
			for (int i = 0; i < count; i++)
			{
				for (int j = 0; j < count; j++)
				{
					Console.Write(board[i * count + j].TurnVisited.ToString().PadLeft(3) + " ");
				}
				Console.WriteLine();
			}
		}

		private static Cell SelectNextCell(Cell current, List<Cell> board)
		{
			var topLeft = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col - 1);
			var leftTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col - 2);
			var rightTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col + 2);
			var topRight = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col + 1);

			var bottomLeft = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col - 1);
			var leftBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col - 2);
			var rightBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col + 2);
			var bottomRight = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col + 1);

			return new List<Cell>
				{
			 rightBottom,rightTop,leftBottom, leftTop, bottomRight, topRight, topLeft, bottomLeft
				}
			.Where(c => c != null && !c.IsVisited)
			.ToList()
			.OrderBy(c => CalculatePosibleMoves(c, board))
			.First();
		}

		private static int CalculatePosibleMoves(Cell current, List<Cell> board)
		{
			var topLeft = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col - 1);
			var leftTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col - 2);
			var rightTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col + 2);
			var topRight = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col + 1);

			var bottomLeft = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col - 1);
			var leftBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col - 2);
			var rightBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col + 2);
			var bottomRight = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col + 1);

			return new List<Cell>
					{
						topLeft, topRight, leftTop, rightTop, bottomLeft, bottomRight, leftBottom, rightBottom
					}
			.Count(c => c != null && !c.IsVisited);
		}
	}
}

public class Cell
{
	public byte Row { get; set; }

	public byte Col { get; set; }

	public bool IsVisited { get; set; }

	public int TurnVisited { get; set; }
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