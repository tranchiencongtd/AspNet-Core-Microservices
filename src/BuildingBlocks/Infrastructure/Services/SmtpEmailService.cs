﻿using Contracts.Services;
using Shared.Services.Email;
using Serilog;
using Contracts.Configurations;
using MailKit.Net.Smtp;
using MimeKit;
using Infrastructure.Configurations;

namespace Infrastructure.Services
{
  public class SmtpEmailService : ISmtpEmailService
  {
    private readonly ILogger _logger;
    private readonly EmailSMTPSettings _settings;
    private readonly SmtpClient _smtpClient;
    public SmtpEmailService(ILogger logger, EmailSMTPSettings settings)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _settings = settings ?? throw new ArgumentNullException(nameof(settings));
      _smtpClient = new SmtpClient();
    }

    public void SendEmail(MailRequest request)
    {
      throw new NotImplementedException();
    }

    public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
    {
      var emailMessage = new MimeMessage
      {
        Sender = new MailboxAddress(_settings.DisplayName, request.From ?? _settings.From),
        Subject = request.Subject,
        Body = new BodyBuilder()
        {
          HtmlBody = request.Body
        }.ToMessageBody()
      };

      if(request.ToAddresses.Any())
      {
        foreach (var toAddress in request.ToAddresses)
        {
          emailMessage.To.Add(MailboxAddress.Parse(toAddress));
        }
      } else
      {
        var toAddress = request.ToAddress;
        emailMessage.To.Add(MailboxAddress.Parse(toAddress));
      }

      try
      {
        await _smtpClient.ConnectAsync(_settings.SMTPServer, _settings.Port, _settings.UseSsl, cancellationToken);
        await _smtpClient.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);
        await _smtpClient.SendAsync(emailMessage, cancellationToken);
        await _smtpClient.DisconnectAsync(true, cancellationToken);
      }
      catch (Exception ex)
      {
        _logger.Error(ex.Message, ex);
        throw;
      }
      finally
      {
        await _smtpClient.DisconnectAsync(true, cancellationToken);
        _smtpClient.Dispose();
      }
    }
  }
}
