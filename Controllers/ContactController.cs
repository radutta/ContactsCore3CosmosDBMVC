using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsCore3CosmosDBMVC.Models.Abstract;
using ContactsCore3CosmosDBMVC.Models.Entities;
using ContactsCore3CosmosDBMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsCore3CosmosDBMVC.Controllers
{
  public class ContactController : Controller
  {
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactController> _logger;

    public ContactController(IContactRepository contactRepository, ILogger<ContactController> logger)
    {
      _contactRepository = contactRepository;
      _logger = logger;
    }

    // public async Task<IActionResult> Index()
    // {
    //   var contactList = await _contactRepository.GetAllContactsAsync();
    //   List<Contact> contactList = new List<Contact>();
    //   foreach (var item in contactList)
    //   {
    //     contactList.Add(new Contact
    //     {
    //       ContactName = item.PartitionKey,
    //       Phone = item.RowKey,
    //       ContactType = item.ContactType,
    //       Email = item.Email
    //     });
    //   }
    //   return View(contactList);
    // }

    public async Task<IActionResult> Index(string contactName = null, string phone = null)
    {
      List<Contact> contactList = new List<Contact>();
      if (string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(phone))
      {
        contactList = await _contactRepository.GetAllContactsAsync();
      }
      else if (!string.IsNullOrEmpty(contactName) && !string.IsNullOrEmpty(phone))
      {
        var contact = await _contactRepository.FindContactCPAsync(contactName, phone);
        contactList.AddRange(contact);
      }

      else if (!string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(phone))
      {
        contactList = await _contactRepository.FindContactsByContactNameAsync(contactName);
      }
      else if (string.IsNullOrEmpty(contactName) && !string.IsNullOrEmpty(phone))
      {
        contactList = await _contactRepository.FindContactByPhoneAsync(phone);
        // var contact = await _contactRepository.FindContactByRowKeyAsync(phone);
        // contactList.Add(contact);
      }
      List<ContactViewModel> contactViewModelList = new List<ContactViewModel>();
      foreach (var item in contactList)
      {
        contactViewModelList.Add(new ContactViewModel
        {
          Id = item.Id,
          ContactName = item.ContactName,
          Phone = item.Phone,
          ContactType = item.ContactType,
          Email = item.Email
        });
      }
      _logger.LogInformation($"--- Get the records from the CosmosDB ---");
      return View(contactViewModelList);
    }


    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ContactViewModel model)
    {
      model.Id = Guid.NewGuid().ToString();
      if (ModelState.IsValid)
      {
        Contact contact = new Contact { Id = model.Id, ContactName = model.ContactName, Phone = model.Phone, Email = model.Email, ContactType = model.ContactType };
        //_logger.LogInformation($"Contact Id: {contact.Id}");
        var contactResult = await _contactRepository.CreateAsync(contact);
        if (contactResult != null)
        {
          TempData["newcontact"] = $"New Contact, {contactResult.ContactName} Added";
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("", "New Contact Could not be created");
        }
      }
      return View();
    }

    public async Task<IActionResult> Edit(string id, string contactName)
    {
      var contact = await _contactRepository.FindContactAsync(id);

      var editContact = new ContactViewModel { Id = contact.Id, ContactName = contact.ContactName, Phone = contact.Phone, ContactType = contact.ContactType, Email = contact.Email };
      return View(editContact);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ContactViewModel model)
    {
      if (ModelState.IsValid)
      {
        var editContact = new Contact { Id = model.Id, ContactName = model.ContactName, Phone = model.Phone, ContactType = model.ContactType, Email = model.Email };
        var updateContact = await _contactRepository.UpdateAsync(editContact);
        if (updateContact != null)
        {
          return RedirectToAction("Index");
        }
      }
      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id, string contactName)
    {
      await _contactRepository.DeleteAsync(id, contactName);
      return RedirectToAction("Index");
    }
  }
}