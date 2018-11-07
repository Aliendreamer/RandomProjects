namespace Panda.Services.PandaAutomapper
{
    using System;
    using AutoMapper;
    using Domain.Enums;
    using Domain.Models;
    using System.Globalization;
    using Infrastructure.ViewModels.OutputModels;

    public class PackageDateResolver : IValueResolver<Package, PackageDetailsViewModel, string>
    {
        public string Resolve(Package source, PackageDetailsViewModel destination, string destMember, ResolutionContext context)
        {
            switch (source.Status)
            {
                case PackageStatus.Pending:
                    destMember = "N/A";
                    break;

                case PackageStatus.Shipped:
                    // ReSharper disable once PossibleInvalidOperationException
                    destMember = source.EstimatedDeliveryDate.Value.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                    break;

                case PackageStatus.Delivered:
                    // ReSharper disable once PossibleInvalidOperationException
                    destMember = "Delivered";
                    break;

                case PackageStatus.Acquired:
                    throw new InvalidOperationException("Acquired packages shouldn't be accessible");

                default:
                    throw new InvalidOperationException("Something went wrong with admin options");
            }

            return destMember;
        }
    }
}