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
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 AND s.Deleted = 0 ORDER BY StaffCode;";
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
            string strSQL2 = "INSERT INTO staffs(StaffID, StaffCode, StaffPassword, StaffRoleID, GenderID, StaffFirstname, StaffLastname, StaffAddress1, StaffAddress2, StaffCity, StaffZipcode, StaffTel, StaffMobile, StaffPictureName, StaffEmail, ProvinceID) ";
            //strSQL2 += "VALUES ('" + maxid + "','" + item.StaffCode + "','" + item.StaffPassword + "','" + item.StaffRoleID + "','" + item.GenderID + "','" + item.StaffFirstname + "','" + item.StaffLastname + "','" + item.Address1 + "','" + item.Address2 + "','" + item.City + "','" + item.Zipcode + "','" + item.Telephone + "','" + item.Mobile + "','" + item.Email + "','" + item.ProvinceID + "')";
            strSQL2 += "VALUES ('" + maxid + "','" + item.StaffCode + "','" + item.StaffPassword + "','" + item.StaffRoleID + "','" + item.GenderID + "','" + item.StaffFirstname + "','" + item.StaffLastname + "','" + item.Address1 + "','" + item.Address2 + "','" + item.City + "','" + item.Zipcode + "','" + item.Telephone + "','" + item.Mobile + "','" + item.Picture + "','" + item.Email + "','" + item.ProvinceID + "')";
            objDB.sqlExecute(strSQL2, objConn);
            objConn.Close();

            return staff.ToArray();
        }

        public Staff PostStaffIndexAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            Staff staffData = new Staff();
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 AND s.Deleted = 0 AND StaffID = " + item.StaffID + " ORDER BY StaffID;";
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

        public IEnumerable<Staff> PostEditPasswordStaffAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            List<Staff> editpassword = new List<Staff>();

            string strSQL = "UPDATE staffs SET StaffPassword = '" + item.StaffPassword + "'";
            strSQL += "WHERE StaffID = '" + item.StaffID + "';";
            objDB.sqlExecute(strSQL, objConn);
            objConn.Close();

            return editpassword;
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
            string strSQL = "SELECT * FROM staffrole WHERE Deleted = 0";
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
            string strSQL = "SELECT * FROM provinces WHERE LangID = 1 ORDER BY ProvinceName";
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

        public IEnumerable<PermissionItem> PostPermissionItemAll(PermissionItem item)
        {
            objConn = objDB.EstablishConnection();
            List<PermissionItem> permissionItem = new List<PermissionItem>();
            string strSQL = "SELECT PermissionItemID FROM staffrole sr INNER JOIN staffaccess sa ON sa.StaffRoleID = sr.StaffRoleID WHERE sr.StaffRoleID = '"+item.StaffRoleID+"';";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();

            PermissionItem PermissionItem = new PermissionItem();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                        PermissionItem.PermissionItemID += Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                    else
                        PermissionItem.PermissionItemID += "," + Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                    
                }
            }
            permissionItem.Add(PermissionItem);

            return permissionItem.ToArray();
        }

        public IEnumerable<StaffRoleAccess> GetStaffRoleAccessAll(int id)
        {
            objConn = objDB.EstablishConnection();
            List<StaffRoleAccess> staffaccess = new List<StaffRoleAccess>();
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
                        access.PermissionItemID += "," + Convert.ToInt32(dt.Rows[i]["PermissionItemID"].ToString());
                }
            }
            staffaccess.Add(access);

            return staffaccess;
        }

        public IEnumerable<PermissionItemdata> PostPermissionGroupAll(PermissionItemdata item)
        {
            objConn = objDB.EstablishConnection();
            List<PermissionItemdata> manage = new List<PermissionItemdata>();
            string strSQL = "SELECT pg.PermissionGroupID, pg.PermissionGroupName FROM permissionitems pt ";
            strSQL += "INNER JOIN permissiongroup pg ON pg.PermissionGroupID = pt.PermissionGroupID ";
            strSQL += "LEFT JOIN staffaccess sa ON sa.PermissionItemID = pt.PermissionItemID ";
            strSQL += "WHERE pt.PermissionItemParent = 0 AND pt.Deleted = 0 AND sa.StaffRoleID = '" + item.StaffRoleID + "' ";
            strSQL += "GROUP BY sa.StaffRoleID, pt.PermissionGroupID ORDER BY sa.StaffRoleID, pt.PermissionGroupID, pt.PermissionItemID;";
            string strSQLitem = "SELECT pg.PermissionGroupID, pt.PermissionItemUrl, pt.PermissionItemID, pt.PermissionItemName FROM permissionitems pt ";
            strSQLitem += "INNER JOIN permissiongroup pg ON pg.PermissionGroupID = pt.PermissionGroupID ";
            strSQLitem += "LEFT JOIN staffaccess sa ON sa.PermissionItemID = pt.PermissionItemID ";
            strSQLitem += "WHERE pt.PermissionItemParent = 0 AND pt.Deleted = 0 AND sa.StaffRoleID = '" + item.StaffRoleID + "' ";
            strSQLitem += "GROUP BY pt.PermissionItemID;";

            DataTable dt = objDB.List(strSQL, objConn);
            DataTable dtitem = objDB.List(strSQLitem, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                // Create Main Array

                // Create Object

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PermissionItemdata manageStaff = new PermissionItemdata();
                    manageStaff.GroupName = Convert.ToString(dt.Rows[i]["PermissionGroupName"]);
                    // Add Group Name AS String

                    DataRow[] dr = dtitem.Select(" PermissionGroupID = " + dt.Rows[i]["PermissionGroupID"].ToString());

                    // Add Group Parent AS Array
                    List<PermissionItemParent> manageParent = new List<PermissionItemParent>();

                    if (dr.Length > 0)
                    {
                        for (int j = 0; j < dr.Length; j++)
                        {
                            PermissionItemParent manageStaffparent = new PermissionItemParent();
                            // Add Object PermissionGroupID
                            manageStaffparent.PermissionGroupID = Convert.ToString(dr[j]["PermissionGroupID"]);
                            // Add Object PermissionItemUrl
                            manageStaffparent.PermissionItemUrl = Convert.ToString(dr[j]["PermissionItemUrl"]);
                            // Add Object PermissionItemID
                            manageStaffparent.PermissionItemID = Convert.ToString(dr[j]["PermissionItemID"]);
                            // Add Object PermissionItemName
                            manageStaffparent.PermissionItemName = Convert.ToString(dr[j]["PermissionItemName"]);

                            manageParent.Add(manageStaffparent);
                        }
                    }
                    manageStaff.GroupParent = manageParent;
                    manage.Add(manageStaff);
                }
            }
            return manage.ToArray();           
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
            int rowaccessid;
            int i = 0;

            string strSQL2 = "SELECT MAX(StaffAccessID) AS rowaccessid FROM staffaccess ;";
            DataTable dt1 = objDB.List(strSQL2, objConn);
            rowaccessid = Convert.ToInt32(dt1.Rows[0]["rowaccessid"].ToString());
            int maxaccessid = rowaccessid + 1;

            StringBuilder strSQL3 = new StringBuilder();

            strSQL3.Append("DELETE FROM staffaccess WHERE StaffRoleID = '" + staffaccess.StaffRoleID + "';");

            for (i = 0; i < staffaccess.PermissionItemID.Count; i++)
            {
                strSQL3.Append("INSERT INTO staffaccess(StaffAccessID, StaffRoleID, PermissionItemID) VALUES (" + (maxaccessid + i) + "," + staffaccess.StaffRoleID + "," + staffaccess.PermissionItemID[i] + ");");
            }

            objDB.sqlExecute(strSQL3.ToString(), objConn);
            objConn.Close();


            return staffAccess.ToArray();
        }

        public IEnumerable<StaffRole> PostStaffRoleDeleteAll(StaffRole item)
        {
            objConn = objDB.EstablishConnection();
            List<StaffRole> staffroledelete = new List<StaffRole>();

            string strSQL = "UPDATE staffrole SET Deleted = '" + item.Deleted + "'";
            strSQL += "WHERE StaffRoleID = '" + item.StaffRoleID + "';";
            objDB.sqlExecute(strSQL, objConn);
            objConn.Close();

            return staffroledelete.ToArray();
        }

        public Staff PostLoginAll(Staff item)
        {
            objConn = objDB.EstablishConnection();
            Staff login = new Staff();
            string strSQL = "SELECT *, CONCAT(s.StaffFirstname,' ', s.StaffLastname) AS NameStaff FROM staffs s INNER JOIN StaffRole sr ON sr.StaffRoleID = s.StaffRoleID INNER JOIN Provinces p ON p.ProvinceID = s.ProvinceID INNER JOIN Gender g ON g.GenderID = s.GenderID WHERE p.LangID = 1 AND s.Deleted = 0 AND StaffCode = '" + item.StaffCode + "' AND StaffPassword = '" + item.StaffPassword + "' ORDER BY StaffCode; ";
            DataTable dt = objDB.List(strSQL, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                login.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());
                login.StaffRoleName = dt.Rows[0]["StaffRoleName"].ToString();
                login.StaffPassword = dt.Rows[0]["StaffPassword"].ToString();
                login.StaffRoleID = Convert.ToInt32(dt.Rows[0]["StaffRoleID"].ToString());
                login.StaffFirstname = dt.Rows[0]["StaffFirstname"].ToString();
                login.StaffLastname = dt.Rows[0]["StaffLastname"].ToString();
                login.StaffName = dt.Rows[0]["NameStaff"].ToString();
                login.StaffCode = dt.Rows[0]["StaffCode"].ToString();
                login.Gender = dt.Rows[0]["GenderName"].ToString();
                login.GenderID = Convert.ToInt32(dt.Rows[0]["GenderID"].ToString());
                login.Address1 = dt.Rows[0]["StaffAddress1"].ToString();
                login.Address2 = dt.Rows[0]["StaffAddress2"].ToString();
                login.City = dt.Rows[0]["StaffCity"].ToString();
                login.Province = dt.Rows[0]["ProvinceName"].ToString();
                login.ProvinceID = Convert.ToInt32(dt.Rows[0]["ProvinceID"].ToString());
                login.Zipcode = dt.Rows[0]["StaffZipcode"].ToString();
                login.Telephone = dt.Rows[0]["StaffTel"].ToString();
                login.Mobile = dt.Rows[0]["StaffMobile"].ToString();
                login.Picture = dt.Rows[0]["StaffPictureName"].ToString();
                login.Email = dt.Rows[0]["StaffEmail"].ToString();
                login.status = "true";
                
            }
            else {
                login.status = "false";

            }
            return login;
        }

        public Staff PostStaffImageAll(string imageName, int staffid)
        {
            objConn = objDB.EstablishConnection();
            Staff staffdelete = new Staff();

            string strSQL = "UPDATE staffs SET StaffPictureName = '" + imageName + "'";
            //strSQL += "WHERE StaffID = '" + StaffID + "';";
            strSQL += "WHERE StaffID = '" + staffid + "';";
            objDB.sqlExecute(strSQL, objConn);
            objConn.Close();

            return staffdelete;
        }

    }
}