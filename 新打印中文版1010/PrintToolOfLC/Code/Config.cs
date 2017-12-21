using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

[Serializable()]
public class JadeApp
{
	/// <summary>
	/// 称重串口名称
	/// </summary>
	public string PortName { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public string From_Name { get; set; }


	/// <summary>
	/// 发货电话
	/// </summary>
	public string From_Phone { get; set; }


	/// <summary>
	/// 发货邮编
	/// </summary>
	public string From_Post { get; set; }


	/// <summary>
	/// 发货地址
	/// </summary>
	public string From_Address { get; set; }


	/// <summary>
	/// 数据库链接字符串
	/// </summary>
	public string ConnectionString { get; set; }

	/// <summary>
	/// 开机启动
	/// </summary>
	public bool IsStartRun { get; set; }


	/// <summary>
	/// 打印类型
	/// </summary>
	public int PrintType { get; set; }

    /// <summary>
    /// 打印机ID
    /// </summary>
    public int PrinterID { get; set; }

}

public class ConfigManage
{

	static JadeApp _JadeApp;
	static readonly string jadeAppFileName = Comm.RootPath + @"\JadeApp.config";
	static ConfigManage()
	{
		_JadeApp = Read<JadeApp>(jadeAppFileName);
	}

	/// <summary>
	/// 应用配置信息
	/// </summary>
	public static JadeApp JadeApp
	{
		get
		{
			if (_JadeApp == null)
			{
				_JadeApp = Read<JadeApp>(jadeAppFileName);
				if (_JadeApp == null)
				{
					_JadeApp = new JadeApp { From_Name="Jade", From_Post="321000", From_Address="" };
				}
			}
			return _JadeApp;
		}

	}

	/// <summary>
	/// 保存配置数据
	/// </summary>
	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		jadeAppFileName.CreateDirectory();

		try
		{
			using (FileStream stream = new FileStream(jadeAppFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				XmlSerializer sz = new XmlSerializer(JadeApp.GetType());
				sz.Serialize(stream, JadeApp);
			}
		}
		catch (Exception ex)
		{
			Comm.AddLog("保存APP配置信息时异常", ex);
		}
	}



	/// <summary>
	/// 读取配置数据
	/// </summary>
	public static T Read<T>(string fileName)
	{
		var m = default(T);
		if (!File.Exists(fileName)) { return default(T); }
		try
		{
			XmlSerializer xs = new XmlSerializer(typeof(T));
			using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				m = (T)xs.Deserialize(stream);
			}
		}
		catch (Exception ex)
		{
			Comm.AddLog("读取APP配置信息时异常", ex);
		}
		return m;
	}
}

