using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AruCikk
{
    public partial class Form1 : Form
    {
        BindingList<Item> _items = new BindingList<Item>();

        public Form1()
        {
            InitializeComponent();
            addButton.Enabled = false;
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = _items;
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var reader = new StreamReader((string)e.Argument,System.Text.Encoding.Default))
            {
                string line;
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    line = line.Replace("\"", "").Trim();
                    string[] elements = line.Split(';');
                    Item item = new Item();
                    item.ItemName = elements[0];
                    item.ItemNo = elements[1];
                    item.Barcode = elements[2];
                    item.Unit = elements[3];
                    Invoke(new Action(() =>
                    {
                        _items.Add(item);
                    }));
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            addButton.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "CSV fajlok|*.csv";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(save.OpenFile(), System.Text.Encoding.Default))
                    {
                        foreach (Item item in _items)
                        {
                            string output = item.ItemName + ";" + item.ItemNo + ";" + item.Barcode +
                                ";" + item.Unit;
                            writer.WriteLine(output);
                        }
                    }
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.edit = true;
            addItem.item = (Item)bindingSource1.Current;
            if (addItem.ShowDialog() == DialogResult.OK)
            {
                _items[bindingSource1.Position] = addItem.item;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddItem addItem = new AddItem();
            addItem.edit = false;
            if (addItem.ShowDialog() == DialogResult.OK)
            {
                Item tmpItem = addItem.item;
                //MessageBox.Show(tmpItem.Unit);
                _items.Add(tmpItem);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.CheckFileExists = true;
                open.Multiselect = true;
                open.Filter = "CSV fajlok|*.csv";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (var reader = new StreamReader(open.OpenFile()))
                    {
                        string line;
                        line = reader.ReadLine();
                        line = line.Trim('"');
                        line = line.Replace('"', ' ');
                        string[] columns = line.Split(';');
                        dataGridView1.Columns[0].HeaderText = columns[0];
                        dataGridView1.Columns[1].HeaderText = columns[1];
                        dataGridView1.Columns[2].HeaderText = columns[2];
                        dataGridView1.Columns[3].HeaderText = columns[3];
                        //MessageBox.Show(line);
                    }


                    backgroundWorker1.RunWorkerAsync(open.FileName);
                }
            }
        }
    }
}
