using Microsoft.AspNet.Identity.EntityFramework;

namespace Pandora.BackEnd.Model.AppDomain
{
    public class AppRole : IdentityRole
    {
        public AppRole() { }

        public AppRole(string name, string description) : base(name)
        {
            _description = description;
        }

        private string _description;

        public string Description { get => _description; set => _description = value; }
    }
}
