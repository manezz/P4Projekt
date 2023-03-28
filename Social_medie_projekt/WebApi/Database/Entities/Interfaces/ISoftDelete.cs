namespace WebApi.Database.Entities.Interfaces
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
