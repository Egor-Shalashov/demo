using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo.forms
{
    public partial class FormMainManager : Form
    {
        int ID = 0;
        public FormMainManager(int global_id)
        {
            InitializeComponent();
            ID = global_id;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
