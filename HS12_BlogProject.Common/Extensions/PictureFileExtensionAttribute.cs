using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HS12_BlogProject.Common.Extensions
{
	public class PictureFileExtensionAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			IFormFile file = (IFormFile)value;

			if (file != null)
			{
				var extension = Path.GetExtension(file.FileName).ToLower();
				string[] extensions = { "jpg", "jpeg", "png" };

				bool result = extensions.Any(x => x.EndsWith(extension));

				if (!result)
				{
					return new ValidationResult("Valid format is 'jpg', 'jpeg', 'png'");
				}
			}
			return ValidationResult.Success;
		}
	}
}
