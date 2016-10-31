using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double val4 = 0;
        String selectedOperation = "";
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Selected_Number(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            String value = b.Content.ToString();
            tbPreview.Text += value;
            val4 = Double.Parse(value);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Length > 0)
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
            }
        }

        private void Delete_All(object sender, RoutedEventArgs e)
        {
            tb.Text = "0";
            tbPreview.Text = "";

        }

        private void Equals(object sender, RoutedEventArgs e)
        {
            int index = 0;
            double result = 0, val1 = 0, val2 = 0, val3 =0;
            //String previewText = tb.Text.ToString();
            //try
            //{
            //    result = calculateValues(operation, val1, val2);
            //    tb.Text = "" + result;
            //    tbPreview.Text = "";


            //}
            //catch (Exception exc)
            //{
            //    tb.Text = "Error calculating values!";
            //}

            try
            {
                String text = tbPreview.Text.ToString();
                String viewText = tb.Text.ToString();
                if (viewText.Equals("0") || viewText.Equals(""))
                {
                    if (checkSecondValue(text))
                    {
                        index = getIndex(text);
                        //op = tbPreview.Text.Substring(index, 1);
                        val1 = Convert.ToDouble(text.Substring(0, index));
                        val2 = Convert.ToDouble(text.Substring(index + 1, text.Length - index - 1));
                        result = calculateValues(selectedOperation, val1, val2);
                        tb.Text = "" + result;
                        val4 = 0;
                        //Console.WriteLine(result);

                    }
                    else
                    {
                    }
                }
                else
                {
                    val3 = Double.Parse(tb.Text.ToString());
                    result = calculateValues(selectedOperation, val3, val4);
                    tb.Text = "" + result;
                }
            }
            catch (Exception exc)
            {
                tb.Text = "Error calculating values!";
            }



        }

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        public int getIndex(String text)
        {
            int index = 0;
            if (text.Contains("+"))
            {
                index = text.IndexOf("+");
            }
            else if (text.Contains("-"))
            {
                index = text.IndexOf("-");
            }
            else if (text.Contains("*"))
            {
                index = text.IndexOf("*");
            }
            else if (text.Contains("/"))
            {
                index = text.IndexOf("/");
            }else
            {
                index = -1;
            }
            return index;
        }

        public string getOperation(String text)
        {
            string operation = "";
            if (text.Contains("+"))
            {
                operation ="+";
            }
            else if (text.Contains("-"))
            {
                operation = "-";
            }
            else if (text.Contains("*"))
            {
                operation = "*";
            }
            else if (text.Contains("/"))
            {
                operation = "/";
            }
            else
            {
                operation = "0"; 
            }
            return operation ; 
        }

        public Boolean checkSecondValue(String text)
        {
            bool isExist = false;
            try{
                int index = getIndex(text);
                double val2 = Convert.ToDouble(tbPreview.Text.Substring(index + 1, tbPreview.Text.Length - index - 1));
                isExist = true;
            }catch (Exception exc)
            {
                isExist = false;
            }
            return isExist;

        }

        public Boolean checkOperation(String text)
        {
            bool isExist = false;
            if (text.Contains("+") || text.Contains("-") || text.Contains("*") || text.Contains("/"))
            {
                isExist = true;
            }
            else
            {
                isExist = false;
            }
            return isExist;

        }

        public double calculateValues(String operation, double val1, double val2)
        {
            double result = 0;
            if (operation == "+")
            {
                //var sum = tb.Text.Split('+').Select(int.Parse).Sum(c => c);
                //tb.Text += "=" + sum.ToString();
                result = val1 + val2;
            }
            else if (operation == "-")
            {
                result = (val1 - val2);
            }
            else if (operation == "*")
            {
                result = (val1 * val2);
            }
            else
            {
                result = (val1 / val2);
            }
            return result;
        }

        private void Selected_Operation(object sender, RoutedEventArgs e)
        {
            String text1 = tbPreview.Text.ToString();
            Button b = (Button)sender;
            String operation = b.Content.ToString();
            selectedOperation = operation;
            int index = 0;
            double result = 0, val1 = 0, val2 = 0, val3 = 0;
            String previewText = tbPreview.Text.ToString();
            String previousOperation = getOperation(previewText);
            bool enteredOperation = checkOperation(previewText); 
            if (!enteredOperation)
            {
                tbPreview.Text += operation;
            }
            else if (tb.Text.ToString().Equals("0") && enteredOperation)
            {
                String text = tbPreview.Text.ToString();
                if (checkSecondValue(text))
                {
                    index = getIndex(text);
                    //op = tbPreview.Text.Substring(index, 1);
                    val1 = Convert.ToDouble(text.Substring(0, index));
                    val2 = Convert.ToDouble(text.Substring(index + 1, text.Length - index - 1));
                    result = calculateValues(previousOperation, val1, val2);
                    tb.Text = "" + result;
                    tbPreview.Text += operation;
                    //Console.WriteLine(result);

                }
                else
                {

                }
            }else if (!tb.Text.ToString().Equals("0"))
            {
                val3 = Double.Parse(tb.Text.ToString());
                result = calculateValues(previousOperation, val3, val4);
                tb.Text = "" + result;
                tbPreview.Text += operation;
            }

    }

        
    }
}
