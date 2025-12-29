/*
 * lufer
 * ISI
 * OAuth
 * Models
 * */
namespace AuthCore.Models;

/// <summary>
/// Record is similar to class, with automatic Properties
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
/// <param name="Roles"></param>
public record User(Guid Id, string Name, string Email, string Password, string[] Roles);