namespace DotnetAPI.Dtos
{
    public partial class PostToEditDto
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }



        public PostToEditDto()
        {
            if (PostTitle == null)
            {
                PostTitle = "";
            }
            if (PostContent == null)
            {
                PostContent = "";
            }
        }
    }
}
//sirf posrt content and tittle chaiye baki sab auto generate hoga database me implicitly
//without hume kuch karne ki jarurat nahi hai