using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOSRS.Shared.Models;

namespace WOSRS.Server.Models;

[Table("application_users")]
public class ApplicationUser : IdentityUser
{
    [Key]
    [Column("application_user_id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override string Id { get => base.Id; set => base.Id = value; }

    [Column("email", TypeName = "text")]
    public override string Email { get => base.Email; set => base.Email = value; }

    [Column("username", TypeName = "text")]
    public override string UserName { get => base.UserName; set => base.UserName = value; }

    [Column("password_hash", TypeName = "text")]
    public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

    [Column("normalized_email", TypeName = "text")]
    public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }

    [Column("normalized_username", TypeName = "text")]
    public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }

    [Column("phone_number", TypeName = "text")]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    [Column("phone_number_confirmed", TypeName = "boolean")]
    public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }

    [Column("email_confirmed", TypeName = "boolean")]
    public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

    [Column("access_faile_count", TypeName = "int")]
    public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

    [Column("concurrency_stamp", TypeName = "text")]
    public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

    [Column("security_stamp", TypeName = "text")]
    public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }

    [Column("two_factor_enabled", TypeName = "boolean")]
    public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }

    [Column("lockout_enabled", TypeName = "boolean")]
    public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }

    [Column("lockout_end", TypeName = "timestamp")]
    [NotMapped]
    public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }
}
