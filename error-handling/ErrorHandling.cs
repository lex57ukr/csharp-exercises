using System;


public static class ErrorHandling
{
    public static void HandleErrorByThrowingException()
    {
        throw new Exception(
            "MS should have made this class abstract!"
        );
    }

    public static int? HandleErrorByReturningNullableType(string input)
    {
        return "1" == input
            ? (int?) 1
            : null;
    }

    public static bool HandleErrorWithOutParam(string input, out int result)
    {
        var success = "1" == input;

        result = success
            ? 1
            : default(int);

        return success;
    }

    public static void DisposableResourcesAreDisposedWhenExceptionIsThrown(
        IDisposable disposableObject
    )
    {
        using (disposableObject)
        {
            throw new Exception("This code hurts my feelings.");
        }
    }
}
