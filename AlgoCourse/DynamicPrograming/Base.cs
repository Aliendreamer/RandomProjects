namespace DynamicPrograming
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
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
	}
}
