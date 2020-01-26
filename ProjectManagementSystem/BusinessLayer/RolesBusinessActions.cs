using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DatabaseLayer;
using WebAPI.Models;

namespace WebAPI.BusinessLayer
{
    public class RolesBusinessActions
    {
        #region Create New Role

        public bool CreateRole(string roleName, bool enquiryViewAccess, bool enquiryEditAccess, bool enquiryDeleteAccess,
            bool customerViewAccess, bool customerEditAccess, bool customerDeleteAccess, bool projectViewAccess, bool projectEditAccess, bool projectDeleteAccess,
            bool otherActivityViewAccess, bool otherActivityEditAccess, bool otherActivityDeleteAccess,
            bool taskUpdateViewAccess, bool taskUpdateEditAccess, bool taskUpdateDeleteAccess,
            bool adminViewAccess, bool adminEditAccess, bool adminDeleteAccess, bool expenseViewAccess, bool expenseEditAccess, bool expenseDeleteAccess,
            bool rolesViewAccess, bool rolesEditAccess, bool rolesDeleteAccess, bool reportsViewAccess, bool reportsEditAccess, bool reportsDeleteAccess,
            bool assignUserToProjView, bool assignUserToProjEdit, bool assignUserToProjDelete,
            bool incomeView, bool incomeEdit, bool incomeDelete,
            bool quotationView, bool quotationEdit, bool quotationDelete,
            int addedBy)
        {
            RolesDataAccess obj = new RolesDataAccess();
            return obj.CreateRole(roleName, enquiryViewAccess, enquiryEditAccess, enquiryDeleteAccess,
            customerViewAccess, customerEditAccess, customerDeleteAccess, projectViewAccess, projectEditAccess, projectDeleteAccess,
            otherActivityViewAccess, otherActivityEditAccess, otherActivityDeleteAccess,
            taskUpdateViewAccess, taskUpdateEditAccess, taskUpdateDeleteAccess,
            adminViewAccess, adminEditAccess, adminDeleteAccess, expenseViewAccess, expenseEditAccess, expenseDeleteAccess,
            rolesViewAccess, rolesEditAccess, rolesDeleteAccess, reportsViewAccess, reportsEditAccess, reportsDeleteAccess,
            assignUserToProjView, assignUserToProjEdit, assignUserToProjDelete,incomeView,incomeEdit,incomeDelete,quotationView,quotationEdit,quotationDelete,
            addedBy);

        }

        #endregion

        #region Update Role

        public bool UpdateRole(string roleId, string roleName, bool enquiryViewAccess, bool enquiryEditAccess, bool enquiryDeleteAccess,
            bool customerViewAccess, bool customerEditAccess, bool customerDeleteAccess, bool projectViewAccess, bool projectEditAccess, bool projectDeleteAccess,
            bool otherActivityViewAccess, bool otherActivityEditAccess, bool otherActivityDeleteAccess,
            bool taskUpdateViewAccess, bool taskUpdateEditAccess, bool taskUpdateDeleteAccess,
            bool adminViewAccess, bool adminEditAccess, bool adminDeleteAccess, bool expenseViewAccess, bool expenseEditAccess, bool expenseDeleteAccess,
            bool rolesViewAccess, bool rolesEditAccess, bool rolesDeleteAccess, bool reportsViewAccess, bool reportsEditAccess, bool reportsDeleteAccess,
            bool assignUserToProjView, bool assignUserToProjEdit, bool assignUserToProjDelete,
             bool incomeView, bool incomeEdit, bool incomeDelete,
            bool quotationView, bool quotationEdit, bool quotationDelete,
            int addedBy)
        {
            RolesDataAccess obj = new RolesDataAccess();
            return obj.UpdateRole(roleId, roleName, enquiryViewAccess, enquiryEditAccess, enquiryDeleteAccess,
            customerViewAccess, customerEditAccess, customerDeleteAccess, projectViewAccess, projectEditAccess, projectDeleteAccess,
            otherActivityViewAccess, otherActivityEditAccess, otherActivityDeleteAccess,
            taskUpdateViewAccess, taskUpdateEditAccess, taskUpdateDeleteAccess,
            adminViewAccess, adminEditAccess, adminDeleteAccess, expenseViewAccess, expenseEditAccess, expenseDeleteAccess,
            rolesViewAccess, rolesEditAccess, rolesDeleteAccess, reportsViewAccess, reportsEditAccess, reportsDeleteAccess,
            assignUserToProjView, assignUserToProjEdit, assignUserToProjDelete,
            incomeView, incomeEdit, incomeDelete, quotationView, quotationEdit, quotationDelete,
            addedBy);

        }

        #endregion

        #region Get Role Name And Id

        public List<RolesModel> GetAllRoleNameAndId()
        {
            RolesDataAccess obj = new RolesDataAccess();
            var result = obj.GetAllRoleNameAndId();
            return MapRoleNamesAndId(result);
        }

        private List<RolesModel> MapRoleNamesAndId(DataTable dt)
        {
            List<RolesModel> result = new List<RolesModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new RolesModel();
                    obj.RoleID = Convert.ToInt32(dr["RoleID"]);
                    obj.RoleName = dr["RoleName"].ToString();
                    result.Add(obj);
                }
            }

            return result;





        }

        #endregion

        #region Get Role Details By User Id

        public List<RolePermissionModel> GetRoleDetailsByUserId(string userId)
        {
            RolesDataAccess obj = new RolesDataAccess();
            var result = obj.GetRoleDetailsByUserId(userId);
            return MapRoleDetails(result);
        }

        #endregion

        #region Get Role Details By Role Id

        public RoleModel GetRoleDetailsByRoleId(string roleId)
        {
            RolesDataAccess obj = new RolesDataAccess();
            var result = obj.GetRoleDetailsByRoleId(roleId);
            return MapRolesDetails(result);
        }

        #endregion

        #region Delete Role

        public bool DeleteRole(string roleId, int addedBy)
        {
            RolesDataAccess obj = new RolesDataAccess();
            return obj.DeleteRole(roleId, addedBy);
        }

        #endregion

        private List<RolePermissionModel> MapRoleDetails(DataTable dt)
        {
            List<RolePermissionModel> result = new List<RolePermissionModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new RolePermissionModel();
                    obj.ActionName = dr["RoleTypeName"].ToString();
                    obj.ViewAccess = dr["IsViewAccess"].ToString();
                    obj.EditAccess = dr["IsEditAccess"].ToString();
                    obj.DeleteAccess = dr["IsDeleteAccess"].ToString();
                    result.Add(obj);
                }
            }

            return result;
        }

        private RoleModel MapRolesDetails(DataTable dt)
        {
            RoleModel output = new RoleModel();
            List<RolePermissionModel> result = new List<RolePermissionModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    output.RoleName = dr["RoleName"].ToString();
                    var obj = new RolePermissionModel();
                    obj.ActionName = dr["RoleTypeName"].ToString();
                    obj.ViewAccess = dr["IsViewAccess"].ToString();
                    obj.EditAccess = dr["IsEditAccess"].ToString();
                    obj.DeleteAccess = dr["IsDeleteAccess"].ToString();
                    result.Add(obj);
                }

                output.RolePermissions = result;
            }

            return output;
        }

        #region Get User detailsby Id



        #endregion

    }
}
