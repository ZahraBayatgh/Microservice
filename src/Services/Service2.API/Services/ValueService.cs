namespace Service2.Services
{
    public class ValueService : IValueService
    {
        public string[] GetValues(string messageTime)
        {
            return new string[] { "value1", "value2", messageTime };
        }
    }
}
