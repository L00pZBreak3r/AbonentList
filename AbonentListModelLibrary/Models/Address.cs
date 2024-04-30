using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbonentListModelLibrary.Models
{
  public class Address : IEquatable<Address>
  {
    public int Id { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }
    [Required]
    public string Street { get; set; } = string.Empty;
    [Required]
    public string Building { get; set; } = string.Empty;

    public string? Apartment { get; set; }

    public int? AssignedStreetId { get; set; }

    public AssignedStreet? AssignedStreet { get; set; }

    public ICollection<Abonent>? Abonents { get; set; }

    public ICollection<PhoneNumber>? PhoneNumbers { get; set; }

    public bool Equals(Address? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((Country == null) && (other!.Country == null) || (Country != null) && Country.Equals(other!.Country, StringComparison.CurrentCultureIgnoreCase)) &&
            ((State == null) && (other!.State == null) || (State != null) && State.Equals(other!.State, StringComparison.CurrentCultureIgnoreCase)) &&
            ((City == null) && (other!.City == null) || (City != null) && City.Equals(other!.City, StringComparison.CurrentCultureIgnoreCase)) &&
            Street.Equals(other!.Street, StringComparison.CurrentCultureIgnoreCase) &&
            Building.Equals(other!.Building, StringComparison.CurrentCultureIgnoreCase) &&
            ((Apartment == null) && (other!.Apartment == null) || (Apartment != null) && Apartment.Equals(other!.Apartment, StringComparison.CurrentCultureIgnoreCase));
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals(obj as Address);
    }

    public override int GetHashCode()
    {
      return (Country?.GetHashCode() ?? 0) ^ (State?.GetHashCode() ?? 0) ^ (City?.GetHashCode() ?? 0) ^ Street.GetHashCode() ^ Building.GetHashCode() ^ (Apartment?.GetHashCode() ?? 0);
    }

    public override string ToString()
    {
      return string.Join(", ", Country, State, City, Street, Building, Apartment);
    }
  }
}
