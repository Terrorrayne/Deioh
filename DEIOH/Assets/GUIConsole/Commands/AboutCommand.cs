// --------------------------------------
//  Unity Foundation
//  AboutCommand.cs
//  copyright (c) 2014 Nicholas Ventimiglia, http://avariceonline.com
//  All rights reserved.
//  -------------------------------------
// 
using UnityEngine;

namespace GUIConsole
{
    /// <summary>
    /// Extends the console with 'about me' command
    /// </summary>
    [AddComponentMenu("GUIConsole/About")]
    public class AboutCommand : MonoBehaviour
    {
        [Multiline]
        public string Text =
        "\r\n"+
        "GUIConsole by Nicholas Ventimiglia 2013 \r\n" +
        "A free console application template for Unity3d. \r\n" +
        "www.AvariceOnline.Com \r\n";

        void Awake()
        {
            ConsoleContext.Instance.Commands.Add(new ConsoleCommand
            {
                Label = "About",
                Method = () => ConsoleContext.Instance.LogOutput(Text)
            });
        }

    }
}
