namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e =>
            new { e.UserId, e.BankAccountId, e.CreditCardId })
            .IsUnique();

            builder.Property(e => e.Type)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.PaymentMethods)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(e => e.CreditCard)
           .WithOne(cc => cc.PaymentMethod)
           .HasForeignKey<PaymentMethod>(e => e.CreditCardId);

            builder.HasOne(e => e.BankAccount)
            .WithOne(ba => ba.PaymentMethod)
            .HasForeignKey<PaymentMethod>(e => e.BankAccountId);
        }
    }
}
