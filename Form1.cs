using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;

using System.Threading;
using WMPLib;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        OpenFileDialog dialog1;
        SaveFileDialog dialog_save;

        /////////////////////////

        string name_playlist;

        bool play = false;
        bool pause = false;
        bool stop = true;

        string text_for_text_box_1;

        int number_of_song;

        Playlists my_playlist = new Playlists();

        List<string> files_to_play = new List<string>();

        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        System.Media.SoundPlayer snd = new System.Media.SoundPlayer();



        /////////////////////////



        private void remove_list()
        {
            listBox1.Items.Clear();

            int i = 1;
            foreach (string name in files_to_play)
            {
                listBox1.Items.Add(i + ". " + name_to_perfect_name(name));

                ++i;
            }

        }

        private void open_playlist(string name)
        {
            wplayer.controls.stop();


            StreamReader read = new StreamReader(name);

            files_to_play.Clear();

            while (read.Peek() >= 0)
            {
                files_to_play.Add(read.ReadLine());
            }
            read.Close();

            remove_list();

            name_playlist = name;

            if (files_to_play.Count != 0)
            {
                listBox1.SelectedIndex = 0;
                number_of_song = 0;
            }

            my_playlist.open_playlist(name_to_perfect_name(name));

            my_playlist.save();
        }

        private void remove_mp3()
        {
            button1.BackgroundImage = Image.FromFile(@"pictures\1play_play.png", false);
            button1.BackgroundImageLayout = ImageLayout.Stretch;

            button3.BackgroundImage = Image.FromFile(@"pictures\1pause.png", false);
            button3.BackgroundImageLayout = ImageLayout.Stretch;

            button2.BackgroundImage = Image.FromFile(@"pictures\1stop.png", false);
            button2.BackgroundImageLayout = ImageLayout.Stretch;

            play = true;
            pause = false;
            stop = false;

            wplayer.controls.play();

        }

        private void play_mp3(string name)
        {

            progressBar1.Value = 0;

            button1.BackgroundImage = Image.FromFile(@"pictures\1play_play.png", false);
            button1.BackgroundImageLayout = ImageLayout.Stretch;

            button3.BackgroundImage = Image.FromFile(@"pictures\1pause.png", false);
            button3.BackgroundImageLayout = ImageLayout.Stretch;

            button2.BackgroundImage = Image.FromFile(@"pictures\1stop.png", false);
            button2.BackgroundImageLayout = ImageLayout.Stretch;



            play = true;
            pause = false;
            stop = false;

            wplayer.URL = name;

            wplayer.controls.play();


            textBox1.Text = "";

            textBox1.Text = name_to_perfect_name(files_to_play[number_of_song]);

            for (int i = 0; i < 120 - textBox1.Text.Length; ++i)
            {
                textBox1.Text += " ";
            }

            
                
            

        }

        private void pause_mp3()
        {
            play = false;
            pause = true;
            stop = false;

            button1.BackgroundImage = Image.FromFile(@"pictures\1play.png", false);
            button1.BackgroundImageLayout = ImageLayout.Stretch;

            button3.BackgroundImage = Image.FromFile(@"pictures\1pause_pause.png", false);
            button3.BackgroundImageLayout = ImageLayout.Stretch;

            button2.BackgroundImage = Image.FromFile(@"pictures\1stop.png", false);
            button2.BackgroundImageLayout = ImageLayout.Stretch;

            wplayer.controls.pause();
        }

        private void stop_mp3()
        {
            progressBar1.Value = 0;

            play = false;
            pause = false;
            stop = true;

            button1.BackgroundImage = Image.FromFile(@"pictures\1play.png", false);
            button1.BackgroundImageLayout = ImageLayout.Stretch;

            button3.BackgroundImage = Image.FromFile(@"pictures\1pause.png", false);
            button3.BackgroundImageLayout = ImageLayout.Stretch;

            button2.BackgroundImage = Image.FromFile(@"pictures\1stop_stop.png", false);
            button2.BackgroundImageLayout = ImageLayout.Stretch;

            wplayer.controls.stop();
        }

        private void play_wav(string name)
        {
            snd.SoundLocation = name;
            snd.Play();
        }

        private string name_directory_to_perfect_name(string name)
        {
            string result = "";

            int i = name.Length - 1;

            while (name[i] != '\\')
            {
                result = name[i] + result;
                --i;
            }


            return result;
        }

        private void open_directory(string name_directory)
        {
            foreach (string File in Directory.GetFiles(name_directory))
            {
                files_to_play.Add(File);
            }

            foreach (string Path in Directory.GetDirectories(name_directory))
            {
                open_directory(Path);
            }
        }

        private string name_to_perfect_name(string name)
        {
            string result = "";
            int i = name.Length - 1;



            while (name[i] != '.')
            {
                --i;
            }
            --i;
            while (name[i] != '\\')
            {
                result = name[i] + result;
                --i;
            }

            return result;
        }

        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (pause == false)
                {
                    if (play == false)
                    {
                        number_of_song = listBox1.SelectedIndex;
                        play_mp3(files_to_play[listBox1.SelectedIndex]);
                    }
                    else
                    {
                        if (files_to_play[listBox1.SelectedIndex] != wplayer.URL)
                        {
                            number_of_song = listBox1.SelectedIndex;
                            play_mp3(files_to_play[listBox1.SelectedIndex]);
                        }
                    }
                }
                else
                {
                    remove_mp3();
                }

            }
            catch
            {

            }
        }

        ////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = 1000;
            

            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
            trackBar1.Value = 20;
            




            try
            {
                button1.BackgroundImage = Image.FromFile(@"pictures\1play.png", false);
                button1.BackgroundImageLayout = ImageLayout.Stretch;

                button3.BackgroundImage = Image.FromFile(@"pictures\1pause.png", false);
                button3.BackgroundImageLayout = ImageLayout.Stretch;

                button2.BackgroundImage = Image.FromFile(@"pictures\1stop.png", false);
                button2.BackgroundImageLayout = ImageLayout.Stretch;

                button4.BackgroundImage = Image.FromFile(@"pictures\1delete.png", false);
                button4.BackgroundImageLayout = ImageLayout.Stretch;

                button5.BackgroundImage = Image.FromFile(@"pictures\1end.png", false);
                button5.BackgroundImageLayout = ImageLayout.Stretch;

                button6.BackgroundImage = Image.FromFile(@"pictures\1for.png", false);
                button6.BackgroundImageLayout = ImageLayout.Stretch;

                button7.BackgroundImage = Image.FromFile(@"pictures\1plus_song.png", false);
                button7.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
            }

            try
            {
                List<string> names_list = my_playlist.get_list_of_playlist();
                try
                {
                    button8.Text = names_list[0];
                }
                catch
                {

                }
                try
                {
                    button9.Text = names_list[1];
                }
                catch
                {

                }
                try
                {
                    button10.Text = names_list[2];
                }
                catch
                {

                }
            }
            catch
            {

            }

            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;

            timer1.Interval = 500; //интервал между срабатываниями 1000 миллисекунд
            timer1.Start();

            timer2.Interval = 50; //интервал между срабатываниями 1000 миллисекунд
            timer2.Start();

            Font font = new Font("Times New Roman", 12.0f,
                        FontStyle.Bold | FontStyle.Regular | FontStyle.Regular);
            textBox1.Font = font;
            textBox1.ForeColor = System.Drawing.Color.Red;

            listBox1.ItemHeight = 17;
            

            wplayer.settings.volume = 20;

            try
            {
                open_playlist("playlists\\" + button8.Text + ".pll");
                my_playlist.open_playlist(button8.Text);
            }
            catch
            {
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                open_directory(dialog.SelectedPath);

                foreach (string name in files_to_play)
                {
                    listBox1.Items.Add(name_to_perfect_name(name));
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pause_mp3();
            //textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                files_to_play.RemoveAt(listBox1.SelectedIndex);


                int index = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(index);
                listBox1.SelectedIndex = index - 1;
            }
            catch
            {

            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            number_of_song = listBox1.SelectedIndex;
            wplayer.URL = files_to_play[listBox1.SelectedIndex];
            play_mp3(files_to_play[listBox1.SelectedIndex]);
        }

        private void сохранитьКакToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save playlist";
            dialog.Filter = "playlist (*.pll)|*.pll";
            dialog.InitialDirectory = Application.StartupPath.ToString() + "\\playlists";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(dialog.FileName);

                foreach (string name in files_to_play)
                {
                    write.WriteLine(name);
                }

                write.Close();

            }

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open playlist";
            dialog.Filter = "playlist (*.pll)|*.pll";
            dialog.InitialDirectory = Application.StartupPath.ToString() + "\\playlists";

            //dialog.FilterIndex = 2;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                open_playlist(dialog.FileName);
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != 0)
            {
                if (number_of_song - 1 >= 0)
                {
                    listBox1.SelectedIndex = number_of_song - 1;
                    --number_of_song;
                    if (play)
                    {
                        play_mp3(files_to_play[number_of_song]);
                    }
                    else
                    {
                        textBox1.Text = name_to_perfect_name(files_to_play[number_of_song]);
                    }
                }
                pause = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != files_to_play.Count)
            {
                if (number_of_song + 1 <= files_to_play.Count - 1)
                {
                    listBox1.SelectedIndex = number_of_song + 1;
                    ++number_of_song;
                    if (play)
                    {
                        play_mp3(files_to_play[number_of_song]);
                    }
                    else
                    {
                        textBox1.Text = name_to_perfect_name(files_to_play[number_of_song]);
                    }
                }
                pause = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop_mp3();
            textBox1.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                open_directory(dialog.SelectedPath);

                int i;
                if (listBox1.Items.Count != 0)
                {
                    i = listBox1.Items.Count;
                }
                else
                {
                    i = 1;
                }
                foreach (string name in files_to_play)
                {
                    listBox1.Items.Add(i + ". " + name_to_perfect_name(name));

                    ++i;
                }
            }

            StreamWriter write = new StreamWriter(name_playlist);

            foreach (string name in files_to_play)
            {
                write.WriteLine(name);
            }

            write.Close();

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Save playlist";
            dialog.Filter = "playlist (*.pll)|*.pll";
            dialog.InitialDirectory = Application.StartupPath.ToString() + "\\playlists";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(dialog.FileName);

                write.Close();


                name_playlist = dialog.FileName;

                my_playlist.add_playlist(name_to_perfect_name(name_playlist));

                my_playlist.save();

            }


        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter(name_playlist);

            foreach (string name in files_to_play)
            {
                write.WriteLine(name);
            }

            write.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                open_playlist("playlists\\" + button8.Text + ".pll");
                my_playlist.open_playlist(button8.Text);
            }
            catch
            {
                listBox1.Items.Clear();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                open_playlist("playlists\\" + button9.Text + ".pll");
                my_playlist.open_playlist(button9.Text);
            }
            catch
            {
                listBox1.Items.Clear();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                open_playlist("playlists\\" + button10.Text + ".pll");
                my_playlist.open_playlist(button10.Text);
            }
            catch
            {
                listBox1.Items.Clear();
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Font myFont;
            Brush myBrush;
            int i = e.Index;


            myFont = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular);
            myBrush = Brushes.DarkGreen;


            e.Graphics.DrawString(listBox1.Items[i].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (play == true)
            {

                textBox1.Text = textBox1.Text.Substring(1) + textBox1.Text[0];
                

            }
            
        }

        

        private void progressBar1_Click(object sender, EventArgs e)
        {
            //progressBar1.Value=progressBar1.Controls

            


            //wplayer.controls.currentPosition = (int)(progressBar1.Value/progressBar1.Maximum)*wplayer.controls.currentItem.duration;
        }

        private void progressBar1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (play)
            {
                double percent = 0;
                percent = ((double)((double)wplayer.controls.currentPosition + 0.01) / (double)((double)wplayer.controls.currentItem.duration + 0.0));
                if (wplayer.controls.currentPosition != 0)
                {
                    progressBar1.Value = (int)(percent * (double)(progressBar1.Maximum + 0.0));
                }
                else
                {
                    progressBar1.Value = 0;
                }

                if (progressBar1.Value >= progressBar1.Maximum-1)
                {
                    if (listBox1.SelectedIndex != files_to_play.Count)
                    {
                        if (number_of_song + 1 <= files_to_play.Count - 1)
                        {
                            listBox1.SelectedIndex = number_of_song + 1;
                            ++number_of_song;
                            if (play)
                            {
                                play_mp3(files_to_play[number_of_song]);
                            }
                            else
                            {
                                textBox1.Text = name_to_perfect_name(files_to_play[number_of_song]);
                            }
                        }
                        pause = false;
                    }
                }
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void progressBar1_BindingContextChanged(object sender, EventArgs e)
        {
            
        }

        private void progressBar1_BackColorChanged(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            wplayer.settings.volume = trackBar1.Value;
        }

       

        private void progressBar1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void progressBar1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void progressBar1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void progressBar1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (play)
                {
                    decimal pos = 0M;
                    pos = ((decimal)e.X / (decimal)progressBar1.Width) * 1000;
                    pos = Convert.ToInt32(pos);

                    if (pos >= progressBar1.Minimum && pos <= progressBar1.Maximum)
                    {
                        progressBar1.Value = (int)pos;
                        
                        //progressBar1.Value / progressBar1.Maximum
                        wplayer.controls.currentPosition = ((double)((double)(progressBar1.Value + 0.01) / (double)(progressBar1.Maximum + 0.01))) * wplayer.controls.currentItem.duration;
                       

                    }
                }
                if (pause)
                {
                    decimal pos = 0M;
                    pos = ((decimal)e.X / (decimal)progressBar1.Width) * 1000;
                    pos = Convert.ToInt32(pos);

                    if (pos >= progressBar1.Minimum && pos <= progressBar1.Maximum)
                    {
                        progressBar1.Value = (int)pos;
                        
                        wplayer.controls.currentPosition = ((double)((double)(progressBar1.Value + 0.01) / (double)(progressBar1.Maximum + 0.01))) * wplayer.controls.currentItem.duration;
                      
                    }
                }
            }
        }
    }



    ///////////////////////////////////////////////////////////////////



    public class Path_in_directory
    {
        private string name;
        private List<string> name_of_song = new List<string>();

        public Path_in_directory(string name)
        {
            this.name = name;
        }

        public void add_song(string name)
        {
            name_of_song.Add(name);
        }

        public string get_name()
        {
            return name;
        }

        public List<string> get_name_of_song()
        {
            return name_of_song;
        }
    }

    public class Playl
    {
        private string name;
        private int count_open;

        public Playl(string name, int count_open)
        {
            this.name = name;
            this.count_open = count_open;
        }

        public string get_name()
        {
            return name;
        }

        public int get_count_open()
        {
            return count_open;
        }

        static public int MyClassCompareCount(Playl mf1, Playl mf2)
        {
            return mf1.get_count_open().CompareTo(mf2.get_count_open());
        }

        public void plus_count()
        {
            ++count_open;
        }
    }


    public class Playlists
    {
        private List<Playl> play_lists = new List<Playl>();

        public Playlists()
        {
            StreamReader read = new StreamReader("data\\name_playlists.txt");
            string n;
            int c;

            while (read.Peek() >= 0)
            {
                n = read.ReadLine();
                c = int.Parse(read.ReadLine());
                play_lists.Add(new Playl(n, c));
            }

            read.Close();
        }

        public void open_playlist(string name)
        {
            for (int i = 0; i < play_lists.Count; ++i)
            {
                if (name == play_lists[i].get_name())
                {
                    play_lists[i].plus_count();
                    break;
                }
            }
        }

        public void add_playlist(string name)
        {
            play_lists.Add(new Playl(name, 1));
        }

        public void save()
        {
            StreamWriter write = new StreamWriter("data\\name_playlists.txt");

            for (int i = 0; i < play_lists.Count; ++i)
            {
                write.WriteLine(play_lists[i].get_name());
                write.WriteLine(play_lists[i].get_count_open());
            }
            write.Close();

        }

        public List<string> get_list_of_playlist()
        {
            List<string> result = new List<string>();

            play_lists.Sort(Playl.MyClassCompareCount);

            try
            {
                result.Add(play_lists[2].get_name());
            }
            catch
            {

            }
            try
            {
                result.Add(play_lists[1].get_name());
            }
            catch
            {

            }
            try { result.Add(play_lists[0].get_name()); }
            catch
            {

            }


            return result;
        }
    }



    //////
}

