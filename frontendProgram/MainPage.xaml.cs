﻿using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
namespace frontendProgram
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            loadBookmarks();


        }

        private void loadBookmarks()
        {
            string[] bookmarks = {
 "loop 2", "loop 3", "loop 4", "loop 5",
                    "loop 6", "loop 7", "loop 8", "loop 9", "loop 10",
                    "loop 11", "loop 12", "loop 13", "loop 14", "loop 15",
                    "loop 16", "loop 17", "loop 18", "loop 19", "loop 20",
                    "loop 21", "loop 22", "loop 23", "loop 24", "loop 25",
                    "loop 26", "loop 27", "loop 28", "loop 29", "loop 30",
                    "loop 31", "loop 32", "loop 33", "loop 34", "loop 35",
                    "loop 36", "loop 37", "loop 38", "loop 39", "loop 40",
                    "loop 41", "loop 42", "loop 43", "loop 44", "loop 45",
                    "loop 46", "loop 47", "loop 48", "loop 49", "loop 50",
                    "loop 51", "loop 52", "loop 53", "loop 54", "loop 55",
                    "loop 56", "loop 57", "loop 58", "loop 59", "loop 60",
                    "loop 61", "loop 62", "loop 63", "loop 64", "loop 65",
                    "loop 66", "loop 67", "loop 68", "loop 69", "loop 70",
                    "loop 71", "loop 72", "loop 73", "loop 74", "loop 75",
                    "loop 76", "loop 77", "loop 78", "loop 79", "loop 80",
                    "loop 81", "loop 82", "loop 83", "loop 84", "loop 85",
                    "loop 86", "loop 87", "loop 88", "loop 89", "loop 90",
                    "loop 91", "loop 92", "loop 93", "loop 94", "loop 95",
                    "loop 96", "loop 97", "loop 98", "loop 99", "loop 100"
                };

            foreach (string bookmark in bookmarks)
            {
                var Button = new Button { Text = bookmark };
                Button.Margin = 2;
                TableOfBookmarks.Children.Add(Button);
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\bbdnet3169\\Desktop\\AAAAAAAAAA.txt";
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    Process.Start("notepad.exe", filePath);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex + "file does not exist");
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            TableOfBookmarks.Children.Clear();
            loadBookmarks();
        }
    }

}
