namespace PokerGamesRSF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Rating).IsRequired();
            builder.Property(u => u.Chips).IsRequired();
            builder.HasMany(u => u.GameSessionsAsPlayer1)
                   .WithOne(gs => gs.Player1)
                   .HasForeignKey(gs => gs.Player1Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.GameSessionsAsPlayer2)
                   .WithOne(gs => gs.Player2)
                   .HasForeignKey(gs => gs.Player2Id)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.PlayerCards)
                   .WithOne(pc => pc.User)
                   .HasForeignKey(pc => pc.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.PlayerActions)
                   .WithOne(pa => pa.User)
                   .HasForeignKey(pa => pa.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.Bets)
                   .WithOne(b => b.User)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
