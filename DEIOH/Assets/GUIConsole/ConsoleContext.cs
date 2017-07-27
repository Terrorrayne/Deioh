// --------------------------------------
//  Unity Foundation
//  ConsoleContext.cs
//  copyright (c) 2014 Nicholas Ventimiglia, http://avariceonline.com
//  All rights reserved.
//  -------------------------------------
// 

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIConsole
{
    #region sub objects

    /// <summary>
    /// This Enum holds the message types used to easily control the formatting and display of a message.
    /// </summary>
    public enum MessageType
    {
        Text,
        Warning,
        Error,
        Success,
        Output,
        Input,
    }

    /// <summary>
    /// A console line item
    /// </summary>
    public struct ConsoleItem
    {
        readonly string _text;
        public string Text
        {
            get { return _text; }
        }

        readonly string _formatted;
        public string Formatted
        {
            get { return _formatted; }
        }

        readonly MessageType _type;
        public MessageType Type
        {
            get { return _type; }
        }

        readonly Color _color;
        public Color Color
        {
            get { return _color; }
        }

        public ConsoleItem(MessageType type, string text)
        {
            _text = text;
            _type = type;
            switch (_type)
            {
                case MessageType.Warning:
                    _formatted = string.Format("<< {0}", text);
                    _color = ConsoleContext.Instance.WarningColor;
                    break;
                case MessageType.Error:
                    _formatted = string.Format("<< {0}", text);
                    _color = ConsoleContext.Instance.ErrorColor;
                    break;
                case MessageType.Success:
                    _formatted = string.Format("<< {0}", text);
                    _color = ConsoleContext.Instance.SuccessColor;
                    break;
                case MessageType.Output:
                    _formatted = string.Format("<< {0}", text);
                    _color = ConsoleContext.Instance.OutputColor;
                    break;
                case MessageType.Input:
                    _formatted = string.Format(">> {0}", text);
                    _color = ConsoleContext.Instance.InputColor;
                    break;
                default:
                    _formatted = text;
                    _color = ConsoleContext.Instance.TextColor;
                    break;
            }
        }
    }

    /// <summary>
    /// For processing Text input
    /// </summary>
    public class ConsoleInterpreter
    {
        public string Label;
        public Action<string> Method;
    }

    /// <summary>
    /// Button to add to the menu
    /// </summary>
    public class ConsoleCommand
    {
        public string Label;
        public Action Method;
    }
    #endregion

    
    /// <summary>
    /// The console context is the centerpiece of the console system.
    /// It is the datamodel to which views may observer
    /// </summary>
    public class ConsoleContext
   
    {
        #region static
        public static readonly ConsoleContext Instance = new ConsoleContext();
        #endregion

        #region props / fields

        // Default color of the standard display text.

        public Color TextColor = Color.white;
        public Color WarningColor = Color.yellow;
        public Color ErrorColor = Color.red;
        public Color SuccessColor = Color.green;
        public Color InputColor = Color.green;
        public Color OutputColor = Color.cyan;

        public readonly List<ConsoleItem> Items = new List<ConsoleItem>();
        public readonly List<ConsoleCommand> Commands = new List<ConsoleCommand>();
        public readonly List<ConsoleInterpreter> Interpreters = new List<ConsoleInterpreter>();

        #endregion
        
        /// <summary>
        /// write only
        /// </summary>
        public void Log(object message, MessageType type)
        {
            Items.Add(new ConsoleItem(type, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogText(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Text, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogError(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Error, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogWarning(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Warning, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogSuccess(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Success, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogInput(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Input, message.ToString()));
        }

        /// <summary>
        /// write only
        /// </summary>
        public void LogOutput(object message)
        {
            Items.Add(new ConsoleItem(MessageType.Output, message.ToString()));
        }

        /// <summary>
        /// Input for a command
        /// </summary>
        /// <param name="message"></param>
        public void Submit(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                LogInput(string.Empty);
                return;
            }

            message = message.Trim();

            Items.Add(new ConsoleItem(MessageType.Input, message));

            foreach (var interpreter in Interpreters)
            {
                interpreter.Method.Invoke(message);
            }
        }

        /// <summary>
        /// clear writes
        /// </summary>
        public void Clear()
        {
            Items.Clear();
        }
    }
}