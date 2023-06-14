using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;

namespace k1
{
    public partial class EM2 : Form
    {
        public EM2()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');

                    if (parts.Length == 2)
                    {
                        string adr = parts[0].Trim();
                        string[] values = parts[1].Trim().Split(' ');

                        if (values.Length == 3)
                        {
                            string kom = values[0].Trim();
                            string a1 = values[1].Trim();
                            string a2 = values[2].Trim();

                            DataGridViewRow rowToUpdate = null;
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == adr)
                                {
                                    rowToUpdate = row;
                                    break;
                                }
                            }

                            if (rowToUpdate != null)
                            {
                                rowToUpdate.Cells[1].Value = kom;
                                rowToUpdate.Cells[2].Value = a1;
                                rowToUpdate.Cells[3].Value = a2;
                            }
                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            
            int currentRow = 0;
            int zFlag = 0;
            int omegaFlag = 0;
            while (currentRow < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[currentRow];

                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    string adr = row.Cells[0].Value.ToString();    
                    string command = row.Cells[1].Value.ToString();  
                    string a1 = row.Cells[2].Value.ToString();       
                    string a2 = row.Cells[3].Value.ToString();
                   

                    if (command == "ПЕР") //Пересилання значення з A2 в А1
                    {
                        float a1Value = float.Parse(a1);
                        float a2Value = float.Parse(a2);

                        int a1Index = (int)a1Value - 1;
                        int a2Index = (int)a2Value - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueToTransferA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            dataGridView1.Rows[a1Index].Cells[3].Value = valueToTransferA2;
                        }
                    }

                    else if (command == "ПДЧ")//Додавання дійсних чисел: A1 = A1 + A2

                    {
                        int a1Index = int.Parse(a1) - 1; 
                        int a2Index = int.Parse(a2) - 1; 

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString(); 
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString(); 

                            double num1 = double.Parse(valueA1);
                            double num2 = double.Parse(valueA2);
                            double sum = num1 + num2;
                            if (sum == 0)
                            {
                                omegaFlag = 0;
                                zFlag = 1;
                            }
                            else if (sum < 0)
                            {
                                omegaFlag = 1;
                                zFlag = 0;
                            }
                            else
                            {
                                omegaFlag = 2;
                                zFlag = 0;
                            }
                            dataGridView1.Rows[a1Index].Cells[3].Value = sum.ToString(); 
                        }
                    }

