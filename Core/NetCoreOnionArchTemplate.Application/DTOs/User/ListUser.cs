namespace NetCoreOnionArchTemplate.Application.DTOs.User
{
	public class ListUser
	{
        public int Id{ get; set; }
        public string Email{ get; set; }
        public string UserName{ get; set; }
        public string NameSurname{ get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
