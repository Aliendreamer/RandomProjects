namespace Panda.Services.PandaCommons
{
    using System.Globalization;
    using AutoMapper;
    using Domain.Enums;
    using Domain.Models;
    using Infrastructure.ViewModels.OutputModels;

    public class PackageDateResolver : IValueResolver<Package, PackageDetailsViewModel, string>
    {
        public string Resolve(Package source, PackageDetailsViewModel destination, string destMember, ResolutionContext context)
        {
            if (source.Status == PackageStatus.Pending)
            {
                destMember = "N/A";
            }
            else if (source.Status == PackageStatus.Shipped)
            {
                // ReSharper disable once PossibleInvalidOperationException
                destMember = source.EstimatedDeliveryDate.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                destMember = "Delivered";
            }

            return destMember;
        }
    }
}