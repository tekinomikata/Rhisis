﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Rhisis.Database.Entities;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Rhisis.Database.Context
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<DbUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the characters.
        /// </summary>
        public DbSet<DbCharacter> Characters { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public DbSet<DbItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the mails.
        /// </summary>
        public DbSet<DbMail> Mails { get; set; }

        /// <summary>
        /// Gets or sets the taskbar shortcuts.
        /// </summary>
        public DbSet<DbShortcut> TaskbarShortcuts { get; set; }

        /// <summary>
        /// Gets or sets the quests.
        /// </summary>
        public DbSet<DbQuest> Quests { get; set; }

        /// <summary>
        /// Create a new <see cref="DatabaseContext"/> instance.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public DatabaseContext(DbContextOptions options, DatabaseConfiguration configuration)
            : base(options)
        {
            if (string.IsNullOrEmpty(configuration.EncryptionKey))
                return;

            _encryptionProvider = new AesProvider(
                Convert.FromBase64String(configuration.EncryptionKey), 
                Enumerable.Repeat<byte>(0, 16).ToArray(), 
                CipherMode.CBC, 
                PaddingMode.Zeros);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_encryptionProvider != null)
                modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.Entity<DbUser>()
                .HasIndex(c => new { c.Username, c.Email })
                .IsUnique();
            modelBuilder.Entity<DbCharacter>()
                .HasMany(x => x.ReceivedMails).WithOne(x => x.Receiver);
            modelBuilder.Entity<DbCharacter>()
                .HasMany(x => x.SentMails).WithOne(x => x.Sender);

            // Configure quest entity
            modelBuilder.Entity<DbQuest>()
                .HasIndex(x => new { x.QuestId, x.CharacterId })
                .IsUnique();
        }

        /// <summary>
        /// Migrates the database schema.
        /// </summary>
        public void Migrate() => Database.Migrate();

        /// <summary>
        /// Check if the database exists.
        /// </summary>
        /// <returns></returns>
        public bool DatabaseExists() => (this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
    }
}
