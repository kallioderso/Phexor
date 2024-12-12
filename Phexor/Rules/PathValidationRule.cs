using System.IO;
using System.Windows.Controls;

namespace Phexor.Rules;

public class PathValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    {
        string path = value as string;

        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            return new ValidationResult(false, "Der eingegebene Pfad ist ungültig.");
        }

        return ValidationResult.ValidResult;
    }
}
