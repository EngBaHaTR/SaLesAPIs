﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaLes__APIs.Entity;

public partial class PermissionPolicyUser
{
    [Key]
    public Guid Oid { get; set; }

    public string StoredPassword { get; set; }

    public bool? ChangePasswordOnFirstLogon { get; set; }

    public string UserName { get; set; }

    public bool? IsActive { get; set; }

    public int? OptimisticLockField { get; set; }

    public int? GCRecord { get; set; }

    public int? ObjectType { get; set; }

    public int? AccessFailedCount { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public virtual XPObjectType ObjectTypeNavigation { get; set; }

    public virtual ICollection<PermissionPolicyUserLoginInfo> PermissionPolicyUserLoginInfos { get; set; } = new List<PermissionPolicyUserLoginInfo>();

    public virtual ICollection<PermissionPolicyUserUsers_PermissionPolicyRoleRole> PermissionPolicyUserUsers_PermissionPolicyRoleRoles { get; set; } = new List<PermissionPolicyUserUsers_PermissionPolicyRoleRole>();
}