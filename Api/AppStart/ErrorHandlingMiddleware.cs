using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Exceptions;
using Domain.Base;
using Domain.Responses;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next) => this.next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorCode = (exception is ApiException ex) ? ex.Code : HttpStatusCode.InternalServerError;
        var erroViewModel = new ResponseBase() { IsFailure = true, Errors = new List<string> { exception.Message } };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)errorCode;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(erroViewModel));
    }
}