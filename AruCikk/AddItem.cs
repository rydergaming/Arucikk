using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AruCikk
{
    public partial class AddItem : Form
    {
        public Item item;
        public bool edit = false;
        public Item currentItem;

        public AddItem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            item = new Item();
            item.ItemName = textBox1.Text;
            item.ItemNo = textBox2.Text;
            item.Barcode = textBox3.Text;
            item.Unit = comboBox1.SelectedItem.ToString();            

        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            
            if (edit)
            {
                textBox1.Text = item.ItemName;
                textBox2.Text = item.ItemNo;
                textBox3.Text = item.Barcode;
                comboBox1.ValueMember = item.Unit;
                
            }
            else
                comboBox1.SelectedIndex = 0;


        }
    }
}
