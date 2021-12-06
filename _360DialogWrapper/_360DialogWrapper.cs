using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace _360DialogWrapper
{


    public static class _360DialogWrapper
    {
        public static String _360DialogAPIkey;


        private static Dictionary<string, string> APIHeader = new Dictionary<string, string> { { "D360-API-KEY", _360DialogAPIkey } };
        private static String SendTextMessageEndpoint = "https://waba.360dialog.io/v1/messages";
        private static String SendMediaMessageEndpoint = "https://waba.360dialog.io/v1/media";
        private static string CheckContactsEndpoint = "https://waba.360dialog.io/v1/contacts";
        private static string DeleteMediaEndpoint = "https://waba.360dialog.io/v1/media/";
        private static Dictionary<string, string> dic_mimemediatypes = new Dictionary<string, string>()
        {
            { "document_pdf","application/pdf" },{"document_docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"}
        };
        private static Dictionary<string, Int32> dic_fileextensionstodoc = new Dictionary<string, Int32>()
        {
            { ".pdf",(Int32)mediatype.document_pdf},
            { ".doc",(Int32)mediatype.document_word},
            { ".docx",(Int32)mediatype.document_word}

        };
        private static void setAPIHeader()
        {
            APIHeader["D360-API-KEY"] = _360DialogAPIkey;
        }
        private static List<string> uploadedids;
        /// <summary>
        /// Get a list of uploaded media during your session.
        /// </summary>
        public  static List<string> session_uploaded_media { get { return uploadedids; } }

        private static string getwa_id(string phonenumber)
        {
            try
            {
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                _360DialogContactsEndpointMessage msg = new _360DialogContactsEndpointMessage();
                msg.contacts.Add(phonenumber);
                var resp = cls_Requests.POST(CheckContactsEndpoint, serializer1.Serialize(msg), APIHeader);
                var respObj = serializer1.Deserialize<_360DialogContactsEndpointResponse>(resp);
                if (!String.IsNullOrEmpty(respObj.contacts[0].wa_id) && respObj.contacts[0].status == "valid")
                {
                    return respObj.contacts[0].wa_id;

                }
                else
                {
                    throw new Exception("Invalid_Number");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private static void isphonenumbervalid(string phonenumber)
        {
            try
            {
                if (!Regex.IsMatch(phonenumber, @"^\+\d{1,3}\d+"))
                {
                    throw new ArgumentException("phonenumber must have the following format: `+countrycode``phonenumber`->+9613446677;+96171190337");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phonenumber">format +96171abcdef or +9613abcdef</param>
        /// <param name="message">Message to send</param>
        /// <returns>OK</returns>
        public static string SendTextMessage(string phonenumber, string message)
        {
            try
            {

                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
                setAPIHeader();
                isphonenumbervalid(phonenumber);


                phonenumber = getwa_id(phonenumber);
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                _360DialogTextMessage msg = new _360DialogTextMessage() { to = phonenumber, text = new _360DialogTextMessagetext(message) };
                var msg1 = cls_Requests.POST(SendTextMessageEndpoint, serializer1.Serialize(msg), APIHeader);
                return "OK";


            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Invalid_Number") > -1)
                {
                    throw new Exception("Invalid Phone Number");
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Used to upload a file to whatsapp server.
        /// </summary>
        /// <param name="mediaType">set to notspecified in case filepath is specified.</param>
        /// <param name="fileData">File Content embedded in bytes array</param>
        /// <param name="filepath">Physical location of the file.</param>
        /// <returns>Whatsapp MediaID of the file</returns>
        public static string UploadMedia(mediatype mediaType = mediatype.notspecified, byte[] fileData = null, string filepath = "")
        {
            string _mediaid = "";
            try
            {
                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
                setAPIHeader();


                JavaScriptSerializer serializer1 = new JavaScriptSerializer();


                if (fileData == null && String.IsNullOrEmpty(filepath))
                {
                    throw new ArgumentException("File Content Or Path must be Provided.");
                }
                if (fileData != null && !String.IsNullOrEmpty(filepath))
                {
                    throw new ArgumentException("fileData and filepath cannot be provided together.");
                }
                if (mediaType != mediatype.notspecified && !dic_mimemediatypes.ContainsKey(mediaType.ToString()))
                {
                    throw new ArgumentException("Invalid Media Type Provided");
                }
                if (mediaType == mediatype.notspecified && fileData != null)
                {
                    throw new ArgumentException("You have provided binary data without specifying the mediaType.");
                }
                if (mediaType != mediatype.notspecified && !String.IsNullOrEmpty(filepath))
                {
                    throw new ArgumentException("mediaType will be infered from file, please set mediaType to notspecified");
                }

                string contenttype = "";
                if (mediaType == mediatype.notspecified)
                {
                    var ext = Path.GetExtension(filepath);
                    contenttype = ((mediatype)dic_fileextensionstodoc[ext]).ToString();
                    contenttype = dic_mimemediatypes[contenttype];
                }
                else
                {
                    contenttype = dic_mimemediatypes[mediaType.ToString()];
                }

                var mediaupload = cls_Requests.POSTMediaFile(SendMediaMessageEndpoint, contenttype, filepath, fileData, APIHeader);
                var mediauploadresp = serializer1.Deserialize<_360DialogUploadedMediaResponse>(mediaupload);
                _mediaid = mediauploadresp.media[0].id;

                if(uploadedids == null)
                {
                    uploadedids = new List<string>();
                }
                uploadedids.Add(_mediaid);
                return _mediaid;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Invalid_Number") > -1)
                {
                    throw new Exception("Invalid Phone Number");
                }
                else
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Send A file to a contact.
        /// Works in 2 different modes:
        /// 1- Provide file path or content.
        ///     1.1- content : specify mediaType and fileData 
        ///     1.2- file path : specify filepath
        /// 2- Provide a valid mediaid returned by the UploadMedia API
        /// </summary>
        /// <param name="phonenumber">format +96171abcdef or +9613abcdef</param>
        /// <param name="mediaType">set to not specified in case filepath is specified.</param>
        /// <param name="fileData">File Content embedded in bytes array</param>
        /// <param name="filepath">Physical location of the file.</param>
        /// <param name="caption">The caption to send with the file</param>
        /// <param name="mediaid">Uploaded Media ID returned by the UploadMedia API</param>
        /// <returns>OK</returns>
        public static string SendMediaFile(string phonenumber, mediatype mediaType = mediatype.notspecified,
            byte[] fileData = null, string filepath = "", string caption = "", string mediaid = "")
        {
            string _mediaid = "";
            try
            {
                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
                setAPIHeader();
                isphonenumbervalid(phonenumber);
                phonenumber = getwa_id(phonenumber);


                JavaScriptSerializer serializer1 = new JavaScriptSerializer();

                if (mediaid == "")
                {
                    _mediaid = UploadMedia(mediaType, fileData, filepath);
                }
                else
                {
                    _mediaid = mediaid;
                }


                _360DialogMediaDocumentMessage msg = new _360DialogMediaDocumentMessage() { to = phonenumber, document = new _360DialogMediaDocumentMessagedocument(_mediaid, caption) };
                string body = serializer1.Serialize(msg);

                var ret = cls_Requests.POST(SendTextMessageEndpoint, body, APIHeader);



                return "OK";
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Invalid_Number") > -1)
                {
                    throw new Exception("Invalid Phone Number");
                }
                else
                {
                    throw;
                }
            }

        }
        /// <summary>
        /// returns true if successfully deleted
        /// </summary>
        /// <param name="mediaid">The mediaid return by UploadMedia API</param>
        /// <returns></returns>
        public static Boolean DeleteMedia(string mediaid)
        {
            try
            {

                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
                setAPIHeader();
                var ret = cls_Requests.DELETEMediaFile(DeleteMediaEndpoint + mediaid, APIHeader);
                if (uploadedids == null)
                {
                    uploadedids = new List<string>();
                }
                uploadedids.Remove(mediaid);
                return true;

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("404") > -1)
                {
                    return true; //media not found, accept response as a successfull delete operation since media does not exists
                }
                else
                {
                    throw;
                }

            }
        }




    }
    internal class _360DialogContactsEndpointMessage
    {
        public _360DialogContactsEndpointMessage()
        {
            blocking = "wait";
            force_check = false;
            contacts = new List<string>();
        }
        public string blocking { get; set; }
        public List<string> contacts { get; set; }
        public Boolean force_check { get; set; }

    }
    internal class _360DialogContactsEndpointResponse
    {
        public List<_360DialogContactsEndpointResponseContactObj> contacts;
        public object meta;
    }
    internal class _360DialogContactsEndpointResponseContactObj
    {
        public string input { get; set; }
        public string status { get; set; }
        public string wa_id { get; set; }
    }
    internal class _360DialogTextMessage
    {
        internal _360DialogTextMessage()
        {
            recipient_type = "individual";
            type = "text";
        }
        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public _360DialogTextMessagetext text { get; set; }
    }
    internal class _360DialogTextMessagetext
    {
        internal _360DialogTextMessagetext(string _msg)
        {
            body = _msg;
        }
        public string body { get; set; }

    }
    internal class _360DialogMediaImageMessage
    {
        internal _360DialogMediaImageMessage()
        {
            recipient_type = "individual";
            type = "image";
        }

        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public _360DialogMediaImageMessageimage image { get; set; }
    }
    internal class _360DialogMediaImageMessageimage
    {
        internal _360DialogMediaImageMessageimage(string _id, string _caption)
        {
            id = _id;
            caption = _caption;
        }
        public string id { get; set; }
        public string caption { get; set; }

    }
    internal class _360DialogMediaDocumentMessage
    {
        internal _360DialogMediaDocumentMessage()
        {
            recipient_type = "individual";
            type = "document";
        }

        public string recipient_type { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public _360DialogMediaDocumentMessagedocument document { get; set; }
    }
    internal class _360DialogMediaDocumentMessagedocument
    {
        internal _360DialogMediaDocumentMessagedocument(string _id, string _caption)
        {
            id = _id;
            caption = _caption;
        }
        public string id { get; set; }
        public string caption { get; set; }

    }
    internal class _360DialogUploadedMediaResponse
    {
        public List<_360DialogUploadedMediaResponseid> media;
        public object meta;
    }
    internal class _360DialogUploadedMediaResponseid
    {
        public string id { get; set; }
    }




    //    Media
    //Supported Content-Types
    //audio
    //audio/aac, audio/mp4, audio/amr, audio/mpeg, audio/ogg; codecs=opus Note: The base audio/ogg type is not supported.
    //document
    //Any valid MIME-type.
    //image
    //image/jpeg, image/png
    //sticker
    //image/webp
    //video
    //video/mp4, video/3gpp
    //Notes:
    //Only H.264 video codec and AAC audio codec is supported.Only videos with a single audio stream are supported.
    public enum mediatype
    {
        notspecified,
        document_pdf,
        document_word


    }


}
