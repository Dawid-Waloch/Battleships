using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Statki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int[,] planszaPrzeciwnika = new int[12, 12];
        public int[,] plansza = new int[10, 10];
        public bool czyTwojaKolej = true;
        public int wynikGracza = 0;
        public int wynikKomputera = 0;
        public int iterator = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            plansza = new int[10, 10] {
                { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                { 1, 0, 0, 0, 1, 1, 0, 0, 1, 0 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 1, 1, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            dataGridView1.ColumnCount = 10;
            int x = plansza.GetLength(0);
            int y = plansza.GetLength(1);

            string[] wiersz = new string[y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    dataGridView1.Columns[j].Width = 30;

                    wiersz[j] = plansza[i, j].ToString();
                }
                dataGridView1.Rows.Add(wiersz);
                dataGridView1.Rows[i].Height = 30;
            }

            Random rand = new Random();
            
            int standardowa_szerokosc_x = 10;
            int standardowa_szerokosc_y = 10;

            int v = standardowa_szerokosc_y;
            int h = standardowa_szerokosc_x;

            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    planszaPrzeciwnika[i, j] = 0;
                }
            }

            int licznik = 1;
            int masztowiec = 4;

            while (masztowiec >= 1)
            {
                for (int i = 0; i < licznik; i++)
                {

                    bool statekUstawiony = false;

                    while (!statekUstawiony)
                    {
                        int pozycjaX = rand.Next(0, 10);
                        int pozycjaY = rand.Next(0, 10);
                        int kierunek = rand.Next(1, 5);

                        if (CzyDaSieUstawic(planszaPrzeciwnika, pozycjaX, pozycjaY, kierunek, masztowiec))
                        {
                            UstawStatek(planszaPrzeciwnika, pozycjaX, pozycjaY, kierunek, masztowiec);
                            statekUstawiony = true;
                        }
                    }
                }

                licznik++;
                masztowiec--;
            }

            dataGridView2.ColumnCount = 10;

            int xPrzeciwnikia = 10;
            int yPrzeciwnika = 10;

            string[] wierszPrzeciwnika = new string[y];

            for(int i = 0;i < xPrzeciwnikia; i++)
            {
                for(int j = 0;j < yPrzeciwnika; j++)
                {
                    dataGridView2.Columns[j].Width = 30;
                    if(planszaPrzeciwnika[i, j] == 1)
                    {
                        wierszPrzeciwnika[j] = planszaPrzeciwnika[i, j].ToString();
                    } else if(planszaPrzeciwnika[i, j] == 2)
                    {
                        wierszPrzeciwnika[j] = planszaPrzeciwnika[i, j].ToString();
                    } else
                    {
                        wierszPrzeciwnika[j] = " ";
                    }
                    
                }
                dataGridView2.Rows.Add(wierszPrzeciwnika);
                dataGridView2.Rows[i].Height = 30;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.ClearSelection();

            if (czyTwojaKolej)
            {
                int x = e.RowIndex;
                int y = e.ColumnIndex;
                Strzal(x, y, dataGridView2);

                if(wynikGracza == 20)
                {
                    MessageBox.Show("Wygrałeś");
                    wynikGracza = 0;
                    Close();
                }
            }

            while(!czyTwojaKolej)
            {
                Random random = new Random();
                int xP = random.Next(0, 10);
                int yP = random.Next(0, 10);
                Strzal(xP, yP, dataGridView1);

                if(wynikKomputera == 20)
                {
                    MessageBox.Show("Komputer wygrał");
                    wynikKomputera = 0;
                    Close();
                }
            }
        }

        public void Strzal(int x, int y, DataGridView dataGridView)
        {
            if (dataGridView == dataGridView2)
            {
                WpiszZnaki(dataGridView2, x, y, planszaPrzeciwnika, ref wynikGracza);
            } else
            {
                WpiszZnaki(dataGridView1, x, y, plansza, ref wynikKomputera);
            }
        }

        public void WpiszZnaki(DataGridView dataGridView, int x, int y, int[,] danaPlansza, ref int punkty)
        {

            if (danaPlansza[x, y] != 1 && danaPlansza[x, y] != 4 && danaPlansza[x, y] != 9)
            {
                dataGridView.Rows[x].Cells[y].Value = "+";
                dataGridView.Rows[x].Cells[y].Style.BackColor = Color.Green;

                if (czyTwojaKolej)
                {
                    czyTwojaKolej = false;
                    danaPlansza[x, y] = 4;
                }
                else
                {
                    czyTwojaKolej = true;
                    danaPlansza[x, y] = 4;
                }

            }
            else if (danaPlansza[x, y] == 1)
            {
                dataGridView.Rows[x].Cells[y].Value = "X";
                dataGridView.Rows[x].Cells[y].Style.BackColor = Color.Red;
                punkty++;

                if (x - 1 >= 0 && y + 1 < 10)
                {
                    dataGridView.Rows[x - 1].Cells[y + 1].Value = "+";
                    dataGridView.Rows[x - 1].Cells[y + 1].Style.BackColor = Color.Green;
                    danaPlansza[x - 1, y + 1] = 4;
                }

                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    dataGridView.Rows[x - 1].Cells[y - 1].Value = "+";
                    dataGridView.Rows[x - 1].Cells[y - 1].Style.BackColor = Color.Green;
                    danaPlansza[x - 1, y - 1] = 4;
                }

                if (x + 1 < 10 && y + 1 < 10)
                {
                    dataGridView.Rows[x + 1].Cells[y + 1].Value = "+";
                    dataGridView.Rows[x + 1].Cells[y + 1].Style.BackColor = Color.Green;
                    danaPlansza[x + 1, y + 1] = 4;
                }

                if (x + 1 < 10 && y - 1 >= 0)
                {
                    dataGridView.Rows[x + 1].Cells[y - 1].Value = "+";
                    dataGridView.Rows[x + 1].Cells[y - 1].Style.BackColor = Color.Green;
                    danaPlansza[x + 1, y - 1] = 4;
                }

                danaPlansza[x, y] = 9;
            }
        }

        public bool CzyDaSieUstawic(int[,] planszaPrzeciwnika, int x, int y, int kierunek, int dlugosc)
        {
            int v = 10;
            int h = 10;

            for (int i = 0; i < dlugosc; i++)
            {
                int temp_x = x;
                int temp_y = y;

                switch (kierunek)
                {
                    case 1:
                        {
                            temp_x = x - i;
                            break;
                        }
                    case 2:
                        {
                            temp_x = x + i;
                            break;
                        }
                    case 3:
                        {
                            temp_y = y + i;
                            break;
                        }
                    case 4:
                        {
                            temp_y = y - i;
                            break;
                        }
                }

                if (temp_x < 0 || temp_x >= h || temp_y < 0 || temp_y >= v || planszaPrzeciwnika[temp_y, temp_x] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static void UstawStatek(int[,] planszaPrzeciwnika, int x, int y, int kierunek, int dlugosc)
        {

            int temp_x = x;
            int temp_y = y;
            int jeden_bok = 0, drugi_bok = 0, przod = 0, tyl = 0;

            for (int i = 0; i < dlugosc; i++)
            {
                temp_x = x;
                temp_y = y;

                switch (kierunek)
                {
                    case 1:
                        {
                            temp_x = x - i;
                            tyl = x + 1;
                            przod = x - dlugosc;
                            jeden_bok = y + 1;
                            drugi_bok = y - 1;

                            break;
                        }
                    case 2:
                        {
                            temp_x = x + i;
                            tyl = x - 1;
                            jeden_bok = y + 1;
                            drugi_bok = y - 1;
                            przod = x + dlugosc;
                            break;
                        }
                    case 3:
                        {
                            temp_y = y + i;
                            tyl = y - 1;
                            przod = y + dlugosc;
                            jeden_bok = x + 1;
                            drugi_bok = x - 1;
                            break;
                        }
                    case 4:
                        {
                            temp_y = y - i;
                            tyl = y + 1;
                            przod = y - dlugosc;
                            jeden_bok = x + 1;
                            drugi_bok = x - 1;
                            break;
                        }
                }

                planszaPrzeciwnika[temp_y, temp_x] = 1;
                if (kierunek == 1 || kierunek == 2)
                {
                    if(jeden_bok < 10)
                    {
                        planszaPrzeciwnika[jeden_bok, temp_x] = 2;
                    }

                    if (drugi_bok >= 0)
                    {
                        planszaPrzeciwnika[drugi_bok, temp_x] = 2;
                    }
                }
                else
                {
                    if (jeden_bok < 10)
                    {
                        planszaPrzeciwnika[temp_y, jeden_bok] = 2;
                    }
                    
                    if(drugi_bok >= 0)
                    {
                        planszaPrzeciwnika[temp_y, drugi_bok] = 2;
                    }
                }

            }

            if (kierunek == 1 || kierunek == 2)
            {
                if (przod >= 0 && przod < 10)
                {
                    planszaPrzeciwnika[temp_y, przod] = 2;

                    if (temp_y - 1 >= 0)
                    {
                        planszaPrzeciwnika[temp_y - 1, przod] = 2;
                    }

                    if(temp_y + 1 < 10)
                    {
                        planszaPrzeciwnika[temp_y + 1, przod] = 2;
                    }
                }

                if(tyl >= 0 && tyl < 10)
                {
                    planszaPrzeciwnika[temp_y, tyl] = 2;

                    if (temp_y - 1 >= 0)
                    {
                        planszaPrzeciwnika[temp_y - 1, tyl] = 2;
                    }

                    if (temp_y + 1 < 10)
                    {
                        planszaPrzeciwnika[temp_y + 1, tyl] = 2;
                    }
                }
            }
            else
            {
                if (przod >= 0 && przod < 10)
                {
                    planszaPrzeciwnika[przod, temp_x] = 2;

                    if (temp_x - 1 >= 0)
                    {
                        planszaPrzeciwnika[przod, temp_x - 1] = 2;
                    }

                    if(temp_x + 1 < 10)
                    { 
                        planszaPrzeciwnika[przod, temp_x + 1] = 2;
                    }
                }

                if(tyl >= 0 && tyl < 10)
                {
                    planszaPrzeciwnika[tyl, temp_x] = 2;

                    if (temp_x - 1 >= 0)
                    {
                        planszaPrzeciwnika[tyl, temp_x - 1] = 2;
                    }

                    if(temp_x + 1 < 10)
                    {
                        planszaPrzeciwnika[tyl, temp_x + 1] = 2;
                    }
                }
            }
        }
    }
}
