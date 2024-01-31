using System.Text.RegularExpressions;

namespace Domain._Utils.Validators;

public static class CpfValidator
{
    public static bool IsValidCPF(string cpf)
    {
        // Remove any non-digit characters from the CPF
        string cleanCPF = Regex.Replace(cpf, @"\D", "");

        // Check if the cleaned CPF has 11 digits
        if (cleanCPF.Length != 11)
        {
            return false;
        }

        // Calculate the first verification digit
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += int.Parse(cleanCPF[i].ToString()) * (10 - i);
        }
        int firstDigit = (sum % 11 < 2) ? 0 : 11 - (sum % 11);

        // Calculate the second verification digit
        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += int.Parse(cleanCPF[i].ToString()) * (11 - i);
        }
        int secondDigit = (sum % 11 < 2) ? 0 : 11 - (sum % 11);

        // Check if the calculated digits match the provided ones
        return (int.Parse(cleanCPF[9].ToString()) == firstDigit && int.Parse(cleanCPF[10].ToString()) == secondDigit);
    }
}
