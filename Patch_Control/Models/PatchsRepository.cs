using MySql.Data.MySqlClient;
using POSMySQL.POSControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
            string sql = "SELECT p.PatchsID, sv.SoftwareVersionID, st.SoftwareTypeID, DATE_FORMAT(p.PatchsInsertDate,'%d %M %Y') AS insertDate, ";
            sql += "DATE_FORMAT(p.PatchsUpdateDate, '%d %M %Y') AS updateDate, p.PatchsUpdateBy, ";
            sql += "st.SoftwareTypeName, p.PatchsName, sv.SoftwareVersionName, p.PatchsDescription, ";
            sql += "p.PatchsInsertBy FROM patchs p ";
            sql += "LEFT JOIN softwareversion sv ON p.SoftwareVersionID = sv.SoftwareVersionID ";
            sql += "LEFT JOIN softwaretype st ON sv.SoftwareTypeID = st.SoftwareTypeID ";
            sql += "ORDER BY p.PatchsInsertDate";

            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patchs patchInfo = new Patchs();
                    patchInfo.PatchsID = Convert.ToInt32(dt.Rows[i]["PatchsID"].ToString());
                    patchInfo.SoftwareVersionID = Convert.ToInt32(dt.Rows[i]["SoftwareVersionID"].ToString());
                    patchInfo.SoftwareTypeID = Convert.ToInt32(dt.Rows[i]["SoftwareTypeID"].ToString());
                    patchInfo.PatchsName = dt.Rows[i]["PatchsName"].ToString();
                    patchInfo.PatchsInsertDate = dt.Rows[i]["insertDate"].ToString();
                    patchInfo.PatchsUpadateDate = dt.Rows[i]["updateDate"].ToString();
                    patchInfo.SoftwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
                    patchInfo.PatchsDescription = dt.Rows[i]["PatchsDescription"].ToString();
                    patchInfo.SoftwareVersionName = dt.Rows[i]["SoftwareVersionName"].ToString();
                    patchInfo.PatchsInsertBy = dt.Rows[i]["PatchsInsertBy"].ToString();
                    patchInfo.PatchsUpdateBy = dt.Rows[i]["PatchsUpdateBy"].ToString();
                    patchs.Add(patchInfo);
                }
            }
            return patchs.ToArray();
        }

        public IEnumerable<SoftwareType> getSoftwareType()
        {
            objConn = objDB.EstablishConnection();
            List<SoftwareType> sofType = new List<SoftwareType>();
            string sql = "SELECT * FROM softwaretype";

            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SoftwareType softwareType = new SoftwareType();
                    softwareType.SoftwareTypeID = Convert.ToInt32(dt.Rows[i]["SoftwareTypeID"].ToString());
                    softwareType.SoftwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
                    sofType.Add(softwareType);
                }
            }
            return sofType.ToArray();
        }

        public IEnumerable<SoftwareVersion> getSoftwareVersion()
        {
            objConn = objDB.EstablishConnection();
            List<SoftwareVersion> softVer = new List<SoftwareVersion>();
            string sql = "SELECT * FROM softwareversion";

            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SoftwareVersion softwareVer = new SoftwareVersion();
                    softwareVer.SoftwareVersionID = Convert.ToInt32(dt.Rows[i]["SoftwareVersionID"].ToString());
                    softwareVer.SoftwareVersionName = dt.Rows[i]["SoftwareVersionName"].ToString();
                    softVer.Add(softwareVer);
                }
            }
            return softVer.ToArray();
        }
    }
}