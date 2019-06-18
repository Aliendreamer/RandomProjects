namespace TryNhibernate.Classes.Mappings
{
	using FluentNHibernate.Mapping;

	public class UniversityMap:ClassMap<University>
	{

		public UniversityMap()
		{
			Schema("TestNhibernate.dbo");
			Table("University");
			Id(x => x.Id);
			Map(x => x.Name);
			HasMany(x => x.Students)
				.Cascade.AllDeleteOrphan().KeyColumn("UniversityId");
			HasMany(x => x.UniversityCourses)
				.Cascade.AllDeleteOrphan().KeyColumn("UniversityId");

		}
	}
}
