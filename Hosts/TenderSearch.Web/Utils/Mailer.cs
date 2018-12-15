using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TenderSearch.Contracts.Infrastructure;
using TenderSearch.Data;
using TenderSearch.Data.Security;
using TenderSearch.Web.Configurations;
using Eml.ControllerBase.Mvc.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TenderSearch.Web.Utils
{
    public class Mailer
    {
        public static void SendEmail(string callbackUrl, string username, string address)
        {
            const string lineBreak = "<br>";
            const string qoute = "\"";

            var applicationNameConfig = new ApplicationNameConfig();
            var emailStyleConfig = new EmailStyleConfig();
            var style = emailStyleConfig.Value;
            var messageBody = $"<p style={qoute}{style}{qoute}>" + $"Dear {username},{lineBreak}{lineBreak}" +
                              $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.{lineBreak}{lineBreak}" +
                              $"Thank you for registering.{lineBreak}{lineBreak}{lineBreak}" +
                              "This is an automated message. Do not reply." + "</p>";
            var message = new MailMessage
            {
                Subject = $"{applicationNameConfig.Value} Reset Password",
                Body = messageBody
            };


            message.To.Add(new MailAddress(address)); //Todo uncomment after debugging.
            SendEmail(message);
            //Forgot Password Confirmation
        }

        //TODO update urls
        public static string GetUrlLink(eArea currentStep, string baseAddress, string id)
        {
            var urlLink = "";

            eArea toStage;

            switch (currentStep)
            {

                case eArea.Admins:

                    toStage = eArea.Users; //TODO determine the correct value destination
                    urlLink = string.Format("{0}{1}/Txn_Module?ParentId={2}&ReturnToParentListURL={0}{1}/Proposal", baseAddress, toStage, id);

                    break;

                case eArea.Registration:

                    toStage = eArea.UserManagers;
                    urlLink = string.Format("{0}{1}/AspNetUserRole?ParentId={2}&ReturnToParentListURL={0}{1}/AspNetUser", baseAddress, toStage, id);

                    break;

                case eArea.Users:
                case eArea.UserManagers:
                default:
                    throw new Exception("Method: GetUrlLink. CurrentStep not supported: " + currentStep.ToString());
            }

            return urlLink;
        }

        public static void SendEmail(eArea fromStage, MailMessage message)
        {

            eArea toStage;

            switch (fromStage)
            {
                case eArea.Admins:

                    toStage = eArea.Users;
                    RefillMessageTo(message, toStage);
                    break;

                case eArea.Users:

                    return; //not applicable

                case eArea.UserManagers:

                    return; //not applicable

                case eArea.Registration:

                    toStage = eArea.UserManagers;
                    RefillMessageTo(message, toStage);

                    break;

                default:
                    throw new Exception("Parameter is not yet supported: " + fromStage);
            }

            SendEmail(message);
        }

        public static void SendEmail(AspNetUserRole item, string urlLink)
        {
            const string lineBreak = "<br>";
            const string qoute = "\"";

            var applicationNameConfig = new ApplicationNameConfig();
            var message = new MailMessage {Subject = $"{applicationNameConfig.Value} Permission"};
            var emailStyleConfig = new EmailStyleConfig();
            var style = emailStyleConfig.Value;

            if (string.IsNullOrWhiteSpace(item.OldRoleName)) //added
            {
                message.Body = $"<p style={qoute}{style}{qoute}>" + $"Dear {item.UserName},{lineBreak}{lineBreak}" +
                               $"The administrator has granted you the role  <b>{item.RoleName}</b>.{lineBreak}{lineBreak}" +
                               $"Click <a href='{urlLink}'>here</a> to login.{lineBreak}{lineBreak}" +
                               $"Thank you for registering.{lineBreak}{lineBreak}{lineBreak}" +
                               "This is an automated message. Do not reply." + "</p>";
            }
            else //edited
            {
                message.Body = $"<p style={qoute}{style}{qoute}>" + $"Dear {item.UserName},{lineBreak}{lineBreak}" +
                               $"The administrator has changed your role from <b>{item.OldRoleName}</b> to <b>{item.RoleName}</b>.{lineBreak}{lineBreak}" +
                               $"Click <a href='{urlLink}'>here</a> to login.{lineBreak}{lineBreak}" +
                               $"If this is not your expected 'role', please contact your TeanderSearch Administrator.{lineBreak}{lineBreak}" +
                               $"Thank you.{lineBreak}{lineBreak}{lineBreak}" +
                               "This is an automated message. Do not reply." + "</p>";
            }

            message.To.Add(new MailAddress(item.Email)); //Todo uncomment after debugging.
            SendEmail(message);
        }

        //public static string GetMessageBody(Contract item, string urlLink)
        //{
        //    const string lineBreak = "<br>";

        //    var emailStyleConfig = new EmailStyleConfig();
        //    var style = emailStyleConfig.Value;

        //    return $"<p style='{style}'>" + $"Hi,{lineBreak}{lineBreak}" +
        //           $"{item.ContractType} is now ready for processing.{lineBreak}{lineBreak}" +
        //           $"Click <a href='{urlLink}'>here</a> to login.{lineBreak}{lineBreak}" +
        //           $"Thank you.{lineBreak}{lineBreak}{lineBreak}" + "This is an automated message. Do not reply." + "</p>";
        //}

        private static void SendEmail(MailMessage message)
        {
            var smtpFromConfig = new SmtpFromConfig();
            var smtpDisplayNameConfig = new SmtpDisplayNameConfig();
            var smtpHostConfig = new SmtpHostConfig();
            var smtpPortConfig = new SmtpPortConfig();
            var smtpGhostConfig = new SmtpGhostConfig();
            var sMtpFrom = smtpFromConfig.Value;
            var sMtpDisplayName = smtpDisplayNameConfig.Value;
            var sMtpHost = smtpHostConfig.Value;
            var sMtpPort = smtpPortConfig.Value;
            var sMtpGhost = smtpGhostConfig.Value;

            if (!string.IsNullOrWhiteSpace(sMtpGhost)) message.Bcc.Add(new MailAddress(sMtpGhost)); //Todo remove email after debugging.

            message.From = new MailAddress(sMtpFrom, sMtpDisplayName, System.Text.Encoding.UTF8);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Port = sMtpPort;
                smtp.Host = sMtpHost;
                //smtp.Send(message);//TODO uncomment when SMSTP server becomes available
            }

        }

        private static void RefillMessageTo(MailMessage message, eArea ToStage)
        {
            var emails = GetEmails(ToStage.ToString());

            foreach (var e in emails)
            {
                message.To.Add(new MailAddress(e));
            }
        }

        private static IEnumerable<string> GetEmails(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TenderSearchDb()));
            var role = roleManager.Roles.FirstOrDefault(r => r.Name == roleName);

            if (role == null) throw new Exception("Role does not exists: " + roleName);

            var userStore = new UserStore<ApplicationUser>(new TenderSearchDb());

            return userStore.Users
                    .Where(r => r.Roles.Count(x => x.RoleId == role.Id
                                                   && !string.IsNullOrEmpty(r.Email)) > 0)
                    .Select(r => r.Email)
                    .Distinct()
                    .ToList()
                ;
        }

        public static List<AspNetUser> GetUsers()
        {
            var userStore = new UserStore<ApplicationUser>(new TenderSearchDb());

            return userStore.Users
                .OrderBy(r => r.Email)
                .ThenBy(r => r.UserName)
                .Select(r => new AspNetUser { UserName = r.UserName, Email = r.Email })
                .ToList();
        }
    }
}