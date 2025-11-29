using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    [Required(ErrorMessage = "Invalid Title")]
    [MaxLength(80, ErrorMessage = "Maximum allowed length is 80")]
    public string Title { get; set; }  = string.Empty;
    
    [Required(ErrorMessage = "Invalid description")]
    [MaxLength(200, ErrorMessage = "Maximum allowed length is 200")]
    public string Description { get; set; }  = string.Empty;
}