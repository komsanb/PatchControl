using MySql.Data.MySqlClient;
using POSMySQL.POSControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace Patch_Control.Models
{
    public class PatchsRepository : IPatchsRepository
    {
        CDBUtil objDB = new CDBUtil();
        MySqlConnection objConn = new MySqlConnection();

        public IEnumerable<Patchs> getPatchInformations()
        {
            objConn = objDB.EstablishConnection();
            List<Patchs> patchs = new List<Patchs>();
            string sql = "SELECT p.PatchsID, p.PatchsName, st.SoftwareTypeName, sv.SoftwareVersionName, p.PatchsDescription, p.PatchsInsertDate, p.PatchsInsertBy ";
            sql += "FROM patchs p LEFT JOIN softwareversion sv ON p.SoftwareVersionID = sv.SoftwareVersionID ";
            sql += "LEFT JOIN softwaretype st ON sv.SoftwareTypeID = st.SoftwareTypeID ";
            sql += "ORDER BY PatchsInsertDate DESC";

            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patchs patchInfo = new Patchs();
                    patchInfo.PatchsID = Convert.ToInt32(dt.Rows[i]["PatchsID"].ToString());
                    patchInfo.PatchsName = dt.Rows[i]["PatchsName"].ToString();
                    patchInfo.SoftwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
                    patchInfo.PatchsDescription = dt.Rows[i]["PatchsDescription"].ToString();
                    patchInfo.PatchsInsertDate = Convert.ToDateTime(dt.Rows[i]["PatchsInsertDate"].ToString());
                    patchInfo.PatchsInsertBy = dt.Rows[i]["PatchsInsertBy"].ToString();
                    patchInfo.SoftwareVersionName = dt.Rows[i]["SoftwareVersionName"].ToString();
                    patchs.Add(patchInfo);
                }
            }
            return patchs.ToArray();
        }

        public IEnumerable<SoftwareVersion> getSoftwareVersion()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SoftwareType> getSoftwareType()
        {
            throw new NotImplementedException();
        }
    }
}