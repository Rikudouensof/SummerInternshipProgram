namespace SummerInternshipProgram.API.Helpers.Implementation
{
    public static class GeneralHelper
    {

        public static string DateTimeToString(DateTime input)
        {
            return input.ToString("dd/MM/yyyy");
        }

        public static DateTime StringToDate(string input)
        {
            return DateTime.Parse(input);
        }

        public static string GetNewRequestId()
        {
            Random rnd = new Random();
            int myRandomNo = rnd.Next(100000000, 999999999);
            var requestId = $"Req{myRandomNo}";
            return requestId;
        }
    }
}
