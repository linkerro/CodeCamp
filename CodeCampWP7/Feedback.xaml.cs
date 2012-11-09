using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CodeCampWP7
{
    public partial class Feedback : PhoneApplicationPage
    {
        public Feedback()
        {
            DataContext = App.ViewModel.FeedbackModel;
            InitializeComponent();
        }
    }
}