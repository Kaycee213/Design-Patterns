using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GreatQuotes.Data;
using GreatQuotes.ViewModels;

namespace GreatQuotes.Droid
{
    public class QuoteLoader : IQuoteLoader
    {
        const string FileName = "quotes.xml";

        public string DefaultData { get; private set; }

        public IEnumerable<GreatQuoteViewModel> Load()
        {
            XDocument doc = null;

            string filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName);

            if (File.Exists(filename))
            {
                try
                {
                    doc = XDocument.Load(filename);
                }
                catch
                {
                }
            }

            if (doc == null)
                doc = XDocument.Parse(DefaultData);

            if (doc.Root != null)
            {
                foreach (var entry in doc.Root.Elements("quote"))
                {
                    yield return new GreatQuoteViewModel(new GreatQuote(
                        entry.Attribute("author").Value,
                        entry.Value));
                }
            }
        }

        public void Save(IEnumerable<GreatQuoteViewModel> quotes)
        {
            string filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName);

            if (File.Exists(filename))
                File.Delete(filename);

            XDocument doc = new XDocument(
                new XElement("quotes",
                    quotes.Select(q =>
                        new XElement("quote", new XAttribute("author", q.Author))
                        {
                            Value = q.QuoteText
                        })));

            doc.Save(filename);
        }

    }

}    
