using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace OnlineControl
{
    public static class Utility
    {
        //Conveniece function to display a brief screen message
        public static void ShowMsg(string message)
        {
            InformationManager.DisplayMessage(new InformationMessage(message));
        }

    }
}
