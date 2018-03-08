﻿using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context.ConfigEntity
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityType)
        {
            entityType.HasKey(f => f.Id);

            entityType.Property(f => f.Email)
                .IsRequired()
                .HasMaxLength(30);

            entityType.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            entityType.Property(f => f.FullName)
                .HasMaxLength(100);

            entityType.Property(f => f.NickName)
                .HasMaxLength(100);

            entityType.Property(f => f.Registration)
                .IsRequired();

            entityType.Property(f => f.PasswordHash)
               .IsRequired();

            entityType.Property(f => f.PasswordSalt)
               .IsRequired();
        }
    }
}