using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data.OleDb;
using System.Net.Mail;
using System.Text;
using System.Globalization;

public partial class Home : System.Web.UI.Page
{
    Helper hp = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        //load();
        if (!IsPostBack)
        {
            //ddload();
            
        }
    }
    //public void load()
    //{
    //    List<String> lst = new List<String>();
    //    String strSql = String.Empty;
    //    strSql = "SELECT DOCTYPE FROM DOORGRP.W_DOCTYPES ORDER BY SORT ASC";
    //    lst = Helper.getStringListDB2(strSql);
    //    foreach (String dt in lst)
    //    {
    //      //  cbxDocTypeOrders.Items.Add(dt);
    //    }

    //}
    public void ddload()
    {
        DataSet ds = new DataSet();
        string sql = "SELECT DOCTYPE FROM DOORGRP.W_DOCTYPES ORDER BY SORT ASC";
        ds = hp.loadddl(sql);

        ddldocumenttypeorderdocument.DataSource = ds;
        ddldocumenttypeorderdocument.DataTextField = "DOCTYPE";
        //DrpProjectName.DataValueField = "ProjectRefId";
        ddldocumenttypeorderdocument.DataBind();


    }

    [WebMethod]
    public static clsOrderData validatefile(string filename, string system,string company)
    {
        clsOrderData orderdata = new clsOrderData();

        if (system == "0")
        {
            string doctype = ProcessOrderData(filename);
            string[] b = doctype.Split('*');
            string details=GetOrderData(b[0].ToString(),system,company);
            string[] c = details.Split('*');
            orderdata.orderNumber = b[0].ToString();
            orderdata.doctypes = b[1].ToString().ToString();
            if (b[2].ToString() != "")
            {
                orderdata.indexes = b[2].ToString();
            }
            else
            {
                orderdata.indexes = "";
            }
            if (c[0].ToString() != "")
            {
                orderdata.distNumber = c[0].ToString();
                orderdata.branch = c[1].ToString();
                orderdata.poNumber = c[2].ToString();
                orderdata.status = "1";
                orderdata.company = company;
            }
            else
            {
                orderdata.status = "0";
            }
            
        }
        else
        {
            //ProcessDrawingData(filename);
        }


        return orderdata;
    }

    public static string ProcessOrderData(String fileName)
    {
        string doctypeorders = string.Empty;
        string indexes = string.Empty;
        if (!fileName.Equals(""))
        {
           
                String ordNo = string.Empty;
                ordNo = fileName.Substring(0, fileName.IndexOf("."));
                if (ordNo.Contains("-") || (ordNo.Contains("_")))
                {
                    ordNo = ordNo.Replace("_", "-");
                    String[] ordData;
                    ordData = ordNo.Split('-');
                    ordNo = ordData[0];
                    if (ordData.Length > 1 && !ordData[1].Trim().Equals(""))
                    {
                        doctypeorders = Helper.getStringSql(string.Format("SELECT DOCTYPE FROM DOORGRP.W_DOCTYPES WHERE TRIM(DOCCODE) = '{0}'", ordData[1].ToUpper().Trim()));
                    }
                    else
                    {
                        doctypeorders = "Miscellaneous";
                    }
                    if (ordData.Length >= 3)
                    {
                        indexes = ordData[2];
                    }
                    else
                    {
                        indexes = "";
                    }
                }
                doctypeorders = ordNo+"*"+doctypeorders + "*" + indexes;
        }
        return doctypeorders;
    }

    public static string GetOrderData(String ordNo,string system,string company)
    {
            
        clsOrderData odm = new clsOrderData();
        odm.orderNumber = ordNo.ToUpper();
        if (company=="0" && system =="0")
        {
            odm = Helper.getCurriesOrderData(odm);
        }
        else if (company == "1" && system == "0")
        {
            odm = Helper.getGrahamOrderData(odm);
        }
        else if (company == "2" && system == "0")
        {
            odm = Helper.getCecoOrderData(odm);
        }
        else if (company == "3" && system == "0")
        {
            odm = Helper.getFrameworksOrderData(odm);
        }
        string details;
            
        if (odm.distNumber != null && !odm.distNumber.Trim().Equals(""))
        {
            details=odm.distNumber+"*"+odm.branch+"*"+odm.poNumber+"*"+odm.status;
        }
        else
        {
            details="0";
        }

        return details;
    }
    
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Process p = new Process();
        p.StartInfo.FileName = ".exe";
        p.Start();

    }

    private string getWorkflowLocation(int compIndex)
    {
        string workflowLocation = string.Empty;

        if (compIndex == 0)
        {
            workflowLocation = ConfigurationManager.AppSettings["CurriesWorkflowFolders"].ToString();
        }
        else if (compIndex == 1)
        {
            workflowLocation = ConfigurationManager.AppSettings["GrahamWorkflowFolders"].ToString();
        }
        else if (compIndex == 2)
        {
            workflowLocation = ConfigurationManager.AppSettings["CecoWorkflowFolders"].ToString();
        }
        else if (compIndex == 3)
        {
            workflowLocation = ConfigurationManager.AppSettings["FrameworksWorkflowFolders"].ToString(); ;
        }
        return workflowLocation;
    }

    private string getDocumentCode(string doctype)
    {
        string res = string.Empty;

        if (!doctype.Equals(""))
        {
            ddldocumenttypeorderdocument.BackColor = Color.Green;
            res = Helper.getStringSql(string.Format("SELECT DOCCODE FROM DOORGRP.W_DOCTYPES WHERE TRIM(DOCTYPE) = '{0}'", doctype));
        }
        else
        {
            ddldocumenttypeorderdocument.BackColor = Color.Red;
            res = "";
        }
        return res;
    }

    private string incrementFileName(string fileName, int i)
    {

        string ordNo = fileName.Substring(0, fileName.IndexOf("."));
        string fileType = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
        if (ordNo.Contains("-") || (ordNo.Contains("_")))
        {
            ordNo = ordNo.Replace("_", "-");
            String[] ordData;
            ordData = ordNo.Split('-');
            ordNo = ordData[0];
            string s = ordData[1].Trim();
            string a = string.Empty;
            if (ordData.Length > 1 && !s.Equals(""))
            {
                if (s.Length > 2)
                {
                    s = s.Substring(0, 2);
                }

                if (s.StartsWith("0"))
                {
                    if (!s.Equals("00"))
                    {
                        s = "0";
                    }
                }
                else
                {
                    s = s.Substring(0, 1);
                }
            }
            else
            {
                s = "X";
            }

            if (ordData.Length >= 3)
            {
                a = ordData[2].Trim();
            }

            if (a.Equals(""))
            {
                fileName = string.Format("{0}-{1}{2}{3}", ordNo, s, i.ToString(), fileType);
            }
            else
            {
                fileName = string.Format("{0}-{1}{2}-{3}{4}", ordNo, s, a, i.ToString(), fileType);
            }
        }
        return fileName;
    }

    private void sendEmail(string orderNumber)
    {
        OleDbConnection oledb = new OleDbConnection(ConfigurationManager.ConnectionStrings["CurriesOleDB"].ConnectionString);
        string email = string.Empty;
        try
        {
            oledb.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = oledb;
            if (ddlcompany.SelectedIndex == 0)
            {
                cmd.CommandText = "SELECT EMAIL1 FROM DOORGRP.W_USERS WHERE I_PROFILE = (SELECT ORDOWNER FROM CURRIE.ORDRHDR WHERE ORDNO = '" + orderNumber + "')";
            }
            else if (ddlcompany.SelectedIndex == 1)
            {
                cmd.CommandText = "SELECT EMAIL1 FROM DOORGRP.W_USERS WHERE I_PROFILE = (SELECT OWNER FROM GRAHAMP2.ORDHDR WHERE ORDNO = '" + orderNumber + "')";
            }
            else if (ddlcompany.SelectedIndex == 2)
            {
                cmd.CommandText = string.Format("SELECT EMAIL1 FROM DGRPQADTA.W_USERS WHERE CECO_PROFILE = (SELECT OWNER FROM DGRPQADTA.W_WRKORDST WHERE COMPANY='Frameworks' AND ORDNO = '{0}' AND TIME_CREATED=(SELECT MAX(TIME_CREATED) FROM DGRPQADTA. W_WRKORDST WHERE COMPANY='Frameworks' AND ORDNO = '{0}'))", orderNumber);
            }
            else if (ddlcompany.SelectedIndex == 3)
            {
                cmd.CommandText = string.Format("SELECT EMAIL1 FROM DGRPQADTA.W_USERS WHERE CECO_PROFILE = (SELECT OWNER FROM DGRPQADTA.W_WRKORDST WHERE COMPANY='Frameworks' AND ORDNO = '{0}' AND TIME_CREATED=(SELECT MAX(TIME_CREATED) FROM DGRPQADTA. W_WRKORDST WHERE COMPANY='Frameworks' AND ORDNO = '{0}'))", orderNumber);
            }

            email = cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            email = string.Empty;
        }
        finally
        {
            if (!email.Trim().Equals(""))
            {
                try
                {
                    MailMessage mail = new MailMessage("FROM@STRING.COM", "TO@STRING.COM");
                    mail.Subject = string.Format("New document added for order {0} - Document Type ({1})", orderNumber, ddldocumenttypeorderdocument.SelectedValue);
                    string fileName = Server.MapPath("~/Temp/" + file_nameid.InnerText).ToString();
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    mail.Body = "This is an automated email, do not reply.%0D%0DOrder: " + orderNumber + "%0D%0DDocument Type: " + ddldocumenttypeorderdocument.SelectedValue + "%0DFilename: " + fileName + "";
                    mail.IsBodyHtml = true;
                    mail.BodyEncoding = System.Text.Encoding.UTF8;

                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = "mailto:" + email + "?subject=" + mail.Subject + "&body=" + mail.Body;
                    proc.Start();
                }
                catch (Exception ex)
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }

    private string processWorkflowDoc(string orderNo, string fileName)
    {
        //todo: add all companies
        string workflowLocation = getWorkflowLocation(ddlcompany.SelectedIndex);
        string fileType = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));

        if (ddldocumenttypeorderdocument.SelectedValue.ToString().Equals("ORIGINAL ORDER"))
        {
            try
            {
                if (!Directory.Exists(string.Format("{0}{1}", workflowLocation, orderNo)))
                {
                    Directory.CreateDirectory(string.Format("{0}{1}", workflowLocation, orderNo));
                    //rename file to final order (01)
                    string ordno01 = string.Format("{0}-01{1}", orderNo, fileType);
                    // move 01 copy to workflow folder
                    File.Copy(Server.MapPath("~/Temp/"+ fileName), string.Format("{0}{1}\\{2}", workflowLocation, orderNo, ordno01));
                    return "Import";
                }
                else
                {
                    string doctype = getDocumentCode(ddldocumenttypeorderdocument.Text);
                    string docName = string.Format("{0}-{1}{2}", orderNo, doctype, fileType);
                    for (int i = 1; File.Exists(string.Format("{0}{1}\\{2}", workflowLocation, orderNo, docName)); i++)
                        docName = incrementFileName(docName, i);

                    File.Copy(Server.MapPath("~/Temp/"+ fileName), string.Format("{0}{1}\\{2}", workflowLocation, orderNo, docName));

                    sendEmail(orderNo);

                    return "NoImport";
                }
            }
            catch (Exception e)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Error processing file')</script>");
                //MessageBox.Show(string.Format("{0}", e.Message), "Error processing file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "Error";
            }
        }
        else if (Directory.Exists(string.Format("{0}{1}", workflowLocation, orderNo)))
        {
            try
            {
                string doctype = getDocumentCode(ddldocumenttypeorderdocument.Text);
                string docName = string.Format("{0}-{1}{2}", orderNo, doctype, fileType);
                if (!File.Exists(string.Format("{0}{1}\\{2}", workflowLocation, orderNo, docName)))
                {
                    File.Copy(Server.MapPath("~/Temp/"+ fileName), string.Format("{0}{1}\\{2}", workflowLocation, orderNo, docName));
                }
                else
                {
                    string newName = string.Format("{0}{1}\\{2}", workflowLocation, orderNo, docName);
                    for (int i = 1; File.Exists(newName); i++)
                    {
                        fileName = incrementFileName(fileName, i);
                        newName = string.Format("{0}{1}\\{2}", workflowLocation, orderNo, fileName);
                    }

                    File.Copy(Server.MapPath("~/Temp/"+ fileName), newName);
                }

                sendEmail(orderNo);

                return "NoImport";
            }
            catch (Exception e)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Error processing file')</script>");
                //MessageBox.Show(string.Format("{0}", e.Message), "Error processing file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "Error";
            }
        }
        else
        {
            return "Import";
        }
    }

    private bool validFile(string FileName)
    {
        bool isValid = false;
        try
        {
            using (Stream stream = new FileStream(FileName, FileMode.Open))
            {
                isValid = true;
            }
        }
        catch
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('File is in use and needs to be closed before processing')</script>");
            //MessageBox.Show("File is in use and needs to be closed before processing", "File in use", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        return isValid;
    }

    private Boolean validateData(RVIImportModel rim)
    {
        Boolean res = true;
        StringBuilder sb = new StringBuilder();
        if (rim.fileStream.Length != 0)
        {
            sb.Append("The following fields are required: ");
            if (list.SelectedIndex == 0) //orders
            {
                if (rim.systemIndex1.Equals(""))
                {
                    sb.Append("[Order Number] ");
                    res = false;
                }
                if (rim.systemIndex3.Equals(""))
                {
                    sb.Append("[Distributor] ");
                    res = false;
                }
                if (rim.systemIndex4.Equals("") && ddlcompany.SelectedIndex < 2)
                {
                    sb.Append("[Branch] ");
                    res = false;
                }
                if (rim.systemIndex5.Equals(""))
                {
                    sb.Append("[PO Number] ");
                    res = false;
                }
            }
            else if (list.SelectedIndex == 1) //drawings
            {
                if (rim.systemIndex1.Equals(""))
                {
                    sb.Append("[Drawing Number] ");
                    res = false;
                }

                if (rim.systemIndex2.Equals(""))
                {
                    sb.Append("[Revision] ");
                    res = false;
                }
                if (rim.systemIndex7.Equals(""))
                {
                    sb.Append("[Document Type] ");
                    res = false;
                }
            }

        }
        if (res == false)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('" + sb.ToString() + "')</script>");
            //MessageBox.Show(sb.ToString());
        }
        return res;
    }

    private Boolean processOrderDoc()
    {
        Boolean res = false;
        RVIImportModel rim = new RVIImportModel();
        rim.SystemCode = "1";
        rim.systemIndex1 = txtorderno.Text.ToUpper();
        rim.systemIndex2 = ddlcompany.Text.Substring(0, 4);
        rim.systemIndex3 = txtdistributor.Text; //dist
        rim.systemIndex4 = txtbranch.Text; //branch
        rim.systemIndex5 = txtpono.Text; //PO number
        rim.systemIndex6 = txtdate.Value.ToString();
        rim.systemIndex7 = ddldocumenttypeorderdocument.Text;
        rim.systemIndex8 = ddladditionalinfo.Text;
        rim.imageFileName = Server.MapPath("~/Temp/" + file_nameid.InnerText);
        rim.fileName = file_nameid.InnerText;
        String[] f = rim.fileName.Split('.');
        rim.imageType = f[1];
        File.SetAttributes(rim.imageFileName, FileAttributes.Normal);
        FileStream fs = new FileStream(rim.imageFileName, FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        Byte[] bb = br.ReadBytes(int.Parse(br.BaseStream.Length.ToString()));
        rim.fileStream = Convert.ToBase64String(bb);
        fs.Close();
        if (validateData(rim) == true)
        {
            Boolean succ = false;
            succ = RVIImportModel.UploadtoRVI(rim);
            if (succ == true)
            {
                clearscreen();
                //ListViewItem lvi = new ListViewItem();
                //lvi.Text = "";
                //lvi.SubItems.Add("Upload Complete");
                //file_nameid.InnerText=""
                //lvwFiles.Items.Add(lvi);
                res = true;
            }
            else
            {
                RVIImportModel.Log(String.Format("Import failed for {0}", rim.systemIndex1));
            }
        }
        return res;
    }

    private Boolean processDrawing()
    {
        Boolean res = false;
        RVIImportModel rim = new RVIImportModel();
        if (!txtrevision.Text.Equals(""))
        {
            rim.SystemCode = "2";
            rim.systemIndex1 = txtdrawno.Text.ToUpper();
            rim.systemIndex2 = txtrevision.Text.ToUpper();
            rim.systemIndex3 = txtrunno.Text; //Run
            rim.systemIndex4 = txtcallno.Text; //Call
            rim.systemIndex5 = ddlproduct.Text; //Product
            rim.systemIndex6 = txtdate.Value.ToString();
            rim.systemIndex7 = ddldocumenttypedrawing.Text;
            //rim.systemIndex8 = ddladditionalinfo.Text;
            rim.imageFileName = Server.MapPath("~/Temp/" + file_nameid.InnerText);
            rim.fileName = file_nameid.InnerText;
            String[] f = rim.fileName.Split('.');
            rim.imageType = f[1];
            File.SetAttributes(rim.imageFileName, FileAttributes.Normal);
            FileStream fs = new FileStream(rim.imageFileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bb = br.ReadBytes(int.Parse(br.BaseStream.Length.ToString()));
            rim.fileStream = Convert.ToBase64String(bb);
            fs.Close();
            if (validateData(rim) == true)
            {
                Boolean succ = false;
                succ = RVIImportModel.UploadtoRVI(rim);
                if (succ == true)
                {
                    clearscreen();
                    //ListViewItem lvi = new ListViewItem();
                    //lvi.Text = "";
                    //lvi.SubItems.Add("Upload Complete");
                    //lvwFiles.Items.Clear();
                    //lvwFiles.Items.Add(lvi);
                    res = true;
                }
            }

            else
            {
                RVIImportModel.Log(String.Format("Import failed for {0}", rim.systemIndex1));
            }
        }
        else
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('Revision is a required field; If a Revision is not available enter \'-\' in the field')</script>");
            //MessageBox.Show("Revision is a required field; If a Revision is not available enter '-' in the field");
        }
        return res;
    }

    private bool uploadDocument()
    {
        bool result = false;
        if (!file_nameid.InnerText.Equals("") && !file_nameid.InnerText.Equals(""))
        {
            String thisDate = DateTime.Now.ToString("yyyyMMdd");
            String destPath = String.Format(ConfigurationManager.AppSettings["CopyPath"].ToString(), thisDate);
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }
            String fullPath = destPath + "\\" + file_nameid.InnerText;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            File.Copy(Server.MapPath("~/Temp/" + file_nameid.InnerText), fullPath);

            if (list.SelectedIndex == 0)
            {
                result = processOrderDoc();
            }
            else if (list.SelectedIndex == 1)
            {
                result = processDrawing();
            }
        }
        else
        {
            result = false;
        }
        return result;
    }

    protected void button_submit(object sender, EventArgs e)
    {
        bool clearScreen = true;
        if (!file_nameid.InnerText.Equals("") && validFile(Server.MapPath("~/Temp/" + file_nameid.InnerText)) == true)
        {
            if (list.SelectedIndex == 0)
            {
                string workflowdoc = string.Empty;
                workflowdoc = processWorkflowDoc(txtorderno.Text, file_nameid.InnerText);
                if (workflowdoc.Equals("Import"))
                {
                    if (ddldocumenttypeorderdocument.SelectedIndex != -1)
                    {
                        if (uploadDocument() == false)
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Error during upload')</script>");
                            //MessageBox.Show("Error during upload", "RVI import error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ddldocumenttypeorderdocument.BackColor = Color.Red;
                        clearScreen = false;
                    }
                }
            }
            else if (list.SelectedIndex == 1)
            {
                uploadDocument();
            }
            if (clearScreen == true)
            {
                //closeApplication();
            }
        }
        else
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('Either there is not a file attached \r\n or the file is open in another application \r\n Add a file or close the application.')</script>");
            //MessageBox.Show("Either there is not a file attached \r\n or the file is open in another application \r\n Add a file or close the application.", "File validation.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void clearscreen()
    {
        ddladditionalinfo.SelectedIndex = -1;
        ddldocumenttypeorderdocument.SelectedIndex = -1;
        txtbranch.Text = "";
        txtdistributor.Text = "";
        txtorderno.Text = "";
        txtpono.Text = "";
        txtdrawno.Text = "";
        txtrunno.Text = "";
        txtcallno.Text = "";
        txtrevision.Text = "";
        //ListViewItem lvi = new ListViewItem();
        //lvi.Text = "";
        //lvi.SubItems.Add("Upload Complete");
        //lvwFiles.Items.Clear();
        //lvwFiles.Items.Add(lvi);
    }
}


