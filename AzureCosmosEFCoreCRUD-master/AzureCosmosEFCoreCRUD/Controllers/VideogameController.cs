using AzureCosmosEFCoreCRUD.DBContext;
using AzureCosmosEFCoreCRUD.HubConfig;
using AzureCosmosEFCoreCRUD.Models;
using AzureCosmosEFCoreCRUD.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AzureCosmosEFCoreCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideogameController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;
        private IHubContext<VideogameHub> _hub;
        public IAzureEFRepository _IAzureEFRepository { get; set; }

        public VideogameController(ApplicationDbContext applicationDbContext,
            IConfiguration configuration,
            ILogger<VideogameController> logger,
            IHubContext<VideogameHub> hub,
            IAzureEFRepository IAzureEFRepository)
        {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
            _logger = logger;
            _hub = hub;
            _IAzureEFRepository = IAzureEFRepository;
        }

        //GET: api/GetAllVideogames
        #region GetAllVideogames
        [HttpGet("GetAllVideogames")]
        public async Task<ActionResult<IList<Item>>> GetAllItems()
        {
            try
            {
                var videogames = await applicationDbContext.Item.ToListAsync();
                if (videogames != null)
                {
                    return Ok(videogames);
                }
                return NotFound("No results found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Some error occured while retrieving data");
            }
        }
        #endregion GetAllVideogames

        //GET: api/GetLocationNamesByChar
        #region GetLocationNamesByChar
        [HttpGet("GetLocationNamesByChar")]
        public async Task<ActionResult<IList<Item>>> GetLocationNamesByChar(string searchString)
        {
            IEnumerable<Item> itemLists = null;

            try
            {
                itemLists = await _IAzureEFRepository.GetLocationNamesByCharAsync(searchString);

                if (itemLists.Count() == 0)
                {
                    return NoContent();
                }

                if (itemLists != null)
                {

                    var resultitemLists = JsonConvert.SerializeObject(itemLists);

                    return Ok(resultitemLists);
                }

                return NotFound("No results found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Some error occured while retrieving data");
            }
        }
        #endregion GetLocationNamesByChar


        //GET: api/GetLocationNamesDropDown
        #region GetLocationNamesDropDown
        [HttpGet("GetLocationNamesDropDown")]
        public async Task<ActionResult<IList<ItemList>>> GetLocationNamesDropDown()
        {
            IEnumerable<ItemList> itemLists = null;

            try
            {
                List<ItemList> itemListss = new List<ItemList>{
                   new ItemList{Id = "1", LocationName = "Germany"},
                   new ItemList{Id = "2", LocationName = "France"}
                   };

                if (itemListss.Count() == 0)
                {
                    return NoContent();
                }

                if (itemListss != null)
                {

                    var resultitemLists = JsonConvert.SerializeObject(itemListss);

                    return Ok(resultitemLists);
                }

                return NotFound("No results found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Some error occured while retrieving data");
            }
        }
        #endregion GetLocationNamesByChar
    }
}
