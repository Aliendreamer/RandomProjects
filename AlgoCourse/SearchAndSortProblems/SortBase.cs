namespace SearchAndSortProblems
{
	using System;
	using System.Collections.Generic;

	public class SortBase
	{
		private IList<int> SortedCollection { get; set; }

		public IEnumerable<int> SortedSequenceWithBubleSort(int[] array)
		{
			this.SortedCollection = new List<int>(array);
			int index = 0;
			bool swapped = true;

			while (swapped)
			{
				swapped = false;
				for (int i = 0; i < this.SortedCollection.Count - 1; i++)
				{
					var current = SortedCollection[i];
					var next = SortedCollection[i + 1];
					if (current > next)
					{
						SortedCollection[i] = next;
						SortedCollection[i + 1] = current;
						swapped = true;
					}
				}
			}

			return this.SortedCollection;
		}

		public void PrintCollection()
		{
			Console.WriteLine(string.Join(" ", this.SortedCollection));
		}

		public int GetElementWithBinarySearch(int[] arr, int num)
		{
			var result = Array.BinarySearch(arr, 0, arr.Length, num);
			if (result < 0)
			{
				result = -1;
			}
			return result;
		}
	}
}