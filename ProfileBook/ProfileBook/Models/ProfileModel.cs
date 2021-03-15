using System;
using SQLite;

namespace ProfileBook.Models
{
    [Table("Profiles")]
    public class ProfileModel : IEntityBase
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public string ProfileImagePath { get; set; }

        public string NickName { get; set; }

        public string Name { get; set; }

        public DateTime InsertionTime { get; set; }
    }
}
