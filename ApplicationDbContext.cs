﻿using Microsoft.EntityFrameworkCore;
using HospitalApi.Models;

namespace HospitalApi
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
