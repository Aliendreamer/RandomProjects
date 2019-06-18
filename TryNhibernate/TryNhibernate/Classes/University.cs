// ReSharper disable VirtualMemberCallInConstructor
namespace TryNhibernate.Classes
{
	using System.Collections.Generic;

	public class University
	{

		public University()
		{
			this.Students=new List<Student>();
			this.UniversityCourses=new List<Course>();
		}
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual  IEnumerable<Student> Students { get; set; }
		public virtual IEnumerable<Course>UniversityCourses { get; set; }
	}
}
