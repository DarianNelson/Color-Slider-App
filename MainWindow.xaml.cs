using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Color_Slider_App {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }//end main
        private void updateRectangle(byte red, byte green, byte blue, byte alpha) {
            Color brushColor = Color.FromArgb(alpha, red, green, blue);
            SolidColorBrush brush = new SolidColorBrush(brushColor);
            rctColor.Fill = brush;
        }//end function

        private void UpdateLabels() {

            //GET COLOR FROM RECTANGLE
            SolidColorBrush tempBrush = (SolidColorBrush)rctColor.Fill;
            Color brushColor = tempBrush.Color;
            
            //GET COMPONENT VALUES FROM THE COLOR
            byte alpha = brushColor.A;
            byte red = brushColor.R;
            byte green = brushColor.G;
            byte blue = brushColor.B;

            //SHOW COMPONENTS AS A BINARY NUMBER
            string binaryString = $"{Convert.ToString(alpha, 2).PadLeft(8, '0')} {Convert.ToString(red, 2).PadLeft(8, '0')} {Convert.ToString(green, 2).PadLeft(8, '0')} {Convert.ToString(blue, 2).PadLeft(8, '0')}";
            if (lblBinary != null) {
                lblBinary.Content = binaryString;
            }//end if

            //ARGB
            byte[] values = { blue, green, red, alpha };
            uint intValue = BitConverter.ToUInt32(values,0);

            if(lblInteger != null) {
                lblInteger.Content = intValue.ToString();
            }//end if

            //SHOW COLOR'S HEX VALUE
            string hexString = $"{Convert.ToString(intValue, 16).PadLeft(8, '0')}";
            if(lblHex != null) {
                lblHex.Content = hexString;
            }//end if

        }//end event
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            byte red = (byte)sldRed.Value;
            byte green = (byte)sldGreen.Value;
            byte blue = (byte)sldBlue.Value;
            byte alpha = (byte)sldAlpha.Value;

            if (txtRed != null) {
                txtRed.Text = red.ToString();
            }//end if
            if (txtGreen != null) {
                txtGreen.Text = green.ToString();
            }//end if
            if (txtBlue != null) {
                txtBlue.Text = blue.ToString();
            }//end if
            if (txtAlpha != null) {
                txtAlpha.Text = alpha.ToString();
            }//end if
            
            updateRectangle(red, green, blue, alpha);
            UpdateLabels();
        }//end event

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            byte red;
            byte green;
            byte blue;
            byte alpha;

            if (txtRed != null) {
                byte.TryParse(txtRed.Text, out red);
                sldRed.Value = red;
            }//end if

            if (txtGreen != null) {
                byte.TryParse(txtGreen.Text, out green);
                sldGreen.Value = green;
            }//end if

            if (txtBlue != null) {
                byte.TryParse(txtBlue.Text, out blue);
                sldBlue.Value = blue;
            }//end if

            if (txtAlpha != null) {
                byte.TryParse(txtAlpha.Text, out alpha);
                sldAlpha.Value = alpha;
            }//end if

        }//end event

        #region HELPER FUNCTIONS
        #region Arrays
        static void FillArray(char[,] array, char char1) {
            for (int y = 0; y < array.GetLength(1); y++) {
                for (int x = 0; x < array.GetLength(0); x++) {
                    array[x, y] = char1;
                }//end for
            }//end for
        }//end function

        static void PrintArray(char[,] array, char char1) {
            //LOOP THROUGH ALL ROWS
            for (int y = 0; y < array.GetLength(1); y++) {
                //LOOP THROUGH ALL COLUMNS OF EACH ROW
                for (int x = 0; x < array.GetLength(0); x++) {

                    //OUTPUT VALUE IN ARRAY AT CURRENT COLUMN
                    Console.Write(array[x, y] + " ");
                }//end for

                //MOVE TO NEXT LINE FOR THE NEXT ROW
                Console.WriteLine();
            }//end for
        }//end function
        #endregion Arrays

        #region Input/Try Parse

        //WRITE TO CONSOLE
        static void Print(string message) {
            Console.WriteLine(message);
        }//end function

        //PARSE INPUT & TRY PARSE INPUT
        static string Input(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }//end function

        static decimal InputDecimal(string message) {
            string value = Input(message);
            return decimal.Parse(value);
        }//end function

        static decimal TryInputDecimal(string message) {
            //VARIABLES
            decimal parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DECIMAL HAS BEEN SUBMITTED
            do {
                gotParsed = decimal.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static double InputDouble(string message) {
            string value = Input(message);
            return double.Parse(value);
        }//end function

        static double TryInputDouble(string message) {
            //VARIABLES
            double parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DOUBLE HAS BEEN SUBMITTED
            do {
                gotParsed = double.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static int InputInt(string message) {
            string value = Input(message);
            return int.Parse(value);
        }//end function

        static int TryInputInt(string message) {
            //VARIABLES
            int parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID INT HAS BEEN SUBMITTED
            do {
                gotParsed = int.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function
        #endregion Input/Try Parse

        #region Bools

        //BOOL INPUT YES OR NO (Y/N)
        static bool InputYesNo(string message) {
            //WRITE MESSAGE TO CONSOLE FOR INPUT 
            Console.Write(message);

            //GET THE KEY THAT WAS PRESSED
            char keyPressed = Console.ReadKey().KeyChar;

            //FORCE A NEW LINE
            Console.WriteLine();

            //CONVERT KEY PRESSED TO LOWER CASE
            char lowerCaseKey = char.ToLower(keyPressed);

            //COMAPRE
            bool pressedYes = lowerCaseKey == 'y';

            return pressedYes;
        }//end function
        #endregion Bool

        #region Colors
        //PRINT AND CHANGE COLORS
        static void PrintColor(string message, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }//end function
        static void ConsoleSetForeColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[38;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBackColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[48;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBlock(int xPos, int yPos, byte[] color) {
            //STORE OLD COLORS
            ConsoleColor origForeground = Console.ForegroundColor;
            ConsoleColor origBackground = Console.BackgroundColor;

            //SET BLOCK COLOR
            byte red = color[0];
            byte grn = color[1];
            byte blu = color[2];

            ConsoleSetForeColor(red, grn, blu);
            ConsoleSetBackColor(red, grn, blu);

            //MOVE CURSOR TO POSITION
            Console.SetCursorPosition(xPos, yPos);

            //DRAW BLOCK
            Console.Write(" ");

            //CHANGE COLORS BACK
            Console.ForegroundColor = origForeground;
            Console.BackgroundColor = origBackground;
        }//end function
        #endregion Colors

        #endregion HELPER FUNCTIONS

    }//end class
}//end namespace
