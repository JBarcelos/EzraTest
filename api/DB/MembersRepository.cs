using System;
using System.Collections.Generic;
using System.Linq;

using EzraTest.Models;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EzraTest.DB
{
    public class MembersRepository : DbContext, IMembersRepository
    {
        private string _connectionString;
        public DbSet<Member> Members { get; set; }

        public MembersRepository(string connectionString)
        {
            _connectionString = $"Data Source={connectionString}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Member>()
                .Property(e => e.Id)
                .HasConversion(v => v.ToString("N"), v => Guid.Parse(v));
        }

        /// <inheritdoc />
        public IEnumerable<Member> GetMembers()
        {
            return Members;
        }

        /// <inheritdoc />
        public Member GetMember(Guid id)
        {
            return Members.Where(member => member.Id == id).SingleOrDefault();
        }

        /// <inheritdoc />
        public void AddMember(Member member)
        {
            member.Id = Guid.NewGuid();
            Members.Add(member);
            SaveChanges();
        }

        /// <inheritdoc />
        public void UpdateMember(Guid id, Member member)
        {
            Member record = GetMember(id);
            if(record != null)
            {
                record.Name = member.Name;
                record.Email = member.Email;
                SaveChanges();
            }
        }

        /// <inheritdoc />
        public void DeleteMember(Guid id)
        {
            Member record = GetMember(id);
            if(record != null)
            {
                Members.Remove(record);
                SaveChanges();
            }
        }
    }
}