﻿namespace EncodingApi
{
    public class AddMediaResponse : Internal.ResponseBase
    {
        public AddMediaResponse()
            : this("<response/>")
        {
        }

        public AddMediaResponse(string xml)
            : base(xml)
        {
        }

        public string MediaId
        {
            get
            {
                return GetXmlElementInnerText("MediaID");
            }
        }
    }
}
