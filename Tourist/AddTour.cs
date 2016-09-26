using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tourist
{
    public partial class AddTour : Form
    {
        private string stName = null;
        public AddTour()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CorrectEnter(string str, Label lb)
        {
            if(str == "")
            {
                lb.Text = "error";
                return true;
            }
            else
            {
                lb.Text = "";
                return false;
            }
                
        }
        private bool NotUnique()
        {
            Form1 main = this.Owner as Form1;
            int i = 0;
            foreach (TourData tour in main.tourList)
            {
                i++;
                if (tour.name == TourNameBox.Text)
                    if (tour.region == RegionBox.Text)
                        //if (tour.days == day)
                        if (tour.country == CountryBox.Text)
                        {
                            if (i - 1 != main.changei)
                            {
                                labelAddTourError.Visible = true;
                                return true;
                            }
                        }
            }
            labelAddTourError.Visible = false;
            return false;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1; // Определяем владельца
            bool err;
            err = CorrectEnter(CountryBox.Text, LabelCountry);
            err |= CorrectEnter(RegionBox.Text, LabelRegion);
            err |= CorrectEnter(TourNameBox.Text, LabelTourName);
            err |= CorrectEnter(CostsBox.Text, LabelCosts);
            err |= NotUnique();
            //TO DO: Проверка цены на цифры
            if (!err)
            {
                if (AddButton.Text == "Add")
                {
                    main.tourList.Add(new TourData
                    {
                        country = CountryBox.Text,
                        region = RegionBox.Text,
                        name = TourNameBox.Text,
                        days = Convert.ToInt32(DayBox.Text),
                        cost = Convert.ToInt32(CostsBox.Text)
                        //TO DO: Проверка цены на цифры
                    });
                    this.Close();
                }
                else
                {
                    int i = 0;
                    foreach (TourData tour in main.tourList)
                    {
                        i++;
                        if (tour.name == TourNameBox.Text)
                            if (tour.region == RegionBox.Text)
                                if (tour.country == CountryBox.Text)
                                {
                                    if (i - 1 != main.changei)
                                    {
                                        labelAddTourError.Visible = true;
                                        break;
                                    }
                                }

                    }
                    if (labelAddTourError.Visible != true)
                    {
                        string namet = main.selectTreeTour.name;
                        int day = main.selectTreeTour.days;
                        string region = main.selectTreeTour.region;
                        string coun = main.selectTreeTour.country;
                        int cost = main.selectTreeTour.cost;
                        foreach (TourData tour in main.tourList)
                        {
                            if (tour.name == namet)
                                if (tour.region == region)
                                    if (tour.days == day)
                                        if (tour.country == coun)
                                        {
                                            if (tour.pictureFile != null)
                                                File.Copy(main.appDir + main.dataFileText + tour.pictureFile, main.appDir + @"\\pic.png");
                                            if (tour.descriptionTourFile != null)
                                                File.Copy(main.appDir + main.dataFileText + tour.descriptionTourFile, main.appDir + @"\\txt.rtf");
                                            main.change.name = TourNameBox.Text;  // Нашли наш тур в Листе туров
                                            main.change.country = CountryBox.Text;
                                            main.change.days = Convert.ToInt32(DayBox.Text);
                                            main.change.cost = Convert.ToInt32(CostsBox.Text);
                                            main.change.region = RegionBox.Text;
                                            break;
                                        }
                        }
                        main.changeTour = true;
                        this.Close();
                    }
                }
            }
           
        }

        private void AddTour_Load(object sender, EventArgs e)
        {

        }

        private void CostsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
