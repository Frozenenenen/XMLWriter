using System;
using System.Collections.Generic;
using System.Text;

namespace XMLWriter
{
    class WriteRepToXML
    {
        public string[] FillListRep(int stepCountMax, List<string> steps, List<string> stepTexts, List<string> stepAnims, List<string> stepSpecial)
        {

            List<String> list = new List<string> { };
            list.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            list.Add("<RepairCollection>");
            list.Add("\t" + "<Repairs>");

            for (int i = 0; i <= stepCountMax; i++)
            {
                //Console.WriteLine(i);
                try
                {

                    list.Add("\t\t" + "<Repair step=\"" + steps[i] + "\">");
                    list.Add("\t\t\t" + "<content>" + stepTexts[i] + "</content>");
                    if (stepAnims[i] == "")
                    {
                        list.Add("\t\t\t" + "<anim>" + "default" + "</anim>");
                    }
                    else
                    {
                        list.Add("\t\t\t" + "<anim>" + stepAnims[i] + "</anim>");
                    }

                    if (stepSpecial[i] == "")
                    {
                        list.Add("\t\t\t" + "<specialStep>" + "false" + "</specialStep>");
                    }
                    else
                    {
                        list.Add("\t\t\t" + "<specialStep>" + stepSpecial[i] + "</specialStep>");
                    }

                    list.Add("\t\t" + "</Repair>");
                }
                catch (Exception)
                {

                    Console.WriteLine("Fehler beim FillListRep()");
                }
            }


            list.Add("\t" + "</Repairs>");
            list.Add("</RepairCollection>");

            string[] output = list.ToArray();

            return output;
        }
    }
}
