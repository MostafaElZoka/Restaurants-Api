
namespace Restaurant.Inftastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationlaity";
    public const string LessThan20 = "LessThan20";
    public const string OwnsAtLeast2 = "OwnsAtLeast2";
}

public static class AppClaimsNames
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
    public const string FullName = "FullName";

}
