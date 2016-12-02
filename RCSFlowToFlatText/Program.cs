using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RCSFlowToFlatText
{
    class Program
    {
        enum XMLFlowStates
        {
            Init,
            GotRCSFlowData,
            GotHeader,
            GotFlowData,
            GotF,
            GotT,
            GotFF
        }

        static void Main(string[] args)
        {   
            try
            {
                if (args.Length != 1)
                {
                    throw new Exception("You must supply an RCS flow filename");
                }
                var filename = args[0];

                var level = 0;
                XMLFlowStates state = XMLFlowStates.Init;
                using (var stream = new FileStream(filename, FileMode.Open))
                {
                    using (var xmlreader = XmlReader.Create(stream))
                    {
                        while(xmlreader.Read())
                        {
                            if (xmlreader.NodeType == XmlNodeType.Element)
                            {
                                if (state == XMLFlowStates.Init && xmlreader.Name == "RCSFlowData")
                                {
                                    state = XMLFlowStates.GotRCSFlowData;
                                }
                                else if (state == XMLFlowStates.GotRCSFlowData && xmlreader.Name == "Header")
                                {
                                    state = XMLFlowStates.GotHeader;
                                }
                                else if (state == XMLFlowStates.GotHeader && xmlreader.Name == "FlowData")
                                {
                                    state = XMLFlowStates.GotFlowData;
                                }
                                else if (state == XMLFlowStates.GotFlowData && xmlreader.Name == "F")
                                {
                                    for (var i = 0; i < xmlreader.AttributeCount; ++i)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                string progname = Path.GetFileNameWithoutExtension(codeBase);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }
        }
    }
}
