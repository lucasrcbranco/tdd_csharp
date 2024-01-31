namespace Domain.Tests._Utils;

public static class AssertExtensions
{
    public static void AssertExceptionWithMessageCheck(
        this ArgumentException exception,
        string errorMessage)
    {
        if (Equals(errorMessage, exception.Message))
        {
            Assert.True(true);
        }
        else
        {
            Assert.False(true, @$"Was expectind the following error message: '{errorMessage}'
                                    but received the message: '{exception.Message}' instead");
        }
    }
}
