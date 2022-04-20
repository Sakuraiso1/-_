using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Дима_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a;
            if(int.TryParse(textBox1.Text, out a) == true)
            {
                a = int.Parse(textBox1.Text);
                dataGridView1.RowCount = a;
                dataGridView1.ColumnCount = a;
                for (int i = 0; i < a; i++)
                {
                    for (int j = 0; j < a; j++)
                    {
                        if (i == j)
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                    }
                }

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Columns[i].HeaderText = i.ToString();
                    dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                }
            }
            else
            {
                MessageBox.Show("Введи число!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox2.Text , out n) == true)
            {
                n = int.Parse(textBox2.Text);
                dataGridView1.RowCount += n;
                dataGridView1.ColumnCount += n;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        if (i == j)
                        {
                            dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                    }
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Columns[i].HeaderText = i.ToString();
                    dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                }
            }
            else
            {
                MessageBox.Show("Введи число!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (i > j) { dataGridView1.Rows[i].Cells[j].Value = rand.Next(0, 20); }

                }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (j > i) { dataGridView1.Rows[i].Cells[j].Value = dataGridView1.Rows[j].Cells[i].Value; }
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            Random rnd = new Random();
            dataGridView2.RowCount = dataGridView1.Rows.Count;
            dataGridView2.ColumnCount = dataGridView1.Rows.Count;
            int[,] distance = new int[dataGridView1.Rows.Count, dataGridView1.Rows.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                for (int j = 0; j < dataGridView1.Rows.Count; ++j)
                    distance[i, j] = int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString());
            for (int k = 0; k < dataGridView1.Rows.Count; ++k)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    
                    for (int j = 0; j < dataGridView1.Rows.Count; ++j)
                    {
                        Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                        if (distance[i, k] + distance[k, j] < distance[i, j])
                            distance[i, j] = distance[i, k] + distance[k, j];
                        if(distance[i, j] != int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()))
                        {
                            dataGridView2.Rows[i].Cells[j].Style.BackColor = randomColor;
                            dataGridView2.Rows[j].Cells[i].Style.BackColor = dataGridView2.Rows[i].Cells[j].Style.BackColor;
                        }
                        if (i == j)
                        {
                            dataGridView2.Rows[i].Cells[j].ReadOnly = true;
                            dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Gray;
                            dataGridView2.Rows[i].Cells[j].Value = 0;
                        }
                    }
                }
            }
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; ++j) 
                {
                    dataGridView2.Rows[i].Cells[j].Value = distance[i, j];
                    dataGridView2.Rows[i].Cells[j].ReadOnly = true;
                    dataGridView2.Columns[i].HeaderText = i.ToString();
                    dataGridView2.Rows[i].HeaderCell.Value = i.ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            for (int i = 1; i < dataGridView1.Rows.Count + 1; i++)
            {
                for (int j = 1; j < dataGridView1.Rows.Count + 1; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    graph.AddEdge($"{i}", $"{j}");
                }
            }
            for (int i = 1; i < dataGridView1.Rows.Count + 1; i++)
            {
                graph.FindNode($"{i}").Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }
    }
}
