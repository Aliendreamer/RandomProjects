namespace TryNhibernate.Classes
{
	using System.Collections.Generic;

	public class Student
	{

		public Student()
		{
			// ReSharper disable once VirtualMemberCallInConstructor
			this.StudentCourses=new List<StudentCourse>();
		}
		public  virtual  int Id { get; set; }
		public virtual string FirstName { get; set; }
		public  virtual string LastName { get; set; }
		public virtual  University University { get; set; }
		public virtual  IEnumerable<StudentCourse> StudentCourses { get; set; }
		public  virtual decimal AverageGrade { get; set; }
	}
}
