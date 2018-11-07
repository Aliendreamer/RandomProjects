namespace Panda.Services
{
    using AutoMapper;
    using Interfaces;
    using System.Linq;
    using Domain.Models;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;
    using Domain.Enums;
    using Infrastructure.ViewModels.OutputModels;

    public class ReceiptsService : BaseService, IReceiptsService
    {
        public IEnumerable<ReceiptDisplayModel> IndexModels(string userName)
        {
            var user = GetUserByName(userName);
            List<ReceiptDisplayModel> receipts = new List<ReceiptDisplayModel>();

            if (user.Role.ToString() == nameof(UserRole.Admin))
            {
                receipts = this.Db.Receipts.ProjectTo<ReceiptDisplayModel>().ToList();
            }
            else
            {
                receipts = this.Db.Receipts.Where(x => x.Recipient.Username == user.Username)
                    .ProjectTo<ReceiptDisplayModel>().ToList();
            }

            return receipts;
        }

        public ReceiptDetailsViewModel GetReceiptById(int id)
        {
            var result = this.Db.Receipts.Include(x => x.Package)
                .Include(x => x.Recipient)
                .First(x => x.Id == id);
            var receipt = Mapper.Map<ReceiptDetailsViewModel>(result);

            return receipt;
        }

        private User GetUserByName(string name)
        {
            return this.Db.Users.First(x => x.Username == name);
        }
    }
}