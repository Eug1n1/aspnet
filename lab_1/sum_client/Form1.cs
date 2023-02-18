using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sum_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "X", this.textBox_x.Text },
                { "Y", this.textBox_y.Text  }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://localhost:44366/sum", content);

            this.textBox_x.Text = await response.Content.ReadAsStringAsync();
            this.textBox_y.Clear();
        }
    }
}
