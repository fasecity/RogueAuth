using RogueIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RogueIdentity.Interfaces
{
    interface IMyUserRepository
    {
        IEnumerable<ApplicationUser> GetAllPeople();

        ApplicationUser GetMyUser(string userId);

        void AddMyUser(ApplicationUser newPerson);

        void UpdateMyUser(string id, ApplicationUser userToUpdate);

        void DeleteMyUser(string id, ApplicationUser userToDelete);

        void UpdateMyUsersList(IEnumerable<ApplicationUser> updatedUsersList);




    }
}
