﻿namespace API.Errors;

public class ApiExceptions(int statusCode, string message, string? details)
{
    public int StatusCode { get; } = statusCode;
    public string Message { get; } = message;
    public string? Details { get; } = details;
}
