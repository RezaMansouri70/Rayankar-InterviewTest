using DomainClass.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.SqlServer.EfConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {


            builder.Property(cb => cb.FirstName)
              .IsRequired()
               .HasMaxLength(200);

            builder.Property(cb => cb.LastName)
             .IsRequired()
              .HasMaxLength(200);

            builder.Property(cb => cb.Email)
             .IsRequired()
              .HasMaxLength(200);


            builder.Property(cb => cb.BankAccountNumber)
             .IsRequired()
              .HasMaxLength(200);

        }
    }
}
