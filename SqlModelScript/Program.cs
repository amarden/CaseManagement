using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlModelScript
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Directory.GetCurrentDirectory();
            string sqlDir = directory.Replace("SqlModelScript\\bin\\Debug", "CaseManagement\\Models\\SQL");
            foreach(var file in new DirectoryInfo(sqlDir).GetFiles())
            {
                if(file.Name == "SQLDatabase.cs" || file.Name == "Client.cs")
                {
                    continue;
                }
                var myFile = new FileInfo(file.Name);
                StreamReader sr = new StreamReader(file.FullName);
                string textLine;
                List<string> lines = new List<string>();
                while((textLine = sr.ReadLine()) != null)
                {
                    if(textLine.TrimStart().StartsWith("public virtual ICollection"))
                    {
                        lines.Add("\t\t[JsonIgnore]");
                        lines.Add(textLine);
                        continue;
                    }
                    else if(textLine.TrimStart().StartsWith("using System;"))
                    {
                        lines.Add("\tusing Newtonsoft.Json;");
                        lines.Add(textLine);
                        continue;
                    }
                    else if(textLine.TrimStart().StartsWith("public virtual Client"))
                    {
                        lines.Add("\t\t[JsonIgnore]");
                        lines.Add(textLine);
                        continue;
                    }
                    lines.Add(textLine);
                }
                sr.Close();
                StreamWriter wr = new StreamWriter(file.FullName);
                foreach(var line in lines)
                {
                    wr.WriteLine(line);
                }
                wr.Close();
            }
        }
    }
}
