using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class TransactionRepository : ITransactionRepository
    {
        private MarketContext db;

        public TransactionRepository(MarketContext db)
        {
            this.db = db;
        }

        public IEnumerable<Transaction> Get(string cashierName)
        {
            var Transactions = db.Transactions.Where(e => e.CashierName == cashierName).ToList();
            if (Transactions.Count == 0)
            {
                Transactions = db.Transactions.ToList();
            }
            return Transactions;
        }

        public IEnumerable<Transaction> GetByDay(string cashierName, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return db.Transactions.Where(x => x.TimeStamp.Date == date.Date);
            else
                return db.Transactions.Where(x =>
                EF.Functions.Like(x.CashierName, $"%{cashierName}%") &&
                (x.TimeStamp.Date == date.Date));
        }

        public void Save(string cashierName, int productId, string productName, double price, int beforeQuantity, int soldQuantity)
        {
            var transaction = new Transaction
            {
                ProductId = productId,
                ProductName = productName,
                TimeStamp = DateTime.Now,
                SoldQuantity = soldQuantity,
                BeforeQuantity = beforeQuantity,
                Price = price,
                CashierName = cashierName
            };

            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
        {
            if (string.IsNullOrWhiteSpace(cashierName))
                return db.Transactions.Where(x => (x.TimeStamp.Date >= startDate.Date) &&
                                               (x.TimeStamp.Date <= endDate.Date.AddDays(1).Date));
            else
                return db.Transactions.Where(x =>
               EF.Functions.Like(x.CashierName, $"%{cashierName}%") &&
                (x.TimeStamp.Date >= startDate.Date) && (x.TimeStamp.Date <= endDate.Date.AddDays(1).Date));
        }
    }
}
