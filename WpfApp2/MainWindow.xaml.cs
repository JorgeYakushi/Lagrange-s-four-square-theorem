using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }




        int contadorCorrecto = 0;
        int cantidad = 0;
        int maxA, minA, maxB, minB, maxC, minC, d;

        void Main(int n)
        {
            var list = new List<Tuple<int, int, int, int>>();
            maxA = Convert.ToInt32(Math.Floor(Math.Sqrt(n)));
            minA = Convert.ToInt32(Math.Ceiling(Math.Sqrt(n / 4)));
            int[] arrayA = new int[maxA - minA + 1];
            for (int i = 0; i < arrayA.Length; i++)
            {
                arrayA[i] = (minA + i) * (minA + i);
                maxB = Convert.ToInt32(Math.Floor(Math.Sqrt(n - arrayA[i])));
                minB = Convert.ToInt32(Math.Ceiling(Math.Sqrt((n - arrayA[i]) / 3)));
                int[] arrayB = new int[maxB - minB + 1];
                for (int ii = 0; ii < arrayB.Length; ii++)
                {
                    arrayB[ii] = (minB + ii) * (minB + ii);
                    if (arrayB[ii] > arrayA[i])
                    {
                        continue;
                    }
                    maxC = Convert.ToInt32(Math.Floor(Math.Sqrt(n - arrayA[i] - arrayB[ii])));
                    minC = Convert.ToInt32(Math.Ceiling(Math.Sqrt((n - arrayA[i] - arrayB[ii]) / 2)));
                    int[] arrayC = new int[maxC - minC + 1];
                    for (int iii = 0; iii < arrayC.Length; iii++)
                    {
                        arrayC[iii] = (minC + iii) * (minC + iii);
                        if (arrayC[iii] > arrayB[ii])
                        {
                            continue;
                        }
                        d = Convert.ToInt32(Math.Floor(Math.Sqrt(n - arrayA[i] - arrayB[ii] - arrayC[iii])));
                        cantidad++;
                        if (d * d == Convert.ToInt32(n - arrayA[i] - arrayB[ii] - arrayC[iii]))
                        {
                            list.Add(new Tuple<int, int, int, int>(Convert.ToInt32(Math.Sqrt(arrayA[i])), Convert.ToInt32(Math.Sqrt(arrayB[ii])), Convert.ToInt32(Math.Sqrt(arrayC[iii])), d));
                            contadorCorrecto++;
                        }
                    }
                }
            }
            list.Sort((x, y) => {
                int result = y.Item1.CompareTo(x.Item1);
                return result == 0 ? y.Item2.CompareTo(x.Item2) : result;
            });
            listBox.ItemsSource = list;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
            cantidad = 0;
            contadorCorrecto = 0;
            DateTime inicio = new DateTime();
            inicio = DateTime.Now;

            int n = Convert.ToInt32(textBox.Text);
            Main(n);
    



            DateTime fin = new DateTime();
            fin = DateTime.Now;
            labelNum2.Content = fin.Subtract(inicio).TotalSeconds;
            labelNum4_Copy.Content = contadorCorrecto;
            labelNum4_Copy1.Content = listBox.Items.Count;
            labelNum4.Content = cantidad;
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        static void Quicksort(int[] arr, int begin, int end)
        {
            int pivot = arr[(begin + (end - begin) / 2)];
            int left = begin;
            int right = end;
            while (left <= right)
            {
                while (arr[left] > pivot)
                {
                    left++;
                }
                while (arr[right] < pivot)
                {
                    right--;
                }
                if (left <= right)
                {
                    Swap(arr, left, right);
                    left++;
                    right--;
                }
            }
            if (begin < right)
            {
                Quicksort(arr, begin, left - 1);
            }
            if (end > left)
            {
                Quicksort(arr, right + 1, end);
            }
        }
        static void Swap(int[] items, int x, int y)
        {
            int temp = items[x];
            items[x] = items[y];
            items[y] = temp;
        }





    }
}
