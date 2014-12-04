using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ToastPOC
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;

			MyCommand = new DelegateCommand<string>(o => MessageBox.Show(o.ToString()));
		}

		public DelegateCommand<string> MyCommand { get; set; }

		private void ShowButton_Click(object sender, RoutedEventArgs e)
		{
			MyMessage = "hello world!";
		}

		public string MyMessage
		{
			get { return m_MyMessage; }
			set
			{
				m_MyMessage = value;
				RaisePropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private string m_MyMessage;

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}