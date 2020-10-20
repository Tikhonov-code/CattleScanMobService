using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string url = "http://193.27.73.63/Mob_service/Default.aspx?action=mob_0";
        //string url = "http://localhost:51161/Default.aspx?action=mob_0";

        loginToken lgt = new loginToken();
        lgt.email = "mobil@mob.mo";
        lgt.password = "09ebc5cb5447dd77465cf1001634943a";

        string result = HttpPostRequest(url, lgt);
        //TextBox1.Text += "\r\n" + result;

    }
    private string HttpPostRequest(string url, object data)
    {
        string responseText = string.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Access-Control-Allow-Origin", "*");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(data);
                streamWriter.Write(json);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = streamReader.ReadToEnd();

            }
        }
        catch (WebException ex)
        {
            responseText = "WebException: " + ex.Message;
        }
        return responseText;
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string url = "http://193.27.73.63/Mob_service/Default.aspx?action=mob_1";
        //string url = "http://localhost:51161/Default.aspx?action=mob_0";

        loginToken lgt = new loginToken();
        lgt.email = "mobil@mob.mo";
        lgt.password = "09ebc5cb5447dd77465cf1001634943a";
        lgt.token = "3fd6243ac1d4d76fc8356fad04ef6211d46bcf3c99b9177a816dd4fc46e8a010";

        string result = HttpPostRequest(url, lgt);
        //TextBox1.Text += "\r\n  Farm Stat:   " + result;
    }
}