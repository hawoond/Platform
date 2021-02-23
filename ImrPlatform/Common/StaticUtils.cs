using ICSharpCode.SharpZipLib.Zip;
using ImrPlatform.View.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImrPlatform.Common
{
    class StaticUtils
    {
        public static ucLogin loginWindow = null;

        public static bool? ShowLoginWindow()
        {
            if(loginWindow != null)
            {
                return false;
            }

            loginWindow = new ucLogin();
            return loginWindow.ShowDialog();
        }

        public static void CloseLoginWindow()
        {
            if(loginWindow != null)
            {
                loginWindow.Close();
                loginWindow = null;
            }
        }

        /// <summary>
        /// DataRow를 Class로
        /// </summary>
        /// <param name="oClass"></param>
        /// <param name="drResult"></param>
        /// <returns></returns>
        public static object DataRowToClass(object oClass, DataRow drResult)
        {
            string sPropName = string.Empty;
            string sPropValue = string.Empty;
            for (int i = 0; i < oClass.GetType().GetProperties().Length; i++)
            {
                sPropName = oClass.GetType().GetProperties()[i].Name;
                sPropValue = drResult[oClass.GetType().GetProperties()[i].Name].ToString();
                oClass.GetType().GetProperties()[i].SetValue(oClass, sPropValue);
            }

            return oClass;
        }

        /// <summary>
        /// 이미지를 이미지소스로
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static System.Windows.Media.ImageSource ImageToImageSource(System.Drawing.Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        /// <summary>
        /// 압축 풀기
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string DeCompressFolder(byte[] folder)
        {
            string sPath = System.IO.Path.GetTempPath()+ @"tempPrevImg.zip";
            string unzipPath = System.IO.Path.GetTempPath() + @"tempPrevImg";
            
            //디렉토리 있으면 지우고 새로 만들기
            if (Directory.Exists(unzipPath))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                Directory.Delete(unzipPath, true);
                Directory.CreateDirectory(unzipPath);
            }
            else
            {
                Directory.CreateDirectory(unzipPath);
            }

            while (!Directory.Exists(unzipPath))
            {
                
            }

            //파일 쓰기
            File.WriteAllBytes(sPath, folder);
            //ZipFile
            ZipInputStream zipStream = new ZipInputStream(File.OpenRead(sPath));
            ZipEntry entry;

            //압축풀기 시자악
            while ((entry = zipStream.GetNextEntry()) != null)
            {
                string Fullname = unzipPath + @"\" + entry.Name;
                string FileName = Path.GetFileName(Fullname);

                if (FileName != string.Empty)
                {
                    FileStream streamWriter = File.Create(Fullname);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);

                        if (size > 0) streamWriter.Write(data, 0, size);
                        else break;
                    }
                    streamWriter.Close();
                }
            }

            zipStream.Close();

            return unzipPath;
        }

        /// <summary>
        /// 압축 풀기
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string DeCompressFolder(byte[] folder,string sTempZipName, string sUnzipFileName)
        {
            string sPath = System.IO.Path.GetTempPath() + sTempZipName + ".zip";
            string unzipPath = sUnzipFileName;

            //디렉토리 있으면 지우고 새로 만들기
            if (!Directory.Exists(unzipPath))
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                //Directory.Delete(unzipPath, true);
                Directory.CreateDirectory(unzipPath);
            }
            //else
            //{
            //    Directory.CreateDirectory(unzipPath);
            //}

            //while (!Directory.Exists(unzipPath))
            //{

            //}

            //파일 쓰기
            File.WriteAllBytes(sPath, folder);
            //ZipFile
            ZipInputStream zipStream = new ZipInputStream(File.OpenRead(sPath));
            ZipEntry entry;
            string Fullname = string.Empty;
            //압축풀기 시자악
            while ((entry = zipStream.GetNextEntry()) != null)
            {
                Fullname = unzipPath + @"\" + entry.Name;
                string FileName = Path.GetFileName(Fullname);

                if (FileName != string.Empty)
                {
                    FileStream streamWriter = File.Create(Fullname);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);

                        if (size > 0) streamWriter.Write(data, 0, size);
                        else break;
                    }
                    streamWriter.Close();
                }
            }

            zipStream.Close();

            return Fullname;
        }

        /// <summary>
        /// 에러 DataSet 리턴
        /// </summary>
        /// <param name="sErrorMsg"></param>
        /// <returns></returns>
        public static DataSet ExceptionDataSetResult(string sErrorMsg)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //dt.TableName = "Table";
            dt.TableName = "Error";
            dt.Columns.Clear();
            dt.Columns.Add("ERROR_YN");
            dt.Columns.Add("ERROR_MESSAGE");
            dt.Rows.Add(new string[] { "Y", sErrorMsg });
            ds.Tables.Add(dt);
            return ds;
        }
    }
}
