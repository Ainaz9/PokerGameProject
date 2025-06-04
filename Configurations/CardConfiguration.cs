using PokerGamesRSF.Models;

namespace PokerGamesRSF.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Rank).IsRequired();
            builder.Property(c => c.Suit).IsRequired();
            builder.HasMany(c => c.PlayerCards)
                   .WithOne(pc => pc.Card)
                   .HasForeignKey(pc => pc.CardId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
