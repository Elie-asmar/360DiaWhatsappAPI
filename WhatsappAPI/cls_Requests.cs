﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Script.Serialization;
using System.Data;

public static class cls_Requests
{

    
    public static string POST(string url, string body, Dictionary<string,string> HEADERS = null)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            if (HEADERS != null)
                foreach (var kvp in HEADERS)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }
              
            var data = Encoding.ASCII.GetBytes(body);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string POSTMediaFile(string url, string filepath, Dictionary<string, string> HEADERS = null)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            if (HEADERS != null)
                foreach (var kvp in HEADERS)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }

            //var data = Encoding.ASCII.GetBytes(body);
            Byte[] data = File.ReadAllBytes(filepath);
            request.Method = "POST";
            request.ContentType = "application/pdf";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string GET(string url)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
          

            var responseString = "";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                else
                    responseString = "";

                response.Close();
            }

            return responseString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public static List<Dictionary<string, object>> DataTableToDictionary(DataTable dt)
    {
        try
        {
            List<Dictionary<string, object>> Rows = new List<Dictionary<string, object>>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                        row.Add(col.ColumnName, dr[col]);
                    Rows.Add(row);
                }
            }
            return Rows;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static string JSON_Stringify(object obj)
    {
        try
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(obj);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static object JSON_Parse(string str)
    {
        try
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Deserialize<object>(str);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static DataTable ReadToDataTable(string str, string constr)
    {
        try
        {
            using (SqlConnection cnsql = new SqlConnection(constr))
            {
                using (SqlCommand cmsql = new SqlCommand(str, cnsql))
                {
                    cnsql.Open();
                    using (SqlDataReader drsql = cmsql.ExecuteReader())
                    {
                        using (DataTable dt = new DataTable())
                        {
                            dt.Load(drsql);
                            return dt.Copy();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
