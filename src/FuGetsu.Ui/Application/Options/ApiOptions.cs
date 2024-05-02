using System.ComponentModel.DataAnnotations;

namespace FuGetsu.Ui.Application.Options;

public sealed class ApiOptions
{
    public static string SectionName => "Api";

    [Required]
    public string BaseUrl { get; set; } = null!;
}