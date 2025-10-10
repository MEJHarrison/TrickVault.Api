using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrickVault.Api.Constants;
using TrickVault.Api.Models;

namespace TrickVault.Api.Data.Configurations
{
    public class TrickConfiguration : IEntityTypeConfiguration<Trick>
    {
        public void Configure(EntityTypeBuilder<Trick> builder)
        {
            builder.HasIndex(c => c.Title)
                .IsUnique();

            if (TrickVaultDbContext.UseTestData)
            {
                // --- Trick Seed Data (for testing purposes ONLY) ---
                builder.HasData(
                    new Trick
                    {
                        Id = TrickIds.PullRabbit,
                        Title = "Pull Rabbit from Hat",
                        Effect = "The magician shows an empty hat, then pulls a rabbit out of it",
                        Setup = "Put rabbit in hat",
                        Method = "Magician shows an empty hat, then reaches into secret compartment and removes hidden rabbit",
                        Patter = "Look, the hat is completely empty. Except for this here ribbit!",
                        Comments = "This is great with children.",
                        Credits = "Many poor magicians."
                    },
                    new Trick
                    {
                        Id = TrickIds.PickACard,
                        Title = "Pick a Card",
                        Effect = "Magician has audience member select a random card. The card is shuffled back into the deck. Then the magician finds the selected card.",
                        Method = "Spectator selects a card. The card is returned to the deck and the deck is then shuffled. Then through secret means (not given here), the magician is able to find the selected card.",
                        Patter = "Pick a card, any card! Show the audience. Now put it back anywhere in the deck. I'm going to shuffle the cards a few times. Now, I couldn't possibly know the location of your card, right? Yet here it is!"
                    }
                );
            }
        }
    }
}
