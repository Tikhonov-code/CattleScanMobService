using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResponseError
/// </summary>
public class ResponseError
{
    public string status { get; set; }
    public string message { get; set; }

    public ResponseError()
    {
        status = "error";
    }
}
public class ResponseOK
{
    public string status { get; set; }
    public JObject data { get; set; }

    public ResponseOK()
    {
        status = "ok";
    }
}