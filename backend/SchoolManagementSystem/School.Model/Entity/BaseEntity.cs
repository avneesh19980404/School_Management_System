using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.Model.Entity
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as BaseEntity);
        }

        public bool Equals(BaseEntity other)
        {
            return other != null &&
                   Id.Equals(other.Id) &&
                   CreatedAt == other.CreatedAt &&
                   UpdatedAt == other.UpdatedAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CreatedAt, UpdatedAt);
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return EqualityComparer<BaseEntity>.Default.Equals(left, right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }
    }
}
