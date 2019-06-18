namespace TryNhibernate.Classes
{
	using System.Collections.Generic;

	public class Course
	{
		public Course()
		{
			// ReSharper disable once VirtualMemberCallInConstructor
			this.EnrolledStudents=new List<StudentCourse>();
		}
		public virtual int Id {get; set;}
		public virtual int TimePeriod { get; set;}
		public virtual string Name { get; set; }
		public virtual decimal Grade { get; set; }
		public virtual University University { get; set;}
		public virtual IEnumerable<StudentCourse> EnrolledStudents  {get; set;}

	}
}
