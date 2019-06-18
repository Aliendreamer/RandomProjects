using System;

namespace TryNhibernate
{
	using System.Linq;
	using Classes;

	class Program
	{
		static void Main(string[] args)
		{

			using (var session = NhibernateHelper.OpenSession())
			{
				var students = session.Query<Student>().ToList();
				var uni = session.Query<University>().ToList();
				var courses = session.Query<Course>().ToList();

				//Console.WriteLine("ddd");
				foreach (var st in students)
				{
					foreach (var c in st.StudentCourses)
					{
						Console.WriteLine($"{c.Course.Name} {c.Course.University.Name}");

					}
				}

				foreach (var st in courses)
				{
					foreach (var c in st.EnrolledStudents
					)
					{
						Console.WriteLine($"{c.Student.FirstName} {c.Student.University.Name}");

					}
				}

			}
		
		}
	}
}
