using System;
using System.IO;
using System.Text;
using System.Security;

public class RVIImportModel
{
    //System transaction number
    private String mTransactionNumber;
    public String transactionNumber
    {
        get { return mTransactionNumber; }
        set { mTransactionNumber = value; }
    }


    /** The id associated with this processing preference */
    private String mSystemCode = "";
    public String SystemCode
    {
        get { return mSystemCode; }
        set { mSystemCode = value; }
    }

    /** This relates directly to the field System index 1 */
    private String mSystemIndex1 = "";
    public String systemIndex1
    {
        get { return mSystemIndex1; }
        set { mSystemIndex1 = value; }
    }

    /** This relates directly to the field System index 2 */
    private String mSystemIndex2 = "";
    public String systemIndex2
    {
        get { return mSystemIndex2; }
        set { mSystemIndex2 = value; }
    }

    /** This relates directly to the field System index 3 */
    private String mSystemIndex3 = "";
    public String systemIndex3
    {
        get { return mSystemIndex3; }
        set { mSystemIndex3 = value; }
    }

    /** This relates directly to the field System index 4 */
    private String mSystemIndex4 = "";
    public String systemIndex4
    {
        get { return mSystemIndex4; }
        set { mSystemIndex4 = value; }
    }

    /** This relates directly to the field System index 5 */
    private String mSystemIndex5 = "";
    public String systemIndex5
    {
        get { return mSystemIndex5; }
        set { mSystemIndex5 = value; }
    }

    /** This relates directly to the field System index 6 */
    private String mSystemIndex6 = "";
    public String systemIndex6
    {
        get { return mSystemIndex6; }
        set { mSystemIndex6 = value; }
    }

    /** This relates directly to the field System index 7 */
    private String mSystemIndex7 = "";
    public String systemIndex7
    {
        get { return mSystemIndex7; }
        set { mSystemIndex7 = value; }
    }

    /** This relates directly to the field System index 8 */
    private String mSystemIndex8 = "";
    public String systemIndex8
    {
        get { return mSystemIndex8; }
        set { mSystemIndex8 = value; }
    }

    /** This relates directly to the field System index 9 */
    private String mSystemIndex9 = "";
    public String systemIndex9
    {
        get { return mSystemIndex9; }
        set { mSystemIndex9 = value; }
    }

    /** This relates directly to the field System index 10 */
    private String mSystemIndex10 = "";
    public String systemIndex10
    {
        get { return mSystemIndex10; }
        set { mSystemIndex10 = value; }
    }

    /** This relates directly to the field System index 11 */
    private String mSystemIndex11 = "";
    public String systemIndex11
    {
        get { return mSystemIndex11; }
        set { mSystemIndex11 = value; }
    }

    /** This relates directly to the field System index 12 */
    private String mSystemIndex12 = "";
    public String systemIndex12
    {
        get { return mSystemIndex12; }
        set { mSystemIndex12 = value; }
    }

    /** This relates directly to the field System index 13 */
    private String mSystemIndex13 = "";
    public String systemIndex13
    {
        get { return mSystemIndex13; }
        set { mSystemIndex13 = value; }
    }

    /** This relates directly to the field System index 14 */
    private String mSystemIndex14 = "";
    public String systemIndex14
    {
        get { return mSystemIndex14; }
        set { mSystemIndex14 = value; }
    }

    /** Image Path */
    private String mFileName = "";
    public String fileName
    {
        get { return mFileName; }
        set { mFileName = value; }
    }

    /** Image File Name */
    private String mImageFileName = "";
    public String imageFileName
    {
        get { return mImageFileName; }
        set { mImageFileName = value; }
    }

    /** Image Type* */
    private String mImageType = "";

    public String imageType
    {
        get { return mImageType; }
        set { mImageType = value; }
    }

    private String mFileStream;

    public String fileStream
    {
        get { return mFileStream; }
        set { mFileStream = value; }
    }

