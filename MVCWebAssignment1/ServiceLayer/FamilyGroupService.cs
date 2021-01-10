using MVCWebAssignment1.DAL;
using MVCWebAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebAssignment1.ServiceLayer
{
    public class FamilyGroupService
    {
        private IFamilyGroupRepository _familyGroupRepository;
        private ApplicationDbContext _applicationUserContext;

        public FamilyGroupService()
        {
            _familyGroupRepository = new FamilyGroupRepository(new FamilyGroupContext());
            _applicationUserContext = new ApplicationDbContext();
        }

        public FamilyGroupService(IFamilyGroupRepository familyGroupRepository)
        {
            this._familyGroupRepository = familyGroupRepository;
        }

        public List<FamilyGroup> GetIndex()
        {
            return _familyGroupRepository.GetFamilyGroups().ToList();
        }

        public FamilyGroupViewModel GetDetails(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Expected integer");
            }


            //Create View Model
            FamilyGroupViewModel familyGroupViewModel = new FamilyGroupViewModel();

            //Add object of family group to view model
            familyGroupViewModel.FamilyGroup = _familyGroupRepository.GetFamilyGroupById(id);
            var groupId = familyGroupViewModel.FamilyGroup.FamilyGroupId;

            if (familyGroupViewModel.FamilyGroup == null)
            {
                throw new HttpException("Family group not found.");
            }

            IList<ApplicationUser> familyGroupMembers = new List<ApplicationUser>();

            if (_applicationUserContext != null)
            {
                //Add list of familygroup members to model
                foreach (var user in _applicationUserContext.Users.ToList())
                {
                    if (user.FamilyGroupId > 0)
                    {
                        if (user.FamilyGroupId == groupId)
                        {
                            familyGroupMembers.Add(user);
                        }
                    }
                }

                familyGroupViewModel.FamilyGroupMembers = familyGroupMembers;
            }


            return familyGroupViewModel;
        }

        public ServiceResponse CreateAction(FamilyGroup familyGroup)
        {
            _familyGroupRepository.InsertFamilyGroup(familyGroup);
            _familyGroupRepository.Save();

            return new ServiceResponse { Result = true };
        }

        public void Dispose()
        {
            _familyGroupRepository.Dispose();
        }
    }

}