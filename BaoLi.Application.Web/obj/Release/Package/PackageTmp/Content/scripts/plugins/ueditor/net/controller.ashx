<%@ WebHandler Language="C#" Class="UEditorHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class UEditorHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Handler action = null;
        switch (context.Request["action"])
        {
            case "config":
                action = new ConfigHandler(context);
                break;
            case "uploadimage":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Configs.GetStringList("imageAllowFiles"),
                    PathFormat = Configs.GetString("imagePathFormat"),
                    SizeLimit = Configs.GetInt("imageMaxSize"),
                    UploadFieldName = Configs.GetString("imageFieldName")
                });
                break;
            case "uploadscrawl":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = new string[] { ".png" },
                    PathFormat = Configs.GetString("scrawlPathFormat"),
                    SizeLimit = Configs.GetInt("scrawlMaxSize"),
                    UploadFieldName = Configs.GetString("scrawlFieldName"),
                    Base64 = true,
                    Base64Filename = "scrawl.png"
                });
                break;
            case "uploadvideo":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Configs.GetStringList("videoAllowFiles"),
                    PathFormat = Configs.GetString("videoPathFormat"),
                    SizeLimit = Configs.GetInt("videoMaxSize"),
                    UploadFieldName = Configs.GetString("videoFieldName")
                });
                break;
            case "uploadfile":
                action = new UploadHandler(context, new UploadConfig()
                {
                    AllowExtensions = Configs.GetStringList("fileAllowFiles"),
                    PathFormat = Configs.GetString("filePathFormat"),
                    SizeLimit = Configs.GetInt("fileMaxSize"),
                    UploadFieldName = Configs.GetString("fileFieldName")
                });
                break;
            case "listimage":
                action = new ListFileManager(context, Configs.GetString("imageManagerListPath"), Configs.GetStringList("imageManagerAllowFiles"));
                break;
            case "listfile":
                action = new ListFileManager(context, Configs.GetString("fileManagerListPath"), Configs.GetStringList("fileManagerAllowFiles"));
                break;
            case "catchimage":
                action = new CrawlerHandler(context);
                break;
            default:
                action = new NotSupportedHandler(context);
                break;
        }
        action.Process();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}