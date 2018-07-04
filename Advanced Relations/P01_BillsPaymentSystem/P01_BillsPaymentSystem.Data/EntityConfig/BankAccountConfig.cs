namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Property(e => e.Balance)
                .IsRequired();

            builder.Property(e => e.BankName)
            .HasMaxLength(50)
            .IsUnicode(true)
            .IsRequired();

            builder.Property(e => e.SwiftCode)
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

            builder.Ignore(e => e.PaymentMethodId);
        }
    }
}
