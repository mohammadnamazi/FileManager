using FileManager.DBAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.DBAccess.Context
{
    public class FileManagerContext : DbContext
    {
        public FileManagerContext(DbContextOptions<FileManagerContext> options)
           : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
