namespace Models
{
    public class Users
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }

        public class usersdbcontext : dbcontext
        {
            public dbset<users> users { get; set; }
        }
    }
}
