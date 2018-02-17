using RogueIdentity.Interfaces;
using RogueIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RogueIdentity.AppUserRepo
{
    public class AppUserRepository : IMyUserRepository
    {
        public void AddMyUser(ApplicationUser newPerson)
        {
            throw new NotImplementedException();
        }

        public void DeleteMyUser(string id, ApplicationUser userToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllPeople()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetMyUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateMyUser(string id, ApplicationUser userToUpdate)
        {
            throw new NotImplementedException();
        }

        public void UpdateMyUsersList(IEnumerable<ApplicationUser> updatedUsersList)
        {
            throw new NotImplementedException();
        }
    }
}
