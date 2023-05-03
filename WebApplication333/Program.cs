using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Microservice", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Microservice v1");
    c.RoutePrefix = string.Empty;
});

var users = new List<User>();

app.MapGet("/", () => "Hello World!");

app.MapPost("/users", (User user) =>
{
    users.Add(user);
    return Results.Ok();
});

app.MapDelete("/users/{id}", (int id) =>
{
    var user = users.Find(u => u.Id == id);
    if (user != null)
    {
        users.Remove(user);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPut("/users/{id}", (int id, User updatedUser) =>
{
    var user = users.Find(u => u.Id == id);
    if (user != null)
    {
        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/users", () =>
{
    return Results.Ok(users);
});

app.Run();

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}


