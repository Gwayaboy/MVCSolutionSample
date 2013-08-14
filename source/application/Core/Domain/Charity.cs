using System;
using System.Linq;
using System.Collections.Generic;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Core.Commands.Administration;
using Intrigma.DonorSpace.Core.Domain.Base;

namespace Intrigma.DonorSpace.Core.Domain
{
    public class Charity : AggregateRoot
    {
        public Charity()
        {
            AdministratorCollection = new HashSet<Administrator>();
            DonorCollection = new HashSet<Donor>();
        }

        internal ICollection<Administrator> AdministratorCollection { get; set; }
        public virtual IEnumerable<Administrator> Administrators { get { return AdministratorCollection; } }

        public virtual void AddAdministrators(params Administrator[] administrators)
        {
            AdministratorCollection.AddUniquelyMany(administrators);
        }

        internal ICollection<Donor> DonorCollection { get; set; }
        public virtual IEnumerable<Donor> Donors { get { return DonorCollection; } }

        public virtual AuthenticationType AuthenticationType { get; protected set; }

        public virtual string SiteName { get; internal protected set; }

        public virtual string Description { get;  internal protected set; }

        public virtual string WebServiceUrl { get;  internal protected set; }

        public virtual void AddDonors(params Donor[] donors)
        {
            DonorCollection.AddUniquelyMany(donors);
        }
        
        public virtual bool IsAdministrator(string userName)
        {
            return Administrators.Any(a => a.UserName == userName);
        }

        public void UpdateAuthenticationDetails(string webServiceUrl)
        {
            WebServiceUrl = webServiceUrl;
        }

        public void RemoveAdministrator(string userName)
        {
            RemoveAdministrator(GetAdministrator(a => a.UserName == userName));
        }

        public void RemoveAdministrator(Administrator administratorToBeDeleted)
        {
            administratorToBeDeleted.RemoveRole(Role.Administrator);
            AdministratorCollection.Remove(administratorToBeDeleted);
        }

        private Administrator GetAdministrator(Func<Administrator,bool> predicate)
        {
            var administrator = Administrators.SingleOrDefault(predicate);
            if (administrator == null)
            {
                throw new BusinessRuleException("The administrator does not exist");
            }

            return administrator;
        }
        
        public void UpdateAdministrator(EditAdministratorCommand editedAdministrator)
        {
            var administrator = GetAdministrator(a => a.Id == editedAdministrator.Id);
           
            administrator.Update(editedAdministrator);
        }
    }
}