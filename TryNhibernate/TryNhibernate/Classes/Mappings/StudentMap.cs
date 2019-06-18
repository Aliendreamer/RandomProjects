namespace TryNhibernate.Classes.Mappings
{
	using FluentNHibernate.Mapping;

	public class StudentMap:ClassMap<Student>
	{

		public StudentMap()
		{
			Schema("TestNhibernate.dbo");
			Table("Students");
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
			Map(x => x.AverageGrade).Not.Insert().Not.Update();
			References(x => x.University).Column("UniversityId");
			//HasManyToMany(x => x.StudentCourses)
			//	.Cascade.All()
			//	.Table("StudentCourse").ParentKeyColumn("StudentId").ChildKeyColumn("CourseId");
			HasMany(x => x.StudentCourses)
				.Cascade.AllDeleteOrphan()
				.Fetch.Join()
				.Inverse().KeyColumn("StudentId");
		}
	}
}
