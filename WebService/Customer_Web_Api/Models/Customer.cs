using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace Customer_Web_Api.Models;

public partial class Customer
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;
}
