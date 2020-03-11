using System;

namespace ContactsCore3CosmosDBMVC.ViewModels
{
  public class ErrorViewModel
  {
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
  }
}