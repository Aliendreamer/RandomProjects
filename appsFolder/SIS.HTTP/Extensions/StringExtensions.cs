namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {

        public static string Capitalize(string line)
        {
            char firstLetter = line.ToUpper().ToCharArray()[0];
            string secondPart = line.ToLower().Substring(1);

            string capitalizedString = firstLetter + secondPart;

            return capitalizedString;
        }

    }
}
