using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Calculadora
{
    [DesignerCategory("Code")]
    public partial class Form1 : Form
    {
        private TextBox txtPantalla;

        public Form1()
        {
            InitializeComponent();
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Calculadora";
            this.Size = new Size(320, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(30, 30, 30);

            txtPantalla = new TextBox
            {
                Location = new Point(15, 15),
                Size = new Size(275, 40),
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right,
                ReadOnly = true,
                BackColor = Color.Black,
                ForeColor = Color.LawnGreen
            };
            this.Controls.Add(txtPantalla);

            string[,] botones = {
                { "C", "CE", "<-", "/" },
                { "7", "8", "9", "x" },
                { "4", "5", "6", "-" },
                { "1", "2", "3", "+" },
                { "0", ".", "=", "" }
            };

            int startX = 15, startY = 70, width = 62, height = 50, gap = 8;

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 4; c++)
                {
                    string texto = botones[f, c];
                    if (string.IsNullOrEmpty(texto)) continue;

                    Button btn = new Button
                    {
                        Text = texto,
                        Font = new Font("Arial", 12, FontStyle.Bold),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    btn.FlatAppearance.BorderSize = 0;

                    if (texto == "=")
                    {
                        btn.Size = new Size(width * 2 + gap, height);
                        btn.Location = new Point(startX + c * (width + gap), startY + f * (height + gap));
                        btn.BackColor = Color.FromArgb(76, 175, 80);
                        btn.Click += btnIgual_Click;
                    }
                    else
                    {
                        btn.Size = new Size(width, height);
                        btn.Location = new Point(startX + c * (width + gap), startY + f * (height + gap));

                        if ("+-/x".Contains(texto))
                        {
                            btn.BackColor = Color.FromArgb(255, 149, 0);
                            btn.Click += btnAgregar_Click;
                        }
                        else if (texto == "C" || texto == "CE")
                        {
                            btn.BackColor = Color.FromArgb(165, 165, 165);
                            btn.ForeColor = Color.Black;
                            btn.Click += btnLimpiar_Click;
                        }
                        else if (texto == "<-")
                        {
                            btn.BackColor = Color.FromArgb(165, 165, 165);
                            btn.ForeColor = Color.Black;
                            btn.Click += btnRetroceso_Click;
                        }
                        else
                        {
                            btn.BackColor = Color.FromArgb(51, 51, 51);
                            btn.Click += btnAgregar_Click;
                        }
                    }
                    this.Controls.Add(btn);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (txtPantalla.Text == "Error" || txtPantalla.Text.StartsWith("No se puede")) txtPantalla.Text = "";
            txtPantalla.Text += b.Text;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPantalla.Text = "";
        }

        private void btnRetroceso_Click(object sender, EventArgs e)
        {
            if (txtPantalla.Text.Length > 0 && txtPantalla.Text != "Error" && !txtPantalla.Text.StartsWith("No se puede"))
            {
                txtPantalla.Text = txtPantalla.Text.Substring(0, txtPantalla.Text.Length - 1);
            }
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtPantalla.Text))
                {
                    string op = txtPantalla.Text.Replace("x", "*");
                    var res = new DataTable().Compute(op, null);
                    string resultadoTexto = res.ToString();

                    if (resultadoTexto.Contains("Infinity") || resultadoTexto.Contains("Infinito") || resultadoTexto == "NaN")
                    {
                        txtPantalla.Text = "No se puede dividir entre cero";
                    }
                    else
                    {
                        txtPantalla.Text = resultadoTexto;
                    }
                }
            }
            catch
            {
                txtPantalla.Text = "Error";
            }
        }
    }
}
