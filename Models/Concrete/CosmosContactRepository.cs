using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using ContactsCore3CosmosDBMVC.Models.Abstract;
using ContactsCore3CosmosDBMVC.Models.Entities;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ContactsCore3CosmosDBMVC.Models.Concrete
{
  public class CosmosContactRepository : IContactRepository
  {
    private readonly ILogger<CosmosContactRepository> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _distributedCache;
    private readonly string _cosmosEndpoint;
    private readonly string _cosmosKey;
    private readonly string _databaseId;
    private readonly string _containerId;
    private Database _database;
    private Container _container;
    private CosmosClient _cosmosClient;

    public CosmosContactRepository(IOptions<CosmosUtility> cosmosUtility, ILogger<CosmosContactRepository> logger, IConfiguration configuration, IDistributedCache distributedCache)
    {
      _logger = logger;
      _configuration = configuration;
      _distributedCache = distributedCache;
      _cosmosEndpoint = cosmosUtility.Value.CosmosEndpoint;
      _cosmosKey = cosmosUtility.Value.CosmosKey;
      _databaseId = "multiDb";
      _containerId = "contact";

      _cosmosClient = new CosmosClient(_cosmosEndpoint, _cosmosKey);
      _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId).GetAwaiter().GetResult();
      _database = _cosmosClient.GetDatabase(_databaseId);
      _database.CreateContainerIfNotExistsAsync(_containerId, "/contactName").GetAwaiter().GetResult();
      _container = _database.GetContainer(_containerId);

      _database = _cosmosClient.GetDatabase(_databaseId);
      _container = _database.GetContainer(_containerId);
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
      return null;
    }

    public async Task<Contact> FindContactAsync(string id)
    {
      return null;
    }
    public async Task<List<Contact>> FindContactCPAsync(string contactName, string phone)
    {
      return null;
    }

    public async Task<List<Contact>> FindContactByPhoneAsync(string phone)
    {
      return null;
    }

    public async Task<List<Contact>> FindContactsByContactNameAsync(string contactName)
    {
      return null;

    }

    public async Task<List<Contact>> GetAllContactsAsync()
    {
      return null;
    }

    public async Task<Contact> UpdateAsync(Contact contact)
    {
      return null;
    }

    public async Task DeleteAsync(string id, string contactName)
    {
      ItemResponse<Contact> contactResponse = await _container.DeleteItemAsync<Contact>(id, new PartitionKey(contactName));
    }
  }
}