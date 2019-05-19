namespace DynamicPrograming
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	[SuppressMessage("ReSharper", "PossibleNullReferenceException")]
	public class Base
	{
		protected long[][] coefficents { get; set; }
		
		public  void GetBinomialCoefficents()
		{
			int n = int.Parse(Console.ReadLine());
			int k = int.Parse(Console.ReadLine());

			this.coefficents = new long[n + 1][];
			this.coefficents[0] = new long[] { 1 };

			long result = BinomialCoefficients(n, k);
			Console.WriteLine(result);
		}

		public void LongestZigZagSequence()
		{

			var numbers = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();

			int count = numbers.Length;
			int[] len = new int[count];
			int[] prev = new int[count];
			int maxLen = 0;
			int lastIndex = -1;

			for (int i = 0; i < count; i++)
			{
				len[i] = 1;
				prev[i] = -1;
				for (int j = 0; j < i; j++)
				{
					if (numbers[i] > numbers[j])
					{
						if (j == 0 && len[j] + 1 > len[i])
						{
							len[i] = len[j] + 1;
							prev[i] = j;
						}
						else if (prev[j] >= 0 && numbers[prev[j]] > numbers[j] && len[j] + 1 > len[i])
						{
							len[i] = len[j] + 1;
							prev[i] = j;
						}
					}
					else
					{
						if (j == 0 && len[j] + 1 > len[i])
						{
							len[i] = len[j] + 1;
							prev[i] = j;
						}
						else if (prev[j] >= 0 && numbers[prev[j]] < numbers[j] && len[j] + 1 > len[i])
						{
							len[i] = len[j] + 1;
							prev[i] = j;
						}
					}
				}

				if (len[i] > maxLen)
				{
					maxLen = len[i];
					lastIndex = i;
				}
			}

			var result = new Stack<int>();
			while (lastIndex >= 0)
			{
				result.Push(numbers[lastIndex]);
				lastIndex = prev[lastIndex];
			}

			Console.WriteLine(string.Join(" ",result.ToArray()));
		}		
		
		private long BinomialCoefficients(int n, int k)
		{
			if (this.coefficents[n] == null)
			{
				this.coefficents[n] = new long[n + 1];
			}

			if (k < 0 || k >= this.coefficents[n].Length)
			{
				return 0;
			}

			if (this.coefficents[n][k] != 0)
			{
				return this.coefficents[n][k];
			}

			long coef = BinomialCoefficients(n - 1, k - 1) + BinomialCoefficients(n - 1, k);
			this.coefficents[n][k] = coef;
			return coef;
		}

		public void Presents()
		{
			var presents = Console.ReadLine()
				.Split(' ')
				.Select(int.Parse)				
				.ToArray();

			var totalSum = presents.Sum();
	
			var sums = new int[totalSum + 1];

			for (int i = 1; i < sums.Length; i++)
			{
				sums[i] = -1;
			}

			for (int presentIndex = 0; presentIndex < presents.Length; presentIndex++)
			{
				for (int prevSumIndex=totalSum -presents[presentIndex];prevSumIndex >=0; prevSumIndex--)
				{
					if (sums[prevSumIndex]!=-1 && sums[prevSumIndex+presents[presentIndex]]==-1)
					{
						sums[prevSumIndex + presents[presentIndex]] = presentIndex;
					}
				}
			}

			var half = totalSum / 2;

			for (int i = half; i >=0; i--)
			{

				if (sums[i]==-1)
				{
					continue;					
				}

			Console.WriteLine($"{totalSum-i-i}");
			Console.WriteLine($"Alan:{i} Bob{totalSum-i}");
			Console.Write($"Alan takes:");
			while (i!=0)
			{
				Console.Write($"{presents[sums[i]]}");
				i -= presents[sums[i]];
			}
		}

		}

		public void CombinationOfCoins()
		{
			int[] coins = Console.ReadLine()
				.Split()
				.Select(int.Parse)
				.ToArray();

			int sum = int.Parse(Console.ReadLine());

			int[,] maxCombCount = new int[coins.Length + 1, sum + 1];
				for (int i = 0; i <= coins.Length; i++)
				{
					maxCombCount[i, 0] = 1;
				}

				for (int i = 1; i <= coins.Length; i++)
				{
					for (int j = 1; j <= sum; j++)
					{
						if (coins[i - 1] <= j)
						{
							maxCombCount[i, j] = maxCombCount[i - 1, j] + maxCombCount[i, j - coins[i - 1]];
						}
						else
						{
							maxCombCount[i, j] = maxCombCount[i - 1, j];
						}
					}
				}

				Console.WriteLine(maxCombCount[coins.Length, sum]);
			}

		public void LimitedCoinsCombinations()
		{
			var coins = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
			var sum = int.Parse(Console.ReadLine());
			int[,] maxCount = new int[coins.Length + 1, sum + 1];
			for (int i = 0; i <= coins.Length; i++)
			{
				maxCount[i, 0] = 1;
			}

			for (int i = 1; i <= coins.Length; i++)
			{
				for (int j = sum; j >= 0; j--)
				{
					var a = coins[i - 1];
					

					if (coins[i - 1] <= j && maxCount[i - 1, j - coins[i - 1]] != 0)
					{
						var b = maxCount[i - 1, j - coins[i - 1]];

						maxCount[i, j]++;
					}
					else
					{
						var c= maxCount[i - 1, j];
						maxCount[i, j] = maxCount[i - 1, j];
					}
				}
			}

			int count = 0;
			for (int i = 0; i <= coins.Length; i++)
			{
				if (maxCount[i, sum] != 0)
				{
					count++;
				}
			}
			Console.WriteLine(count);

		}

		public void ConnectingCablesAlgo()
		{
			int[] permutationRow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(int.Parse).ToArray();

			var orderedRow = permutationRow.OrderBy(x => x).ToArray();
			int maxPairs = GetCount(orderedRow,permutationRow,orderedRow.Length-1,permutationRow.Length-1);
			Console.WriteLine(maxPairs);	

		}

		private int GetCount(int[]ordered,int[]permutated,int x,int y)
		{

			if (x <0 || y < 0)
			{
				return 0;

			}

			if (permutated[x]==ordered[y])
			{
				return 1 + GetCount(ordered,permutated,x - 1, y - 1);
			}
			else
			{
				int reducedOne = GetCount(ordered, permutated, x - 1, y);
				int reduceTwo = GetCount(ordered, permutated, x, y - 1);
				return Math.Max(reduceTwo, reducedOne);
			}
		}

		public void MinimumEditDistance()
		{
			char[] delimeters = new[] { '=', ' ' };
			int replaceCost = Console.ReadLine().Split(delimeters, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray()[0];
			int insertCost = Console.ReadLine().Split(delimeters, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray()[0];
			int deleteCost = Console.ReadLine().Split(delimeters, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray()[0];
			string first = Console.ReadLine().Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).ToArray()[1].Trim();
			string second = Console.ReadLine().Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).ToArray()[1].Trim();
			int[,] editCost = new int[first.Length+1,second.Length+1];

			Compute(editCost,first, second,deleteCost,insertCost,replaceCost);
		}

		private void Compute(int[,]editCost,string first, string second,int deleteCost,int insertCost,int replaceCost)
		{
			int firstLen = first.Length;
			int secondLen = second.Length;

			for (int row = 0; row <= firstLen; row++)
			{
				editCost[row, 0] = row * deleteCost;
			}

			for (int col = 0; col <= secondLen; col++)
			{
				editCost[0, col] = col * insertCost;
			}

			for (int row = 1; row <= firstLen; row++)
			{
				for (int col = 1; col <= secondLen; col++)
				{
					int cost = second[col - 1] == first[row - 1] ? 0 : replaceCost;

					int delete = editCost[row - 1, col] + deleteCost;
					int replace = editCost[row - 1, col - 1] + cost;
					int insert = editCost[row, col - 1] + insertCost;

					editCost[row, col] = Math.Min(Math.Min(delete, insert), replace);
				}
			}

			PrintResult(editCost,first, second, firstLen, secondLen,replaceCost,deleteCost,insertCost);
		}

		private void PrintResult(int [,]editCost,string first, string second, int firstLen, int secondLen,int replaceCost,int deleteCost,int insertCost)
		{
			Console.WriteLine("Minimum edit distance: " + editCost[firstLen, secondLen]);

			var resultOperations = new Stack<string>();
			int fIndex = firstLen;
			int sIndex = secondLen;
			while (fIndex > 0 && sIndex > 0)
			{
				if (first[fIndex - 1] == second[sIndex - 1])
				{
					fIndex--;
					sIndex--;
				}
				else
				{
					int replace = editCost[fIndex - 1, sIndex - 1] + replaceCost;
					int delete = editCost[fIndex - 1, sIndex] + deleteCost;
					int insert = editCost[fIndex, sIndex - 1] + insertCost;
					if (replace <= delete && replace <= insert)
					{
						resultOperations.Push($"REPLACE({fIndex - 1}, {second[sIndex - 1]})");
						fIndex--;
						sIndex--;
					}
					else if (delete < replace && delete < insert)
					{
						resultOperations.Push($"DELETE({fIndex - 1})");
						fIndex--;
					}
					else
					{
						resultOperations.Push($"INSERT({sIndex - 1}, {second[sIndex - 1]})");
						sIndex--;
					}
				}
			}

			while (fIndex > 0)
			{
				resultOperations.Push($"DELETE({fIndex - 1})");
				fIndex--;
			}

			while (sIndex > 0)
			{
				resultOperations.Push($"INSERT({sIndex - 1}, {second[sIndex - 1]})");
				sIndex--;
			}

			foreach (string resultOperation in resultOperations)
			{
				Console.WriteLine(resultOperation);
			}
		}
	}
}
