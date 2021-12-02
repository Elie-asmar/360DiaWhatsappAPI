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
        private static String SendTextMessageEndpoint = "https://waba.360dialog.io/v1/messages";
        private static String SendMediaMessageEndpoint = "https://waba.360dialog.io/v1/media";
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

        public static string SendTextMessage(string phonenumber, string message)
        {
            try
            {
                if (!Regex.IsMatch(phonenumber, @"^\d{1,3}-\d+"))
                {
                    throw new ArgumentException("phonenumber must have the following format: `countrycode`-`phonenumber`->961-3446677;961-71190337");
                }
                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
                phonenumber = phonenumber.Replace("-", "");
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                _360DialogTextMessage msg = new _360DialogTextMessage() { to = phonenumber, text = new _360DialogTextMessagetext(message) };
                var msg1 = cls_Requests.POST(SendTextMessageEndpoint, serializer1.Serialize(msg), new Dictionary<string, string>() { { "D360-API-KEY", _360DialogAPIkey } });
                return msg1;


            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("404") > -1)
                {
                    throw new Exception("Number not found in contact list. Consider the Opt-in option");
                }
                else
                {
                    throw;
                }
            }
        }

        public static string SendMediaFile(string phonenumber, mediatype mediaType = mediatype.notspecified, byte[] fileData = null, string filepath = "", string caption = "")
        {
            try
            {
                if (!Regex.IsMatch(phonenumber, @"^\d{1,3}-\d+"))
                {
                    throw new ArgumentException("phonenumber must have the following format: `countrycode`-`phonenumber`->961-3446677;961-71190337");
                }
                if (String.IsNullOrEmpty(_360DialogAPIkey))
                {
                    throw new Exception("360 Dialog API is missing, please set the _360DialogAPIkey variable");
                }
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
                if(mediaType == mediatype.notspecified && fileData != null)
                {
                    throw new ArgumentException("You have provided binary data without specifying the mediaType.");
                }
                if (mediaType != mediatype.notspecified && !String.IsNullOrEmpty(filepath))
                {
                    throw new ArgumentException("mediaType will be infered from file, please set mediaType to notspecified");
                }

                phonenumber = phonenumber.Replace("-", "");
                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
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

                var mediaupload = cls_Requests.POSTMediaFile(SendMediaMessageEndpoint, contenttype, filepath, fileData, new Dictionary<string, string>() { { "D360-API-KEY", _360DialogAPIkey } });
                var mediauploadresp = serializer1.Deserialize<_360DialogUploadedMediaResponse>(mediaupload);
                _360DialogMediaDocumentMessage msg = new _360DialogMediaDocumentMessage() { to = phonenumber, document = new _360DialogMediaDocumentMessagedocument(mediauploadresp.media[0].id, caption) };
                string body = serializer1.Serialize(msg);

                var ret = cls_Requests.POST(SendTextMessageEndpoint, body, new Dictionary<string, string>() { { "D360-API-KEY", _360DialogAPIkey } });
                return ret;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("404") > -1)
                {
                    throw new Exception("Number not found in contact list. Consider the Opt-in option");
                }
                else
                {
                    throw;
                }
            }

        }




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
