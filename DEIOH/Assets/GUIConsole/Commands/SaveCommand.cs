// --------------------------------------
//  Unity Foundation
//  SaveCommand.cs
//  copyright (c) 2014 Nicholas Ventimiglia, http://avariceonline.com
//  All rights reserved.
//  -------------------------------------
// 
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace GUIConsole
{


    /// <summary>
    /// Extends the console with 'about me' command
    /// </summary>
    [AddComponentMenu("GUIConsole/Save")]
    public class SaveCommand : MonoBehaviour
    {
        private void Awake()
        {
            ConsoleContext.Instance.Commands.Add(
                new ConsoleCommand
                {
                    Label = "Save",
                    Method = () => DoSave()
                });
        }

        private void DoSave()
        {

#if UNITY_WEBPLAYER
            ConsoleContext.Instance.LogWarning("Save is not supported on web player");
#else

            var sb = new StringBuilder();

            foreach (var message in ConsoleContext.Instance.Items)
            {
                sb.AppendLine(message.Formatted);
            }

            var n = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            var path = Application.persistentDataPath + "/Console/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText(path + n, sb.ToString());


            ConsoleContext.Instance.LogOutput(String.Format("Console Saved To '{0}'", n));

#endif

        }
    }
}