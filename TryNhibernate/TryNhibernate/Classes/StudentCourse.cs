namespace TryNhibernate.Classes
{
	public class StudentCourse
	{
		public virtual int Id { get; set; }
		public virtual Student Student { get; set; }
		public virtual Course Course { get; set; }
	}
}
