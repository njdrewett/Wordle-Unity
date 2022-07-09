using System;

public class WordRepositoryException : Exception
{
    private WordRepositoryException() : base() {
    }

    public WordRepositoryException(string message) : base(message) {
    }

    public WordRepositoryException(string message, Exception exception) : base(message, exception) { 
    }

}
