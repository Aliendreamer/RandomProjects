namespace RecursionProblems
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class Recursion<T>
	{
		private T[] Collection { get; set; }
		private int Index {get;set;}

		public Recursion()
		{
				
		}

		public Recursion(int index)
		{
			this.Index = index;
		}
		public Recursion(T[] arr)
		{
			this.Collection =arr;
		}

		public void GetCollection(T[] arr)
		{
			this.Collection = arr;
		}


		public void GetIndex(int index)
		{
			this.Index = index;
		}

		public IEnumerable<T> ReverseCollection()
		{
			if (!this.Collection.Any())
			{
				throw new ArgumentException("the massive shouldn't be empty!");
			}
			var currentIndex = 0;
			
			this.Collection = this.Collection.OrderBy(x => x).ToArray();
			var middlePoint = this.Collection.Count() / 2;
			var collectionReversed= ShiftRecursively(currentIndex, middlePoint);
			return collectionReversed;
		}

		private IEnumerable<T> ShiftRecursively(int currentIndex,int middlePoint)
		{
		
			var current = this.Collection[currentIndex];
			var indexToSwap = (this.Collection.Length-1) - currentIndex;
			var elementToChange = this.Collection[indexToSwap];
			var holder = current;
			
			this.Collection[currentIndex] = elementToChange;
			this.Collection[indexToSwap] = holder;
		
			if (currentIndex < middlePoint)
			{
				ShiftRecursively(currentIndex + 1, middlePoint);
			}
			return this.Collection;
		}


		public void NestedLoopsRecursion(int n)
		{
			
		}
	}
}
