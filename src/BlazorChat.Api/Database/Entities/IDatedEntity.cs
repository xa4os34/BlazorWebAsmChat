namespace BlazorChat.Api.Database.Entities;

public interface IDatedEntity 
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool IsModified => ModifiedAt is not null;
}
