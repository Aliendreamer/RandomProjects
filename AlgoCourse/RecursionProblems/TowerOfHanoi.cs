namespace RecursionProblems
{
	using System.Collections.Generic;
	using System.Linq;

	public class TowerOfHanoi
	{

		public TowerOfHanoi(int size)
		{
			Size = size;
			this.Source= new Stack<int>(Enumerable.Range(1, this.Size).Reverse());
			this.Destination=new Stack<int>(this.Size);
			this.Intermediate=new Stack<int>(this.Size);
		}

		private int Size { get; set; }

		private Stack<int> Source { get; set; }

		private Stack<int> Destination { get; set;}
		
		private Stack<int> Intermediate { get; set; }

		public void MoveDiscs()
		{
			if (this.Size == 1)
			{
				Destination.Push(Source.Pop());
			}
			else
			{
				var disk = this.Source.Pop();
				Intermediate.Push(disk);
				Destination.Push(Intermediate.Pop());
				this.Size--;
				MoveDiscs();
			}
		}

	}
}
