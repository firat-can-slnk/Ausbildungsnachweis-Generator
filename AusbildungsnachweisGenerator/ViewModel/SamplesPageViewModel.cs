using AusbildungsnachweisGenerator.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows;
using AusbildungsnachweisGenerator.Helper;
using System.ComponentModel;

namespace AusbildungsnachweisGenerator.ViewModel
{
    public class SamplesPageViewModel : ObservableObject
    {
        public ObservableCollection<PanelItem> PanelItems { get; set; }
        public ObservableCollection<FormatItem> FormatItems { get; set; }
        public string InfoMessage { get; }

        private static string assetsPath = Path.Combine(Environment.CurrentDirectory, "Assets");

        public ICommand OpenPathCommand { get; set; } = new RelayCommand(() => IOHelper.OpenWithDefaultProgram(assetsPath));

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

            FormatItems = new()
            {
                new("Heftnummer", "{{noteNr}}"),
                new("Vor- und Nachname des Auszubildenden", "{{fullName}}"),
                new("Adresse des Auszubildenden", "{{fullAddress}}"),
                new("Ausbildungsberuf", "{{jobName}}"),
                new("Fachrichtung / Schwerpunkt", "{{jobSubject}}"),
                new("Name des Ausbildungsbetrieb", "{{company}}"),
                new("Verantwortliche/r Ausbilder/in", "{{instructor}}"),
                new("Beginn der Ausbildung (Datum)", "{{appStart}}"),
                new("Ende der Ausbildung (Datum)", "{{appEnd}}"),
                new("Ausbildungsjahr", "{{year}}"),
                new("Ausbildende Abteilung", "{{section}}"),
                new("Ausbildungswoche vom", "{{weekStart}}"),
                new("Ausbildungswoche bis", "{{weekEnd}}"),
                new("Stunden von Montag-Freitag", "{{hours}}"),
                new("Stunden am Samstag ('---' wenn am Sa. nicht gearbeitet wird)", "{{hoursSat}}"),
                new("'---' wenn am Samstag nicht gearbeitet wird, sonst leer", "{{saturday}}")
            };

            var sb = new StringBuilder();
            sb.AppendLine("Es ist auch möglich die Vorlagen anzupassen oder komplett auszutauschen.\n");
            sb.AppendLine("Die Vorlagen liegen im Hauptverzeichnis des Programms im Assets Ordner als .docx-Datei und können frei bearbeitet oder ausgetauscht werden.\n");
            sb.AppendLine("Es muss nur darauf geachtet werden, dass der Dateiname unverändert bleibt.");
            InfoMessage = sb.ToString();
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
    public class FormatItem : ObservableObject
    {
        private string meaning;
        private string variable;

        public string Meaning
        {
            get => meaning;
            set => SetProperty(ref meaning, value);
        }
        public string Variable
        {
            get => variable;
            set => SetProperty(ref variable, value);
        }

        public FormatItem(string meaning, string variable)
        {
            Meaning = meaning;
            Variable = variable;
        }
    }
}