using CalculatorCommand.View_Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
using NCalc;
namespace CalculatorCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand? NumbersCommand { get; set; }
        public ICommand? OperatorsCommand { get; set; }
        public ICommand? DeleteCommand { get; set; }
        public int SavedFontSize { get; set; } = 10;


        public string? Num1 { get; set; }
        public char? Op { get; set; } = null;
        public string? Num2 { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            NumbersCommand = new RelayCommand(NumbersAddCommand, CanExecuteCalculateCommand);
            OperatorsCommand = new RelayCommand(OperatorsAddedCommand, CanExecuteCalculateCommand);
            DeleteCommand = new RelayCommand(EreaseCommand, CanExecuteCalculateCommand);
        }

        void EreaseCommand(object? parameter)
        {
            Num1 = null;
            Num2 = null;
            Op = null;
            Calc_Equal_TextBlock.Text="";
            Calc_Txt_Block.Text = "";
            Sum_Button.IsEnabled = true;
            Minus_Button.IsEnabled = true;
            Multiply_Button.IsEnabled = true;
            Divide_Button.IsEnabled = true;
        }
        void OperatorsAddedCommand(object? parameter)
        {
            try
            {

                if (Sum_Button.IsEnabled)
                {
                    Sum_Button.IsEnabled = false;
                    Minus_Button.IsEnabled = false;
                    Multiply_Button.IsEnabled = false;
                    Divide_Button.IsEnabled = false;
                    Calc_Txt_Block.Text = $"{Num1} {Op} {Num2}";
                }
                else
                {
                    NCalc.Expression exp = new NCalc.Expression(Calc_Txt_Block.Text.ToString());
                    Calc_Equal_TextBlock.Text = Convert.ToSingle(exp.Evaluate()).ToString();
                    Calc_Txt_Block.Text = Convert.ToSingle(exp.Evaluate()).ToString();
                    Num1 = Calc_Equal_TextBlock.Text.ToString();
                    Sum_Button.IsEnabled = true;
                    Minus_Button.IsEnabled = true;
                    Multiply_Button.IsEnabled = true;
                    Divide_Button.IsEnabled = true;
                    Op = null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        void NumbersAddCommand(object? parameter)
        {
            try
            {
                Calc_Txt_Block.Text = $"{Num1} {Op} {Num2}";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        bool CanExecuteCalculateCommand(object? parametr)
        {
            return Convert.ToSingle(Num1) > 0 && Convert.ToSingle(Num2) > 0;
        }

        private void Number_Button_Click(object sender, RoutedEventArgs e)
        {


            Button? btn = sender as Button;
            if (Op != '+' || Op != '-' || Op != '*' || Op != '/') Num1 += btn?.Content.ToString();
            else Num2 += btn?.Content.ToString();
            if (Calc_Txt_Block.Text.Length > SavedFontSize - 1)
            {
                Calc_Txt_Block.FontSize -= 20;
                SavedFontSize = Calc_Txt_Block.Text.Length + 10;
            }

        }
    }
}
