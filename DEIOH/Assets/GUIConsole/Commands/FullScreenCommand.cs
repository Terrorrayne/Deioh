// --------------------------------------
//  Unity Foundation
//  FullScreenCommand.cs
//  copyright (c) 2014 Nicholas Ventimiglia, http://avariceonline.com
//  All rights reserved.
//  -------------------------------------
// 
using UnityEngine;

namespace GUIConsole
{
    /// <summary>
    /// Extends the console with 'Full Screen' command
    /// </summary>
    [AddComponentMenu("GUIConsole/FullScreen")]
    public class FullScreenCommand : MonoBehaviour
    {
        void Awake()
        {
            ConsoleContext.Instance.Commands.Add(new ConsoleCommand
            {
                Label = "Full Screen",
                Method = () =>
                {
                    Screen.fullScreen = !Screen.fullScreen;
                }
            });
        }
    }
}