using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GreatQuotes;
using GreatQuotes.Data;
using GreatQuotes.ViewModels;

public class QuoteManager
{
    public static QuoteManager Instance { get; private set; }

    readonly IQuoteLoader loader;
    public IList<GreatQuoteViewModel> Quotes { get; private set; }

    public QuoteManager(IQuoteLoader loader)
    {
        if (Instance != null)
        {
            throw new Exception("Can only create a single QuoteManager.");
        }
        Instance = this;
        this.loader = loader;
        Quotes = new ObservableCollection<GreatQuoteViewModel>(loader.Load());
    }

    internal void Save()
    {
        throw new NotImplementedException();
    }

    internal void SayQuote(GreatQuoteViewModel quote)
    {
        throw new NotImplementedException();
    }
}

    
   
