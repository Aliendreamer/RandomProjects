namespace Exam.App.ViewModels.OutputModels
{
    using System.Collections.Generic;

    public class HomeIndexViewModel
    {
        public string PackageType { get; set; }
        public ICollection<PackageHomeModel> Packages { get; set; }
    }
}