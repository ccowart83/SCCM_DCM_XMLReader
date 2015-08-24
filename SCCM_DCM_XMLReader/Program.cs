using System;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SCCM_DCM_XMLReader
{


    class Program
    {
        public class SimpleSetting
        {
            public string LogicalName { get; set; }
            public string DataType { get; set; }
            public LogicalNameDisplayName DisplayName { get; set; }
            public RegistryDiscoverySourceInfo RegistryDiscoverySource { get; set; }

        }
        public class RegistryDiscoverySourceInfo
        {
            public string Hive { get; set; }
            public string Depth { get; set; }

        }

        public class LogicalNameDisplayName
        {
            public string Text { get; set; }
        }

        static void Main(string[] args)
        {

            List<SimpleSetting> simplesettings = GetSimple();
           foreach (SimpleSetting simplesetting in simplesettings)
            {
                Console.WriteLine("{0,17}{1,12}{2,22}", simplesetting.LogicalName, simplesetting.DataType, simplesetting.DisplayName.Text);
            }
            

            Console.ReadKey();

        }

        static public List<SimpleSetting> GetSimple()
        {

            /*List<Plugin> plugins = new List<Plugin>(from plugin in document.Descendants(ns + "Plugin")
                                                    select new Plugin
                                                    {
                                                        Name = (String)plugin.Attribute("Name"),
                                                        Type = (String)plugin.Attribute("Type"),
                                                        Implementation = new PluginImplementation()
                                                        {
                                                            EntryPoint = (String)plugin.Element(ns + "Runtime").Element(ns + "Implementation").Attribute("EntryPoint")
                                                        },
                                                        Files = new List<PluginFile>(from implementation in plugin.Descendants(ns + "File")
                                                                                     select new PluginFile
                                                                                     {
                                                                                         Name = (String)implementation.Attribute("Name")
                                                                                     })
                                                    });
                                                    */
            XDocument document = XDocument.Load(@"c:\\Temp\\Auth.xml");
            XNamespace ns = document.Root.Name.Namespace;

            List<SimpleSetting> simplesettings = new List<SimpleSetting>(from simplesetting in document.Descendants(ns + "SimpleSetting")
                                                                         select new SimpleSetting
                                                                         {
                                                                             LogicalName = (String)simplesetting.Attribute("LogicalName"),
                                                                             DataType = (String)simplesetting.Attribute("DataType"),
                                                                             DisplayName = new LogicalNameDisplayName()
                                                                             {
                                                                                 Text = (string)simplesetting.Element(ns + "Annotation").Element(ns + "DisplayName").Attribute("Text")
                                                                             }
                                                                         });
return simplesettings;
}
                
        }
    }

