namespace TimeRegistrationDemo.Services.Dtos.Shared
{
    public class RoleDto
    {
        public string Id { get; }
        public string Description { get; }

        public RoleDto(string id, string description)
        {
            Id = id;
            Description = description;
        }        
    }
}
