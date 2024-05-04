namespace MovieApi.DTO.ResponseTO
{
    public class UserResponseTo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public bool IsAdmin {  get; set; }
        
        public UserResponseTo()
        {

        }
    }
}
