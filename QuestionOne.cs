using System;
using System.Collections.Generic;
using System.Xml;


namespace CodingQuestions
{ 
    class QuestionOne
    {

        /// <summary>
        /// Given an EDIFACT message as a string array (where is array element is a new line), returns the 2nd and 3rd element of each LOC element.
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

    public class runCode
    {
        static void Main(string[] args)
        {
            QuestionOne quest = new QuestionOne();
            string[] edifact = System.IO.File.ReadAllLines(@"..\..\EDIFACTMessage.txt");


            string[] result = quest.GetLocs(edifact);

            foreach(string res in result)
            {
                Console.WriteLine(res);
            }
            Console.ReadKey();
        }
    }
}