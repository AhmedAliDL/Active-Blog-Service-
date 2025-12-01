
using Active_Blog_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Active_Blog_Service.Context.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

            builder.HasOne(e => e.Department)
                 .WithMany().HasForeignKey(e => e.DepartmentId);

        }
    }
}
