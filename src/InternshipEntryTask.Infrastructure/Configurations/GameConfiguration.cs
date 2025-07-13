using InternshipEntryTask.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternshipEntryTask.Infrastructure.Configurations;

/// <summary>
/// Конфигурация для <see cref="GameModel"/>
/// </summary>
public class GameConfiguration : IEntityTypeConfiguration<GameModel>
{
    public void Configure(EntityTypeBuilder<GameModel> builder)
    {
        builder
            .ToTable("games");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(x => x.AccessKeyPlayerX)
            .HasColumnName("anonymous_player_x_id");

        builder
            .Property(x => x.AccessKeyPlayerO)
            .HasColumnName("anonymous_player_o_id");

        builder
            .Property(x => x.NextMove)
            .HasColumnName("next_move")
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired();

        builder
            .Property(x => x.Width)
            .HasColumnName("width")
            .IsRequired();

        builder
            .Property(x => x.Height)
            .HasColumnName("height");

        builder
            .Property(x => x.JoinKey)
            .HasColumnName("join_key");

        ConfigureRelations(builder);
        ConfigureIndexes(builder);
    }

    private void ConfigureIndexes(EntityTypeBuilder<GameModel> builder)
    {
        builder
            .HasIndex(x => x.AccessKeyPlayerO)
            .IsUnique();

        builder
            .HasIndex(x => x.AccessKeyPlayerX)
            .IsUnique();

        builder
            .HasIndex(x => x.JoinKey)
            .IsUnique();
    }

    private void ConfigureRelations(EntityTypeBuilder<GameModel> builder)
    {
        builder
            .HasMany(x => x.Moves)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId);
    }
}