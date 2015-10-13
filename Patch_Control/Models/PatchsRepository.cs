using MySql.Data.MySqlClient;
using POSMySQL.POSControl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

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
            sql += "WHERE p.Deleted = 0 AND p.Activated = 1 ";
            sql += "ORDER BY PatchsUpdateDate DESC";

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
                sqlInsertPatchInfors += "PatchsInsertDate, PatchsUpdateDate, PatchsInsertBy,  PatchsVersionNumber, Activated) ";
                sqlInsertPatchInfors += "VALUES('" + maxPatchID + "', '" + maxPatchParentVersionID 
                    + "', '" + items.patchsName.ToString() + "', '" + items.patchsDescription.ToString() 
                    + "', '" + items.patchsInsertDate.ToString() + "', '" + items.patchsUpdateDate.ToString()
                    + "', '" + items.patchsInsertBy.ToString() + "', '" + items.patchsVersionNumber.ToString() + "', '0');";
                sqlInsertPatchInfors += "INSERT INTO patchparentversion(PatchParentVersionID, PatchsID, SoftwareVersionID, SoftwareTypeID, StaffID) ";
                sqlInsertPatchInfors += "VALUES('" + maxPatchParentVersionID + "', '" + maxPatchID 
                    + "','" + items.softwareVersionID.ToString() + "','" + items.softwareTypeID.ToString() 
                    + "', '" + Convert.ToInt32(items.staffID.ToString()) + "'); ";
                //sqlInsertPatch += "INSERT INTO softwareversion(SoftwareVersionID, SoftwareVersionName)";
                //sqlInsertPatch += "VALUES('" + maxSoftwareVersionID + "', '" + items.SoftwareVersionName.ToString() + "');";
                sqlInsertPatchInfors += "COMMIT;";

                objDB.sqlExecute(sqlInsertPatchInfors, objConn);
                objConn.Close();

                return patchInfors.ToArray();          
        }

        public Files postFilesInformations(string path, string fileName, int staffID)
        {
            Files files = new Files();

            objConn = objDB.EstablishConnection();
            string sqlFileID = "SELECT MAX(FilesID) AS FilesID FROM files";

            DataTable dtFileID = objDB.List(sqlFileID, objConn);
            int maxFilesID = Convert.ToInt32(dtFileID.Rows[0]["FilesID"].ToString()) + 1;

            string sqlPatchsID = "SELECT MAX(p.PatchsID) AS PatchsID FROM patchs p";
            sqlPatchsID += " INNER JOIN patchparentversion ppv ON p.PatchsID = ppv.PatchsID";
            sqlPatchsID += " INNER JOIN staffs s ON ppv.StaffID = s.StaffID";
            sqlPatchsID += " WHERE s.StaffID = '" + staffID + "'";

            DataTable dtPatchsID = objDB.List(sqlPatchsID, objConn);
            int maxPatchsID = Convert.ToInt32(dtPatchsID.Rows[0]["PatchsID"].ToString());

            string sqlFileInfors = "BEGIN;";
            sqlFileInfors += " INSERT INTO files(FilesID, FilesName, FilesPath)";
            sqlFileInfors += " VALUES('" + maxFilesID + "', '" + fileName + "', '" + path + "');";
            sqlFileInfors += " UPDATE patchparentversion SET FilesID = '" + maxFilesID + "' WHERE PatchsID = '" + maxPatchsID + "';";
            sqlFileInfors += " UPDATE patchs SET Activated = 1 WHERE PatchsID = '" + maxPatchsID + "';";
            sqlFileInfors += " COMMIT;";

            objDB.sqlExecute(sqlFileInfors, objConn);
            objConn.Close();

            return files;

        }

        public IEnumerable<MyPatch> getMyPatch(int staffID)
        {
            objConn = objDB.EstablishConnection();
            List<MyPatch> titlePatchs = new List<MyPatch>();
            string sqlMyPatch = "SELECT p.PatchsID, p.PatchsName, st.SoftwareTypeID, sv.SoftwareVersionID, ";
            sqlMyPatch += "CONCAT(sv.SoftwareVersionName, '.', p.PatchsVersionNumber) AS SoftwareVersionName, s.StaffFirstname, ";
            sqlMyPatch += "st.SoftwareTypeName, DATE_FORMAT(p.PatchsInsertDate, '%d %M %Y') AS PatchsInsertDate, p.Activated";
            sqlMyPatch += " FROM patchparentversion ppv";
            sqlMyPatch += " INNER JOIN softwareversion sv ON ppv.SoftwareVersionID = sv.SoftwareVersionID";
            sqlMyPatch += " INNER JOIN softwaretype st ON ppv.SoftwareTypeID = st.SoftwareTypeID";
            sqlMyPatch += " INNER JOIN patchs p ON ppv.PatchsID = p.PatchsID";
            sqlMyPatch += " INNER JOIN staffs s ON ppv.StaffID = s.StaffID";
            sqlMyPatch += " WHERE s.StaffID = '" + staffID + "' AND p.Deleted = 0 ";
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
                    myPatch.staffFirstname = dt.Rows[i]["StaffFirstname"].ToString();
                    myPatch.activated = Convert.ToInt32(dt.Rows[i]["Activated"].ToString());
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
            myPatchsDetails.staffFirstname = dt.Rows[0]["StaffFirstname"].ToString();

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

        public IEnumerable<Email> sentEmail(Email items)
        {            
            objConn = objDB.EstablishConnection();
            List<Email> sentMail = new List<Email>();

            string sqlEmail = "SELECT s.StaffEmail FROM staffs s";
            sqlEmail += " INNER JOIN staffrole sr ON sr.StaffRoleID = s.StaffRoleID";
            sqlEmail += " WHERE sr.StaffRoleID = '" + Convert.ToInt32(items.staffRoleID.ToString()) + "'";
            DataTable dtSentMail = objDB.List(sqlEmail, objConn);

            string sqlPatchInfor = "SELECT p.PatchsName,";
            sqlPatchInfor += " CONCAT(sv.SoftwareVersionName, '.', p.PatchsVersionNumber) AS SoftwareVersion,";
            sqlPatchInfor += " st.SoftwareTypeName, p.PatchsDescription,";
            sqlPatchInfor += " DATE_FORMAT(p.PatchsInsertDate, '%d %M %Y') AS PatchsInsertDate,";
            sqlPatchInfor += " DATE_FORMAT(p.PatchsUpdateDate, '%d %M %Y') AS PatchsUpdateDate,";
            sqlPatchInfor += " p.PatchsInsertBy, p.PatchsUpdateBy";
            sqlPatchInfor += " FROM patchparentversion pv";
            sqlPatchInfor += " INNER JOIN patchs p ON p.PatchsID = pv.PatchsID";
            sqlPatchInfor += " INNER JOIN softwareversion sv ON sv.SoftwareVersionID = pv.SoftwareVersionID";
            sqlPatchInfor += " INNER JOIN softwaretype st ON st.SoftwareTypeID = pv.SoftwareTypeID";
            sqlPatchInfor += " INNER JOIN staffs s ON s.StaffID = pv.StaffID";
            sqlPatchInfor += " INNER JOIN staffrole sr ON sr.StaffRoleID = s.StaffRoleID";
            sqlPatchInfor += " WHERE p.PatchsID = (SELECT MAX(PatchsID) FROM patchparentversion pv";
            sqlPatchInfor += " INNER JOIN staffs s ON s.StaffID = pv.StaffID";
            sqlPatchInfor += " WHERE s.StaffID = '" + Convert.ToInt32(items.staffID.ToString()) + "')";
            objConn.Close();

            if (dtSentMail.Rows.Count > 0)
            {
                    string hostAddr = ConfigurationManager.AppSettings["Host"].ToString();
                    string mailAuthen = ConfigurationManager.AppSettings["MailAuthen"].ToString();
                    string passAuthen = ConfigurationManager.AppSettings["PassAuthen"].ToString();

                    MailMessage mailMessage = new MailMessage();                    
                    mailMessage.From = new MailAddress(mailAuthen);
                    mailMessage.Subject = "HELLO";
                    mailMessage.Body = "THIS IS A TEST";
                    mailMessage.IsBodyHtml = true;

                for(int i = 0; i < dtSentMail.Rows.Count; i++)
                {
                    string staffEmail = dtSentMail.Rows[i]["StaffEmail"].ToString();
                    string[] splitRecipients = staffEmail.Split(',');

                    foreach(var mailTo in splitRecipients)
                    {
                        mailMessage.To.Add(mailTo); 
                    }                    
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = hostAddr;
                smtp.EnableSsl = true;

                NetworkCredential networkCre = new NetworkCredential();
                networkCre.UserName = mailAuthen;
                networkCre.Password = passAuthen;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCre;
                smtp.Port = 465;

                try
                {
                    smtp.Send(mailMessage);//ON ERROR
                    Console.WriteLine("Message Sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            return sentMail;
        }
    }
}