using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;


public class Comm
{
    public static string RootPath = System.Windows.Forms.Application.StartupPath + @"\Log\";
    public static string StartupPath = Application.StartupPath;//程序运行路径
    public static string AotuUpdateUrl = "http://nuget.jaderd.com/JadeAutoUpdate/AotoUpdate.xml";
    public static string AppName = "JadePT";

    /// <summary>
    /// 添加日志
    /// </summary>
    /// <param name="log"></param>
    /// <param name="ex"></param>
    public static void AddLog(string log, Exception ex = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<hr color=red>");
        sb.AppendFormat("记录时间：{0:yyyy-MM-dd HH:mm:ss }<br>", DateTime.Now);
        sb.AppendFormat("记录日志：{0} <br>", log);
        if (ex != null)
        {
            sb.AppendFormat("异常说明：{0}<br>", ex.Message);
            sb.AppendFormat("异常信息：{0}<br>", ex.ToString());
        }
        sb.AppendLine("<hr size=1>");
        File.AppendAllText(RootPath.CreateDirectory() + "log.html", sb.ToString(), Encoding.UTF8);
    }
    /// <summary>
    /// 添加日志
    /// </summary>
    /// <param name="log"></param>
    /// <param name="ex"></param>
    public static void AddMyLog(string log, string filename, Exception ex = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<hr color=red>");
        sb.AppendFormat("记录时间：{0:yyyy-MM-dd HH:mm:ss }<br>", DateTime.Now);
        sb.AppendFormat("记录日志：{0} <br>", log);
        if (ex != null)
        {
            sb.AppendFormat("异常说明：{0}<br>", ex.Message);
            sb.AppendFormat("异常信息：{0}<br>", ex.ToString());
        }
        sb.AppendLine("<hr size=1>");
        File.AppendAllText(RootPath.CreateDirectory() + filename + "log.html", sb.ToString(), Encoding.UTF8);
    }
    /// <summary>
    /// 查看日志
    /// </summary>
    public static void ShowLog(string fielname)
    {
        //Process.Start(RootPath + "log.html");  //打开日志文件
        Process.Start(RootPath + fielname + "log.html");  //打开日志文件
    }

    /// <summary>
    /// 判断文件是否存在
    /// </summary>
    /// <returns></returns>
    public static void IsExitFile(string filename)
    {
        if (!File.Exists(RootPath.CreateDirectory() + filename + "log.html"))
        {
            FileStream fs = new FileStream(RootPath.CreateDirectory() + filename + "log.html", FileMode.Create, FileAccess.Write);//创建写入文件 
            fs.Close();

        }
    }

    ///// <summary>
    ///// 版本升级
    ///// </summary>
    //public static void VersionUpgrade()
    //{
    //    string filePath = System.IO.Path.Combine(StartupPath, "AutoUpdate.exe");
    //    CreateFile(filePath, Jade.PrintTool.Properties.Resources.AutoUpdate);
    //    System.Diagnostics.Process.Start(filePath, "-url " + AotuUpdateUrl + " -proc " + AppName + ".exe");
    //    Comm.OutSystem();
    //}

    /// <summary>
    /// 是否有更新
    /// </summary>
    /// <returns></returns>
    public static bool CheckUpdate()
    {
        ManageUpdate mu = GetLatestVersionInfo();   //得到最后的版本信息
        if (mu != null)
            return mu.VersionNumber > ManageUpdate.ParseVersionNumber(AssemblyVersion);
        else
            return false;
    }

    /// <summary>
    /// 获得程序最新版本信息
    /// </summary>
    /// <returns></returns>
    public static ManageUpdate GetLatestVersionInfo()
    {
        return new ManageUpdate(AotuUpdateUrl);
    }

