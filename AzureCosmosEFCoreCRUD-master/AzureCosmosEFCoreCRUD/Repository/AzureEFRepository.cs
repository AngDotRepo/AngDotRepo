using AzureCosmosEFCoreCRUD.DBContext;
using AzureCosmosEFCoreCRUD.Models;
using AzureCosmosEFCoreCRUD.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureCosmosEFCoreCRUD.Repository
{
    public class AzureEFRepository : IAzureEFRepository
    {
        readonly ApplicationDbContext _context;

        public AzureEFRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //GetLocationNamesByChar
        #region GetLocationNamesByChar
        public async Task<IEnumerable<Item>> GetLocationNamesByCharAsync(string searchString)
        {
            var Parameter = new SqlParameter
            {
                ParameterName = "@searchString",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.NVarChar,
                Value = searchString,
                Size = 100
            };

            return await _context.Item.FromSqlRaw($"exec uspGetLocationNamesByChar @searchString", Parameter).ToListAsync();
        }
        #endregion GetLocationNamesByChar

        //[System.Obsolete]
        //public async Task<IEnumerable<ItemList>> GetLocationNamesDropDown()
        //{

        //    return await _context.LocationNamesDropDown.($"exec uspGetLocationNamesDropDown").ToListAsync();
        //}
        //#endregion

    }
}
