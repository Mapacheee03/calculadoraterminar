using System;
using Xamarin.Forms;

namespace Calculadora_OGV_
{
    public partial class MainPage : ContentPage
    {
        private double numeroUno = 0, numRespuesta = 0;
        private int operador = -1;
        private bool hayPunto = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void IgualarValores(string operando, int valor)
        {
            double resultadoLblNumber;
            if (double.TryParse(lblNumber.Text, out resultadoLblNumber))
            {
                numeroUno = resultadoLblNumber;
            }
            else
            {
                numeroUno = 0;
            }

            spnFirst.Text = numeroUno.ToString();
            lblNumber.Text = "0";
            spnSecond.Text = operando;
            operador = valor;
            hayPunto = false;
        }

        private void IngresarNumero(string numero)
        {
            if (numero == ".")
            {
                if (!hayPunto)
                {
                    lblNumber.Text += numero;
                    hayPunto = true;
                }
            }
            else
            {
                if (lblNumber.Text == "0" || lblNumber.Text == "0.")
                {
                    lblNumber.Text = numero;
                }
                else
                {
                    lblNumber.Text += numero;
                }
            }
        }

        private void BtnAC(object sender, EventArgs e)
        {
            numeroUno = 0; numRespuesta = 0; hayPunto = false;
            spnFirst.Text = ""; spnSecond.Text = ""; spnThird.Text = ""; lblNumber.Text = "0";
        }

        private void BtnOperador(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string operando = button.Text;
                int valor = -1;

                switch (operando)
                {
                    case "+":
                        valor = 0;
                        break;
                    case "-":
                        valor = 1;
                        break;
                    case "*":
                        valor = 2;
                        break;
                    case "÷":
                        valor = 3;
                        break;
                }

                if (operador != -1)
                {
                    BtnEquals(sender, e); // Realizar una operación pendiente antes de cambiar el operador.
                }

                IgualarValores(operando, valor);
            }
        }

        private void BtnEquals(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(spnFirst.Text) && !string.IsNullOrEmpty(spnSecond.Text))
            {
                spnThird.Text = lblNumber.Text;

                double numeroDos = 0;
                if (double.TryParse(spnThird.Text, out double resultadoSpnThird))
                {
                    numeroDos = resultadoSpnThird;
                }

                switch (operador)
                {
                    case 0:
                        numRespuesta = numeroUno + numeroDos;
                        break;
                    case 1:
                        numRespuesta = numeroUno - numeroDos;
                        break;
                    case 2:
                        numRespuesta = numeroUno * numeroDos;
                        break;
                    case 3:
                        if (numeroUno == 0) { numeroDos = 1; }
                        numRespuesta = numeroUno / numeroDos;
                        break;
                }

                lblNumber.Text = numRespuesta.ToString();
                numeroUno = numRespuesta;
                operador = -1;
                hayPunto = false;
            }
        }

        private void ClickPoint(object sender, EventArgs e)
        {
            IngresarNumero(".");
        }

        private void ClickNumber(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string numero = button.Text;
                IngresarNumero(numero);
            }
        }

        private void Click_C(object sender, EventArgs e)
        {
            string currentText = lblNumber.Text;
            if (!string.IsNullOrEmpty(currentText))
            {
                currentText = currentText.Remove(currentText.Length - 1);
                lblNumber.Text = currentText;
            }
        }
    }
}
