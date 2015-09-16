using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using POSMySQL.POSControl;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Configuration;

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
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 AND s.Deleted = 0 ORDER BY StaffID;";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Staff staffData = new Staff();

                    staffData.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"].ToString());
                    staffData.StaffRoleName = dt.Rows[i]["StaffRoleName"].ToString();
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

        public Staff GetStaff(int id)
        {
            objConn = objDB.EstablishConnection();
            Staff staffData = new Staff();
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 AND s.Deleted = 0 AND StaffID = " + id + " ORDER BY StaffID;";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();

            staffData.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
            staffData.StaffRoleName = dt.Rows[0]["StaffRoleName"].ToString();
            staffData.StaffPassword = dt.Rows[0]["StaffPassword"].ToString();
            staffData.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
            staffData.StaffFirstname = dt.Rows[0]["StaffFirstname"].ToString();
            staffData.StaffLastname = dt.Rows[0]["StaffLastname"].ToString();
            staffData.StaffName = dt.Rows[0]["NameStaff"].ToString();
            staffData.StaffCode = dt.Rows[0]["StaffCode"].ToString();
            staffData.Gender = dt.Rows[0]["GenderName"].ToString();
            staffData.GenderID = Convert.ToInt32(dt.Rows[0]["GenderID"].ToString());
            staffData.Address1 = dt.Rows[0]["StaffAddress1"].ToString();
            staffData.Address2 = dt.Rows[0]["StaffAddress2"].ToString();
            staffData.City = dt.Rows[0]["StaffCity"].ToString();
            staffData.Province = dt.Rows[0]["ProvinceName"].ToString();
            staffData.ProvinceID = Convert.ToInt32(dt.Rows[0]["ProvinceID"].ToString());
            staffData.Zipcode = dt.Rows[0]["StaffZipcode"].ToString();
            staffData.Telephone = dt.Rows[0]["StaffTel"].ToString();
            staffData.Mobile = dt.Rows[0]["StaffMobile"].ToString();
            staffData.Picture = dt.Rows[0]["StaffPictureName"].ToString();
            staffData.Email = dt.Rows[0]["StaffEmail"].ToString();

            return staffData;
        }

        public IEnumerable<Staff> PostStaffAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            List<Staff> staff = new List<Staff>();

            int rowid;

            string strSQL1 = "SELECT MAX(StaffID) AS rowid FROM staffs ;";
            DataTable dt = objDB.List(strSQL1, objConn);
            rowid = Convert.ToInt32(dt.Rows[0]["rowid"].ToString());
            int maxid = rowid + 1;
            string strSQL2 = "INSERT INTO staffs(StaffID, StaffCode, StaffPassword, StaffRoleID, GenderID, StaffFirstname, StaffLastname, StaffAddress1, StaffAddress2, StaffCity, StaffZipcode, StaffTel, StaffMobile, StaffEmail, ProvinceID) ";
            strSQL2 += "VALUES ('" + maxid + "','" + item.StaffCode + "','" + item.StaffPassword + "','" + item.StaffRoleID + "','" + item.GenderID + "','" + item.StaffFirstname + "','" + item.StaffLastname + "','" + item.Address1 + "','" + item.Address2 + "','" + item.City + "','" + item.Zipcode + "','" + item.Telephone + "','" + item.Mobile + "','" + item.Email + "','" + item.ProvinceID + "')";
            //strSQL2 += "VALUES (" + maxid + ",'" + item.StaffCode + "','" + item.StaffPassword + "'," + item.StaffRoleID + "," + item.GenderID + ",'" + item.StaffFirstname + "','" + item.StaffLastname + "','" + item.Address1 + "','" + item.Address2 + "','" + item.City + "','" + item.Zipcode + "','" + item.Telephone + "','" + item.Mobile + "','" + item.Picture + "','" + item.Email + "'," + item.ProvinceID + ")";
            objDB.sqlExecute(strSQL2, objConn);
            objConn.Close();

            return staff.ToArray();
        }

        public IEnumerable<Staff> PostStaffEditAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            List<Staff> staffedit = new List<Staff>();

            string strSQL = "UPDATE staffs SET StaffRoleID = '" + item.StaffRoleID + "', StaffCode = '" + item.StaffCode + "', GenderID = '" + item.GenderID + "', StaffFirstname = '" + item.StaffFirstname + "', StaffLastname = '" + item.StaffLastname + "', StaffAddress1 = '" + item.Address1 + "', StaffAddress2 = '" + item.Address2 + "', StaffCity = '" + item.City.ToString() + "', StaffZipcode = '" + item.Zipcode + "', StaffTel = '" + item.Telephone + "', StaffMobile = '" + item.Mobile + "', StaffEmail = '" + item.Email + "', ProvinceID = '" + item.ProvinceID + "'";
            strSQL += "WHERE StaffID = '" + item.StaffID + "';";
            objDB.sqlExecute(strSQL, objConn);
            objConn.Close();

            return staffedit;
        }

        public IEnumerable<Staff> PostStaffDeleteAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            List<Staff> staffdelete = new List<Staff>();

            string strSQL = "UPDATE staffs SET Deleted = '" + item.Deleted + "'";
            strSQL += "WHERE StaffID = '" + item.StaffID + "';";
            objDB.sqlExecute(strSQL, objConn);
            objConn.Close();

            return staffdelete;
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

        public StaffRole GetStaffRoleAll(int id)
        {
            objConn = objDB.EstablishConnection();
            StaffRole staffrole = new StaffRole();
            string strSQL = "SELECT * FROM staffrole sr INNER JOIN staffaccess sa ON sa.StaffRoleID = sr.StaffRoleID WHERE sr.StaffRoleID = '" + id + "'";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();

            staffrole.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
            staffrole.StaffRoleName = dt.Rows[0]["StaffRoleName"].ToString();

            return staffrole;
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

        public IEnumerable<PermissionItemdata> GetpermissionItemdataAll()
        {
            objConn = objDB.EstablishConnection();
            List<PermissionItemdata> permissionItem = new List<PermissionItemdata>();
            string strSQL = "SELECT * FROM permissionitems";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PermissionItemdata PermissionItemdataData = new PermissionItemdata();
                    PermissionItemdataData.PermissionItemID = Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                    PermissionItemdataData.PermissionGroupID = Convert.ToInt32(dt.Rows[i]["PermissionGroupID"].ToString());
                    PermissionItemdataData.PermissionItemUrl = dt.Rows[i]["PermissionItemUrl"].ToString();
                    PermissionItemdataData.PermissionItemName = dt.Rows[i]["PermissionItemName"].ToString();
                    PermissionItemdataData.PermissionItemParent = Convert.ToInt32(dt.Rows[i]["PermissionItemParent"].ToString());
                    PermissionItemdataData.Deleted = Convert.ToInt32(dt.Rows[i]["Deleted"].ToString());

                    permissionItem.Add(PermissionItemdataData);
                }
            }
            return permissionItem.ToArray();
        }

        public IEnumerable<StaffRoleAccess> GetStaffRoleAccessAll(int id)
        {
            objConn = objDB.EstablishConnection();
            List<StaffRoleAccess> staffaccess = new List<StaffRoleAccess>();
            //List<int> permission = new List<int>();
            string strSQL = "SELECT * FROM staffrole sr INNER JOIN staffaccess sa ON sa.StaffRoleID = sr.StaffRoleID WHERE sr.StaffRoleID = '" + id + "'";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();

            StaffRoleAccess access = new StaffRoleAccess();

            access.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
            access.StaffRoleName = dt.Rows[0]["StaffRoleName"].ToString();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                        access.PermissionItemID += Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                    else
                        access.PermissionItemID += ", " + Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                }
            }
            staffaccess.Add(access);

            return staffaccess;
        }

        public IEnumerable<StaffAccess> PostStaffAccessAll(StaffAccess staffaccess)
        {
            objConn = objDB.EstablishConnection();
            List<StaffAccess> staffAccess = new List<StaffAccess>();

            int rowroleid;
            int rowaccessid;
            int i = 0;

            string strSQL1 = "SELECT MAX(StaffRoleID) AS rowroleid FROM staffrole ;";
            DataTable dt = objDB.List(strSQL1, objConn);
            rowroleid = Convert.ToInt32(dt.Rows[0]["rowroleid"].ToString());
            int maxroleid = rowroleid + 1;

            string strSQL2 = "SELECT MAX(StaffAccessID) AS rowaccessid FROM staffaccess ;";
            DataTable dt1 = objDB.List(strSQL2, objConn);
            rowaccessid = Convert.ToInt32(dt1.Rows[0]["rowaccessid"].ToString());
            int maxaccessid = rowaccessid + 1;


            StringBuilder strSQL3 = new StringBuilder();
            strSQL3.Append("BEGIN;");
            strSQL3.Append("INSERT INTO staffrole(StaffRoleID, StaffRoleName) VALUES (" + maxroleid + ",'" + staffaccess.StaffRoleName + "');");

            for (i = 0; i < staffaccess.PermissionItemID.Count; i++)
            {
                strSQL3.Append("INSERT INTO staffaccess(StaffAccessID, StaffRoleID, PermissionItemID) VALUES (" + (maxaccessid + i) + "," + maxroleid + "," + staffaccess.PermissionItemID[i] + ");");
            }

            strSQL3.Append("COMMIT;");
            objDB.sqlExecute(strSQL3.ToString(), objConn);
            objConn.Close();

            return staffAccess.ToArray();
        }

        public IEnumerable<StaffAccess> PostStaffAccessEditAll(StaffAccess staffaccess)
        {
            objConn = objDB.EstablishConnection();
            List<StaffAccess> staffAccess = new List<StaffAccess>();

            int i = 0;

            StringBuilder strSQL3 = new StringBuilder();

            for (i = 0; i < staffaccess.PermissionItemID.Count; i++)
            {
                strSQL3.Append("UPDATE staffaccess, staffrole SET StaffRoleName = '" + staffaccess.StaffRoleName + "', PermissionItemID = '" + staffaccess.PermissionItemID[i] + "'");               
            }
            strSQL3.Append("WHERE staffrole.StaffRoleID = '" + staffaccess.StaffRoleID + "' AND staffaccess.StaffRoleID = '"+ staffaccess.StaffRoleID + "';");
            objDB.sqlExecute(strSQL3.ToString(), objConn);
            objConn.Close();


            return staffAccess.ToArray();
        }

    }
}