using AusbildungsnachweisGenerator.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AusbildungsnachweisGenerator.ViewModel
{
    public class SamplesPageViewModel : ObservableObject
    {
        public ObservableCollection<PanelItem> PanelItems { get; set; }

        public SamplesPageViewModel()
        {
            PanelItems = new()
            {
                new("Deckblatt", @"../Assets/ProofImages/Ausbildungsnachweis_Original-1.jpg"),
                new("Täglich (2a)", @"../Assets/ProofImages/Ausbildungsnachweis_Original-2.jpg"),
                new("Wöchentlich (2b)", @"../Assets/ProofImages/Ausbildungsnachweis_Original-3.jpg"),
                new("Täglich mit Bezug zum Ausbildungsrahmenplan (3a)", @"../Assets/ProofImages/Ausbildungsnachweis_Original-4.jpg"),
                new("Wöchentlich mit Bezug zum Ausbildungsrahmenplan (3b)", @"../Assets/ProofImages/Ausbildungsnachweis_Original-5.jpg"),
                //new("Sichtvermerke", @"../Assets/ProofImages/Ausbildungsnachweis_Original-6.jpg"),
            };
        }
    }
    public class PanelItem : ObservableObject
    {
        private string title;
        private string imageSource;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public string ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public PanelItem(string title, string imageSource)
        {
            Title = title;
            ImageSource = imageSource;
        }
    }
}