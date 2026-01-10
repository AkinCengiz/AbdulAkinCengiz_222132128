using AbdulAkinCengiz_222132128.Business.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbdulAkinCengiz_222132128.WinFormUI
{
    public partial class MainForm : Form
    {
        private readonly IReservationService _reservationService;
        public MainForm(IReservationService reservationService)
        {
            _reservationService = reservationService;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        
    }
}