                    else if (command == "ВДЧ")//Віднімання дійсних чисел: A1 = A1 - A2
                    {
                        int a1Index = int.Parse(a1) - 1; 
                        int a2Index = int.Parse(a2) - 1; 

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString(); 
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString(); 

                            double num1 = double.Parse(valueA1);
                            double num2 = double.Parse(valueA2);
                            double raz = num1 - num2;
                            if (raz == 0)
                            {
                                omegaFlag = 0;
                                zFlag = 1;
                            }
                            else if (raz < 0)
                            {
                                omegaFlag = 1;
                                zFlag = 0;
                            }
                            else
                            {
                                omegaFlag = 2;
                                zFlag = 0;
                            }
                            dataGridView1.Rows[a1Index].Cells[3].Value = raz.ToString(); 
                        }
                    }

                    else if (command == "МДЧ")//Множення дійсних чисел: A1 = A1 × A2
                    {     
                        int a1Index = int.Parse(a1) - 1; 
                        int a2Index = int.Parse(a2) - 1; 

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString(); 
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString(); 

                            double num1 = double.Parse(valueA1);
                            double num2 = double.Parse(valueA2);
                            double mno = num1 * num2;
                            if (mno == 0)
                            {
                                omegaFlag = 0;
                                zFlag = 1;
                            }
                            else if (mno < 0)
                            {
                                omegaFlag = 1;
                                zFlag = 0;
                            }
                            else
                            {
                                omegaFlag = 2;
                                zFlag = 0;
                            }
                            dataGridView1.Rows[a1Index].Cells[3].Value = mno.ToString(); 
                        }
                    }
                    

                    else if (command == "ДДЧ")//Ділення дійсних чисел: A1 = A1 ÷ A2
                    {
                        int a1Index = int.Parse(a1) - 1; 
                        int a2Index = int.Parse(a2) - 1; 

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                            {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString(); 
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString(); 
                            double num1 = double.Parse(valueA1);
                            double num2 = double.Parse(valueA2);
                             if (num2 != 0)
                             { 
                                 double del = num1 / num2;
                                if (del == 0)
                                {
                                    omegaFlag = 0;
                                    zFlag = 1;
                                }
                                else if (del < 0)
                                {
                                    omegaFlag = 1;
                                    zFlag = 0;
                                }
                                else
                                {
                                    omegaFlag = 2;
                                    zFlag = 0;
                                }
                                dataGridView1.Rows[a1Index].Cells[3].Value = del.ToString(); 
                             }
                             else
                             {
                                 label1.Text = "ДДЧ: Деление на 0.";
                                 return;
                             }                    
                        }
                    }

                    else if (command == "ВЕД")//Введення масиву дійсних чисел у кількості A2, починаючи з адреси A1
                    {
                        int startingRow = int.Parse(a1) - 1;
                        int count = int.Parse(a2);

                        string inputValues = textBox1.Text.Trim();
                        string[] numbers = inputValues.Split(' ');

                        if (numbers.Length == count)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                double value = double.Parse(numbers[i]);
                                dataGridView1.Rows[startingRow + i].Cells[3].Value = value;
                            }
                        }
                        else
                        {
                            label1.Text = "ВЕД: Количество введенных чисел не соответствует значению А2.";
                            return;
                        }
                    }

                    else if (command == "ВЕЦ")//Введення масиву цілих чисел у кількості A2, починаючи з адреси A1
                    {
                        int startingRow = int.Parse(a1) - 1;
                        int count = int.Parse(a2);

                        string inputValues = textBox1.Text.Trim();
                        string[] numbers = inputValues.Split(' ');

                        if (numbers.Length == count)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                if (int.TryParse(numbers[i], out int value))
                                {
                                    dataGridView1.Rows[startingRow + i].Cells[3].Value = value;
                                }
                                else
                                {
                                    label1.Text = "ВЕЦ: Введены некорректные значения чисел.";
                                }
                            }
                        }
                        else
                        {
                            label1.Text = "ВЕЦ: Количество введенных чисел не соответствует значению А2.";
                        }
                    }
                    
                    else if (command == "ПЦЧ")//Додавання цілих чисел: A1 = A1 + A2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            if (int.TryParse(valueA1, out int num1) && int.TryParse(valueA2, out int num2))
                            {
                                int sum = num1 + num2;
                                if (sum == 0)
                                {
                                    omegaFlag = 0;
                                    zFlag = 1;
                                }
                                else if (sum < 0)
                                {
                                    omegaFlag = 1;
                                    zFlag = 0;
                                }
                                else
                                {
                                    omegaFlag = 2;
                                    zFlag = 0;
                                }
                                dataGridView1.Rows[a1Index].Cells[3].Value = sum.ToString();
                            }
                            else
                            {
                                label1.Text = "ПЦЧ: Введены некорректные значения чисел.";
                            }
                        }
                    }

                    else if (command == "ВЦЧ")//Віднімання цілих чисел: A1 = A1 - A2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            if (int.TryParse(valueA1, out int num1) && int.TryParse(valueA2, out int num2))
                            {
                                int raz = num1 - num2;
                                if (raz == 0)
                                {
                                    omegaFlag = 0;
                                    zFlag = 1;
                                }
                                else if (raz < 0)
                                {
                                    omegaFlag = 1;
                                    zFlag = 0;
                                }
                                else
                                {
                                    omegaFlag = 2;
                                    zFlag = 0;
                                }
                                dataGridView1.Rows[a1Index].Cells[3].Value = raz.ToString();
                            }
                            else
                            {
                                 label1.Text = "ВЦЧ: Введены некорректные значения чисел.";
                            }
                        }
                    }

                    else if (command == "МЦЧ")//Множення цілих чисел: A1 = A1 × A2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            if (int.TryParse(valueA1, out int num1) && int.TryParse(valueA2, out int num2))
                            {
                                int mno = num1 * num2;
                                if (mno == 0)
                                {
                                    omegaFlag = 0;
                                    zFlag = 1;
                                }
                                else if (mno < 0)
                                {
                                    omegaFlag = 1;
                                    zFlag = 0;
                                }
                                else
                                {
                                    omegaFlag = 2;
                                    zFlag = 0;
                                }
                                dataGridView1.Rows[a1Index].Cells[3].Value = mno.ToString();
                            }
                            else
                            {
                                label1.Text = "МЦЧ: Введены некорректные значения чисел.";
                            }
                        }
                    }

                    else if (command == "ДЦЧ")//Ділення цілих чисел: A1 = A1 ÷ A2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            if (int.TryParse(valueA1, out int num1) && int.TryParse(valueA2, out int num2))
                            {
                                if (num2 != 0)
                                {
                                    int del = num1 / num2;
                                    if (del == 0)
                                    {
                                        omegaFlag = 0;
                                        zFlag = 1;
                                    }
                                    else if (del < 0)
                                    {
                                        omegaFlag = 1;
                                        zFlag = 0;
                                    }
                                    else
                                    {
                                        omegaFlag = 2;
                                        zFlag = 0;
                                    }
                                    dataGridView1.Rows[a1Index].Cells[3].Value = del.ToString();
                                }
                                else
                                {
                                    label1.Text = "ДЦЧ: Деление на 0.";
                                }
                            }
                            else
                            {
                                label1.Text = "ДЦЧ: Введены некорректные значения чисел.";
                            }
                        }
                    }

                    else if (command == "МОД")//Остача від ділення цілих чисел
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            if (int.TryParse(valueA1, out int num1) && int.TryParse(valueA2, out int num2))
                            {
                                int mod = num1 % num2;
                                if (mod == 0)
                                {
                                    omegaFlag = 0;
                                    zFlag = 1;
                                }
                                else if (mod < 0)
                                {
                                    omegaFlag = 1;
                                    zFlag = 0;
                                }
                                else
                                {
                                    omegaFlag = 2;
                                    zFlag = 0;
                                }
                                dataGridView1.Rows[a1Index].Cells[3].Value = mod.ToString();
                            }
                            else
                            {
                                label1.Text = "МОД: Введены некорректные значения чисел.";
                            }
                        }
                    }

                    else if (command == "ВИД")//Вивід масиву дійсних чисел у кількості A2, починаючи з адреси A1
                    {
                        int startAddress = int.Parse(a1);
                        int count = int.Parse(a2);

                        if (dataGridView1.Rows.Count >= startAddress + count)
                        {
                            string output = "";
                            for (int i = startAddress; i < startAddress + count; i++)
                            {
                                output += dataGridView1.Rows[i - 1].Cells[3].Value.ToString() + " ";
                            }
                            label1.Text = "Масив дійсних чисел: " + output;
                        }
                    }
                    else if (command == "ВИЦ")//Вивід масиву цілих чисел у кількості A2, починаючи з адреси A1
                    {
                        int startRow = int.Parse(a1);
                        int count = int.Parse(a2);

                        if (dataGridView1.Rows.Count >= startRow + count)
                        {
                            string output = "";
                            for (int i = startRow; i < startRow + count; i++)
                            {
                                output += dataGridView1.Rows[i - 1].Cells[3].Value.ToString() + " ";
                            }
                            label1.Text = "Масив цілих чисел: " + output;
                        }
                    }

                    else if (command == "БПР")//Безумовний перехід з поточного рядка на рядок А2
                    {

                        if (int.TryParse(a2, out int targetRow))
                        {
                           
                                currentRow = targetRow - 1; 
                                continue;            
                        }
                        else
                        {
                            label1.Text = "Некорректное значение операнда А2.";
                            break; 
                        }
                    }

                    else if (command == "ПЕЦ")//Переведення цілого числа А1 у дійсне А2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            int num1;
                            if (int.TryParse(valueA1, out num1))
                            {
                                double num2;
                                if (double.TryParse(valueA2, out num2))
                                {
                                    double result = (double)num1;
                                    dataGridView1.Rows[a2Index].Cells[3].Value = result.ToString();
                                }
                                else
                                {
                                    label1.Text = "Некорректное значение в ячейке А2.";
                                }
                            }
                            else
                            { 
                                label1.Text = "Некорректное значение в ячейке А1.";
                            }
                        }
                    }
                    
                    else if (command == "ПДЦ")//Переведення дійсного числа А1 в ціле А2
                    {
                        int a1Index = int.Parse(a1) - 1;
                        int a2Index = int.Parse(a2) - 1;

                        if (a1Index >= 0 && a1Index < dataGridView1.Rows.Count && a2Index >= 0 && a2Index < dataGridView1.Rows.Count)
                        {
                            string valueA1 = dataGridView1.Rows[a1Index].Cells[3].Value.ToString();
                            string valueA2 = dataGridView1.Rows[a2Index].Cells[3].Value.ToString();

                            double num1;
                            if (double.TryParse(valueA1, out num1))
                            {
                                int num2;
                                if (int.TryParse(valueA2, out num2))
                                {
                                    int result = (int)num1;
                                    dataGridView1.Rows[a2Index].Cells[3].Value = result.ToString();
                                }
                                else
                                {
                                    label1.Text = "Некорректное значение в ячейке А2.";
                                }
                            }
                            else
                            {
                                label1.Text = "Некорректное значение в ячейке А1.";
                            }
                        }
                    }

                    else if (command == "УПЗ")//Умовний перехід: якщо прапор Z дорівнює 0 — перехід на рядок A1,якщо прапор Z дорівнює 1 — перехід на рядок A2
                    {
                        if (zFlag == 1 &&  int.TryParse(a1, out int targetRow))
                        {
                            currentRow = targetRow - 1;
                            continue;
                        }
                        else if (zFlag == 0 &&  int.TryParse(a2, out int TtargetRow))
                        {
                            currentRow = TtargetRow - 1;
                            continue;
                        }
                    }
                    else if (command == "УПЛ")//Умовний перехід: якщо прапор 𝜔 дорівнює 0 або 2 — перехід на рядок A1, якщо прапор 𝜔 дорівнює 1 — перехід на рядок A2

                    {
                        if ((omegaFlag == 0 || omegaFlag == 2)  && int.TryParse(a1, out int targetRow))
                        {
                            currentRow = targetRow - 1;
                            continue;
                        }
                        else if (omegaFlag == 1 && int.TryParse(a2, out int TtargetRow))
                        {
                            currentRow = TtargetRow - 1;
                            continue;
                        }
                    }
                    
                    else if (command == "ИТР")//Зрушити A2 (задіяний зараз індекс ячійки масиву) в адресі A1 на A2 елементів
                    {
                        int address1 = int.Parse(a1);
                        int address2 = int.Parse(a2);

                        if (address1 > 0 && address1 <= dataGridView1.Rows.Count && address2 >= 0 && address2 <= dataGridView1.Rows.Count)
                        {
                            string arrayAddressValue = dataGridView1.Rows[address1 - 1].Cells[2].Value.ToString();
                            string elementsToShiftValue = dataGridView1.Rows[address2 - 1].Cells[3].Value.ToString();

                            if (int.TryParse(arrayAddressValue, out int arrayAddress) && int.TryParse(elementsToShiftValue, out int elementsToShift))
                            {
                                int newIndex = arrayAddress + elementsToShift;
                                dataGridView1.Rows[address1 - 1].Cells[2].Value = newIndex.ToString();
                            }          
                        }
                    }
                    else if (command == "ОСТ")
                    {
                        break;
                    }
                }
                currentRow++;
            }
        }
            
        private void button3_Click(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string baseFileName = "data.txt";

            string filePath = Path.Combine(desktopPath, baseFileName);
           
            if (File.Exists(filePath))
            {
                int counter = 1;
                string newFileName = Path.GetFileNameWithoutExtension(baseFileName) + "_" + counter.ToString() + Path.GetExtension(baseFileName);
                string newFilePath = Path.Combine(desktopPath, newFileName);

                while (File.Exists(newFilePath))
                {
                    counter++;
                    newFileName = Path.GetFileNameWithoutExtension(baseFileName) + "_" + counter.ToString() + Path.GetExtension(baseFileName);
                    newFilePath = Path.Combine(desktopPath, newFileName);
                }

                filePath = newFilePath;
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        string adr = row.Cells[0].Value.ToString();
                        string kom = row.Cells[1].Value.ToString();
                        string a1 = row.Cells[2].Value.ToString();
                        string a2 = row.Cells[3].Value.ToString();

                        if (kom == "ПЕР" && (a1 != "000" || a2 != "000"))
                        {
                            string line = $"{adr}: {kom} {a1} {a2}";
                            writer.WriteLine(line);
                        }
                        else if (kom != "ПЕР")
                        {
                            string line = $"{adr}: {kom} {a1} {a2}";
                            writer.WriteLine(line);
                        }
                    }
                }
            }

            MessageBox.Show($"Файл успешно сохранен на рабочем столе с именем: {Path.GetFileName(filePath)}", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Обработчик события для TextBox (ввода чисел)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Ваш код для обработки события
        }

        // Обработчик события для DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowCount = dataGridView1.Rows.Count;

            if (rowCount > 0 && dataGridView1.Rows[rowCount - 1].Cells[0].Value == null)
            {
                rowCount--;
            }

            for (int i = rowCount; i < 512; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.DefaultCellStyle.BackColor = SystemColors.Highlight;
                row.Cells[0].Value = (i + 1).ToString();
                row.Cells[1].Value = "ПЕР";
                row.Cells[2].Value = "000";
                row.Cells[3].Value = "000";
                dataGridView1.Rows.Add(row);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            label1.Text = ""; 
            textBox1.Text = "";

            for (int i = 0; i < 512; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.DefaultCellStyle.BackColor = SystemColors.Highlight;
                row.Cells[0].Value = (i + 1).ToString();
                row.Cells[1].Value = "ПЕР";
                row.Cells[2].Value = "000";
                row.Cells[3].Value = "000";
                dataGridView1.Rows.Add(row);
            }
        }

    }
}

