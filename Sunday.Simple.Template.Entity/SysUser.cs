namespace Sunday.Simple.Template.Entity
{
    public class SysUser : Entity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
    }
}