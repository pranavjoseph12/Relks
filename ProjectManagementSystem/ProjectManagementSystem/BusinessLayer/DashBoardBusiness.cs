using DatabaseLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DashBoardBusiness
    {
        public List<AdminDashBoardModel> GetDashBoardDataForAdmin()
        {
            List<AdminDashBoardModel> objList = new List<AdminDashBoardModel>();
            DashBoardDataAccess objDashboard = new DashBoardDataAccess();
            var result = objDashboard.GetDashBoardDataForAdmin();
            return GetAdminDataAsList(result);
        }

        private List<AdminDashBoardModel> GetAdminDataAsList(DataTable dt)
        {
            List<AdminDashBoardModel> objList = new List<AdminDashBoardModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdminDashBoardModel objAdmin = new AdminDashBoardModel();
                    objAdmin.BranchName = dt.Rows[i]["Name"].ToString();
                    objAdmin.BranchCode = dt.Rows[i]["Name"].ToString();
                   // objAdmin.AmountPending = "000";
                    objAdmin.EnquiryCount = dt.Rows[i]["TotalEnquiry"].ToString();
                    objAdmin.StudentCount = dt.Rows[i]["TotalStudents"].ToString();
                    objList.Add(objAdmin);
                }
            }

            return objList;
        }
        public List<AdminDashBoardModel> GetCenterHeadDashBoard(string centerId)
        {
            List<AdminDashBoardModel> objList = new List<AdminDashBoardModel>();
            DashBoardDataAccess objDashboard = new DashBoardDataAccess();
            var result = objDashboard.GetCenterHeadDashboardData(centerId);
            return GetAdminDataAsList(result);
        }
    }
}
