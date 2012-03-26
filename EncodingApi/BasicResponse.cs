﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using EncodingApi.Extensions;

namespace EncodingApi
{
    /// <summary>
    /// Base class for all response class.
    /// </summary>
    [XmlRoot("response")]
    public abstract class BasicResponse : IXmlSerializable
    {
        /// <summary>
        /// Message from response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// List of errors.
        /// </summary>
        public ICollection<string> Errors { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BasicResponse()
        {
            Message = string.Empty;
            Errors = new List<string>();
        }

        /// <summary>
        /// Reads XML from <c>root</c> into this object instance.
        /// </summary>
        /// <remarks>
        /// When overriden in derived class, call <c>base.ReadXml(XElement)</c> to ensure that the
        /// parent class is read properly.
        /// </remarks>
        /// <param name="root">The XElement to read from.</param>
        protected virtual void ReadXml(XElement root)
        {
            if (root == null) return;

            // Reads <message></message>
            var elem = root.Element("message");
            Message = elem != null ? elem.Value : String.Empty;

            // Reads <errors>...</errors>
            elem = root.Element("errors");
            if (elem != null)
            {
                // Reads <error></error>
                var childs = elem.Elements("error");
                if (childs != null)
                {
                    if (Errors == null)
                    {
                        Errors = new List<string>();
                    }
                    foreach (var item in childs)
                    {
                        Errors.Add(item.Value);
                    }
                }
            }
        }

        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Reads the XML representation into this object instance.
        /// </summary>
        /// <remarks>
        /// Implementer of this method must ensure that all base class properties are correctly
        /// deserialized from its XML, or the derived class should call <c>base.Read(XElement)</c>
        /// to have the properties read from the XElement.
        /// </remarks>
        /// <param name='reader'>The XmlReader to read from.</param>
        public abstract void ReadXml(XmlReader reader);

        /// <summary>
        /// Writes this object into XML representation.
        /// </summary>
        /// <remarks>
        /// When overriden in derived class, call base.WriteXml(XmlWriter) to ensure that
        /// all properties are correctly written to the writer.
        /// </remarks>
        /// <param name='writer'>The XmlWriter to write to.</param>
        public virtual void WriteXml(XmlWriter writer)
        {
            // Writes <message></message>
            writer.WriteSafeElementString("message", Message);
            
            if (Errors != null)
            {
                int count = Errors.Count;
                if (count > 0)
                {
                    // Writes <errors>...</errors>
                    writer.WriteStartElement("errors");
                    foreach (var item in Errors)
                    {
                        // Writes <error></error>
                        writer.WriteSafeElementString("error", item);
                    }
                    writer.WriteEndElement();
                }
            }
        }
    }
}
