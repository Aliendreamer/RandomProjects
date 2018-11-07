namespace Panda.Services.PandaAutomapper
{
    using System;
    using AutoMapper;
    using Domain.Enums;
    using Domain.Models;
    using System.Globalization;
    using Infrastructure.Constants;
    using Infrastructure.ViewModels.OutputModels;

    public class PackageDateResolver : IValueResolver<Package, PackageDetailsViewModel, string>
    {
        public string Resolve(Package source, PackageDetailsViewModel destination, string destMember, ResolutionContext context)
        {
            switch (source.Status)
            {
                case PackageStatus.Pending:
                    destMember = GlobalConstants.Constants.PendingStatusDate;
                    break;

                case PackageStatus.Shipped:
                    // ReSharper disable once PossibleInvalidOperationException
                    destMember = source.EstimatedDeliveryDate.Value.ToString(GlobalConstants.Constants.DatetimeFormat, CultureInfo.InvariantCulture);
                    break;

                case PackageStatus.Delivered:
                    // ReSharper disable once PossibleInvalidOperationException
                    destMember = nameof(PackageStatus.Delivered);
                    break;

                case PackageStatus.Acquired:
                    throw new InvalidOperationException(GlobalConstants.Error.PackageResolverAcquiredError);

                default:
                    throw new InvalidOperationException(GlobalConstants.Error.PackageResolverADefaultError);
            }

            return destMember;
        }
    }
}