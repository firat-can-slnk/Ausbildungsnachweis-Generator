using System;

namespace AusbildungsnachweisGenerator.Helper
{
    [AttributeUsage(AttributeTargets.All)]
    internal class PathAttribute : Attribute
    {
        public PathAttribute(string p)
        {
            Path = p;
        }

        public string Path { get; }
    }
}