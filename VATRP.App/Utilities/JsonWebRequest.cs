﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.vtcsecure.ace.windows.Utilities
{
    public static class JsonWebRequest
    {

        // To provide a generic web request that will attempt to load the information into the 
        // specified (T) class/type
        public static T MakeJsonWebRequestAuthenticated<T>(string webRequestUrl, string userName, string password)
        {
            WebResponse response = null;
            try
            {
                // create a request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webRequestUrl);
                request.Credentials = new NetworkCredential(userName, password);
                request.PreAuthenticate = true;
                request.KeepAlive = false;
                request.ContentLength = 0;
                request.Method = "GET";
                request.Timeout = 30000;

                response = request.GetResponse();
                String jsonResults = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonResults = sr.ReadToEnd();
                    sr.Close();
                }

                try
                {
                    // deserialize json to ResourceInfo List
                    T item = JsonDeserializer.JsonDeserialize<T>(jsonResults.ToString());
                    return item;
                }
                catch (Exception ex)
                {
                    throw new JsonException(JsonExceptionType.DESERIALIZATION_FAILED,"Failed to parse json response. Details: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new JsonException(JsonExceptionType.CONNECTION_FAILED, "Failed to get json information. Details: " + ex.Message, ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }            
            }
        }



        public static T MakeJsonWebRequest<T>(string webRequestUrl)
        {

            //****************************************************************************************************************************************
            //This method call a Web Request on provide URL
            //****************************************************************************************************************************************
            WebResponse response = null;
            try
            {
                // create a request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webRequestUrl);
                request.KeepAlive = false;
                request.ContentLength = 0;
                request.Method = "GET";
                request.Timeout = 30000;

                response = request.GetResponse();
                String jsonResults = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonResults = sr.ReadToEnd();
                    sr.Close();
                }

                try
                {
                    // deserialize json to ResourceInfo List

                    //Friends facebookFriends = new JavaScriptSerializer().Deserialize<Friends>(result);

                    
                    
                    //var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //ser.DeserializeObject(jsonResults);
                    //ser.DeserializeObject()

                    //var items = ser.DeserializeObject<List<T>>(jsonResults);

                    
                    //T item1 = JsonDeserializer.JsonDeserialize<T>(jsonResults.ToString());

                    //************BY MK FOR REMOVE THE PROVIDER AND VERSION FROM JSON STRING *************************
                    int index1 = jsonResults.IndexOf("[");


                   
                        jsonResults = jsonResults.Remove(0, index1 - 1);
                   
                    //jsonResults = jsonResults.Remove(0, index1 - 1);

                    //jsonResults = jsonResults.Remove(0, index1-1);
                    int index2 = jsonResults.IndexOf("]");
                    jsonResults = jsonResults.Remove(index2 + 1, jsonResults.Length - index2-1);
                    //*****************************************************************************************************
                    //jsonResults = jsonResults.Substring(0, index2);
                   //jsonResults= jsonResults.Substring(index1,index2-1);

                    T item = JsonDeserializer.JsonDeserialize<T>(jsonResults.ToString());
                    return item;
                }
                catch (Exception ex)
                {
                    throw new JsonException(JsonExceptionType.DESERIALIZATION_FAILED, "Failed to parse json response. Details: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new JsonException(JsonExceptionType.CONNECTION_FAILED, "Failed to get json information. Details: " + ex.Message, ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }            
            }
        }





        public static string MakeJsonWebRequest(string webRequestUrl)
        {

            //****************************************************************************************************************************************
            //This method call a Web Request on provide URL and RETURN A JSON RESPONSE
            // THIS METHOD IS CREATED BY MK ON DATED 13-12-2016
            //****************************************************************************************************************************************
            WebResponse response = null;
            try
            {
                // create a request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webRequestUrl);
                request.KeepAlive = false;
                request.ContentLength = 0;
                request.Method = "GET";
                request.Timeout = 30000;

                response = request.GetResponse();
                String jsonResults = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    jsonResults = sr.ReadToEnd();
                    sr.Close();
                }

                try
                {
                    // deserialize json to ResourceInfo List
                    //T item = JsonDeserializer.JsonDeserialize<T>(jsonResults.ToString());
                    //return item;

                   return (jsonResults.ToString());
                }
                catch (Exception ex)
                {
                    throw new JsonException(JsonExceptionType.DESERIALIZATION_FAILED, "Failed to parse json response. Details: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new JsonException(JsonExceptionType.CONNECTION_FAILED, "Failed to get json information. Details: " + ex.Message, ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }



    }
}
