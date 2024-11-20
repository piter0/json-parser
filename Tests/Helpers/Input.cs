namespace Tests
{
    internal static class Input
    {
        internal static string GetInvalidInput(string fileNumber)
        {
            return GetInput(fileNumber, true);
        }

        internal static string GetValidInput(string fileNumber)
        {
            return GetInput(fileNumber, false);
        }

        private static string GetInput(string fileNumber, bool isValid)
        {
            var file = isValid ? $"invalid{fileNumber}.json" : $"valid{fileNumber}.json";

            return File.ReadAllText($"{Directory.GetCurrentDirectory() + $"\\Assets\\{file}"}");
        }
    }
}
