namespace DotnetAPI.Dtos
{
    public partial class UserForRegisterationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }


        public UserForRegisterationDto()
        {
            if (FirstName == null)
            {
                FirstName = string.Empty;
            }

            if (LastName == null)
            {
                LastName = string.Empty;
            }

            if (Gender == null)
            {
                Gender = string.Empty;
            }

            if (Email == null)
            {
                Email = "";
            }

            if (Password == null)
            {
                Password = "";
            }

            if (ConfirmPassword == null)
            {
                ConfirmPassword = "";
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