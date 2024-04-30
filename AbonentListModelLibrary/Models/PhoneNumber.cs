using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AbonentListModelLibrary.Enums;
using AbonentListModelLibrary.Utils;

namespace AbonentListModelLibrary.Models
{
  public class PhoneNumber : IEquatable<PhoneNumber>
  {
    public int Id { get; set; }
    [Required]
    public int Number { get; set; }
    [Required]
    public int RegionCode { get; set; }
    [Required]
    public int RegionNumber { get; set; }
    [Required]
    public bool IsPlusRegion { get; set; }
    [Required]
    public bool IsMobile { get; set; }
    [Required]
    public EPhoneNumberType PhoneNumberType { get; set; }
    [NotMapped]
    public string? PhoneNumberTypeString => PhoneNumberType.GetDescription();
    [NotMapped]
    public string FullNumberString => ((IsPlusRegion) ? "+" : "") + $"{RegionCode}({RegionNumber}){Number:D7}";

    public int? AbonentId { get; set; }

    public Abonent? Abonent { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public bool Equals(PhoneNumber? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Number == other!.Number &&
            RegionCode == other!.RegionCode &&
            RegionNumber == other!.RegionNumber;
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals(obj as PhoneNumber);
    }

    public override int GetHashCode()
    {
      return Number ^ RegionCode ^ RegionNumber;
    }

    public override string ToString()
    {
      return FullNumberString;
    }
  }
}
