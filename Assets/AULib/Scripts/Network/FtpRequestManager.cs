using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// FTP Request�� ���� �Ŵ��� Ŭ����
    /// �ÿ� �뵵�� ����Ŷ� �� ���� ���� �ʿ�
    /// </summary>
    public class FtpRequestManager : BaseBehaviour
    {

        [SerializeField] string _targetUrl;
        [SerializeField] string _fileName;
        [SerializeField] string _userName;
        [SerializeField] string _password;
        [SerializeField] bool _usePassive;


        public string TargetUrl => _targetUrl;
        public string FileName => _fileName;


        /// <summary>
        /// FTP �ٿ�ε� ��û
        /// </summary>
        /// <returns></returns>
        public bool FtpDownload()
        {
            return FtpDownload(_targetUrl, _fileName);
        }


        /// <summary>
        /// FTP �ٿ�ε� ��û
        /// Custom �ּ�
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FtpDownload(string targetUrl, string fileName)
        {
            //if (ftpesponse.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
            //{

            //    return false;
            //}

            if (!FtpFileExist(targetUrl, fileName))
            {
                return false;
            }

            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(targetUrl + "/" + fileName);
            ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
            ftpWebRequest.UsePassive = _usePassive;
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;



            using (var localfile = File.Open(Application.persistentDataPath + @"\" + fileName, FileMode.Create))
            using (var ftpStream = ftpWebRequest.GetResponse().GetResponseStream())
            {
                byte[] buffer = new byte[1024];
                int n;
                while ((n = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    localfile.Write(buffer, 0, n);
                }
            }


            return true;

        }

        /// <summary>
        /// FTP ���ε� ��û
        /// </summary>
        public void FtpUpload()
        {
            FtpUpload(_targetUrl, _fileName);
        }


        /// <summary>
        /// FTP ���ε� ��û
        /// Custom �ּ�
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="fileName"></param>
        public void FtpUpload(string targetUrl, string fileName)
        {
            string fullName = targetUrl + "\\" + fileName;
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(fullName);

            ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
            //ftpWebRequest.EnableSsl = true; // TLS/SSL
            ftpWebRequest.UseBinary = false;   // ASCII, Binary(����Ʈ)
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpWebRequest.UsePassive = _usePassive;


            byte[] data = File.ReadAllBytes(Application.persistentDataPath + @"/" + fileName);
            using (var ftpStream = ftpWebRequest.GetRequestStream())
            {
                ftpStream.Write(data, 0, data.Length);
            }

            using (var response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                Debug.Log(response.StatusDescription);
            }
        }


        /// <summary>
        /// FTP ���� ���� ���� Ȯ��
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FtpFileExist(string targetUrl, string fileName)
        {
            bool IsExists = true;

            FtpWebRequest ftpWebRequest = null;
            FtpWebResponse ftpResponse = null;

            try
            {

                ftpWebRequest = (FtpWebRequest)WebRequest.Create(targetUrl + "/" + fileName);
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                if (ftpResponse.StatusCode == System.Net.FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    IsExists = false;
                }

            }
            catch
            {
                IsExists = false;
            }

            return IsExists;
        }
    }
}