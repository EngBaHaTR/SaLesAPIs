﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SaLes__APIs.Entity;

public partial class RowInvoice
{
    public RowInvoice()
    {
         Oid = Guid.NewGuid();  
    }
    public Guid Oid { get; set; }

    public Guid? Product { get; set; }

    public double? SelaPrice { get; set; }

    public int? Quntity { get; set; }

    public Guid? Invoice { get; set; }
    [JsonIgnore]
    public int? OptimisticLockField { get; set; }
    [JsonIgnore]
    public int? GCRecord { get; set; }
    [JsonIgnore]
    public virtual Invoice InvoiceNavigation { get; set; }
    [JsonIgnore]
    public virtual Product ProductNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}