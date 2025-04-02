namespace jwm_photography_api.Helper;

public class Enums
{
    public enum ErrorType
    {
        Error,
        Information
    }

    public enum PhotoOrientation
    {
        landscape,
        portrait,
        square
    }

    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        User
    }
}