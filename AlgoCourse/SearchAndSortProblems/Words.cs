namespace SearchAndSortProblems
{
	
	public class Words
	{
		private  int counter = 0;
		private  int count;
	

		private  int CalculateFactorial(int count)
		{
			int factorial = 1;
			for (int i = 2; i <= count; i++)
			{
				factorial *= i;
			}

			return factorial;
		}

		private  bool IsValid(char[] word)
		{
			for (int i = 1; i < count; i++)
			{
				if (word[i] == word[i - 1])
				{
					return false;
				}
			}

			counter++;
			return true;
		}

		private  void Permutate(char[] word, int start, int n)
		{
			if (start < n)
			{
				for (int i = n - 2; i >= start; i--)
				{
					for (int j = i + 1; j < n; j++)
					{
						if (word[i] != word[j])
						{
							Swap(ref word[i], ref word[j]);
							IsValid(word);
							Permutate(word, i + 1, n);
						}
					}

					char tmp = word[i];
					for (int k = i; k < n - 1;)
					{
						word[k] = word[++k];
					}

					word[n - 1] = tmp;
				}
			}
		}

		private  void Swap(ref char i, ref char j)
		{
			if (i == j)
			{
				return;
			}

			i ^= j;
			j ^= i;
			i ^= j;
		}
	}
}
