using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TheDailyRoutine.Entities;

namespace TheDailyRoutine.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /* public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
         {

         }*/
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) { }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<UserHabit> UserHabits { get; set; }
        public DbSet<HabitProgress> HabitProgresses { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Habit
            modelBuilder.Entity<Habit>()
                .HasMany(h => h.UserHabits)
                .WithOne(uh => uh.Habit)
                .HasForeignKey(uh => uh.HabitId);

            // Configure UserHabit
            modelBuilder.Entity<UserHabit>()
                .HasOne(uh => uh.User)
                .WithMany(u => u.UserHabits)
                .HasForeignKey(uh => uh.UserId);

            modelBuilder.Entity<UserHabit>()
                .HasMany(uh => uh.Progress)
                .WithOne(p => p.UserHabit)
                .HasForeignKey(p => p.UserHabitId);

            // Configure Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
        }
    }
}