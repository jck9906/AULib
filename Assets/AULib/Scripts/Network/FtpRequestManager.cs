using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// FTP Request를 위한 매니저 클래스
    /// 시연 용도로 만든거라 실 사용시 검증 필요
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
        /// FTP 다운로드 요청
        /// </summary>
        /// <returns></returns>
        public bool FtpDownload()
        {
            return FtpDownload(_targetUrl, _fileName);
        }


        /// <summary>
        /// FTP 다운로드 요청
        /// Custom 주소
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
        /// FTP 업로드 요청
        /// </summary>
        public void FtpUpload()
        {
            FtpUpload(_targetUrl, _fileName);
        }


        /// <summary>
        /// FTP 업로드 요청
        /// Custom 주소
        /// </summary>
        /// <param name="targetUrl"></param>
        /// <param name="fileName"></param>
        public void FtpUpload(string targetUrl, string fileName)
        {
            string fullName = targetUrl + "\\" + fileName;
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(fullName);

            ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
            //ftpWebRequest.EnableSsl = true; // TLS/SSL
            ftpWebRequest.UseBinary = false;   // ASCII, Binary(디폴트)
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
        /// FTP 파일 존재 여부 확인
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