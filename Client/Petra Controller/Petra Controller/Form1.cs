using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petra_Controller
{
    public partial class PetraC : Form
    {
        TcpClient client = null;
        public NetworkStream stream = null;
        private bool launched = false;
        public PetraC()
        {
            InitializeComponent();
            IPTB.Text = "10.59.40.64";
            PortTF.Text = "7000";
        }

        // Permet de faire glisser la fenêtre //
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int WM_NCLBUTTONDBLCLK = 0x00A3; 

            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    break;
            }

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);


        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(IPTB.Text);
                Console.WriteLine(PortTF.Text);
                client = new TcpClient(IPTB.Text, Convert.ToInt32(PortTF.Text)/*int.Parse(PortTF.Text)*/);
                stream = client.GetStream();

                
                Byte[] b = new Byte[8];
                stream.Read(b, 0, b.Length);

                if (b[7] == 1) // Occupé ou non?
                {
                    if (b[0] == 1)
                    {
                        Convoyeur1CB.Checked = true;
                    }
                    else
                    {
                        Convoyeur1CB.Checked = false;
                    }
                    if (b[1] == 1)
                    {
                        Convoyeur2CB.Checked = true;
                    }
                    else
                    {
                        Convoyeur2CB.Checked = false;
                    }
                    if (b[2] == 1)
                    {
                        VentouseBrasCB.Checked = true;
                    }
                    else
                    {
                        VentouseBrasCB.Checked = false;
                    }
                    if (b[3] == 1)
                    {
                        VentouseCB.Checked = true;
                    }
                    else
                    {
                        VentouseCB.Checked = false;
                    }
                    if (b[4] == 1)
                    {
                        TapisBrasCB.Checked = true;
                    }
                    else
                    {
                        TapisBrasCB.Checked = false;
                    }
                    if (b[5] == 1)
                    {
                        GrappinCB.Checked = true;
                    }
                    else
                    {
                        GrappinCB.Checked = false;
                    }
                    RailTrackBar.Value = b[6];
                    launched = true;


                    var th = new Thread(GetCapteurs);
                    th.IsBackground = true;
                    th.Start();
                    Thread.Sleep(1000);

                    ControlPanel.Visible = true;
                }
                else
                {
                    stream.Close();
                    stream = null;
                    MessageBox.Show("Quelqu'un s'occupe déjà du Petra !", "Busy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(FormatException arg)
            {
                MessageBox.Show("Vérifiez si vos champs ne sont pas vides \nou que vos entrées soient correctes !", "FormatException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(ArgumentOutOfRangeException arg)
            {
                MessageBox.Show("Argument Out of range", "ArgumentOutOfRangeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(ArgumentException arg)
            {
                MessageBox.Show("Bad Argument", "ArgumentException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(SocketException arg)
            {
                MessageBox.Show("Erreur de réseau, vérifiez si\n- Les données entrées sont correctes\n- Si la machine est accessible via le réseau", "Erreur de réseau..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void GetCapteurs()
        {
            Byte[] b = new Byte[8];
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("Lecture:\0");
            stream.Write(data, 0, data.Length);
            do
            {
                try
                {
                    stream.Read(b, 0, b.Length);

                
                    if (b[0] == 1)
                    {

                        this.Invoke((MethodInvoker)delegate ()
                        {
                            L1CB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            L1CB.Checked = false;
                        });
                    }

                    if (b[1] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            L2CB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            L2CB.Checked = false;
                        });
                    }

                    if (b[2] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            TCB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            TCB.Checked = false;
                        });
                    }

                    if (b[3] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            SCB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            SCB.Checked = false;
                        });
                    }

                    if (b[4] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            CSCB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            CSCB.Checked = false;
                        });
                    }

                    if (b[5] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            APCB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            APCB.Checked = false;
                        });
                    }

                    if (b[6] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            PPCB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            PPCB.Checked = false;
                        });
                    }

                    if (b[7] == 1)
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            DECB.Checked = true;
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate ()
                        {
                            DECB.Checked = false;
                        });
                    }
                }
                catch (System.ObjectDisposedException) { }
                catch (System.IO.IOException) { }
                
            }
            while (true);
        }

        private void Convoyeur1CB_CheckedChanged(object sender, EventArgs e)
        {
            if(launched == true)
            if(Convoyeur1CB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:CONV1_ON\0");
                stream.Write(data, 0, data.Length);

            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:CONV1_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void Convoyeur2CB_CheckedChanged(object sender, EventArgs e)
        {
            if (launched == true)
            if (Convoyeur2CB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:CONV2_ON\0");
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:CONV2_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void VentouseBrasCB_CheckedChanged(object sender, EventArgs e)
        {
            if (launched == true)
            if (VentouseBrasCB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:B_VENT_ON\0");
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:B_VENT_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void VentouseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (launched == true)
            if (VentouseCB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:VENT_ON\0");
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:VENT_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void TapisBrasCB_CheckedChanged(object sender, EventArgs e)
        {
            if (launched == true)
            if (TapisBrasCB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:B_TAP_ON\0");
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:B_TAP_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void GrappinCB_CheckedChanged(object sender, EventArgs e)
        {
            if (launched == true)
            if (GrappinCB.Checked == true)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:PIN_B_TAP_ON\0");
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:PIN_B_TAP_OFF\0");
                stream.Write(data, 0, data.Length);
            }
        }

        private void PetraC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:LOGOUT\0");
                try
                {
                    stream.Write(data, 0, data.Length);
                }
                catch (System.IO.IOException) { }
                catch (System.NullReferenceException) { }
                Thread.Sleep(2);
                client.Close();
            }
        }

        private void RailTrackBar_Scroll(object sender, EventArgs e)
        {
            if(RailTrackBar.Value == 0)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_0\0");
                stream.Write(data, 0, data.Length);
            }
            else if(RailTrackBar.Value == 1)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_1\0");
                stream.Write(data, 0, data.Length);
            }
            else if(RailTrackBar.Value == 2)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_2\0");
                stream.Write(data, 0, data.Length);
            }
            else if(RailTrackBar.Value == 3)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_3\0");
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
