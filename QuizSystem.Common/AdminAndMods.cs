using System;
using System.Collections.Generic;
using System.Text;

namespace QuizSystem.Common
{
    public static class AdminAndMods
    {
        public static List<string> AdminAndModsValue = new List<string>() { GlobalConstants.AdministratorRoleName };

        public static string MyProperty { get => string.Join(",", AdminAndModsValue);  }
    }
}
