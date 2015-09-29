﻿using MySql.Data.MySqlClient;
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
            string sql = "SELECT p.PatchsID, p.PatchsName, sv.SoftwareVersionID, ";
            sql += "CONCAT(sv.SoftwareVersionName, '.', p.PatchsVersionNumber) AS SoftwareVersion, ";
            sql += "st.SoftwareTypeID, st.SoftwareTypeName, p.PatchsDescription, ";
            sql += "DATE_FORMAT(p.PatchsInsertDate, '%d %M %Y') AS PatchsInsertDate, ";
            sql += "DATE_FORMAT(p.PatchsUpdateDate, '%d %M %Y') AS PatchsUpdateDate, ";
            sql += "p.PatchsInsertBy, p.PatchsUpdateBy ";
            sql += "FROM patchparentversion pv ";
            sql += "INNER JOIN patchs p ON p.PatchsID = pv.PatchsID ";
            sql += "INNER JOIN softwareversion sv ON sv.SoftwareVersionID = pv.SoftwareVersionID ";
            sql += "INNER JOIN softwaretype st ON st.SoftwareTypeID = pv.SoftwareTypeID ";
            sql += "WHERE p.Deleted = 0 ";
            sql += "ORDER BY PatchsInsertDate DESC";

            DataTable dt = objDB.List(sql, objConn);
            objConn.Close();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patchs patchInfo = new Patchs();

                    patchInfo.patchsID = Convert.ToInt32(dt.Rows[i]["PatchsID"].ToString());
                    patchInfo.patchsName = dt.Rows[i]["PatchsName"].ToString();
                    patchInfo.softwareVersionID = Convert.ToInt32(dt.Rows[i]["SoftwareVersionID"].ToString());
                    patchInfo.softwareVersionName = dt.Rows[i]["SoftwareVersion"].ToString();
                    patchInfo.softwareTypeID = Convert.ToInt32(dt.Rows[i]["SoftwareTypeID"].ToString());
                    patchInfo.softwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
                    patchInfo.patchsDescription = dt.Rows[i]["PatchsDescription"].ToString();
                    patchInfo.patchsInsertDate = dt.Rows[i]["PatchsInsertDate"].ToString();
                    patchInfo.patchsUpdateDate = dt.Rows[i]["PatchsUpdateDate"].ToString();                   
                    patchInfo.patchsInsertBy = dt.Rows[i]["PatchsInsertBy"].ToString();
                    patchInfo.patchsUpdateBy = dt.Rows[i]["PatchsUpdateBy"].ToString();

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
                    softwareType.softwareTypeID = Convert.ToInt32(dt.Rows[i]["SoftwareTypeID"].ToString());
                    softwareType.softwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
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
                    softwareVer.softwareVersionID = Convert.ToInt32(dt.Rows[i]["SoftwareVersionID"].ToString());
                    softwareVer.softwareVersionName = dt.Rows[i]["SoftwareVersionName"].ToString();
                    softVer.Add(softwareVer);
                }
            }
            return softVer.ToArray();
        }

        public IEnumerable<Patchs> postPatchInformations(Patchs items)
        {
            List<Patchs> patchInfors = new List<Patchs>();

                objConn = objDB.EstablishConnection();
                string sqlPatchID = "SELECT MAX(p.PatchsID) AS PatchsID, ";
                sqlPatchID += "MAX(pv.PatchParentVersionID) AS PatchParentVersionID, ";
                sqlPatchID += "MAX(sv.SoftwareVersionID) AS SoftwareVersionID ";
                sqlPatchID += "FROM patchparentversion pv ";
                sqlPatchID += "INNER JOIN patchs p ON p.PatchsID = pv.PatchsID ";
                sqlPatchID += "INNER JOIN softwareversion sv ON sv.SoftwareVersionID = pv.SoftwareVersionID";

                DataTable dtPatchID = objDB.List(sqlPatchID, objConn);
                int maxPatchID = Convert.ToInt32(dtPatchID.Rows[0]["PatchsID"].ToString()) + 1;

                DataTable dtPatchParentVersionID = objDB.List(sqlPatchID, objConn);
                int maxPatchParentVersionID = Convert.ToInt32(dtPatchParentVersionID.Rows[0]["PatchParentVersionID"].ToString()) + 1;

                DataTable dtSoftwareVersionID = objDB.List(sqlPatchID, objConn);
                int maxSoftwareVersionID = Convert.ToInt32(dtSoftwareVersionID.Rows[0]["SoftwareVersionID"].ToString()) + 1;

                string sqlInsertPatchInfors = "BEGIN; ";
                sqlInsertPatchInfors += "INSERT INTO patchs(PatchsID, PatchParentVersionID, PatchsName, PatchsDescription, ";
                sqlInsertPatchInfors += "PatchsInsertDate, PatchsUpdateDate, PatchsInsertBy,  PatchsVersionNumber) ";
                sqlInsertPatchInfors += "VALUES('" + maxPatchID + "', '" + maxPatchParentVersionID 
                    + "', '" + items.patchsName.ToString() + "', '" + items.patchsDescription.ToString() 
                    + "', '" + items.patchsInsertDate.ToString() + "', '" + items.patchsUpdateDate.ToString()
                    + "', '" + items.patchsInsertBy.ToString() + "', '" + items.patchsVersionNumber.ToString() + "');";
                sqlInsertPatchInfors += "INSERT INTO patchparentversion(PatchParentVersionID, PatchsID, SoftwareVersionID, SoftwareTypeID) ";
                sqlInsertPatchInfors += "VALUES('" + maxPatchParentVersionID + "', '" + maxPatchID + "','" + items.softwareVersionID.ToString()
                    + "','" + items.softwareTypeID.ToString() + "'); ";
                //sqlInsertPatch += "INSERT INTO softwareversion(SoftwareVersionID, SoftwareVersionName)";
                //sqlInsertPatch += "VALUES('" + maxSoftwareVersionID + "', '" + items.SoftwareVersionName.ToString() + "');";
                sqlInsertPatchInfors += "COMMIT;";

                objDB.sqlExecute(sqlInsertPatchInfors, objConn);
                objConn.Close();

                return patchInfors.ToArray();          
        }

        public IEnumerable<Files> postFilesInformations()
        {
            List<Files> filesInfors = new List<Files>();

            objConn = objDB.EstablishConnection();
            string sqlFileID = "SELECT MAX(FilesID) AS FilesID FROM files";

            DataTable dtPatchID = objDB.List(sqlFileID, objConn);
            int maxFilesID = Convert.ToInt32(dtPatchID.Rows[0]["FilesID"].ToString()) + 1;

            //string sqlFileInfors = "INSERT INTO files(FilesID, FilesName, FilesPath) ";
            //sqlFileInfors += "VALUES('" + maxFilesID + "', '" + files.FileName.ToString()
            //    + "', '" + files. +"')";

            //objDB.sqlExecute(sqlFileInfors, objConn);
            objConn.Close();

            return filesInfors.ToArray();
        }

        public IEnumerable<MyPatch> getMyPatch()
        {
            objConn = objDB.EstablishConnection();
            List<MyPatch> titlePatchs = new List<MyPatch>();
            string sqlMyPatch = "SELECT p.PatchsID, p.PatchsName, st.SoftwareTypeID, sv.SoftwareVersionID, ";
            sqlMyPatch += "CONCAT(sv.SoftwareVersionName, '.', p.PatchsVersionNumber) AS SoftwareVersionName, s.StaffFirstname, ";
            sqlMyPatch += "st.SoftwareTypeName, DATE_FORMAT(p.PatchsInsertDate, '%d %M %Y') AS PatchsInsertDate";
            sqlMyPatch += " FROM patchparentversion ppv";
            sqlMyPatch += " INNER JOIN softwareversion sv ON ppv.SoftwareVersionID = sv.SoftwareVersionID";
            sqlMyPatch += " INNER JOIN softwaretype st ON ppv.SoftwareTypeID = st.SoftwareTypeID";
            sqlMyPatch += " INNER JOIN patchs p ON ppv.PatchsID = p.PatchsID";
            sqlMyPatch += " INNER JOIN staffs s ON ppv.StaffID = s.StaffID";
            sqlMyPatch += " WHERE s.StaffID = '2' AND p.Deleted = 0 ";
            sqlMyPatch += " ORDER BY p.PatchsInsertDate DESC";

            DataTable dt = objDB.List(sqlMyPatch, objConn);
            objConn.Close();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MyPatch myPatch = new MyPatch();

                    myPatch.patchsID = Convert.ToInt32(dt.Rows[i]["PatchsID"].ToString());
                    myPatch.patchsName = dt.Rows[i]["PatchsName"].ToString();
                    myPatch.softwareTypeName = dt.Rows[i]["SoftwareTypeName"].ToString();
                    myPatch.softwareTypeID = Convert.ToInt32(dt.Rows[i]["SoftwareTypeID"].ToString());
                    myPatch.softwareVersionID = Convert.ToInt32(dt.Rows[i]["SoftwareVersionID"].ToString());
                    myPatch.softwareVersionName = dt.Rows[i]["SoftwareVersionName"].ToString();
                    myPatch.patchsInsertDate = dt.Rows[i]["PatchsInsertDate"].ToString();
                    myPatch.staffFirtname = dt.Rows[i]["StaffFirstname"].ToString();
                    titlePatchs.Add(myPatch);
                }
            }

            return titlePatchs;
        }

        public MyPatch getMyPatchInformations(int patchID)
        {
            objConn = objDB.EstablishConnection();
            MyPatch myPatchsDetails = new MyPatch();
            string sqlMyPatchDetails = "SELECT s.StaffID, p.PatchsID, p.PatchsName, st.SoftwareTypeID, sv.SoftwareVersionID, p.PatchsVersionNumber, ";
            sqlMyPatchDetails += "CONCAT(sv.SoftwareVersionName, '.', p.PatchsVersionNumber) AS SoftwareVersionName, s.StaffFirstname, ";
            sqlMyPatchDetails += "st.SoftwareTypeName, DATE_FORMAT(p.PatchsInsertDate, '%d %M %Y') AS PatchsInsertDate, ";
            sqlMyPatchDetails += "p.PatchsDescription ";
            sqlMyPatchDetails += " FROM patchparentversion ppv";
            sqlMyPatchDetails += " INNER JOIN softwareversion sv ON ppv.SoftwareVersionID = sv.SoftwareVersionID";
            sqlMyPatchDetails += " INNER JOIN softwaretype st ON ppv.SoftwareTypeID = st.SoftwareTypeID";
            sqlMyPatchDetails += " INNER JOIN patchs p ON ppv.PatchsID = p.PatchsID";
            sqlMyPatchDetails += " INNER JOIN staffs s ON ppv.StaffID = s.StaffID";
            sqlMyPatchDetails += " WHERE p.PatchsID = '" + patchID + "' AND p.Deleted = 0 ";
            sqlMyPatchDetails += " ORDER BY p.PatchsInsertDate DESC";

            DataTable dt = objDB.List(sqlMyPatchDetails, objConn);
            objConn.Close();
            
            myPatchsDetails.patchsID = Convert.ToInt32(dt.Rows[0]["PatchsID"].ToString());
            myPatchsDetails.patchsName = dt.Rows[0]["PatchsName"].ToString();
            myPatchsDetails.softwareTypeName = dt.Rows[0]["SoftwareTypeName"].ToString();
            myPatchsDetails.patchsVersionNumber = dt.Rows[0]["PatchsVersionNumber"].ToString();
            myPatchsDetails.patchsDescription = dt.Rows[0]["PatchsDescription"].ToString();
            myPatchsDetails.softwareTypeID = Convert.ToInt32(dt.Rows[0]["SoftwareTypeID"].ToString());
            myPatchsDetails.softwareVersionID = Convert.ToInt32(dt.Rows[0]["SoftwareVersionID"].ToString());
            myPatchsDetails.softwareVersionName = dt.Rows[0]["SoftwareVersionName"].ToString();
            myPatchsDetails.patchsInsertDate = dt.Rows[0]["PatchsInsertDate"].ToString();
            myPatchsDetails.staffFirtname = dt.Rows[0]["StaffFirstname"].ToString();

            return myPatchsDetails;
        }

        public IEnumerable<Patchs> postUpdatePatchInformations(Patchs update)
        {
            List<Patchs> updatePatchInfors = new List<Patchs>();

            objConn = objDB.EstablishConnection();
            string sqlUpdatePatchInformations = "BEGIN;";
            sqlUpdatePatchInformations += " UPDATE patchs SET PatchsName = '" + update.patchsName.ToString()
                + "', PatchsDescription = '" + update.patchsDescription.ToString()
                + "', PatchsUpdateDate = '" + update.patchsUpdateDate.ToString()
                + "', PatchsUpdateBy = '" + update.patchsUpdateBy.ToString()
                + "', PatchsVersionNumber = '" + update.patchsVersionNumber.ToString()
                + "' WHERE PatchsID = '" + Convert.ToInt32(update.patchsID.ToString()) + "';";
            sqlUpdatePatchInformations += " UPDATE patchparentversion SET SoftwareVersionID = '" + Convert.ToInt32(update.softwareVersionID.ToString())
                + "', SoftwareTypeID = '" + Convert.ToInt32(update.softwareTypeID.ToString())
                + "'WHERE PatchsID = '" + Convert.ToInt32(update.patchsID.ToString()) + "'; ";
            sqlUpdatePatchInformations += "COMMIT;";

            objDB.sqlExecute(sqlUpdatePatchInformations, objConn);
            objConn.Close();

            return updatePatchInfors.ToArray();
        }

        public IEnumerable<Patchs> postDeletePatchInformations(int patchID)
        {
            List<Patchs> deletePatchInfors = new List<Patchs>();

            objConn = objDB.EstablishConnection();

            string sqlDeleted = "SELECT Deleted FROM patchs WHERE PatchsID = '" + patchID + "'";
            DataTable dtDeleted = objDB.List(sqlDeleted, objConn);
            int maxDeleted = Convert.ToInt32(dtDeleted.Rows[0]["Deleted"].ToString());
            maxDeleted = 1; // set vaulue equal 1, your patch has been deleted
            string sqlDeletePatchInformations = "UPDATE patchs SET Deleted = '" + maxDeleted
                + "' WHERE PatchsID = '" + patchID + "'";

            objDB.sqlExecute(sqlDeletePatchInformations, objConn);
            objConn.Close();

            return deletePatchInfors.ToArray();
        }
    }
}