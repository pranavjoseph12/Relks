﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;
using WebAPI.Models;
using System.Data;

namespace WebAPI.BusinessLayer
{
    public class UserBusinessActions
    {
        public bool AddUser(string name, string phone, string email, string password, string role, string addedBy, string allowAssignTask, string center)
        {
            UserDataAccess obj = new UserDataAccess();
            return obj.AddUser(name, phone, email, password, role, addedBy, allowAssignTask, center);
        }

        public List<UserDetailsModel> GetAllUsers()
        {
            UserDataAccess obj = new UserDataAccess();
            var result = obj.GetAllUsers();
            return ConvertDataTableToUserList(result);
        }

        public List<UserDetailsModel> GetAllUsersForAssignment()
        {
            UserDataAccess obj = new UserDataAccess();
            var result = obj.GetAllUsersForAssignment();
            return ConvertDataTableToUserList(result);
        }

        #region Get User Details By Id

        public DataTable GetUserDetailsById(int id)
        {
            UserDataAccess obj = new UserDataAccess();
            return obj.GetUserDetailsById(id);
        }

        #endregion

        #region Update Details By Id

        public bool UpdateUserDetailsById(int id, string name, string email, string phone, string password)
        {
            UserDataAccess obj = new UserDataAccess();
            return obj.UpdateUserDetailsById(id, name, email, phone, password);
        }

        #endregion

        #region Delete User

        public bool DeleteUser(string userId, int updatedBy)
        {
            UserDataAccess obj = new UserDataAccess();
            return obj.DeleteUser(userId, updatedBy);
        }

        #endregion

        public List<UserDetailsModel> ConvertDataTableToUserList(DataTable dt)
        {
            List<UserDetailsModel> result = new List<UserDetailsModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserDetailsModel obj = new UserDetailsModel();
                obj.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                obj.UserName = dt.Rows[i]["Name"].ToString();
                obj.Email = dt.Rows[i]["Email"].ToString();
                obj.Phone = dt.Rows[i]["Mobile"].ToString();
                result.Add(obj);
            }

            return result;
        }
    }
}
