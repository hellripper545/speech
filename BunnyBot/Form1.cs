﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;

namespace BunnyBot
{
    public partial class form1 : Form
    {
        SpeechRecognitionEngine user = new SpeechRecognitionEngine();
        SpeechSynthesizer bunny = new SpeechSynthesizer();
        string userName = Environment.UserName;
        private int ranNum;

        public form1()
        {
            InitializeComponent();
        }

        
        private void form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "bunny", "hello", "hai", "hey", "hello bunny", "what is my name", "what time is it", "what day is it", "goodbye", "goodbye bunny", "close bunny", "go offline", "bye", "see you","out of my way","offscreen","come back","onscreen" });
            GrammarBuilder gBuiler = new GrammarBuilder();
            gBuiler.Append(commands);
            Grammar grammer = new Grammar(gBuiler);
            user.LoadGrammarAsync(grammer);
            user.SetInputToDefaultAudioDevice();
            user.SpeechRecognized += User_SpeechRecognized;
                                   
        }

        private void User_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string input = e.Result.Text;
            Random rnd = new Random();
            DateTime now = new DateTime();
            switch (input)
            {
                case "hai":
                case "hello":
                case "hello bunny":
                    if ( now.Hour >= 5 && now.Hour < 12)
                    {
                        bunny.SpeakAsync("Good Morning" +userName );
                    }
                    if (now.Hour >= 12 && now.Hour < 18)
                    {
                        bunny.SpeakAsync("Good Afternoon " +userName);
                    }
                    if (now.Hour >= 18 && now.Hour < 24)
                    {
                        bunny.SpeakAsync("Good Evening " +userName);
                    }
                    if (now.Hour < 5)
                    {
                        bunny.SpeakAsync("Hello " +userName + ", it's getting late");
                    }
                    break;

                case "bunny":
                case "hey":
                    ranNum = rnd.Next(1, 5);
                    if (ranNum == 1)
                    {
                        bunny.SpeakAsync("Yes sir");
                    }
                    else if (ranNum == 2)
                    {
                        bunny.SpeakAsync("Yes?");
                    }
                    else if (ranNum == 3)
                    {
                        bunny.SpeakAsync("How may I help?");
                    }
                    else if (ranNum == 4)
                    {
                        bunny.SpeakAsync("How may I be of assistance?");
                    }
                    break;

                case "what is my name":
                    bunny.SpeakAsync( " " +userName );
                    break;

                case "what time is it":
                    string time = now.GetDateTimeFormats('t')[0];
                    bunny.SpeakAsync("time");
                    break;

                case "what day is it":
                    bunny.SpeakAsync(DateTime.Today.ToString("dddd"));
                    break;

                case "what date is it":
                    bunny.SpeakAsync(DateTime.Today.ToString("dd-MM-yyyy"));
                    break;

                case "out of the way":
                case "offscreen":
                    if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
                    {
                        WindowState = FormWindowState.Minimized;
                        bunny.SpeakAsync("My Apologies");
                    }
                    break;

                case "come back":
                case "onscreen":
                    if (WindowState == FormWindowState.Minimized)
                    {
                        bunny.SpeakAsync("Onscreen Sir");
                        WindowState = FormWindowState.Normal;
                    }
                    break;

                case "go fullscreen":
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                    TopMost = true;
                    bunny.SpeakAsync("Expanding sir");
                    break;
                                        
                case "exit fullscreen":
                    FormBorderStyle = FormBorderStyle.Sizable;
                    WindowState = FormWindowState.Normal;
                    TopMost = false;
                    bunny.SpeakAsync("Exiting sir");
                    break;

                case "goodbye":
                case "bye":
                case "goodbye bunny":
                case "close bunny":
                case "go offline":
                case "see you":
                    if (ranNum > 6)
                    {
                        bunny.SpeakAsync("Farewell");
                        Close();
                    }
                    else
                    {
                        bunny.SpeakAsync("GoodBye");
                        Close();
                    }
                    break;
                   
            }
        }
    }
}
