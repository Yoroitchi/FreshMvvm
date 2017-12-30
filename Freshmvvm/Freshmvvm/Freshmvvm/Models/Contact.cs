using SQLite;
using System;

namespace Freshmvvm.Models
{
    [Table(nameof(Contact))]
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        [NotNull, MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        public bool IsValid()
        {
            return (!String.IsNullOrWhiteSpace(Name));
        }
    }
}
