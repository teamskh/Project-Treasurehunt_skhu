using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using TTM.Classes;
using UnityEngine;

public class FTP
{
    static string ftpPath = "ftp://ydlwnsj25.iptime.org:1617/";
    static string user = "toomuch";
    static string pwd = "find";

    #region ver1.0

    public static string FtpDeleteIMG(DataPath path)
    {
        var uri = ftpPath + path.DirFile();
        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(uri);
        req.Method = WebRequestMethods.Ftp.DeleteFile;
        req.Credentials = new NetworkCredential(user, pwd);

        using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            return resp.StatusDescription;
        }
    }
    
    public static string FtpRenameDir(DataPath path,string name)
    {
        var uri = ftpPath + path.Dir();

        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(uri);
        req.Method = WebRequestMethods.Ftp.Rename;
        req.RenameTo = name;
        req.Credentials = new NetworkCredential(user, pwd);

        using(FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            return resp.StatusDescription;
        }
    }

    public static string FtpMakeDir(string CompetitionName)
    {
        var uri = ftpPath + "JPG/" + CompetitionName;

        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(uri);
        req.Method = WebRequestMethods.Ftp.MakeDirectory;
        req.Credentials = new NetworkCredential(user, pwd);
        
        using(FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            return resp.StatusDescription;
        }
    }

    public static void FtpDeleteDir(string CompetitionName)
    {
        var uri = ftpPath + "JPG/" + CompetitionName;

        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(uri);
       req.Method = WebRequestMethods.Ftp.RemoveDirectory;
            req.Credentials = new NetworkCredential(user, pwd);
            using (FtpWebResponse remvDir = (FtpWebResponse)req.GetResponse())
            {
                Debug.Log(remvDir.StatusDescription);
            }
       
    }
    #endregion

    #region ver2.0

    public static void AvaliablePath(DataPath data)
    {
        try
        {
            if (!Directory.Exists(data[0] + "/JPG")) Directory.CreateDirectory(data[0] + "/JPG");
            if (!Directory.Exists(data[0]+data.Dir())) Directory.CreateDirectory(data[0]+data.Dir()); 
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath + data.Dir());
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            req.Credentials = new NetworkCredential(user, pwd);

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
            {
                Debug.Log("Directory Add: " + resp.StatusDescription);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public static void ImageServerUpload(DataPath path)
    {
        try
        {
            Uri uri = new Uri(ftpPath + path.DirFile());
            string inputFile = path.ToString();

            using (WebClient cli = new WebClient())
            {
                cli.Credentials = new NetworkCredential(user, pwd);

                cli.UploadFileAsync(uri, inputFile);
            }
        }
        catch(WebException e)
        {
            AvaliablePath(path);
            ImageServerUpload(path);
        }
    }

    public static void ImageServerAllDownload(string competition, List<string> list)
    {
        foreach(var name in list)
        {
            ImageServerDownload(new DataPath("JPG/" + competition, name, ".jpg"));
        }
    }

    public static string ImageServerDownload(DataPath path)
    {
        var uri = ftpPath + path.DirFile();
        string outputFile = path.ToString();
        if (!File.Exists(outputFile))
        {
            using (WebClient cli = new WebClient())
            {
                cli.Credentials = new NetworkCredential(user, pwd);

                cli.DownloadFile(uri, outputFile);
            }
        }
        return path.ToString();
    }

    public static void ImageServerDelete(string competition,List<string> files)
    {
        DataPath path = new DataPath("JPG/" + competition);
        path.SetJPG();
        FtpWebRequest req = null;
        FtpWebResponse resp = null;

        foreach(var name in files)
        {
            req = (FtpWebRequest)WebRequest.Create(ftpPath + '/' + path.DirFile());
            req.Method = WebRequestMethods.Ftp.DeleteFile;
            req.Credentials = new NetworkCredential(user, pwd);

            resp = (FtpWebResponse)req.GetResponse();
            resp.Close();
        }
    }

    public static void ImageServerAllIMG(string competition)
    {
        DataPath path = new DataPath("JPG/" + competition);
        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath + path.Dir() + '/');
        req.Method = WebRequestMethods.Ftp.ListDirectory;
        req.Credentials = new NetworkCredential(user, pwd);
        List<string> names = new List<string>();

        using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            using(StreamReader  reader = new StreamReader(resp.GetResponseStream()))
            {
                while (!reader.EndOfStream)
                {
                    names.Add(reader.ReadLine());
                }
            }
        }
        ImageServerDelete(competition, names);
    } 

    public static void ImageServerOne(string competition,string name)
    {
        DataPath path = new DataPath("JPG/" + competition, name);
        path.SetJPG();

        LocalFileUtil.DeletedAdd(competition, name);

        ImageServerUpload(new DataPath("JPG/" + competition, "Deleted", ".txt"));
        List<string> names = new List<string>();
        names.Add(name);
        ImageServerDelete(competition, names);

    }

    public static void ImageServerModified(string competition, string name)
    {
        LocalFileUtil.ModifiedAdd(competition, name);
        ImageServerUpload(new DataPath("JPG/" + competition, "Modified", ".txt"));
        DataPath path = new DataPath("JPG/" + competition, name);
        path.SetJPG();
        ImageServerUpload(path);
    }

    public static void ImageServerRenameDir(string competition,string newname)
    {
        DataPath path = new DataPath("JPG/" + competition);
        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath + path.Dir());
        req.Method = WebRequestMethods.Ftp.Rename;
        req.Credentials = new NetworkCredential(user, pwd);
        req.RenameTo = newname;

        using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse()) {
            Debug.Log(resp.StatusDescription);
        }
    }

    public static void ImageServerRenameFile(string competition,string before,string newname)
    {
        LocalFileUtil.ModifiedRemove(competition, before);
        ImageServerUpload(new DataPath("JPG/" + competition, "Modified", ".txt"));

        LocalFileUtil.DeletedAdd(competition, before);
        ImageServerUpload(new DataPath("JPG/" + competition, "Deleted", ".txt"));
        LocalFileUtil.FileRename(competition, before, newname);

        DataPath path = new DataPath("JPG/" + competition, before);
        path.SetJPG();
        DataPath newpath = new DataPath("JPG/" + competition, newname);
        newpath.SetJPG();
        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpPath + path.DirFile());
        req.Method = WebRequestMethods.Ftp.Rename;
        req.Credentials = new NetworkCredential(user, pwd);
        req.RenameTo = newpath.File();

        using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
        {
            Debug.Log(resp.StatusDescription);
        }
    }

    #endregion
}
