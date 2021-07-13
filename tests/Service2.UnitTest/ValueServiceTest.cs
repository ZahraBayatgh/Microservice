using Service2.Services;
using System;
using Xunit;

namespace Service2.UnitTest
{
    public class ValueServiceTest
    {
        private IValueService valueService;

        public ValueServiceTest()
        {
            valueService = new ValueService();
        }
        [Fact]
        public void GetValue_Test()
        {
            //Assart
            var messageTime = DateTime.Now.ToString();

            // Act
            var result = valueService.GetValues(messageTime);
            var expected = new string[] { "value1", "value2", messageTime };

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
