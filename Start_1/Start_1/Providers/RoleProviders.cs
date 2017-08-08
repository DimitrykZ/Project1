using Start_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Start_1.Providers
{
    public class RoleProviders : RoleProvider  //Класс отвечающий за роли  пользователей.
    {
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

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
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

        public override string[] GetRolesForUser(string username) //Метод возвращает название роли
        {
            string[] roles = new string[] { };
            using (StoreContext db = new StoreContext())
            {
                Person user = null;
                user = db.Clients.FirstOrDefault(u => u.Email == username); //поиск роли по е-мейлу среди клеинтов 
                if (user == null)                                           //и если не найдено,то поиск проходит по менеджерам
                    user = db.Managers.FirstOrDefault(u => u.Email==username);

                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null)
                        roles = new string[] { userRole.Name };
                }
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)// метод выдает результат пользователь авторизирован 
        {                                                                   //или нет
            bool outputResult = false;
            using (StoreContext db = new StoreContext())
            {
                Person user = null;
                user = db.Clients.FirstOrDefault(u => u.Email == username);
                if (user == null)
                    user = db.Managers.FirstOrDefault(u => u.Email == username);

                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null && userRole.Name == roleName)
                        outputResult = true; ;
                }
            }
            return outputResult;
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