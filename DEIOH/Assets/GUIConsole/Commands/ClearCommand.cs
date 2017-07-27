// --------------------------------------
//  Unity Foundation
//  ClearCommand.cs
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
    [AddComponentMenu("GUIConsole/Clear")]
    public class ClearCommand : MonoBehaviour
    {
        void Awake()
        {
            ConsoleContext.Instance.Commands.Add(new ConsoleCommand
            {
                Label = "Clear",
                Method = () => ConsoleContext.Instance.Items.Clear()
            });
        }

    }
}
