using Sobhan.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;

namespace ViewModel.SignalR
{
  public class StatusMy
    {
        public int userid { get; set; }
        public Status statusenum { get; set; }
        public string status => statusenum.GetDescription();
    }
}
