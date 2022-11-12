using AzureCosmosEFCoreCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosEFCoreCRUD.Repository.Interfaces
{
    public interface IAzureEFRepository
    {
        Task<IEnumerable<Item>> GetLocationNamesByCharAsync(string searchString);

        //Task<IEnumerable<ItemList>> GetLocationNamesDropDown();
    }
}
