namespace SearchAndSortProblems
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class SortBase
	{
		private IList<int> SortedCollection { get; set; }

		public IEnumerable<int> SortedSequenceWithBubleSort(int[] array)
		{
			this.SortedCollection = new List<int>(array);
				
			for (int i = 0; i < this.SortedCollection.Count - 1; i++)
			{
					var current = SortedCollection[i];
					var next = SortedCollection[i + 1];
					if (current > next)
					{
						SortedCollection[i] = next;
						SortedCollection[i + 1] = current;
					
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


		public  int MergeSort(int[] arr)
		{
			int[] temp = new int[arr.Length];
			return Sort(arr, temp, 0, arr.Length - 1);
		}

		private  int Sort(int[] arr, int[] temp, int left, int right)
		{
			int mid, invCount = 0;

			if (right > left)
			{
				mid = (right + left) / 2;

				invCount = Sort(arr, temp, left, mid);
				invCount += Sort(arr, temp, mid + 1, right);

				invCount += Merge(arr, temp, left, mid + 1, right);
			}

			return invCount;
		}

		private  int Merge(int[] arr, int[] temp, int left, int mid, int right)
		{
			int i, j, k;

			int invCount = 0;

			i = left;
			j = mid;
			k = left;

			while ((i <= mid - 1) && (j <= right))
			{
				if (arr[i] <= arr[j])
				{
					temp[k++] = arr[i++];
				}
				else
				{
					temp[k++] = arr[j++];
					invCount += mid - i;
				}
			}

			while (i <= mid - 1)
			{
				temp[k++] = arr[i++];
			}

			while (j <= right)
			{
				temp[k++] = arr[j++];
			}

			for (i = left; i <= right; i++)
			{
				arr[i] = temp[i];
			}

			return invCount;
		}
	}
}
