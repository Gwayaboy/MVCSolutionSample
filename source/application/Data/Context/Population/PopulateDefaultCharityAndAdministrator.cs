using System;
using Intrigma.DonorSpace.Core.Domain;
using Intrigma.DonorSpace.Services;

namespace Intrigma.DonorSpace.Infrastructure.Data.EF.Context.Population
{
    public class PopulateCharityAndAdministrator : DataContextPopulator
    {
        private readonly Func<string> _connectionNameProvider;
        public override int ExecutionPriority { get { return 1; } }
        
        public static string DefaultAdministratorUserName = "testAdministrator@gmail.com";
        public static string DefaultAdministratorPassword = "passW0rd";

        public PopulateCharityAndAdministrator(Func<string> connectionNameProvider)
        {
            _connectionNameProvider = connectionNameProvider;
        }

        public override void Populate(DataContext context)
        {
            var defaultAdministrator = CreateDefaultAdministrator();

            CreateAndSaveDefaultCharity(defaultAdministrator, context);

            CreateDefaultAdministratorAccount();
        }

        private void CreateDefaultAdministratorAccount()
        {
            _connectionNameProvider().InitializeWebSecurityFor<Administrator>(x => x.UserName);
            DefaultAdministratorUserName.CreateAccount(DefaultAdministratorPassword);
        }

        private static Administrator CreateDefaultAdministrator()
        {
            var defaultAdministrator =
                new Administrator
                    {
                        UserName = DefaultAdministratorUserName,
                        FullName = "This is a test administrator"
                    };

            defaultAdministrator.AddRole(Role.Administrator);

            return defaultAdministrator;
        }

        private static void CreateAndSaveDefaultCharity(Administrator defaultAdministrator, DataContext context)
        {
            var defaultCharity = new Charity
                {
                    SiteName = "My Charity",
                    Description = "This is a test charity"
                };

            defaultCharity.AddAdministrators(defaultAdministrator);

            context.Add(defaultCharity);
            context.SaveChanges();
        }
    }
}