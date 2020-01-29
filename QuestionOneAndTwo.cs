using System;
using System.Collections.Generic;
using System.Xml;


namespace CodingQuestions
{
    /// <summary>
    /// Used in running test cases for each method.
    /// </summary>
    public class runCode
    {
        /// <summary>
        /// Testing code can go here.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
        }
    }
    class QuestionOne
    {

        /// <summary>
        /// Given an EDIFACT message as a string array (where each array element is a single line of text), returns the 2nd and 3rd element of each LOC element.
        /// </summary>
        /// <param name="edifact"></param>
        /// <returns>string - 2nd and 3rd LOC elements.</returns>
        internal string[] GetLocs(string[] edifact)
        {
            //using a list to avoid needing to know the array length initially.
            List<string> locs = new List<string>();

            //runs through each line of our EDIFACT message
            foreach(string lin in edifact)
            {
                //build an array of LOC elements where LOC is found as the first characters of a new line.
                if(lin.StartsWith("LOC")){
                    string[] elements = lin.Split('+');

                    //iterates through the elements and returns all elements except the LOC string.
                    foreach(string element in elements)
                    {
                        if (element == "LOC")
                            continue;

                        //removes the ' from the end of the final element
                        if (element.EndsWith("'"))
                            locs.Add(element.Substring(0, element.Length - 1));
                        else
                            locs.Add(element);
                    }
                }
            }

            return locs.ToArray();
        }
    }

    class QuestionTwo
    {
        /// <summary>
        /// Given an XMLDocument file and a RefCode(string), returns the RefText for the first match found.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="refCode"></param>
        /// <returns></returns>
        internal string GetMWB(XmlDocument doc, string refCode)
        {
            //Find each of the reference elements and make a list the nodes.
            XmlNodeList nodes = doc.GetElementsByTagName("Reference");

            //Exit if no matching elements are found.
            if (nodes.Count == 0)
                return "";

            //looks for the matching attribute and assigns the return value to an XmlNode
            XmlNode retNode = MatchAttribute(nodes, refCode);

            //if a refCode match was found, return the RefText value, else return an enmpty string.
            if (retNode != null)
                return retNode.InnerText;
            else
                return "";
            
        }

        /// <summary>
        /// Looks for an attribute matching the inputted attribute string and returns the associated node or null if not found
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="attri"></param>
        /// <returns>XmlNode object or null if no matches found</returns>
        private XmlNode MatchAttribute(XmlNodeList nodes, string attri)
        {
            //Iterate through each node, checking if the 'RefCode' attribute matches our required refCode
            foreach (XmlNode node in nodes)
            {
                foreach (XmlAttribute att in node.Attributes)
                {
                    if (att.Value == attri)
                    {
                        return node;
                    }
                }
            }

            //return null if no match found
            return null;
        }
    }

}