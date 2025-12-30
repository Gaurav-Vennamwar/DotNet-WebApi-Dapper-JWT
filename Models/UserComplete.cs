
namespace DotnetAPI.Models
{
public partial class UserComplete
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public bool Active { get; set; }
    public string JobTitle { get; set; }
    public string Department { get; set; }
   public decimal  Salary { get; set; }
    public decimal AvgSalary { get; set; }

    //string has to be nullable otherwise error aayega
     public UserComplete()
    {
        if (FirstName == null)
        {
            FirstName = string.Empty;
        }

        if (LastName == null)
        {
            LastName = string.Empty;
        }

        if (Email == null)
        {
            Email = string.Empty;
        }
        
        if (Gender == null)
        {
            Gender = string.Empty;
        }

         if (JobTitle == null)
        {
            JobTitle = string.Empty;
        }

        if (Department == null)
        {
            Department = string.Empty;
        }

    }
}
}