    /// <summary>
    /// Upload Document to RVI
    /// </summary>
    /// <param name="rim"></param>
    /// <param name="debug"></param>
    /// <returns></returns>
    static public Boolean UploadtoRVI(RVIImportModel rim)
    {
        Boolean result = false;
        //int count = 0;
        try
        {
            StringBuilder input = new StringBuilder();
            input.Append("<rviimport>");

            input.Append("<systemid>");
            input.Append(rim.SystemCode);
            input.Append("</systemid>");

            input.Append("<ordernumber>");
            input.Append(SecurityElement.Escape(rim.systemIndex1));
            input.Append("</ordernumber>");

            input.Append("<company>");
            input.Append(SecurityElement.Escape(rim.systemIndex2));
            input.Append("</company>");

            input.Append("<customernumber>");
            input.Append(SecurityElement.Escape(rim.systemIndex3));
            input.Append("</customernumber>");

            input.Append("<index4>");
            input.Append(SecurityElement.Escape(rim.systemIndex4));
            input.Append("</index4>");

            input.Append("<docid>");
            input.Append(SecurityElement.Escape(rim.systemIndex5));
            input.Append("</docid>");

            input.Append("<createdate>");
            input.Append(SecurityElement.Escape(rim.systemIndex6));
            input.Append("</createdate>");

            input.Append("<documenttype>");
            input.Append(SecurityElement.Escape(rim.systemIndex7));
            input.Append("</documenttype>");

            input.Append("<index8>");
            input.Append(SecurityElement.Escape(rim.systemIndex8));
            input.Append("</index8>");

            input.Append("<index9>");
            input.Append(SecurityElement.Escape(rim.systemIndex9));
            input.Append("</index9>");

            input.Append("<index10>");
            input.Append(SecurityElement.Escape(rim.systemIndex10));
            input.Append("</index10>");

            input.Append("<index11>");
            input.Append(SecurityElement.Escape(rim.systemIndex11));
            input.Append("</index11>");

            input.Append("<index12>");
            input.Append(SecurityElement.Escape(rim.systemIndex12));
            input.Append("</index12>");

            input.Append("<index13>");
            input.Append(SecurityElement.Escape(rim.systemIndex13));
            input.Append("</index13>");

            input.Append("<index14>");
            input.Append(SecurityElement.Escape(rim.systemIndex14));
            input.Append("</index14>");

            input.Append("<filename>");
            input.Append(rim.fileName);
            input.Append("</filename>");

            input.Append("<docext>");
            input.Append(rim.imageType);
            input.Append("</docext>");

            input.Append("<file>");
            input.Append(rim.fileStream);
            input.Append("</file>");

            input.Append("</rviimport>");

            String xmlString = input.ToString();
            String res;
            res = callWebService(xmlString);

            result = true;

        }
        catch (FileNotFoundException fnfe)
        {
            Log(rim.fileName + fnfe.ToString());
        }
        catch (IOException ioe)
        {
            Log(rim.fileName + ioe.ToString());
        }
        catch (Exception e)
        {
            Log(rim.systemIndex1 + e.ToString());
        }
        return result;
    }

    static private String callWebService(String xmlString)
    {
        //  String rviDocumentImportUrl = "http://usmassvm24.doorgroup.com/papps/services/RVIDocumentImport";
        String rviDocumentImportUrl = "D:/Pradeep/Assa Abloy/Source codes- From Client/Document Import Java/RVIDocumentImport";
        RVIDocumentImportService rdis = new RVIDocumentImportService();
        String res = "";
        rdis.Url = rviDocumentImportUrl;
        res = rdis.indexDocument(xmlString);
        rdis.Dispose();
        return res;
    }

    static public void Log(string logMessage)
    {
        using (StreamWriter sw = File.AppendText("C:\\ImportLogs\\" + DateTime.Today.ToString("MMddyyyy") + "Log.txt"))
        {
            sw.Write("\r\n");
            sw.WriteLine("  {0} {1}", DateTime.Now.ToShortTimeString(),
                DateTime.Now.ToShortDateString());
            //sw.WriteLine("  :");
            sw.WriteLine("{0}", logMessage);
            sw.WriteLine("-------------------------------------------");
            // Update the underlying file.
            sw.Flush();
        }
    }
}
