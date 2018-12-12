using FluentNHibernate.Mapping;
using Microsoft.AspNet.Identity;

namespace hackathonishbd.Models.Identity
{
    public class User : IUser<int>
    {
        public virtual int Id { get; protected set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }

        public class Map : ClassMap<User>
        {
            public Map()
            {
                Table("T_usuarios");
                Id(x => x.Id).Column("ID_usuario");
                Map(x => x.UserName).Column("Nombre");
                Map(x => x.Password).Column("Clave");
            }

        }
    }
}