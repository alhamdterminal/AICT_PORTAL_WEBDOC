using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;

namespace WebdocTerminal.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithAttechMentAsync(string email, string subject, string message , string path);

        Task SendBulkEmailAsync(List<UserViewModel> users, string subject, string message);

        Task SendBulkCompanyEmailAsync(List<UsersEmail> users, string subject, string message);

        Task SendBulkEmailAsyncUsers(List<UsersEmail> users, string subject, string message);

        Task UsersSendBulkEmailAsync(List<UsersEmail> users, string subject, string message);
        Task SendEmailWithAttechMentsAsync(string email, string subject, string message,  List<string> path);
    }

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _emailSettings;
        private readonly IHostingEnvironment _env;


        string MailServer;
        int MailPort;
        string SenderName;
        string Sender;
        string Password;
         

        public EmailSender(
            IOptions<EmailConfig> emailSettings,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;

            MailServer = _emailSettings.MailServer;
            MailPort = Convert.ToInt32( _emailSettings.MailPort);
            SenderName =  _emailSettings.SenderName;
            Sender =  _emailSettings.Sender;
            Password =  _emailSettings.Password;
        }

        public async Task SendBulkEmailAsync(List<UserViewModel> users, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                var recipients = new List<MailboxAddress>();

                users = users.Where(u => u.Email != null).ToList();

                foreach (var user in users)
                {
                    try
                    {
                        recipients.Add(new MailboxAddress(user.FirstName + " " + user.LastName, user.Email));
                    }
                    catch (Exception)
                    {

                    }
                }

                mimeMessage.To.AddRange(recipients);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task UsersSendBulkEmailAsync(List<UsersEmail> users, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                var recipients = new List<MailboxAddress>();

                users = users.Where(u => u.Email != null).ToList();

                foreach (var user in users)
                {
                    try
                    {
                        recipients.Add(new MailboxAddress( ""   , user.Email));
                    }
                    catch (Exception)
                    {

                    }
                }

                mimeMessage.To.AddRange(recipients);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendBulkCompanyEmailAsync(List<UsersEmail> users, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                var recipients = new List<MailboxAddress>();

                users = users.Where(u => u.Email != null).ToList();

                foreach (var user in users)
                {
                    try
                    {
                        recipients.Add(new MailboxAddress("" , user.Email));
                    }
                    catch (Exception)
                    {

                    }
                }

                mimeMessage.To.AddRange(recipients);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort  , true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                     //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task SendBulkEmailAsyncUsers(List<UsersEmail> users, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                var recipients = new List<MailboxAddress>();

                users = users.Where(u => u.Email != null).ToList();

                foreach (var user in users)
                {
                    try
                    {
                        recipients.Add(new MailboxAddress(" ", user.Email));
                    }
                    catch (Exception)
                    {

                    }
                }

                mimeMessage.To.AddRange(recipients);

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;
                 
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };


                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task SendEmailWithAttechMentAsync(string email, string subject, string message, string path)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;



                //var builder = new BodyBuilder();

                //// Set the plain-text version of the message text
                //builder.TextBody = @"Hey Alice";

                //// We may also want to attach a calendar event for Monica's party...
                //builder.Attachments.Add(path);

                var multipart = new Multipart("mixed");
                var attachment = new MimePart("image", "png");
                attachment.Content = new MimeContent(File.OpenRead(path));
                attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
                attachment.ContentTransferEncoding = ContentEncoding.Base64;
                attachment.FileName = Path.GetFileName(path);

                multipart.Add(attachment);
                 

                // now set the multipart/mixed as the message body
                mimeMessage.Body = multipart;




                 
                 

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }


        public async Task SendEmailWithAttechMentsAsync(string email, string subject, string message, List<string> path)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(SenderName, Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;
                 
                var multipart = new Multipart("mixed");


                var attachment = new MimePart("image", "png");

                foreach (var item in path)
                {
                    attachment.Content = new MimeContent(File.OpenRead(item));
                    attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
                    attachment.ContentTransferEncoding = ContentEncoding.Base64;
                    attachment.FileName = Path.GetFileName(item);

                    multipart.Add(attachment);

                }



                // now set the multipart/mixed as the message body
                mimeMessage.Body = multipart;







                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(MailServer, MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    //await client.AuthenticateAsync(Sender, Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
