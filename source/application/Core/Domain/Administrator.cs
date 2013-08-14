using System.Collections.Generic;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain
{
    public class Administrator : Entity
    {
        public virtual string FullName { get; internal protected set; }

        public string UserName { get; internal protected set; }


        internal  ICollection<Role> RoleCollection { get; set; }

        public Administrator()
        {
            RoleCollection = new HashSet<Role>();
        }
       

        public void RemoveRole(Role role)
        {
            RoleCollection.Remove(role);
        }

        public void AddRole(Role role)
        {
            if (role == null || role != Role.Administrator)
            {
                throw new BusinessRuleException("Administrator should only have an administrator role");
            }

            RoleCollection.AddUnique(role);

        }

        public void Update(EditAdministratorCommand editedAdministrator)
        {
            UserName = editedAdministrator.Username;
            FullName = editedAdministrator.FullName;
        }

       
    }
}