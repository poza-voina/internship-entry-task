using InternshipEntryTask.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternshipEntryTask.Infrastructure.Configurations;

/// <summary>
/// Конфигурация для <see cref="MoveModel"/>
/// </summary>
public class MoveConfiguration : IEntityTypeConfiguration<MoveModel>
{
    public void Configure(EntityTypeBuilder<MoveModel> builder)
    {
        builder
            .ToTable("moves");

        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.AccessKeyPlayer)
            .HasColumnName("access_key_player")
            .IsRequired();

        builder
            .Property(x => x.GameId)
            .HasColumnName("game_id")
            .IsRequired();

        builder
            .Property(x => x.CellValue)
            .HasColumnName("cell_value")
            .IsRequired();

        builder
            .Property(x => x.Row)
            .HasColumnName("row")
            .IsRequired();

        builder
            .Property(x => x.Column)
            .HasColumnName("column")
            .IsRequired();

        builder
            .Property(x => x.MoveIndex)
            .HasColumnName("move_index")
            .IsRequired();
    }
}