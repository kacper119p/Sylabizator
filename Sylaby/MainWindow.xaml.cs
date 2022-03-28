using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Sylabizator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int caretIndex = textBox.CaretIndex + 1;

            char[] chars = textBox.Text.ToCharArray();

            Regex regex = new Regex("^[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ]");

            char[] newChars = new char[chars.Length];

            int j = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (regex.IsMatch(chars[i].ToString()))
                {
                    newChars[j] = chars[i];
                    j++;
                }
            }

            try
            {
                newChars[0] = char.ToUpper(newChars[0]);
            }
            catch (IndexOutOfRangeException) { }

            textBox.Text = new string(newChars);
            textBox.CaretIndex = caretIndex;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string[] syllabes = Syllabizer.Run(input.Text);

            syllabes[0] = syllabes[0].Substring(0, 1).ToUpper() + syllabes[0].Substring(1);

            string result = string.Empty;
            for (int i = 0; i < syllabes.Length - 1; i++)
            {
                result += syllabes[i] + "-";
            }
            result += syllabes[syllabes.Length - 1];
            output.Content = result;
        }
    }
}
