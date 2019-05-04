using System;
using System.Collections.Generic;
using System.Text;

namespace AzureAdDemo.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
