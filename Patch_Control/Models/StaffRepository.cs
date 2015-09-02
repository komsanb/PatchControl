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


        public IEnumerable<Staff> GetStaffAll()
        {

            objConn = objDB.EstablishConnection();
            List<Staff> staff = new List<Staff>();
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 ORDER BY StaffID;";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Staff staffData = new Staff();

                    staffData.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"].ToString());
                    staffData.StaffRole = dt.Rows[i]["StaffRoleName"].ToString();
                    staffData.StaffPassword = dt.Rows[i]["StaffPassword"].ToString();
                    staffData.StaffRoleID = Convert.ToInt32(dt.Rows[i]["StaffRoleID"].ToString());                
                    staffData.StaffFirstname = dt.Rows[i]["StaffFirstname"].ToString();
                    staffData.StaffLastname = dt.Rows[i]["StaffLastname"].ToString();
                    staffData.StaffName = dt.Rows[i]["NameStaff"].ToString();
                    staffData.StaffCode = dt.Rows[i]["StaffCode"].ToString();
                    staffData.Gender = dt.Rows[i]["GenderName"].ToString();
                    staffData.GenderID = Convert.ToInt32(dt.Rows[i]["GenderID"].ToString());
                    staffData.Address1 = dt.Rows[i]["StaffAddress1"].ToString();
                    staffData.Address2 = dt.Rows[i]["StaffAddress2"].ToString();
                    staffData.City = dt.Rows[i]["StaffCity"].ToString();
                    staffData.Province = dt.Rows[i]["ProvinceName"].ToString();
                    staffData.ProvinceID = Convert.ToInt32(dt.Rows[i]["ProvinceID"].ToString());
                    staffData.Zipcode = dt.Rows[i]["StaffZipcode"].ToString();              
                    staffData.Telephone = dt.Rows[i]["StaffTel"].ToString();
                    staffData.Mobile = dt.Rows[i]["StaffMobile"].ToString();
                    staffData.Picture = dt.Rows[i]["StaffPictureName"].ToString();
                    staffData.Email = dt.Rows[i]["StaffEmail"].ToString();
                    staff.Add(staffData);
                }
            }

            return staff.ToArray();

        }

        public IEnumerable<Staff> PostStaffAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            List<Staff> staff = new List<Staff>();
            try {

                int rowid;
                string strSQL1 = "SELECT MAX(StaffID) AS rowid FROM staffs ;";
                DataTable dt =objDB.List(strSQL1, objConn);
                rowid = Convert.ToInt32(dt.Rows[0]["rowid"].ToString());
                int maxid = rowid + 1;
                string strSQL2 = "INSERT INTO staffs(StaffID, StaffCode, StaffPassword, StaffRoleID, GenderID, StaffFirstname, StaffLastname, StaffAddress1, StaffAddress2, StaffCity, StaffZipcode, StaffTel, StaffMobile, StaffPictureName, StaffEmail, ProvinceID) ";
                strSQL2 += "VALUES (" + maxid + ",'" + item.StaffCode.ToString() + "','" + item.StaffPassword.ToString() + "'," + Convert.ToInt32(item.StaffRoleID) + "," + Convert.ToInt32(item.GenderID) + ",'" + item.StaffFirstname.ToString() + "','" + item.StaffLastname.ToString() + "','" + item.Address1.ToString() + "','" + item.Address2.ToString() + "','" + item.City.ToString() + "','" + item.Zipcode.ToString() + "','" + item.Telephone.ToString() + "','" + item.Mobile.ToString() + "','" + item.Picture.ToString() + "','" + item.Email.ToString() + "'," + Convert.ToInt32(item.ProvinceID) + ")";
                objDB.sqlExecute(strSQL2, objConn);
                objConn.Close();

                return staff.ToArray();
            }
            catch (Exception error){

                return staff.ToArray();
            }
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

        public IEnumerable<Province> GetProvinceAll()
        {
            objConn = objDB.EstablishConnection();
            List<Province> staff = new List<Province>();
            string strSQL = "SELECT * FROM provinces WHERE LangID = 1";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Province staffData = new Province();
                    staffData.ProvinceID = Convert.ToInt32(dt.Rows[i]["ProvinceID"].ToString());
                    staffData.ProvinceName = dt.Rows[i]["ProvinceName"].ToString();

                    staff.Add(staffData);
                }
            }

            return staff.ToArray();

        }

        public IEnumerable<Gender> GetGenderAll()
        {
            objConn = objDB.EstablishConnection();
            List<Gender> staff = new List<Gender>();
            string strSQL = "SELECT * FROM gender";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Gender genderData = new Gender();
                    genderData.GenderID = Convert.ToInt32(dt.Rows[i]["GenderID"].ToString());
                    genderData.GenderName = dt.Rows[i]["GenderName"].ToString();

                    staff.Add(genderData);
                }
            }

            return staff.ToArray();

        }

    }
}