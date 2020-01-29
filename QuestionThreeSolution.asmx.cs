using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace SimpleWebService
{
    /// <summary>
    /// Question Three Running Environment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class QuestionThreeSolution : System.Web.Services.WebService
    {
        /// <summary>
        /// Checks the integrity of an XML document and returns an int based on the results.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>
        /// 0 - if structured correctly
        /// -1 - invalid command specified
        /// -2 - invalid site specified</returns>
        [WebMethod]
        public int ParsePayload(XmlDocument doc)
        {
            //find the declaration
            XmlNode decNode = doc.SelectSingleNode("InputDocument/DeclarationList/Declaration");

            //if the declaration is null then it is automatically invalid
            if (decNode == null)
                return -1;

            //search the decleration attributes for the 'Command' attribute and check if it's equal to 'DEFAULT'. Return -1 if not.
            foreach(XmlAttribute atr in decNode.Attributes)
            {
                if(atr.Name == "Command")
                {
                    if(atr.Value != "DEFAULT")
                        return -1;
                }
            }

            //find the siteID
            XmlNode siteNode = doc.SelectSingleNode("InputDocument/DeclarationList/Declaration/DeclarationHeader/SiteID");

            //If siteID is null then it's automatically invalid
            if (siteNode == null)
                return -2;

            //if siteID isn't DUB then return -2
            if (siteNode.InnerText != "DUB")
                return -2;

            //If we got this far then assume all above checks passed and we can return 0
            return 0;
        }
    }
}
