// IEmailService.cs

using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public interface IEmailService
{
    Task<bool> SendPasswordResetEmail(string userEmail, string resetUrl);
}

public class EmailService : IEmailService
{
    private readonly string _emailFrom = "eleveurconnect@zohomail.eu"; // Votre adresse e-mail
    private readonly string _emailPassword = "fW03hMw1pk7i"; // Votre mot de passe d'e-mail

    public async Task<bool> SendPasswordResetEmail(string userEmail, string resetUrl)
    {
        try
        {
            // Configurer les informations SMTP
            var smtpClient = new SmtpClient("smtp.zoho.eu")
            {
                Port = 587,
                Credentials = new NetworkCredential(_emailFrom, _emailPassword),
                EnableSsl = true,
            };

            // Construire le message e-mail
            var message = new MailMessage(_emailFrom, userEmail)
            {
                Subject = "Réinitialisation de mot de passe",
                Body = $"Pour réinitialiser votre mot de passe, veuillez cliquer sur le lien suivant : {resetUrl}",
                IsBodyHtml = true
            };

            // Envoyer l'e-mail
            await smtpClient.SendMailAsync(message);

            // L'e-mail a été envoyé avec succès
            return true;
        }
        catch (Exception ex)
        {
            // Gérer les erreurs d'envoi d'e-mail ici
            // Vous pouvez journaliser l'erreur ou la renvoyer pour être gérée ailleurs
            Console.WriteLine($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
            return false;
        }
    }
}
