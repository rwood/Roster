using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IronPython.Hosting;

namespace Roster
{
    class PyTemplate
    {
        private int pyTabIndex = 0;
        private readonly string startTag = "<!--py";
        private readonly string bogusStartTag = "<!--";
        private readonly string endTag = "-->";
        private readonly string printTagStart = "{{";
        private readonly string printTagEnd = "}}";
        private List<DirectoryInfo> m_SearchPath = new List<DirectoryInfo>();
        private Dictionary<string, object> m_Globals = new Dictionary<string, object>();
        private string m_script;

        public string PythonScript { get { return m_script; } }

        public PyTemplate()
        {
        }

        public void AddSearchPath(DirectoryInfo path)
        { m_SearchPath.Add(path); }

        public string Render()
        {
            return RunPython(m_script);
        }

        public void Parse(string template)
        {
            m_script = ConvertToPython(template);
        }

        public void WriteScriptToFile(FileStream fs)
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(m_script);
            sw.Close();
            fs.Close();
        }

        private string RunPython(string script)
        {
            PythonEngine engine;
            MemoryStream output;
            GetPythonEngine(out engine, out output);
            try
            {
                engine.Execute(script);
                return GetStringFromMemoryStream(output);
            }
            catch (Exception ex)
            {
                throw new PyTemplateException(ex.Message, ex, m_script, "Template");
            }
        }

        public void AddGlobal(string name, object obj)
        {
            m_Globals.Add(name, obj);
        }

        private void GetPythonEngine(out PythonEngine engine, out MemoryStream output)
        {
            engine = new PythonEngine();
            engine.Execute("import clr");
            foreach (DirectoryInfo di in m_SearchPath)
            {
                if (di.Exists)
                    engine.AddToPath(di.FullName);
            }
            foreach (string name in m_Globals.Keys)
            {
                if (!engine.Globals.ContainsKey(name))
                    engine.Globals.Add(name, m_Globals[name]);
                else
                    engine.Globals[name] = m_Globals[name];
            }
            output = new MemoryStream();
            MemoryStream error = new MemoryStream();
            engine.SetStandardOutput(output);
            engine.SetStandardError(error);
        }
        
        private void GetPythonConsoleEngine(out PythonEngine engine)
        {
            engine = new PythonEngine();
            //engine.Execute("import clr");
            foreach (DirectoryInfo di in m_SearchPath)
            {
                if (di.Exists)
                    engine.AddToPath(di.FullName);
            }
            foreach (string name in m_Globals.Keys)
            {
                if (!engine.Globals.ContainsKey(name))
                    engine.Globals.Add(name, m_Globals[name]);
                else
                    engine.Globals[name] = m_Globals[name];
            }
        }

        private string ConvertToPython(string template)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("from sys import stdout");
            template = template.Replace(printTagStart, "||NOBR||" + startTag + "stdout.write(");
            template = template.Replace(printTagEnd, ")" + endTag + "");
            int tabCount = 0;

            string[] splitArr = new string[1];
            splitArr[0] = startTag;
            string[] arr = template.Split(splitArr, StringSplitOptions.RemoveEmptyEntries);
            splitArr[0] = Environment.NewLine;
            foreach (string str in arr)
            {
                int endTagIndex = str.IndexOf(endTag);
                int bogusTagIndex = str.IndexOf(bogusStartTag);
                if (endTagIndex > -1 && (bogusTagIndex == -1 || bogusTagIndex > endTagIndex))
                {
                    string first = str.Substring(0, endTagIndex);
                    string second = str.Substring(endTagIndex + endTag.Length, str.Length - (endTagIndex + endTag.Length));
                    string[] codeArr = first.Split(splitArr, StringSplitOptions.RemoveEmptyEntries);
                    string[] markupArr = second.Split(splitArr, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string code in codeArr)
                    {
                        string tmpCode = code.Trim();
                        if (tmpCode.Length < 1)
                            continue;

                        if (tmpCode.Contains(":end"))
                        {
                            tmpCode = string.Empty;
                            tabCount--;
                        }
                        if (tmpCode.Contains("import"))
                            continue;
                        if (tmpCode.Contains("clr."))
                            continue;

                        for (int i = 0; i < tabCount; i++)
                            tmpCode = "\t" + tmpCode;

                        sb.AppendLine(tmpCode);

                        if (tmpCode.EndsWith(":"))
                            tabCount++;
                    }
                    foreach (string markup in markupArr)
                    {
                        string tmpMarkup = markup;
                        if (tmpMarkup.Contains("||NOBR||"))
                            tmpMarkup = tmpMarkup.Replace("||NOBR||", string.Empty);
                        else
                            tmpMarkup += Environment.NewLine;
                        if (tmpMarkup.Length < 1)
                            continue;
                        if (tmpMarkup.StartsWith("'") || tmpMarkup.EndsWith("'"))
                            tmpMarkup = "stdout.write(r\"\"\"" + tmpMarkup + "\"\"\")";
                        else
                            tmpMarkup = "stdout.write(r'''" + tmpMarkup + "''')";
                        for (int i = 0; i < tabCount; i++)
                            tmpMarkup = "\t" + tmpMarkup;
                        sb.AppendLine(tmpMarkup);
                    }
                }
                else
                {
                    string tmpMarkup = str.Trim();
                    if (tmpMarkup.Contains("||NOBR||"))
                        tmpMarkup = tmpMarkup.Replace("||NOBR||", string.Empty);
                    else
                        tmpMarkup += Environment.NewLine;
                    if (tmpMarkup.Length < 1)
                        continue;
                    if (tmpMarkup.StartsWith("'") || tmpMarkup.EndsWith("'"))
                        tmpMarkup = "stdout.write(r\"\"\"" + tmpMarkup + "\"\"\")";
                    else
                        tmpMarkup = "stdout.write(r'''" + tmpMarkup + "''')";
                    for (int i = 0; i < tabCount; i++)
                        tmpMarkup = "\t" + tmpMarkup;
                    if (tmpMarkup.EndsWith("||NOBR||"))
                        sb.Append(tmpMarkup.Replace("||NOBR||", ""));
                    else
                        sb.AppendLine(tmpMarkup);
                }
            }
            return sb.ToString();
        }

        private string GetStringFromMemoryStream(MemoryStream m)
        {
            if (m == null || m.Length == 0)
                return null;

            m.Flush();
            m.Position = 0;
            StreamReader sr = new StreamReader(m);
            string s = sr.ReadToEnd();

            return s;
        }

        private MemoryStream GetMemoryStreamFromString(string s)
        {
            if (s == null || s.Length == 0)
                return null;

            MemoryStream m = new MemoryStream();
            StreamWriter sw = new StreamWriter(m);
            sw.Write(s);
            sw.Flush();

            return m;
        }
    }

    [global::System.Serializable]
    public class PyTemplateException : Exception
    {
        public PyTemplateException() { }
        public PyTemplateException(string message) : base(message) { }
        public PyTemplateException(string message, Exception inner) : base(message, inner) { }
        protected PyTemplateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
        public PyTemplateException(string message, Exception inner, string compiledScript, string module)
            : base(message, inner)
        {
            Module = module;
            CompiledScript = compiledScript;
        }
        public string Module = string.Empty;
        public string CompiledScript = string.Empty;
    }
}

