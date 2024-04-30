using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AbonentListModelLibrary.Models
{
  public class AssignedStreet : IEquatable<AssignedStreet>
  {
    public int Id { get; set; }
    [Required]
    public string Street { get; set; } = string.Empty;
    [NotMapped]
    public int AbonentCount => Addresses?.Sum(a => a.Abonents?.Count() ?? 0) ?? 0;

    public ICollection<Address>? Addresses { get; set; }

    public bool Equals(AssignedStreet? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Street.Equals(other!.Street, StringComparison.CurrentCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals(obj as AssignedStreet);
    }

    public override int GetHashCode()
    {
      return Street.GetHashCode();
    }

    public override string ToString()
    {
      return Street;
    }
  }
}
