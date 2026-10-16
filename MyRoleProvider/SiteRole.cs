using EcoLeafPlates.Models;
using EcoLeafPlates.Models.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EcoLeafPlates.MyRoleProvider
{
    public class SiteRole:RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string phoneno)
        {
            List<clsUserDetails> lstUserDetails = UserManager.displayUserDetails().ToList();

            //List<RegisterVM> regVM = RegisterManager.getRegisteredEntries().ToList();
            string data = lstUserDetails.Where(x => x.phoneNo.ToString() == phoneno).FirstOrDefault().Role;
            int userId = lstUserDetails.Where(x => x.phoneNo.ToString() == phoneno).FirstOrDefault().userId;

            HttpContext.Current.Session["userId"] = userId;
            HttpContext.Current.Session["userRole"] = data;

            string[] result = { data };
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}