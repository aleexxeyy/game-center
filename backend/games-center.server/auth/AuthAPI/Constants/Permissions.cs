namespace AuthAPI.Constants;

public static class Permissions
{
    public const string CanViewUsers = "CAN_VIEW_USERS";
    public const string CanManageUsers = "CAN_MANAGE_USERS";
    
    // Метод для получения всех permissions
    public static IEnumerable<string> GetAll()
    {
        yield return CanViewUsers;
        yield return CanManageUsers;
    }
}