    /// <summary>
    /// 程序集版本号，直接在程序的属性中修改
    /// </summary>
    public static string AssemblyVersion
    {
        get
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="fillPath"></param>
    /// <param name="bytes"></param>
    public static void CreateFile(string fillPath, byte[] bytes)
    {
        string filePath = Path.GetDirectoryName(fillPath);
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        if (!File.Exists(fillPath))
        {
            File.WriteAllBytes(fillPath, bytes);   //写入文件
        }
    }

    ///// <summary>
    ///// 初始化程序自检必要文件，一些DLL文件
    ///// </summary>
    //public static void InitFileDetect()
    //{
    //    string filePath = StartupPath + @"\bpladll.dll";
    //    if (!File.Exists(filePath))
    //    {
    //        Comm.CreateFile(filePath, Jade.PrintTool.Properties.Resources.bpladll);
    //    }

    //    filePath = StartupPath + @"\ByUSBInt.dll";
    //    if (!File.Exists(filePath))
    //    {
    //        Comm.CreateFile(filePath, Jade.PrintTool.Properties.Resources.ByUSBInt);
    //    }

    //    filePath = StartupPath + @"\LabelUSBPrintDll.dll";
    //    if (!File.Exists(filePath))
    //    {
    //        Comm.CreateFile(filePath, Jade.PrintTool.Properties.Resources.LabelUSBPrintDll);
    //    }

    //    filePath = StartupPath + @"\Json.dll";
    //    if (!File.Exists(filePath))
    //    {
    //        Comm.CreateFile(filePath, Jade.PrintTool.Properties.Resources.Json);
    //    }

    //    filePath = StartupPath + @"\pm210.bmp";
    //    if (!File.Exists(filePath))
    //    {
    //        Jade.PrintTool.Properties.Resources.pm210.Save(filePath);
    //    }
    //}

    /// <summary>
    /// 退出系统
    /// </summary>
    public static void OutSystem()
    {

        Application.ExitThread();
        System.Environment.Exit(0);
        Process[] processes = Process.GetProcesses();
        for (int i = 0; i < processes.GetLength(0); i++)
        {
            if (processes[i].ProcessName.Equals(AppName))
            {
                processes[i].Kill();
            }
        }
    }


    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteFolder(string dir)
    {
        foreach (string d in Directory.GetFileSystemEntries(dir))
        {
            if (File.Exists(d))
            {
                FileInfo fi = new FileInfo(d);
                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    fi.Attributes = FileAttributes.Normal;
                if (fi.Name != "skin.data")
                {
                    File.Delete(d);      //直接删除其中的文件  
                }
            }
            else
            {
                DirectoryInfo d1 = new DirectoryInfo(d);
                if (d1.GetFiles().Length != 0)
                {
                    DeleteFolder(d1.FullName);////递归删除子文件夹
                }
                Directory.Delete(d);
            }
        }
    }


    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="paths"></param>
    public static void DeleteFiles(params string[] paths)
    {
        foreach (var item in paths)
        {
            File.Delete(item);
        }
    }

    //开机自动启动
    public static void StartRun(bool start, string name, string path)
    {
        RegistryKey rk = Registry.LocalMachine;
        RegistryKey Run = rk.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run"); //注册表设置开机启动
        if (start)
        {
            try
            {
                Run.SetValue(name, path);
                rk.Close();
            }
            catch { }
        }
        else
        {
            try
            {
                Run.DeleteValue(name);
                rk.Close();
            }
            catch { }
        }
    }
}

public static class MrExpand
{
    /// <summary>
    /// 创建目录
    /// </summary>
    public static string CreateDirectory(this string path)
    {
        string tempPath = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(tempPath) && !Directory.Exists(tempPath))
        {
            Directory.CreateDirectory(tempPath);
        }

        return path;
    }
}

public class ManageUpdate
{
    public ManageUpdate(string url)
    {
        ReadUpdateLog(url);
    }

    /// <summary>
    /// 读取服务端更新日志记录
    /// </summary>
    /// <param name="logUrl"></param>
    public void ReadUpdateLog(string logUrl)
    {
        try
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.Encoding = Encoding.UTF8;
            string xmlStr = wc.DownloadString(logUrl);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlStr.Trim());
            XmlNode items = xml.SelectSingleNode("Items");//查找 
            if (items != null)
            {
                this.StartFile = items.Attributes["StartFile"].Value;
                this.Version = items.Attributes["Version"].Value;
                this.Description = items.Attributes["Description"].Value;
                this.UpdateTime = DateTime.Parse(items.Attributes["UpdateTime"].Value);
                this.VersionNumber = ParseVersionNumber(this.Version);

                JadeFile jf;
                FileList.Clear();
                foreach (XmlNode item in items.ChildNodes)
                {
                    jf = new JadeFile { FileName = item.Attributes["FileName"].Value, Url = item.Attributes["Url"].Value };
                    FileList.Add(jf);
                }

            }
        }
        catch (Exception ex)
        {
            Comm.AddLog("读取版本信息", ex);
        }
    }

    private List<JadeFile> FileList = new List<JadeFile>();

    public String StartFile { get; set; }
    public String Version { get; set; }
    public String Description { get; set; }
    public DateTime UpdateTime { get; set; }
    public long VersionNumber { get; set; }

    public static long ParseVersionNumber(string s)
    {
        long num = 0;
        if (!string.IsNullOrEmpty(s)) s = s.Replace(".", "");
        long.TryParse(s, out num);
        return num;
    }

    public class JadeFile
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public String FileName { get; set; }
        /// <summary>
        /// 下载链接
        /// </summary>
        public String Url { get; set; }
    }
}


namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
    public sealed class ExtensionAttribute : Attribute { }

}


