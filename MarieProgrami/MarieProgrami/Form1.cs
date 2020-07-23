using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarieProgrami
{
    public partial class Form1 : Form
    {
        Ram ram;              //class where data is stored.
        string[] rows;        //The array that holds the lines of code the user wrote.
        int Org;              //ORG variable
        int bellek;
        Ram tag;              //The class where user-defined tags are stored.
        public Form1()
        {
            InitializeComponent();
            setupPage();
        }

        public void setupPage()
        {
            //The section that prepares the environment for the loading of new code.
            MBR.Text = "0000";
            AC.Text = "0000";
            MAR.Text = "0000";
            IR.Text = "0000";
            PC.Text = "0000";
            OutReg.Text = "0000";
            listView1.Clear();
            listView2.Clear();

            //Defining global variables
            ram = new Ram();
            ram.bellek = new LinkedList<RamInfo>();
            tag = new Ram();
            tag.bellek = new LinkedList<RamInfo>();
            ram.commandList = new Dictionary<string, string>();
            ram.commandList.Add("JnS", "0");
            ram.commandList.Add("Load", "1");
            ram.commandList.Add("Store", "2");
            ram.commandList.Add("Add", "3");
            ram.commandList.Add("Subt", "4");
            ram.commandList.Add("Input", "5");
            ram.commandList.Add("Output", "6");
            ram.commandList.Add("Halt", "7");
            ram.commandList.Add("Skipcond", "8");
            ram.commandList.Add("Jump", "9");
            ram.commandList.Add("Clear", "A");
            ram.commandList.Add("AddI", "B");
            ram.commandList.Add("JumpI", "C");
            ram.commandList.Add("LoadI", "D");
            ram.commandList.Add("StoreI", "E");
            ram.commandList.Add("ORG", "X");
            ram.commandList.Add("END", "Y");
            ram.commandList.Add("Tag", "Z");

            //Editing Listview1(Memory Table)
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Adres", 100);
            listView1.Columns.Add("Info", 100);

            //Editing Listview2(Tags Table)
            listView2.View = View.Details;
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.Columns.Add("Isim", 100);
            listView2.Columns.Add("Adres", 100);

            //Assigning the Default Input Type
            comboBox1.SelectedItem = "Hexadecimal";
            button3.Enabled = false;
        }

        public void loadTables()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();

            //Writing the read commands into the memory 
            foreach (var row in ram.bellek)
            {
                string[] info = { row.adres, row.info };
                var satir = new ListViewItem(info);
                listView1.Items.Add(satir);
            }

            //Writing the read commands into the Tags Table
            foreach (var row in tag.bellek)
            {
                string[] info = { row.adres, row.info };
                var satir = new ListViewItem(info);
                listView2.Items.Add(satir);
            }
        }


        //Analysis of the code read
        public void READ(string searchField, string adres, string command)
        {
            for (int row = 0; row < rows.Count(); row++)
            {
                if (searchField == rows[row].Split(',')[0].ToString())
                {
                    foreach (var row1 in ram.bellek)
                    {
                        if (row1.adres == bellek.ToString("X4"))
                        {
                            row1.info = command + (Org + row - 1).ToString("X3");
                            break;
                        }
                    }
                    bellek = bellek + 1;
                }
            }
        }

        public void LOAD(string searchString)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == searchString)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    AC.Text = MBR.Text;
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void STORE(string searchString)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == searchString)
                {
                    MAR.Text = row.adres;
                    MBR.Text = AC.Text;
                    row.info = MBR.Text;
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void JnS(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MBR.Text = PC.Text;
                    MAR.Text = row.adres;
                    row.info = MBR.Text;
                    MBR.Text = row.adres;
                    AC.Text = 1.ToString("X4");
                    AC.Text = (Convert.ToInt32(AC.Text, 16) + Convert.ToInt32(MBR.Text, 16)).ToString("X4");
                    PC.Text = AC.Text;
                    run();
                    break;
                }
            }
        }

        public void Clear()
        {
            AC.Text = 0.ToString("X4");
            PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
            run();
        }

        public void Add(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    AC.Text = (Convert.ToInt32(AC.Text, 16) + Convert.ToInt32(MBR.Text, 16)).ToString("X4");
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void JumpI(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    PC.Text = MBR.Text;
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void Subt(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    AC.Text = (Convert.ToInt32(AC.Text, 16) - Convert.ToInt32(MBR.Text, 16)).ToString("X4");
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void Input()
        {

            try
            {
                if (comboBox1.SelectedItem.ToString() == "Hexadecimal")
                {
                    AC.Text = Microsoft.VisualBasic.Interaction.InputBox("Lütfen Hexadecimal bir değer girin:", "Input");
                }
                else if (comboBox1.SelectedItem.ToString() == "Decimal")
                {
                    AC.Text = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Lütfen 10'luk bir değer girin:", "Input")).ToString("X4");
                }
                else if (comboBox1.SelectedItem.ToString() == "Octal")
                {
                    AC.Text = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Lütfen 8'lik bir değer girin:", "Input"), 8).ToString("X4");
                }
                else if (comboBox1.SelectedItem.ToString() == "Binary")
                {
                    AC.Text = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Lütfen Binary bir değer girin:", "Input"), 2).ToString("X4");
                }
            }
            catch
            {
                MessageBox.Show("Girilen input belirtilen formatta değildir.");
            }

            PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
            run();
        }

        public void Output()
        {
            OutReg.Text = AC.Text;
            PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
            run();
        }

        public void Jump(string adress)
        {
            PC.Text = adress;
            PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
            run();
        }

        public void AddI(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    MAR.Text = MBR.Text;
                    foreach (var row1 in ram.bellek)
                    {
                        if (row1.adres == MAR.Text)
                        {
                            MBR.Text = row1.info;
                            break;
                        }
                    }
                    AC.Text = (Convert.ToInt32(AC.Text, 16) + Convert.ToInt32(MBR.Text, 16)).ToString("X4");
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void LoadI(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    MAR.Text = MBR.Text;
                    foreach (var row1 in ram.bellek)
                    {
                        if (row1.adres == MAR.Text)
                        {
                            MBR.Text = row1.info;
                            break;
                        }
                    }
                    AC.Text = MBR.Text;
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void StoreI(string adress)
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == adress)
                {
                    MAR.Text = row.adres;
                    MBR.Text = row.info;
                    MAR.Text = MBR.Text;
                    MBR.Text = AC.Text;
                    foreach (var row1 in ram.bellek)
                    {
                        if (row1.adres == MAR.Text)
                        {
                            row1.info = MBR.Text;
                            break;
                        }
                    }
                    PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                    run();
                    break;
                }
            }
        }

        public void Skipcond(string command)
        {
            int acIntValue = Convert.ToInt32(AC.Text, 16);
            if (acIntValue < 0 && command == "0000")
            {
                PC.Text = (Convert.ToInt32(PC.Text, 16) + 2).ToString("X4");
                run();
                return;
            }
            else if (acIntValue == 0 && command == "0400")
            {
                PC.Text = (Convert.ToInt32(PC.Text, 16) + 2).ToString("X4");
                run();
                return;
            }
            else if (acIntValue > 0 && command == "0800")
            {
                PC.Text = (Convert.ToInt32(PC.Text, 16) + 2).ToString("X4");
                run();
                return;
            }
            PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");            //pc=pc+1
            run();
        }

        public void ORG(int row)            //Creates a memory with 0 as much as the given parameter.
        {
            for (int i = 0; i < row; i++)
            {
                RamInfo ramInfo = new RamInfo();
                ramInfo.adres = (Org + i).ToString("X4");
                ramInfo.info = "0000";
                ram.bellek.AddLast(ramInfo);
            }
        }


        private void Temizle_Click(object sender, EventArgs e)
        {
            button2.Text = "Yükle";
            setupPage();
        }

        private void Yukle_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Yeniden Yükle")
            {
                setupPage();
            }

            if (richTextBox1.Text.Count() <= 0)
            {
                return;
            }
            rows = richTextBox1.Text.Split('\n');           //Splits the values ​​entered by the user.
            for (int row = 0; row < rows.Count(); row++)
            {
                var line = rows[row].Trim().Split(' ');

                if (ram.commandList.ContainsKey(line[0].ToString()))
                {
                    if (ram.commandList[line[0].ToString()] == "X")
                    {
                        Org = Convert.ToInt32(line[1], 16);
                        ORG(1000);
                        bellek = Org;
                    }
                    else if (ram.commandList[line[0].ToString()] == "A")
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "A000";
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (ram.commandList[line[0].ToString()] == "7")
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "7000";
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (ram.commandList[line[0].ToString()] == "8")
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "8" + line[1];
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (ram.commandList[line[0].ToString()] == "5")
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "5000";
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (ram.commandList[line[0].ToString()] == "6")
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "6000";
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (line[0] == "END")
                    {
                        break;
                    }
                    else
                    {
                        READ(line[1], (row - 1).ToString(), ram.commandList[line[0].ToString()]);
                    }
                }
                else   //The part that determines the variables entered by the user.
                {
                    if (line.Count() == 3)
                    {
                        RamInfo ramInfo = new RamInfo();
                        ramInfo.adres = line[0].Split(',')[0];
                        ramInfo.info = (Org + (Convert.ToInt32((row - 1).ToString()))).ToString("X4");
                        tag.bellek.AddLast(ramInfo);
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                if (line[1] == "DEC")
                                {
                                    row1.info = Convert.ToInt32(line[2]).ToString("X4");
                                    break;
                                }
                                else
                                {
                                    row1.info = Convert.ToInt32(line[2], 16).ToString("X4");
                                    break;
                                }
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else if (line.Count() == 1)
                    {
                        foreach (var row1 in ram.bellek)
                        {
                            if (row1.adres == bellek.ToString("X4"))
                            {
                                row1.info = "Z000";
                                break;
                            }
                        }
                        bellek = bellek + 1;
                    }
                    else
                    {
                        MessageBox.Show("Girilen veri tanımsız. Veri: \"" + rows[row] + "\"");
                    }
                }
            }
            loadTables();
            button2.Text = "Yeniden Yükle";
            button3.Enabled = true;
        }

        public void run()
        {
            foreach (var row in ram.bellek)
            {
                if (row.adres == PC.Text)
                {
                    var op = row.info[0];
                    if (op.ToString() == ram.commandList["Load"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');            //Adds zero with 4 digits on the left side.
                        LOAD(row.info.Substring(1).PadLeft(4, '0'));        //Bypass the opcode and read the IR command.
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Store"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        STORE(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["JnS"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        JnS(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Tag"])
                    {
                        PC.Text = (Convert.ToInt32(PC.Text, 16) + 1).ToString("X4");
                        run();
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Clear"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Clear();
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Add"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Add(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["JumpI"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        JumpI(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Subt"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Subt(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Input"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Input();
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Output"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Output();
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Halt"])
                    {
                        loadTables();
                        MessageBox.Show("İşleminiz tamamlanmıştır.", "İşlem Sonucu");
                        break;
                    }
                    else if (op.ToString() == ram.commandList["END"])
                    {
                        loadTables();
                        MessageBox.Show("İşleminiz tamamlanmıştır.", "İşlem Sonucu");
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Jump"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Jump(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["AddI"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        AddI(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["LoadI"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        LoadI(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["StoreI"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        StoreI(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                    else if (op.ToString() == ram.commandList["Skipcond"])
                    {
                        IR.Text = op.ToString().PadLeft(4, '0');
                        Skipcond(row.info.Substring(1).PadLeft(4, '0'));
                        break;
                    }
                }
            }
        }

        private void Calistir_Click(object sender, EventArgs e)
        {
            PC.Text = Org.ToString("X4");           //Assigns 4 - digit ORG value to PC as soon as run is pressed.
            if (ram.bellek.Count() >= 1)            //Runs if there is at least 1 command in memory.
            {
                run();
            }
            button3.Enabled = false;
        }

        private void label17_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/erdogantrpc");
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.SelectedItems.Clear();                //Clean the selected ones
            foreach (ListViewItem row in listView1.Items)
            {
                row.BackColor = Color.White;
            }
            ListView listViewItem = (ListView)sender;       //Define the clicked line. 
            foreach (ListViewItem row in listView1.Items)
            {
                if (row.SubItems[0].Text == listViewItem.FocusedItem.SubItems[1].Text)
                {
                    row.Selected = true;
                    row.Focused = true;
                    row.BackColor = Color.Aqua;
                    row.EnsureVisible();
                    listView1.Select();
                    listView1.SelectedItems.Clear();
                    label9.Text = Convert.ToInt32(row.SubItems[1].Text, 16).ToString();
                    label7.Text = listViewItem.FocusedItem.SubItems[0].Text;
                    label7.BackColor = Color.Aqua;
                    label9.BackColor = Color.Aqua;
                    break;
                }
            }
        }
    }
}
