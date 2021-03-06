﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Todo_Net
{
    /// <summary>
    /// Interaction logic for TodoView.xaml
    /// </summary>
    public partial class TodoView : UserControl
    {
        public TodoView()
        {
            InitializeComponent();
        }

        private void WatermarkTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                // Move focus to MarkAllAsDone button
                markAllAsDoneButton.Focus();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                // Move focus to the list view
                itemsListView.Focus();
            }
        }

    }
}
