using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entities;

namespace Auth.Data;

public static class ModelBuilderConfig
{
	public static void OnModelCreatingBuilder(this ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Device>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.DisplayName).HasMaxLength(200);
			entity.Property(e => e.StatusDescription).HasMaxLength(500);
			entity.Navigation(o => o.Sensors).AutoInclude();

			entity.ComplexProperty(p => p.Coordinate);
		});

		modelBuilder.Entity<Sensor>(entity =>
		{
			entity.Property(e => e.Id).HasMaxLength(200);
			entity.Property(e => e.DisplayName).HasMaxLength(200);
			entity.HasOne(e => e.Device).WithMany(e => e.Sensors).HasForeignKey(e => e.DeviceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
			entity.HasOne(e => e.Group).WithMany(e => e.Sensors).HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.SetNull);
			entity.Navigation(o => o.Group).AutoInclude();
			entity.Property(e => e.StatusDescription).HasMaxLength(500);

			entity.ComplexProperty(p => p.Coordinate);
		});

		modelBuilder.Entity<SensorGroup>(entity =>
		{
			entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
			entity.Navigation(o => o.Sensors).AutoInclude();
		});

		modelBuilder.Entity<Log_IR>(entity =>
		{
			entity.HasOne(e => e.Sensor).WithMany(e => e.Log_IRs).HasForeignKey(e => e.SensorId).IsRequired();

			entity.ComplexProperty(p => p.SensorCoordinate);
		});
	}
}
