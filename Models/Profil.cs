public class Profil
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    // Particulier ou Professionnel
    public string UserType { get; set; } = string.Empty;

    // Numéro de SIREN (pour les professionnels)
    public string Siren { get; set; } = string.Empty;
    public string Facebook { get; set; } = string.Empty;

    public string Instagram { get; set; } = string.Empty;

    public string Twitter { get; set; } = string.Empty;
    public string Youtube { get; set; } = string.Empty;

    public string Tiktok { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;

}
