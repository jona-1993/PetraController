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
            this.Invoke((MethodInvoker)delegate ()
            {
                if (RailTrackBar.Value == 0)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_0\0");
                    stream.Write(data, 0, data.Length);
                }
                else if (RailTrackBar.Value == 1)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_1\0");
                    stream.Write(data, 0, data.Length);
                }
                else if (RailTrackBar.Value == 2)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_2\0");
                    stream.Write(data, 0, data.Length);
                }
                else if (RailTrackBar.Value == 3)
                {
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes("Ecriture:RAIL_POS_3\0");
                    stream.Write(data, 0, data.Length);
                }
            });
        }

        private int SensorState(String arg)
        {
           

            if (arg.Contains("L1"))
            {

                if (L1CB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if(arg.Contains("L2"))
            {
                if (L2CB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("T"))
            {
                if (TCB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("CS"))
            {
                if (CSCB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("S"))
            {
                if (SCB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("AP"))
            {
                if (APCB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("PP"))
            {
                if (PPCB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            else if (arg.Contains("DE"))
            {
                if (DECB.Checked == true)
                    return 1;
                else
                    return 0;
            }
            
            return -1;
        }

        private int Alternative(String arg)
        {
            int cond = -1;
            cond = SensorState(arg); // Etat du capteur
            if (cond != -1)
            {
                if (arg.Contains("=="))
                {
                    if(cond == 1)
                    {
                        if (arg.ToLower().Contains("true"))
                            return 1;
                        else
                            return 0;
                    }
                    else
                    {
                        if (arg.ToLower().Contains("false"))
                            return 1;
                        else
                            return 0;
                    }
                }
                else if(arg.Contains("!="))
                {
                    if (cond == 0)
                    {
                        if (arg.ToLower().Contains("true"))
                            return 1;
                        else
                            return 0;
                    }
                    else
                    {
                        if (arg.ToLower().Contains("false"))
                            return 1;
                        else
                            return 0;
                    }
                }
            }
            else
                return cond;

            return -1;
        }

        private void Test(int test)
        {
            switch (test)
            {
                case 1:
                    Translation("while CS == false: start convoyeur bas && while CS == true: start convoyeur bas && stop convoyeur bas && sleep 1000 && pincer start && changer start && sleep 1000 && start Convoyeur haut && pincer stop && Sleep 500 && changer stop && while L1 == false: start convoyeur haut && stop convoyeur haut");
                    break;
                case 2: Translation("prendre start && sleep 5000 && accrocher start && prendre stop && déplacer 1 && sleep 5000 && déplacer 2 && sleep 5000 && déplacer 3 && sleep 5000 && déplacer 0 && sleep 5000 && accrocher stop");
                    break;
            }
        }
        private void Translation(String cmd)
        {
            
            List<String> cmds = new List<string>();
            int addrprev = 0;
            
            if (cmd.Contains("&&"))
            {
                for (int j = 0; j < cmd.Length; j += 1)
                {
                    try
                    {
                        if ((cmd.ElementAt(j - 1) + cmd.ElementAt(j).ToString()).Equals("&&"))
                        {
                            if (cmds.Count == 0)
                            {
                                cmds.Add(cmd.Substring(addrprev, j + 1));
                            }
                            else
                            {
                                cmds.Add(cmd.Substring(addrprev, j + 1 - addrprev));
                            }
                            addrprev = j + 1;
                        }
                    }
                    catch (ArgumentOutOfRangeException arg) { }
                }
            }
            cmds.Add(cmd.Substring(addrprev, cmd.Length - addrprev));

            
            for (int i = 0; i < cmds.Count; i+=1)
            {
                if (cmds.ElementAt(i).Trim().ToLower().Contains("test"))
                {
                    if (cmds.ElementAt(i).Trim().ToLower().Contains("1"))
                    {
                        Test(1);
                    }
                    else if (cmds.ElementAt(i).Trim().ToLower().Contains("2"))
                    {
                        Test(2);
                    }
                }
                if (cmds.ElementAt(i).Trim().ToLower().Contains("while"))
                {
                    int alt = 0;
                     // Tant que la condition n'est pas satisfaite
                    do
                    {
                        alt = Alternative(cmds.ElementAt(i));
                        if (alt == -1) // Error
                            goto nok;
                        if ((cmds.ElementAt(i).ToLower()).Contains("if"))
                        {
                            if (Alternative(cmds.ElementAt(i)) == 1)
                                goto ok;
                            else
                                goto nok;
                        }
                        ok:;
                        if ((cmds.ElementAt(i).ToLower()).Contains("convoyeur"))
                        {

                            if (cmds.ElementAt(i).ToLower().Contains("bas"))
                            {
                                if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                                {
                                    if (!Convoyeur1CB.Checked)
                                    {
                                        this.Invoke((MethodInvoker)delegate ()
                                        {
                                            Convoyeur1CB.Checked = true;
                                        });
                                        
                                        Convoyeur1CB_CheckedChanged(null, null);
                                    }
                                }
                                else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                                {
                                    if (Convoyeur1CB.Checked)
                                    {
                                        this.Invoke((MethodInvoker)delegate ()
                                        {
                                            Convoyeur1CB.Checked = false;
                                        });
                                        Convoyeur1CB_CheckedChanged(null, null);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR");
                                }
                            }
                            else if (cmds.ElementAt(i).ToLower().Contains("haut"))
                            {
                                if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                                {
                                    if (!Convoyeur2CB.Checked)
                                    {
                                        this.Invoke((MethodInvoker)delegate ()
                                        {
                                            Convoyeur2CB.Checked = true;
                                        });
                                        Convoyeur2CB_CheckedChanged(null, null);
                                    }
                                }
                                else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                                {
                                    if (Convoyeur2CB.Checked)
                                    {
                                        this.Invoke((MethodInvoker)delegate ()
                                        {
                                            Convoyeur2CB.Checked = false;
                                        });
                                        Convoyeur2CB_CheckedChanged(null, null);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("ERROR");
                                }
                            }
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("prendre"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                if (!VentouseBrasCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        VentouseBrasCB.Checked = true;
                                    });

                                    VentouseBrasCB_CheckedChanged(null, null);
                                }
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                if (VentouseBrasCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        VentouseBrasCB.Checked = false;
                                    });
                                    VentouseBrasCB_CheckedChanged(null, null);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("accrocher"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                if (!VentouseCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        VentouseCB.Checked = true;
                                    });
                                    VentouseCB_CheckedChanged(null, null);
                                }
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                if (VentouseCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        VentouseCB.Checked = false;
                                    });
                                    VentouseCB_CheckedChanged(null, null);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("changer"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                if (!TapisBrasCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        TapisBrasCB.Checked = true;
                                    });
                                    TapisBrasCB_CheckedChanged(null, null);
                                }
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                if (TapisBrasCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        TapisBrasCB.Checked = false;
                                    });
                                    TapisBrasCB_CheckedChanged(null, null);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("pincer"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                if (!GrappinCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        GrappinCB.Checked = true;
                                    });
                                    GrappinCB_CheckedChanged(null, null);
                                }
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                if (GrappinCB.Checked)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        GrappinCB.Checked = false;
                                    });
                                    GrappinCB_CheckedChanged(null, null);
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("déplacer"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("0"))
                            {
                                if (RailTrackBar.Value != 0)
                                {
                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        RailTrackBar.Value = 0;
                                    });
                                    RailTrackBar_Scroll(null, null);
                                }
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("1"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    if (RailTrackBar.Value != 1)
                                    {
                                        RailTrackBar.Value = 1;
                                        RailTrackBar_Scroll(null, null);
                                    }
                                });
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("2"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    if (RailTrackBar.Value != 2)
                                    {
                                        RailTrackBar.Value = 2;
                                        RailTrackBar_Scroll(null, null);
                                    }
                                });
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("3"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    if (RailTrackBar.Value != 3)
                                    {
                                        RailTrackBar.Value = 3;
                                        RailTrackBar_Scroll(null, null);
                                    }
                                });
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }

                        }
                        nok:;
                        
                        if ((cmds.ElementAt(i).ToLower()).Contains("sleep"))
                        {
                            int debut = -1, fin = -1;
                            for(int j = 0; j < cmds.ElementAt(i).Length; j += 1)
                            {
                                try
                                {
                                    int.Parse(cmds.ElementAt(i).ElementAt(j).ToString());
                                    
                                    if(debut == -1)
                                    {
                                        debut = j;
                                    }
                                    else
                                    {
                                        fin = j;
                                    }
                                }
                                catch(FormatException arg) { }
                            }
                            if (debut != -1 || fin != -1)
                            {
                                int time = int.Parse(cmds.ElementAt(i).Substring(debut, fin - debut));
                                Thread.Sleep(time);
                            }
                        }
                    }
                    while (alt == 1) ;
                }
                else
                {
                    // Pas de While

                    if ((cmds.ElementAt(i).ToLower()).Contains("if"))
                    {
                        if (Alternative(cmds.ElementAt(i)) == 1)
                            goto ok;
                        else
                            goto nok;
                    }
                    ok:;
                    if ((cmds.ElementAt(i).ToLower()).Contains("convoyeur"))
                    {

                        if (cmds.ElementAt(i).ToLower().Contains("bas"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    Convoyeur1CB.Checked = true;
                                });
                                
                                Convoyeur1CB_CheckedChanged(null, null);
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    Convoyeur1CB.Checked = false;
                                });
                                Convoyeur1CB_CheckedChanged(null, null);
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        else if (cmds.ElementAt(i).ToLower().Contains("haut"))
                        {
                            if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    Convoyeur2CB.Checked = true;
                                });
                                Convoyeur2CB_CheckedChanged(null, null);
                            }
                            else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                            {
                                this.Invoke((MethodInvoker)delegate ()
                                {
                                    Convoyeur2CB.Checked = false;
                                });
                                Convoyeur2CB_CheckedChanged(null, null);
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                    }
                    else if ((cmds.ElementAt(i).ToLower()).Contains("prendre"))
                    {
                        if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                VentouseBrasCB.Checked = true;
                            });
                            VentouseBrasCB_CheckedChanged(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                VentouseBrasCB.Checked = false;
                            });
                            VentouseBrasCB_CheckedChanged(null, null);
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }
                    }
                    else if ((cmds.ElementAt(i).ToLower()).Contains("accrocher"))
                    {
                        if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                VentouseCB.Checked = true;
                            });
                            VentouseCB_CheckedChanged(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                VentouseCB.Checked = false;
                            });
                            VentouseCB_CheckedChanged(null, null);
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }
                    }
                    else if ((cmds.ElementAt(i).ToLower()).Contains("changer"))
                    {
                        if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                TapisBrasCB.Checked = true;
                            });
                            TapisBrasCB_CheckedChanged(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                TapisBrasCB.Checked = false;
                            });
                            TapisBrasCB_CheckedChanged(null, null);
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }
                    }
                    else if ((cmds.ElementAt(i).ToLower()).Contains("pincer"))
                    {
                        if ((cmds.ElementAt(i).ToLower()).Contains("start"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                GrappinCB.Checked = true;
                            });
                            GrappinCB_CheckedChanged(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("stop"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                GrappinCB.Checked = false;
                            });
                            GrappinCB_CheckedChanged(null, null);
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }
                    }
                    else if ((cmds.ElementAt(i).ToLower()).Contains("déplacer"))
                    {
                        if ((cmds.ElementAt(i).ToLower()).Contains("0"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                RailTrackBar.Value = 0;
                            });
                            RailTrackBar_Scroll(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("1"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                RailTrackBar.Value = 1;
                            });
                            RailTrackBar_Scroll(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("2"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                RailTrackBar.Value = 2;
                            });
                            RailTrackBar_Scroll(null, null);
                        }
                        else if ((cmds.ElementAt(i).ToLower()).Contains("3"))
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                RailTrackBar.Value = 3;
                            });
                            RailTrackBar_Scroll(null, null);
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }

                    }
                    nok:;

                    if ((cmds.ElementAt(i).ToLower()).Contains("sleep"))
                    {
                        int debut = -1, fin = -1;
                        for (int j = 0; j < cmds.ElementAt(i).Length; j += 1)
                        {
                            try
                            {
                                int.Parse(cmds.ElementAt(i).ElementAt(j).ToString());

                                if (debut == -1)
                                {
                                    debut = j;
                                }
                                else
                                {
                                    fin = j;
                                }
                            }
                            catch (FormatException arg) { }
                        }
                        if (debut != -1 || fin != -1)
                        {
                            int time = int.Parse(cmds.ElementAt(i).Substring(debut, (fin - debut) + 1));
                            Thread.Sleep(time);
                        }
                    }
                }
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                ShellTB.Text = null;
            });
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var th = new Thread(TranslationStart);
            
            th.IsBackground = true;
            th.Start();
            Thread.Sleep(700);
            //Translation(ShellTB.Text);
        }

        private void TranslationStart()
        {
            Translation(ShellTB.Text);
        }

        private void L1CB_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void ShellTB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                SendButton_Click(null, null);
        }

        private void IPTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectButton_Click(null, null);
            }
        }

        private void PortTF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectButton_Click(null, null);
            }
        }

        private void PetraC_Load(object sender, EventArgs e)
        {

        }
    }
}
