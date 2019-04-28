using System;
using System.Collections.Generic;

namespace RecursionProblems
{
	public class MatruxNeightbouring
	{
		private static char[,] matrix;
		private static SortedSet<Area> Areas = new SortedSet<Area>();

		public void RunCalculations()
		{
			ReadMatrix();
			Cell unmarkedCell;

			while ((unmarkedCell = FindCell()) != null)
			{
				Area current = new Area { Row = unmarkedCell.Row, Col = unmarkedCell.Col, Size = 0 };
				int size = TraverseArea(current.Row, current.Col);
				current.Size = size;
				Areas.Add(current);
			}

			Console.WriteLine($"Total Areas found: {Areas.Count}");
			int counter = 1;
			foreach (var area in Areas)
			{
				Console.WriteLine($"Area #{counter++} at ({area.Row}, {area.Col}), size: {area.Size}");
			}
		}

		private int TraverseArea(int row, int col)
		{
			if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1) || matrix[row, col] == '*' || matrix[row, col] == 'v')
			{
				return 0;
			}
			else
			{
				matrix[row, col] = 'v';
				return 1 + TraverseArea(row - 1, col) + TraverseArea(row + 1, col) + TraverseArea(row, col - 1) + TraverseArea(row, col + 1);
			}
		}

		private Cell FindCell()
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j] != '*' && matrix[i, j] != 'v')
					{
						return new Cell { Row = i, Col = j };
					}
				}
			}

			return null;
		}

		private void ReadMatrix()
		{
			int rows = int.Parse(Console.ReadLine());
			int cols = int.Parse(Console.ReadLine());

			matrix = new char[rows, cols];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				string line = Console.ReadLine();

				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					matrix[i, j] = line[j];
				}
			}
		}

		private class Area : IComparable
		{
			public int Row { get; set; }

			public int Col { get; set; }

			public int Size { get; set; }

			public int CompareTo(object otherr)
			{
				Area other = (Area)otherr;

				if (this.Size != other.Size)
				{
					return other.Size.CompareTo(this.Size);
				}

				if (this.Row != other.Row)
				{
					return this.Row.CompareTo(other.Row);
				}

				return this.Col.CompareTo(other.Col);
			}
		}

		private class Cell
		{
			public int Row { get; set; }

			public int Col { get; set; }
		}
	}
}