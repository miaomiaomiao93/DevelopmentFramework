using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    class HttpLoadFilesHelper
    {
        /// <summary>
        /// Http下载文件支持断点续传
        /// </summary>
        /// <param name="uri">下载地址</param>
        /// <param name="filefullpath">存放完整路径（含文件名）</param>
        /// <param name="size">每次多的大小</param>
        /// <returns>下载操作是否成功</returns>
        public static bool HttpDownLoadFiles(string uri, string filefullpath, int size = 1024)
        {
            try
            {
                string fileDirectory = System.IO.Path.GetDirectoryName(filefullpath);
                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }
                string fileFullPath = filefullpath;
                string fileTempFullPath = filefullpath;
                if (System.IO.File.Exists(fileFullPath))
                {
                    return true;
                }
                else
                {
                    if (System.IO.File.Exists(fileTempFullPath))
                    {
                        FileStream fs = new FileStream(fileTempFullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        byte[] buffer = new byte[512];
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                        request.Timeout = 10000;
                        request.AddRange((int)fs.Length);
                        Stream ns = request.GetResponse().GetResponseStream();
                        long contentLength = request.GetResponse().ContentLength;
                        int length = ns.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            fs.Write(buffer, 0, length);
                            buffer = new byte[512];
                            length = ns.Read(buffer, 0, buffer.Length);
                        }
                        fs.Close();
                        System.IO.File.Move(fileTempFullPath, fileFullPath);
                    }
                    else
                    {
                        FileStream fs = new FileStream(fileTempFullPath, FileMode.Create);
                        byte[] buffer = new byte[512];
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                        request.Timeout = 10000;
                        request.AddRange((int)fs.Length);
                        Stream ns = request.GetResponse().GetResponseStream();
                        long contentLength = request.GetResponse().ContentLength;
                        int length = ns.Read(buffer, 0, buffer.Length);
                        while (length > 0)
                        {
                            fs.Write(buffer, 0, length);
                            buffer = new byte[512];
                            length = ns.Read(buffer, 0, buffer.Length);
                        }
                        fs.Close();
                        System.IO.File.Move(fileTempFullPath, fileFullPath);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
