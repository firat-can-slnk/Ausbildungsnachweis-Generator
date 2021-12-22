using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AusbildungsnachweisGenerator.Extensions
{
    public enum ProofTreeViewType
    {
        Root,
        File,
        Folder
    }
    public class ProofTreeView : ObservableObject
    {
        private string content;
        private ObservableCollection<ProofTreeView> children;
        private ProofTreeViewType type;

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        public ObservableCollection<ProofTreeView> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }
        public ProofTreeViewType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public ProofTreeView(string content, ProofTreeViewType type)
        {
            Content = content;
            Children = new();
            this.type = type;
        }
    }
}