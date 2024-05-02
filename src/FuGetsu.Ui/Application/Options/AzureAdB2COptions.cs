using System.ComponentModel.DataAnnotations;

namespace FuGetsu.Ui.Application.Options;

internal sealed class AzureAdB2COptions
{
    public static string SectionName => "AzureAdB2C";

    [Required]
    public required string Authority { get; set; }

    [Required]
    public required string ClientId { get; set; }

    [Required]
    public required bool ValidateAuthority { get; set; }
}