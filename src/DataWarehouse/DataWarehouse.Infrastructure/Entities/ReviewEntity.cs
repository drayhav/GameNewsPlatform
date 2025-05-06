namespace DataWarehouse.Infrastructure.Entities;

public class ReviewEntity
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; }

    public double Rating { get; set; }
}