﻿using Contracts.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
  public class EmailSMTPSettings : IEmailSMTPSettings
  {
    public string DisplayName { get; set; }
    public bool EnableVerfication { get; set; }
    public string From { get; set; }
    public string SMTPServer { get; set; }
    public bool UseSsl { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
