using System.ComponentModel.DataAnnotations;

namespace FuGetsu.Infrastructure.Stripe.Options;

internal sealed class StrapiOptions
{
    public static string SectionName = "Stripe";

    [Required]
    public required string SecretKey { get; set; }

    [Required]
    public required string PublishableKey { get; set; }
}