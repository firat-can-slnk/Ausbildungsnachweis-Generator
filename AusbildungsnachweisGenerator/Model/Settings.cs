using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Settings
    {
        public List<Profile> Profiles;

        public Settings(List<Profile> profiles)
        {
            Profiles = profiles;
        }
    }
}
