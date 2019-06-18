namespace TryNhibernate.Classes.Mappings
{
	using FluentNHibernate.Mapping;

	public class StudentCourseMap:ClassMap<StudentCourse>
	{

		public StudentCourseMap()
		{
			Schema("TestNhibernate.dbo");
			Table("StudentUser");
			Id(x => x.Id).GeneratedBy.Identity();
			References(x => x.Student)
				.Not.Nullable()
				.Cascade.SaveUpdate()
				.Column("StudentId");
			References(x => x.Course)
				.Not.Nullable()
				.Cascade.SaveUpdate()
				.Column("CourseId");
		}
	}
}
