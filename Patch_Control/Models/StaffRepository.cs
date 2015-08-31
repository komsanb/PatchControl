using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSMySQL.POSControl;
using MySql.Data.MySqlClient;
using System.Data;

namespace Patch_Control.Models
{
    public class StaffRepository : IStaffRepository
    {
        CDBUtil objDB = new CDBUtil();
        MySqlConnection objConn = new MySqlConnection();


        public IEnumerable <Staff> GetStaffAll()
        {
            objConn = objDB.EstablishConnection();
            List<Staff> staff = new List<Staff>();          
            string strSQL = "SELECT *, CONCAT(s.StaffsFirstname,' ', s.StaffsLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffsID INNER JOIN Provinces p ON p.ProvinceID = s.StaffsID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1;";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Staff staffData = new Staff();

                    staffData.StaffRole = dt.Rows[i]["StaffRoleName"].ToString();
                    staffData.StaffRoleID = Convert.ToInt32(dt.Rows[i]["StaffRoleID"].ToString());
                    staffData.StaffName = dt.Rows[i]["NameStaff"].ToString();
                    staffData.StaffFirstname = dt.Rows[i]["StaffsFirstname"].ToString();
                    staffData.StaffLastname = dt.Rows[i]["StaffsLastname"].ToString();
                    staffData.StaffCode = Convert.ToInt32(dt.Rows[i]["StaffsCode"].ToString());
                    staffData.Gender = dt.Rows[i]["GenderName"].ToString();
                    staffData.Address1 = dt.Rows[i]["StaffsAddress1"].ToString();
                    staffData.Address2 = dt.Rows[i]["StaffsAddress2"].ToString();
                    staffData.City = dt.Rows[i]["StaffsCity"].ToString();
                    staffData.Zipcode = dt.Rows[i]["StaffsZipcode"].ToString();
                    staffData.Province = dt.Rows[i]["ProvinceName"].ToString();
                    staffData.Telephone = dt.Rows[i]["StaffsTel"].ToString();
                    staffData.Mobile = dt.Rows[i]["StaffsMobile"].ToString();
                    staffData.Picture = dt.Rows[i]["StaffsPictureName"].ToString();
                    staffData.Email = dt.Rows[i]["StaffEmail"].ToString();
                    staff.Add(staffData);
                }
            }
            
            return staff.ToArray();

        }

        public IEnumerable<StaffRole> GetStaffRoleAll()
        {
            objConn = objDB.EstablishConnection();
            List<StaffRole> staff = new List<StaffRole>();
            string strSQL = "SELECT * FROM staffrole";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StaffRole staffData = new StaffRole();
                    staffData.StaffRoleID = Convert.ToInt32(dt.Rows[i]["StaffRoleID"].ToString());
                    staffData.StaffRoleName = dt.Rows[i]["StaffRoleName"].ToString();
                    
                    staff.Add(staffData);
                }
            }

            return staff.ToArray();

        }

    }
}