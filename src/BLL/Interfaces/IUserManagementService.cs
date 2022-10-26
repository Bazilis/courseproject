namespace BLL.Interfaces
{
    public interface IUserManagementService
    {
        Task<bool> IsLogOutBlockUsers(string[] usersNames, string currentUserName);
        Task UnblockUsers(string[] usersNames);
        Task<bool> IsLogOutDeleteUsers(string[] usersNames, string currentUserName);
        Task AddUsersToRoleAdmin(string[] usersNames);
        Task AddUsersToRoleUser(string[] usersNames);
        Task<bool> IsLogOutRemoveUsersFromRoleAdmin(string[] usersNames, string currentUserName);
        Task RemoveUsersFromRoleUser(string[] usersNames);
    }
}
