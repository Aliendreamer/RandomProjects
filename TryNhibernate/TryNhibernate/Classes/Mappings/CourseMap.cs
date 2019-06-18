namespace TryNhibernate.Classes.Mappings
{
	using FluentNHibernate.Mapping;

	public class CourseMap:ClassMap<Course>
	{
		public CourseMap()
		{
			Schema("TestNhibernate.dbo");
			Table("Courses");
			Id(x => x.Id);
			Map(x => x.Grade);
			Map(x => x.Name);
			Map(x => x.TimePeriod);
			References(x => x.University).Column("UniversityId");
			//HasManyToMany(x => x.EnrolledStudents)
			//	.Cascade.All()
			//	.Table("StudentCourse").ParentKeyColumn("StudentId").ChildKeyColumn("CourseId").Inverse();
			HasMany(x => x.EnrolledStudents)
				.Cascade.AllDeleteOrphan()
				.Fetch.Join()
				.Inverse().KeyColumn("CourseId");
		}
	}
}
