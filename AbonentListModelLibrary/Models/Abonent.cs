using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AbonentListModelLibrary.Enums;

namespace AbonentListModelLibrary.Models
{
  public class Abonent : IEquatable<Abonent>
  {
    public int Id { get; set; }
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    [Required]
    public DateTime ContractDate { get; set; }

    [NotMapped]
    public string FullName => string.Join(' ', LastName, FirstName, MiddleName);

    [Required]
    public int AddressId { get; set; }

    public Address? Address { get; set; }

    public ICollection<PhoneNumber>? PhoneNumbers { get; set; }

    [NotMapped]
    public string? FirstPhoneNumberPrivateString => PhoneNumbers?.FirstOrDefault(a => a.PhoneNumberType == EPhoneNumberType.Private)?.ToString();
    [NotMapped]
    public string? FirstPhoneNumberOrganizationString => PhoneNumbers?.FirstOrDefault(a => a.PhoneNumberType == EPhoneNumberType.Organization)?.ToString();
    [NotMapped]
    public string? FirstPhoneNumberMobileString => PhoneNumbers?.FirstOrDefault(a => a.IsMobile)?.ToString();
    [NotMapped]
    public string? Street => Address?.Street;
    [NotMapped]
    public string? Building => Address?.Building;

    public bool Equals(Abonent? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return LastName.Equals(other!.LastName, StringComparison.CurrentCultureIgnoreCase) && FirstName.Equals(other!.FirstName, StringComparison.CurrentCultureIgnoreCase) && ((MiddleName == null) && (other!.MiddleName == null) || (MiddleName != null) && MiddleName.Equals(other!.MiddleName, StringComparison.CurrentCultureIgnoreCase));
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals(obj as Abonent);
    }

    public override int GetHashCode()
    {
      return LastName.GetHashCode() ^ FirstName.GetHashCode() ^ (MiddleName?.GetHashCode() ?? 0);
    }

    public override string ToString()
    {
      return FullName;
    }
  }
}
