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
      _containerId = "contacts";

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
      ItemResponse<Contact> contactResponse = await _container.CreateItemAsync<Contact>(contact);

      if (contactResponse.StatusCode == HttpStatusCode.Created)
      {
        return contact;
      }
      return null;
    }

    public async Task<Contact> FindContactAsync(string id)
    {
      var sqlQuery = $"Select * from c where c.id='{id}'";
      QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
      FeedIterator<Contact> queryResultIterator = _container.GetItemQueryIterator<Contact>(queryDefinition);
      List<Contact> contactsList = new List<Contact>();
      while (queryResultIterator.HasMoreResults)
      {
        FeedResponse<Contact> currentResultSet = await queryResultIterator.ReadNextAsync();
        foreach (var item in currentResultSet)
        {
          contactsList.Add(item);
        }
        return contactsList[0];
      }
      return null;
    }
    public async Task<List<Contact>> FindContactCPAsync(string contactName, string phone)
    {
      var sqlQuery = $"Select * from c where c.contactName='{contactName}' and c.phone='{phone}'";
      QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
      FeedIterator<Contact> queryResultIterator = _container.GetItemQueryIterator<Contact>(queryDefinition);
      List<Contact> contactsList = new List<Contact>();
      while (queryResultIterator.HasMoreResults)
      {
        FeedResponse<Contact> currentResultSet = await queryResultIterator.ReadNextAsync();
        foreach (var item in currentResultSet)
        {
          contactsList.Add(item);
        }
        return contactsList;
      }
      return null;
    }

    public async Task<List<Contact>> FindContactByPhoneAsync(string phone)
    {
      var sqlQuery = $"Select * from c where c.phone='{phone}'";
      QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
      FeedIterator<Contact> queryResultIterator = _container.GetItemQueryIterator<Contact>(queryDefinition);
      List<Contact> contactsList = new List<Contact>();
      while (queryResultIterator.HasMoreResults)
      {
        FeedResponse<Contact> currentResultSet = await queryResultIterator.ReadNextAsync();
        foreach (var item in currentResultSet)
        {
          contactsList.Add(item);
        }
        return contactsList;
      }
      return null;
    }

    public async Task<List<Contact>> FindContactsByContactNameAsync(string contactName)
    {
      var sqlQuery = $"Select * from c where c.contactName='{contactName}'";
      QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
      FeedIterator<Contact> queryResultIterator = _container.GetItemQueryIterator<Contact>(queryDefinition);
      List<Contact> contactsList = new List<Contact>();
      while (queryResultIterator.HasMoreResults)
      {
        FeedResponse<Contact> currentResultSet = await queryResultIterator.ReadNextAsync();
        foreach (var item in currentResultSet)
        {
          contactsList.Add(item);
        }
        return contactsList;
      }
      return null;
    }

    public async Task<List<Contact>> GetAllContactsAsync()
    {
      var sqlQuery = "Select * from c";

      QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
      FeedIterator<Contact> queryResultIterator = _container.GetItemQueryIterator<Contact>(queryDefinition);
      List<Contact> contactsList = new List<Contact>();
      while (queryResultIterator.HasMoreResults)
      {
        FeedResponse<Contact> currentResultSet = await queryResultIterator.ReadNextAsync();
        foreach (var item in currentResultSet)
        {
          contactsList.Add(item);
        }
        return contactsList;
      }
      return null;
    }

    public async Task<Contact> UpdateAsync(Contact contact)
    {
      ItemResponse<Contact> contactResponse = await _container.ReadItemAsync<Contact>(contact.Id, new PartitionKey(contact.ContactName));
      var contactResult = contactResponse.Resource;

      contactResult.Id = contact.Id;
      contactResult.ContactName = contact.ContactName;
      contactResult.ContactType = contact.ContactType;
      contactResult.Phone = contact.Phone;
      contactResult.Email = contact.Email;

      contactResponse = await _container.ReplaceItemAsync<Contact>(contactResult, contactResult.Id);

      if (contactResponse.Resource != null)
      {
        return contactResponse;
      }
      return null;
    }

    public async Task DeleteAsync(string id, string contactName)
    {
      ItemResponse<Contact> contactResponse = await _container.DeleteItemAsync<Contact>(id, new PartitionKey(contactName));
    }
  }
}