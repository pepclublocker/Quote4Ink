using Microsoft.EntityFrameworkCore;
using Web.Data.Models;

//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Data.Repository
{
    public partial class SQLRepository : IRepository
    {

        public IEnumerable<PriceMatrix> GetUserMatrix(int __salesGroup)
        {
            return _context.PriceMatrices.Where(x => x.SalesGroupID == __salesGroup).Include(i => i.SalesGroup);
        }

        public PriceMatrix GetMatrix(Guid MatrixID)
        {
            return _context.PriceMatrices.Where(x => x.Id == MatrixID).FirstOrDefault();
        }

        public Guid SaveMatrix(PriceMatrix Matrix)
        {

            _context.PriceMatrices.Add(Matrix);
            _context.SaveChanges();

            var newPrice = new PriceMatrixPrice();
            newPrice.MatrixId = Matrix.Id;
            newPrice.Color = 1;
            newPrice.LevelMaxCount = 50;

            _context.PriceMatrixPrices.Add(newPrice);
            _context.SaveChanges();

            return Matrix.Id;
        }

        public void CreateMatrixColumn(Guid __MatrixID)
        {
            //count # colors
            var current = _context.PriceMatrixPrices.Where(a => a.MatrixId == __MatrixID).ToList();

            var maxColor = current.MaxBy(a => a.Color);
            //add 1
            var nextColor = maxColor == null ? 1 : maxColor.Color + 1;


            //add entry for each max val; ue
            var newRows = (from a in current
                           where a.Color == maxColor.Color
                           group a by a.LevelMaxCount into g
                           select new PriceMatrixPrice
                           {
                               Color = nextColor,
                               LevelMaxCount = g.Key,
                               MatrixId = __MatrixID,
                               Price = 1.25M,
                               OtherPrice = 1.50M
                           });
            _context.PriceMatrixPrices.AddRange(newRows);
            _context.SaveChanges();

        }

        public async Task RemoveMatrixColumn(Guid __MatrixID)
        {

            var current = await _context.PriceMatrixPrices
                  .Where(a => a.MatrixId == __MatrixID)
                  .ToListAsync();

            var maxColor = current.MaxBy(a => a.Color).Color;

            //don't allow removing last column
            if (maxColor == 1)
            {
                return;
            }

            var finalQry = (from a in current
                            where a.Color == maxColor
                            group a by a.LevelMaxCount into g
                            select g.First());

            _context.RemoveRange(finalQry);
            await _context.SaveChangesAsync();

        }

        public void CreateMatrixRow(Guid __MatrixID)
        {
            var current = _context.PriceMatrixPrices
               .Where(a => a.MatrixId == __MatrixID)
               .ToList();

            var maxLevel = current.MaxBy(a => a.LevelMaxCount);
            if (maxLevel is null)
            {
                return;
            }

            var nextLevel = maxLevel.LevelMaxCount + 50;
            var newRows = (from a in current
                           where a.LevelMaxCount == maxLevel.LevelMaxCount
                           group a by a.Color into g
                           select new PriceMatrixPrice
                           {
                               Color = g.Key,
                               LevelMaxCount = nextLevel,
                               MatrixId = __MatrixID,
                               Price = 1.25M,
                               OtherPrice = 1.50M
                           });

            _context.PriceMatrixPrices.AddRange(newRows);
            _context.SaveChanges();
        }

        public async Task RemoveMatrixRow(Guid __MatrixID)
        {
            var current = await _context.PriceMatrixPrices
                .Where(a => a.MatrixId == __MatrixID)
                .ToListAsync();

            if (current.GroupBy(current => current.LevelMaxCount).Count() == 1)
            {
                return;
            }

            var maxLevel = current.MaxBy(a => a.LevelMaxCount);
            if (maxLevel is null)
            {
                return;
            }

            var finalQry = (from a in current
                            where a.LevelMaxCount == maxLevel.LevelMaxCount
                            group a by a.Color into g
                            select g.First());

            _context.RemoveRange(finalQry);
            await _context.SaveChangesAsync();
        }

        public async Task MaxLevelCellChange(Guid matrixId, int newLevel, int oldLevel)
        {
            var existing = (from a in _context.PriceMatrixPrices
                            where a.MatrixId == matrixId
                            && a.LevelMaxCount == oldLevel
                            select a).ToList();

            var overlapCheck = (from a in _context.PriceMatrixPrices
                                where a.MatrixId == matrixId
                                && a.LevelMaxCount == newLevel
                                select a).ToList();
            if (overlapCheck.Any())
            {
                return; //cant be same as a nother
            }

            var qry = (from a in _context.PriceMatrixPrices
                       where a.MatrixId == matrixId
                       && a.LevelMaxCount >= oldLevel
                       && a.Color == 1
                       orderby a.LevelMaxCount
                       select a).Take(2).ToList();
            var validNewValue = qry.Count == 1 ||
           newLevel < qry[1].LevelMaxCount;

            if (!validNewValue)
                newLevel = qry[1].LevelMaxCount - 1;

            foreach (var item in existing)
                item.LevelMaxCount = newLevel;

            await _context.SaveChangesAsync();
            return;
        }

        public async Task PriceCellChange(int Item, decimal itemPrice)
        {
            // using var transaction = _context.Database.BeginTransaction();
            var OriginalItem = _context.PriceMatrixPrices.Where(x => x.Id == Item).FirstOrDefault();

            OriginalItem.Price = itemPrice;

            _context.PriceMatrixPrices.Update(OriginalItem);
            await _context.SaveChangesAsync();
            //   await transaction.CommitAsync();

        }

        public PriceMatrixProperties GetProperty(Guid PropertyID)
        {
            return _context.PriceMatrixProperties.Where(x => x.Id == PropertyID).FirstOrDefault();
        }

        public void UpdateProperty(PriceMatrixProperties MatrixProperty)
        {
            _context.PriceMatrixProperties.Update(MatrixProperty);
            _context.SaveChanges();
        }

        public void SaveProperty(PriceMatrixProperties MatrixProperty)
        {

            _context.PriceMatrixProperties.Add(MatrixProperty);
            _context.SaveChanges();

        }

        public bool DeleteProperty(Guid PropertyID)
        {
            var xString = false;
            var xRows = _context.PriceMatrixProperties.Where(x => x.Id == PropertyID).ExecuteDelete();

            if (xRows > 0)
                xString = true;

            return xString;
        }

        //public IEnumerable<PriceMatrixProperties> GetMatrixProperties(Guid MatrixID)
        //{
        //    return _context.priceMatrixProperties.Where(x => x.PriceMatrixID == MatrixID);
        //}

        public IEnumerable<PriceMatrixPrice> GetPriceMatrixPrices(Guid MatrixID)
        {
            return _context.PriceMatrixPrices.Where(x => x.MatrixId == MatrixID).Include(i => i.PriceMatrix);
        }
    }
}
