using CommHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LCWebApp.Controllers.Base
{
    public class ValidCodeController : Controller
    {
        //
        // GET: /ValidCode/
        public ActionResult Index()
        {
            if (Request != null && Request.Cookies["tockenid"] == null) //第一次登陆,写入cookies
            {
                HttpCookie cook = new HttpCookie("tockenid");
                cook.Value = Guid.NewGuid().ToString("N").ToLower();
                Response.SetCookie(cook);
                Response.Cookies.Add(cook);
            }

            string code = CreateRandomCode(4);
            //Session["RandomCode"] = code;

            SessionHelper.Add("RandomCode", code);
            byte[] ms = CreateImage(code);
            return File(ms, "image/jpeg");
        }

        ///  <summary>
        ///  生成随机码
        ///  </summary>
        ///  <param  name="length">随机码个数</param>
        ///  <returns></returns>
        private string CreateRandomCode(int length)
        {
            int rand;
            char code;
            string randomcode = String.Empty;

            //生成一定长度的验证码
            System.Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                rand = random.Next();

                //if (rand % 3 == 0)
                //{
                //    code = (char)('A' + (char)(rand % 26));
                //}
                //else
                //{
                code = (char)('0' + (char)(rand % 10));
                //}

                randomcode += code.ToString();
            }

#if DEBUG

            return randomcode;

#endif

#if !DEBUG

            return randomcode;

#endif
        }

        ///  <summary>
        ///  创建随机码图片
        ///  </summary>
        ///  <param  name="randomcode">随机码</param>
        private byte[] CreateImage(string randomcode)
        {
            int randAngle = 45; //随机转动角度
            int mapwidth = (int)(randomcode.Length * 23);
            Bitmap map = new Bitmap(mapwidth, 28);//创建图片背景
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(Color.AliceBlue);//清除画面，填充背景
            graph.DrawRectangle(new Pen(Color.Black, 0), 0, 0, map.Width - 1, map.Height - 1);//画一个边框
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;//模式

            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown };

            Random rand = new Random();

            //背景噪点生成
            Pen blackPen = new Pen(Color.LightGray, 0);

            for (int i = 0; i < 10; i++)
            {
                int x1 = rand.Next(100);
                int y1 = rand.Next(40);
                int x2 = rand.Next(100);
                int y2 = rand.Next(40);
                Color clr = color[rand.Next(color.Length)];
                graph.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }


            //验证码旋转，防止机器识别
            char[] chars = randomcode.ToCharArray();//拆散字符串成单字符数组

            //文字距中
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown };
            //定义字体
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

            int cindex = rand.Next(c.Length);
            int findex = rand.Next(font.Length);

            for (int i = 0; i < chars.Length; i++)
            {
                Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);//字体样式(参数2为字体大小)
                Brush b = new System.Drawing.SolidBrush(c[cindex]);

                Point dot = new Point(16, 16);
                //graph.DrawString(dot.X.ToString(),fontstyle,new SolidBrush(Color.Black),10,150);//测试X坐标显示间距的
                float angle = rand.Next(-randAngle, randAngle);//转动的度数

                graph.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
                graph.RotateTransform(angle);
                graph.DrawString(chars[i].ToString(), f, b, 1, 1, format);
                graph.RotateTransform(-angle);//转回去
                graph.TranslateTransform(2, -dot.Y);//移动光标到指定位置
            }

            //生成图片
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            map.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] imageByte = ms.ToArray();
            graph.Dispose();
            map.Dispose();
            return imageByte;
        }
	}
}