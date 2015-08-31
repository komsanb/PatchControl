using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSMySQL.POSControl;
using MySql.Data.MySqlClient;
using System.Data;

namespace Patch_Control.Models
{
    public class StaffsRepository : IStaffsRepository
    {
        CDBUtil objDB = new CDBUtil();
        MySqlConnection objConn = new MySqlConnection();

        public IEnumerable<Staffs> getStaffAll()
        {
            objConn = objDB.EstablishConnection();
            List<Staffs> staffs = new List<Staffs>();
            string sql = "SELECT StaffsID, StaffsFirstname FROM staffs";
            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Staffs staffData = new Staffs();
                    staffData.StaffID = Convert.ToInt32(dt.Rows[i]["StaffsID"].ToString());
                    staffData.StaffName = dt.Rows[i]["StaffsFirstname"].ToString();
                    staffs.Add(staffData);
                }
            }

            return staffs.ToArray();
        }
    }
}