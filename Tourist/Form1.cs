using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tourist
{
    internal partial class Form1 : Form
    {
        #region Глобальные переменные
        struct dayNum
        {
            public int minDay;
            public int maxDay;
            public dayNum(int iminDay, int imaxDay)
            {
                minDay = iminDay;
                maxDay = imaxDay;
            }
        }
        Dictionary<string, dayNum> days = new Dictionary<string, dayNum>();

        public string dataFile = @"\AppData\Inf.dat";
        public string dataFileText = @"\AppData\Text\";
        public string appDir = Application.StartupPath;
        public List<TourData> tourList = new List<TourData>();
        public TourData selectTreeTour = new TourData();
        public TourData selectPrevTour = new TourData();
        public TourData change = new TourData();
        public string selectPrevFullPath;
        public int selectPrevi;
        public int changei;
        public bool changeTour = false;
        public int dayChange = 0;
        public string valueChange;
        #endregion
        
        //----------------Инициализация--------------------------//
        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(appDir + @"\AppData"))                //Папка данных существует
                Directory.CreateDirectory(appDir + @"\AppData");
            if (!File.Exists(appDir + dataFile))                        //Файл данных существует
                using (File.Create(appDir + dataFile)) { }
            //Извлеченние сохраненных данных с прошлого запуска программы 
            FileInfo file = new FileInfo(appDir + dataFile);
            long size = file.Length;    
            if (size != 0)                                              // Если файл пустой, то не читаем
            {
                using (Stream input = File.OpenRead(appDir + dataFile))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    tourList = (List<TourData>)bf.Deserialize(input);
                }
            }
            pictureBoxTour.Image = ResourceImage.earth;
            days.Add("All", new dayNum { minDay = 0, maxDay = 255 });                           // Заполняем словарь ключами и их значениями (Дни)
            days.Add("1 - 3", new dayNum { minDay = 1, maxDay = 3 });
            days.Add("4 - 7", new dayNum { minDay = 4, maxDay = 7 });
            days.Add("8 - 14", new dayNum { minDay = 8, maxDay = 14 });
            days.Add(">= 14", new dayNum { minDay = 14, maxDay = 255 });
            dateTimePicker1.Value = DateTime.Now.AddDays(1);                                    // + 1 день к дате в заявке
            foreach (string key in days.Keys)                                                   // заполняем ComboBox дней значениями для выбора пользователем
                comboBoxDays.Items.Add(key);
            comboBoxDays.SelectedIndex = 0;                                                     // Выбираем первый элемент в ComboBox = "All"

            ComboBoxLoadCountry(0);
            comboBoxCountry.SelectedIndex = 0;
            ComboBoxLoadRegion(0);
            comboBoxRegion.SelectedIndex = 0;
            TreeViewLoad();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //----------------Сериализация данных в tourList--------------------------//
        private void DataSerialize()                                                            // Сериализуем - сохраняем данные о турах в файл Inf.dat
        {
            using (Stream output = File.OpenWrite(appDir + dataFile))                           
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(output, tourList);                                                 //Сериализация данных в tourList
            }
        }
        
        //----------------Exit--------------------------//
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)                   // При закрытии сохраняем данные
        {
            DataSerialize();
            Application.Exit();
        }

        //----------------Exit--------------------------//
        private void MenuFileExit_Click(object sender, EventArgs e)
        {
            DataSerialize();
            Application.Exit();
        }

        #region Ввод мастер ключа
        //----------------Ввод мастер ключа--------------------------//
        private void Authorized_Click(object sender, EventArgs e)                               // Ввод мастер ключа
        {
            if (Authorized.ForeColor == Color.Green)                                            // Меняем цвета для визуализации нахождения в мастер режиме
            {
                Authorized.ForeColor = Color.Red;
                changePassword.Visible = false;
                toolStripMenuSave.Visible = false;
                toolStrip1.Visible = false;
                labelTourName.Visible = true;
            }
            else
            {
                PasswordForm PassForm = new PasswordForm();                                     // Диалог вызова ввода пароля
                PassForm.ShowInTaskbar = false;
                PassForm.StartPosition = FormStartPosition.CenterParent;
                PassForm.ShowDialog(this);

                if (Authorized.ForeColor == Color.Green)
                {
                    changePassword.Visible = true;
                    toolStripMenuSave.Visible = true;
                    toolStrip1.Visible = true;
                    labelTourName.Visible = false;
                    TabMenu_Click(sender, e);
                }
            }

        }
        #endregion Ввод мастер ключа
        
        #region Загрузка картинки - кнопка
        //----------------Добавление картинки в тур--------------------------//
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (TabMenu.SelectedTab.Text == "Map")
            {
                TreeNode nod;
                if (treeViewTour.SelectedNode != null)
                {
                    nod = treeViewTour.SelectedNode;
                    if (nod.Level == 2)
                    {
                        FindTour(nod);
                        OpenPictureDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\"; // Открываем Рисунки пользователя
                        DialogResult result = OpenPictureDialog.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            try // безопасная попытка 
                            {
                                if (!Directory.Exists(appDir + dataFileText))                               //Папка существует
                                    Directory.CreateDirectory(appDir + dataFileText);
                                if (!Directory.Exists(appDir + dataFileText + nod.FullPath))                //Папка существует
                                    Directory.CreateDirectory(appDir + dataFileText + nod.FullPath);
                                if (File.Exists(appDir + dataFileText + nod.FullPath + @"\\" + selectTreeTour.cost + ".png"))       // Если картинка есть удаляем ее
                                    File.Delete(appDir + dataFileText + nod.FullPath + @"\\" + selectTreeTour.cost + ".png");
                                selectTreeTour.pictureFile =  nod.FullPath + "\\" + selectTreeTour.cost + ".png";
                                File.Copy(OpenPictureDialog.FileName, appDir + dataFileText + selectTreeTour.pictureFile);      // Сохраняем картинку в нашу БД
                                using (var file = new FileStream(appDir + dataFileText + selectTreeTour.pictureFile, FileMode.Open, FileAccess.Read, FileShare.Inheritable))
                                {
                                    pictureBoxTour.Image = Image.FromStream(file);                                              // Отображение картинки из БД
                                }
                            }
                            catch (Exception ex) // если попытка загрузки не удалась 
                            {
                                // выводим сообщение с причиной ошибки 
                                MessageBox.Show("Не удалось загрузить файл: " + ex.Message);
                            }
                        }
                    }
                    else MessageBox.Show("Выберите название тура, затем нажмите на кнопку с изображением.","Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataSerialize();
                }
            }
            else if(TabMenu.SelectedTab.Text == "Description")                   // Вставляем изображение в текстовое описание тура
            {
                OpenPictureDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\"; // Открываем Рисунки пользователя
                DialogResult result = OpenPictureDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Image MemForImage = Image.FromFile(OpenPictureDialog.FileName);
                    Clipboard.SetImage(MemForImage);
                    richTextBox.Paste();
                }
            }
        }
        #endregion Загрузка картинки - кнопка

        #region Добавление Туров
        //----------------Добавление Туров--------------------------//
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddTour addTour = new AddTour();
            addTour.Owner = this;                                               // Устанавливаем владельца
            addTour.RegionBox.Sorted = true;                                    // Сортировка выпадающих списков в диалоговом окне добавления туристических маршрутов
            addTour.CountryBox.Sorted = true;       
            foreach (TourData tour in tourList)
            {
                if (!addTour.CountryBox.Items.Contains(tour.country))           // Не включаем в список повторяющиеся значения 
                    addTour.CountryBox.Items.Add(tour.country);
                if (!addTour.RegionBox.Items.Contains(tour.region))
                    addTour.RegionBox.Items.Add(tour.region);
            }
            if (addTour.CountryBox.Items.Count != 0)
            {
                addTour.CountryBox.SelectedIndex = 0;                           // Задаем индексы для отображения 
                addTour.RegionBox.SelectedIndex = 0;
            }
            addTour.ShowInTaskbar = false;
            addTour.StartPosition = FormStartPosition.CenterParent;
            addTour.ShowDialog(this);                                           // Показываем диалоговое окно добавления туров
            ComboBoxLoadCountry(0);                                             // Обновляем списки главного окна
            ComboBoxLoadRegion(0);
            DataSerialize();                                                    // Сохраняем
            TreeViewLoad();                                                     // Отображаем обновленное дерево туристических маршрутов
        }
        #endregion Добавление Туров

        #region Выбор в TreeView и отображение информации о ТМ
        //----------------Выбор в TreeView и отображение информации о ТМ--------------------------//
        private void treeViewTour_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 2) // Если выбран лист дерева т.е. конечный узел 
            { 
                int i = FindTour(e.Node);                                       // Ищем выбранный тур. мар. в списке ТМ
                labelTourCost.Text = "Tour costs: " + tourList[i].cost + " P";  // Выводим цену, к-во дней и название маршрута для визуализации
                labelDays.Text = "Tour days: " + tourList[i].days;
                labelTourName.Text = "Туристический маршрут: " + tourList[i].name;
                if (selectTreeTour.pictureFile != null)                         // Картинки нет? то отображаем дефолтную иначе собственную загружаем
                {
                    using (var file = new FileStream(appDir + dataFileText + selectTreeTour.pictureFile, FileMode.Open, FileAccess.Read, FileShare.Inheritable))
                    {
                        pictureBoxTour.Image = Image.FromStream(file);                          // Открыв
                    }
                }
                else pictureBoxTour.Image = ResourceImage.earth;                                // Карты нет, отображаем информационное изображение

                if(selectPrevTour.country != null && selectPrevTour != tourList[i])             // Выбрали другой маршрут чем был прошлый? Сохраняем текст из textBox в прошлый тур 
                {
                    if (selectPrevTour.descriptionTourFile == null)                             // Если еще ни разу не сохранялся текстовый файл, то создаем соответствующие папки
                    {
                        if (!Directory.Exists(appDir + dataFileText))                           //Папка существует
                            System.IO.Directory.CreateDirectory(appDir + dataFileText);
                        if (!Directory.Exists(appDir + dataFileText + selectPrevFullPath))       //Папка существует
                            System.IO.Directory.CreateDirectory(appDir + dataFileText + selectPrevFullPath);
                        richTextBox.SaveFile(appDir + dataFileText + selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf");
                        tourList[selectPrevi].descriptionTourFile = selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf";
                    }
                    else
                        richTextBox.SaveFile(appDir + dataFileText + selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf"); // Сохраняем текстовый файл
                }
                if (tourList[i].descriptionTourFile != null)                                     // Загружаем текстовый файл
                {
                    if (System.IO.File.Exists(appDir + dataFileText + tourList[i].descriptionTourFile))
                    {
                        richTextBox.Clear();
                        richTextBox.LoadFile(appDir + dataFileText + tourList[i].descriptionTourFile);
                    }
                }
                else                                                                            // Описание отсутствует
                {   
                    richTextBox.Clear();
                    richTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    richTextBox.Text = "Описание отсутствует";
                }
                selectPrevTour = tourList[i];                                                   // Запоминаем текущий выбранный ТМ
                selectPrevFullPath = e.Node.FullPath; ;
                selectPrevi = i;
                {
                    textBoxOrderCountry.Text = tourList[i].country;                             // На страницу Order TabMenu заносим соответствующие ТМ данные
                    textBoxOrderRegion.Text = tourList[i].region;
                    textBoxOrderName.Text = tourList[i].name;
                    textBoxOrderDay.Text = tourList[i].days.ToString();
                    textBoxOrderCost.Text = tourList[i].cost.ToString();
                }
            }
        }
        #endregion Выбор в TreeView и отображение информации о ТМ

        #region Загрузка данных в ComboBox
        //----------------Загрузка данных в ComboBox--------------------------//
        void ComboBoxLoadCountry(int i)
        {
            comboBoxCountry.Sorted = true;
            comboBoxCountry.Items.Clear();
            comboBoxCountry.Items.Add("All");
            foreach (TourData tour in tourList)
            {
                if (!comboBoxCountry.Items.Contains(tour.country))
                    comboBoxCountry.Items.Add(tour.country);
            }
        }
        void ComboBoxLoadRegion(int i)
        {
            comboBoxRegion.Sorted = true;
            comboBoxRegion.Items.Clear();
            comboBoxRegion.Items.Add("All");
            foreach (TourData tour in tourList)
            {
                if (comboBoxCountry.Text == "All" || comboBoxCountry.Text == tour.country)
                    if (!comboBoxRegion.Items.Contains(tour.region))
                        comboBoxRegion.Items.Add(tour.region);
            }
        }
        #endregion Загрузка данных в ComboBox

        #region Загрузка данных в TreeView
        //----------------Загрузка данных в TreeView--------------------------//
        void TreeViewLoad()
        {
            IEnumerable<TourData> resultN = tourList.OrderBy(x => x.name).ThenBy(x => x.name).ToList();                 // Сортируем по названию ТМ, региону, стране
            IEnumerable<TourData> resultR = resultN.OrderBy(x => x.region).ThenBy(x => x.region).ToList();
            IEnumerable<TourData> result = resultR.OrderBy(x => x.country).ThenBy(x => x.country).ToList();
            treeViewTour.Nodes.Clear();
            foreach (TourData tour in result)
            {
                // Фильтры по странам, регионам и к-ву дней
                if ((comboBoxCountry.Text == "All" || comboBoxCountry.Text == tour.country) 
                    && (comboBoxRegion.Text == "All" || comboBoxRegion.Text == tour.region) 
                    && (tour.days >= days[comboBoxDays.Text].minDay && days[comboBoxDays.Text].maxDay >= tour.days))
                {
                    if (treeViewTour.Nodes.Count == 0)                      // В TreeView ни одного элемента нет, загружаем первый
                    {
                        treeViewTour.Nodes.Add(new TreeNode(tour.country));
                        treeViewTour.Nodes[0].Nodes.Add(new TreeNode(tour.region));
                        treeViewTour.Nodes[0].Nodes[0].Nodes.Add(new TreeNode(tour.name));
                    }
                    else                                                    // Загружаем элементы в непустой TreeView
                    {
                        bool enter = false;                                         // Переменная указывающая на уникальность элемента т.е. ни разу не совпало
                        int j = 0;
                        for (int i = treeViewTour.Nodes.Count - 1; i >= 0; i--)     // Ищем совпадения на первом уровне узлов
                        {
                            if (treeViewTour.Nodes[i].Text == tour.country)         // Страна
                            {
                                enter = true;
                                j = i;                                              // Запоминаем номер совпавшего 1-го узла, индекс узла страны
                                break;
                            }
                        }
                        if (enter)
                        {
                            int h = 0;
                            enter = false;
                            for (int i = treeViewTour.Nodes[j].Nodes.Count - 1; i >= 0; i--)// Ищем совпадения на 2-ом уровне узлов
                            {
                                if (treeViewTour.Nodes[j].Nodes[i].Text == tour.region)     // Регион, город, местность
                                {
                                    enter = true;
                                    h = i;                                                  // Запоминаем номер совпавшего 2-го узла, индекс узла региона
                                    break;
                                }
                            }
                            if (enter)
                            {
                                enter = false;
                                int k = 0;
                                for (int i = treeViewTour.Nodes[j].Nodes[h].Nodes.Count - 1; i >= 0; i--)// Ищем совпадения на 3-ем уровне узлов
                                {
                                    if (treeViewTour.Nodes[j].Nodes[h].Nodes[i].Text == tour.name)      // Дни
                                    {
                                        enter = true;
                                        k = i;                                                          // Запоминаем номер совпавшего 3-го узла, индекс узла названия ТМ
                                        break;
                                    }
                                }
                                if (enter)          // Уникальный э-нт: название тура 
                                {
                               //     treeViewTour.Nodes[j].Nodes[h].Nodes[k].Nodes.Add(new TreeNode(tour.days.ToString()));
                                }
                                else            // Уникальный э-нт: название тура 
                                {
                                    treeViewTour.Nodes[j].Nodes[h].Nodes.Add(new TreeNode(tour.name));
                               //     treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes[treeViewTour.Nodes[j].Nodes.Count - 1].Nodes[treeViewTour.Nodes[j].Nodes[h].Nodes.Count - 1].Nodes.Add(new TreeNode(tour.days.ToString()));
                                }
                            }
                            else        // Уникальный э-нт: регион, название тура 
                            {
                                treeViewTour.Nodes[j].Nodes.Add(new TreeNode(tour.region));
                                treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes[treeViewTour.Nodes[j].Nodes.Count - 1].Nodes.Add(new TreeNode(tour.name));
                          //      treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes[treeViewTour.Nodes[j].Nodes.Count - 1].Nodes[0].Nodes.Add(new TreeNode(tour.days.ToString()));
                            }
                        }
                        else        // Уникальный э-нт: страна, регион, название тура 
                        {
                            treeViewTour.Nodes.Add(new TreeNode(tour.country));
                            treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes.Add(new TreeNode(tour.region));
                            treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes[0].Nodes.Add(new TreeNode(tour.name));
                       //     treeViewTour.Nodes[treeViewTour.Nodes.Count - 1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode(tour.days.ToString()));
                        }
                    }
                }
            }
            treeViewTour.ExpandAll();       // Развернуть Tree
        }
        //End-------------TreeViewLoad--------------------------//
        #endregion

        #region Фильтры
        //----------------Изменили страну в фильтре отображения--------------------------//
        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxLoadRegion(comboBoxCountry.SelectedIndex);
            comboBoxRegion.SelectedIndex = 0;
            TreeViewLoad();
        }
        //----------------Изменили регион в фильтре отображения--------------------------//
        private void comboBoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeViewLoad();
        }
        //----------------Изменили дни в фильтре отображения--------------------------//
        private void comboBoxDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeViewLoad();
        }
        #endregion Фильтры

        #region Редактирование ТМ
        //----------------Переименовываем папки БД--------------------------//
        private void ChangePathImageAndDescription(TourData tour, string valueChange)
        {
            Directory.Move(appDir + dataFile + tour.country, appDir + dataFile + valueChange);
        }

        //----------------Копируем и удаляем файлы из папок--------------------------//
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Субдериктории
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            // Ксли папок в месте назначении нет
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            // Кописрование в новое место
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
                string delpath = Path.Combine(sourceDirName, file.Name);
                File.Delete(delpath);
            }
            // Копирование субдиректорий в рекурсии
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        //----------------Модифицируем выбранный тур--------------------------//
        private void ButtonModifiTour_Click(object sender, EventArgs e)
        {
            TreeNode nod;
            if (treeViewTour.SelectedNode != null)
            {
                nod = treeViewTour.SelectedNode;
                if (nod.Level == 2)                                             // Если выбрали ТМ, то редактировать можно все значения данного ТМ 
                {
                    changei = FindTour(nod);
                    AddTour addTour = new AddTour();                            // Диалоговое окно добавления тура модифицируем под редактирующее дилоговое окно
                    addTour.Owner = this;                                       // Устанавливаем владельца
                    addTour.AddButton.Text = "Mod";
                    addTour.CountryBox.Text = selectTreeTour.country;           // Заполняем текст боксы данными из вабранного тура
                    addTour.RegionBox.Text = selectTreeTour.region;
                    addTour.DayBox.Value = selectTreeTour.days;
                    addTour.CostsBox.Text = selectTreeTour.cost.ToString();
                    addTour.TourNameBox.Text = selectTreeTour.name;
                    addTour.ShowInTaskbar = false;
                    addTour.StartPosition = FormStartPosition.CenterParent;
                    addTour.ShowDialog(this);
                    if(changeTour)
                    {
                        toolStripButton_Delete_Click(sender, e);
                        tourList.Add(new TourData
                        {
                            country = change.country,
                            region = change.region,
                            name = change.name,
                            days = change.days,
                            cost = change.cost
                        });
                        int countList = tourList.Count - 1;
                        string path = change.country + @"\\" + change.region + @"\\" + change.name;
                        if (File.Exists(appDir + @"\\pic.png"))
                        {
                            if (!Directory.Exists(appDir + dataFileText))                               //Папка существует
                                Directory.CreateDirectory(appDir + dataFileText);
                            if (!Directory.Exists(appDir + dataFileText + path))                        //Папка существует
                                Directory.CreateDirectory(appDir + dataFileText + path);
                            if (File.Exists(appDir + dataFileText + path + @"\\" + change.cost + ".png"))       // Если картинка есть удаляем ее
                                File.Delete(appDir + dataFileText + path + @"\\" + change.cost + ".png");
                            tourList[countList].pictureFile = path + "\\" + change.cost + ".png";
                            File.Copy(appDir + @"\\pic.png", appDir + dataFileText + tourList[countList].pictureFile);      // Сохраняем картинку в нашу БД
                            File.Delete(appDir + @"\\pic.png");
                            using (var file = new FileStream(appDir + dataFileText + tourList[countList].pictureFile, FileMode.Open, FileAccess.Read, FileShare.Inheritable))
                            {
                                pictureBoxTour.Image = Image.FromStream(file);                                              // Отображение картинки из БД
                            }
                        }
                        if(File.Exists(appDir + @"\\txt.rtf"))
                        {
                            if (!Directory.Exists(appDir + dataFileText))                               //Папка существует
                                Directory.CreateDirectory(appDir + dataFileText);
                            if (!Directory.Exists(appDir + dataFileText + path))                        //Папка существует
                                Directory.CreateDirectory(appDir + dataFileText + path);
                            if (File.Exists(appDir + dataFileText + path + @"\\" + change.cost + ".rtf"))       // Если картинка есть удаляем ее
                                File.Delete(appDir + dataFileText + path + @"\\" + change.cost + ".rtf");
                            tourList[countList].descriptionTourFile = path + "\\" + change.cost + ".rtf";
                            File.Copy(appDir + @"\\txt.rtf", appDir + dataFileText + tourList[countList].descriptionTourFile);      // Сохраняем картинку в нашу БД
                            File.Delete(appDir + @"\\txt.rtf");
                            richTextBox.Clear();
                            richTextBox.LoadFile(appDir + dataFileText + tourList[countList].descriptionTourFile);
                        }
                        TreeViewLoad();                                             // Отобразим новое дерево туров
                    }
                    changeTour = false;
                }
                else if (nod.Level == 1)                                        // Регион
                {
                    string st = valueChange;
                    DialodChangeLoad("Region:", false, true, nod);
                    if (st != valueChange)                                      // Небыло изменения Cancel
                    {
                        bool err = false;
                        foreach (TourData tour in tourList)                     // Поиск совпадающих ТМ
                        {
                            if (tour.region == valueChange)
                            {
                                foreach (TourData tournew in tourList)
                                {
                                    if (tournew.country == nod.Parent.Text)
                                        if (tournew.region == nod.Text)
                                            if (tournew.name == tour.name)
                                            {
                                                err = true;
                                                break;
                                            }
                                }
                            }
                            if (err) break;
                        }
                        if (!err)                                               // Ошибки совпадений нет, продолжаем
                        {
                            string oldRegion = null;
                            foreach (TourData tour in tourList)                     // Находим все ТМ с прошлым регионом и меняем на новое значение, переименовывем папки БД
                            {
                                if (tour.region == nod.Text)
                                    if (tour.country == nod.Parent.Text)
                                    {
                                        oldRegion = tour.region;
                                        if (tour.pictureFile != null)
                                            tour.pictureFile = tour.country + "\\" + valueChange + "\\" + tour.name + "\\" + tour.cost + ".png";
                                        if (tour.descriptionTourFile != null)
                                            tour.descriptionTourFile = tour.country + "\\" + valueChange + "\\" + tour.name + "\\" + tour.cost + ".rtf";
                                        tour.region = valueChange;                  // Меняем регион
                                    }
                            }
                            if (oldRegion != null)                                  // Если изменений не было ничего не делаем, иначе задаем новое значение
                            {
                                DirectoryCopy(appDir + dataFileText + nod.Parent.Text + "\\" + oldRegion, appDir + dataFileText + nod.Parent.Text + "\\" + valueChange, true);

                            }

                            ComboBoxLoadRegion(0);
                            TreeViewLoad();
                        }
                        else MessageBox.Show("Ошибка совпадения туристических маршрутов");
                    }
                    valueChange = null;
                }
                else if (nod.Level == 0)                                        // Страна
                { 
                    string st = valueChange;
                    DialodChangeLoad("Country:", false, true, nod);
                    if (st != valueChange)                                      // Небыло изменения Cancel
                    {
                        bool err = false;
                        foreach (TreeNode node in nod.Nodes)                    // Поиск совпадающих ТМ
                        {
                            foreach (TreeNode list in node.Nodes)
                            {
                                foreach (TourData tour in tourList)
                                    if (list.Text == tour.name)
                                        if (node.Text == tour.region)
                                            if (valueChange == tour.country)
                                            {
                                                err = true;
                                                break;
                                            }
                                if (err) break;
                            }
                            if (err) break;
                        }
                        if (!err)                                               // Ошибки совпадений нет, продолжаем
                        {
                            string oldCountry = null;
                            foreach (TourData tour in tourList)
                            {
                                if (tour.country == nod.Text)                       // Находим все ТМ с прошлой страной и меняем на новое значение, переименовывем папки БД
                                {
                                    oldCountry = tour.country;
                                    if (tour.pictureFile != null)
                                        tour.pictureFile = valueChange + "\\" + tour.region + "\\" + tour.name + "\\" + tour.cost + ".png";
                                    if (tour.descriptionTourFile != null)
                                        tour.descriptionTourFile = valueChange + "\\" + tour.region + "\\" + tour.name + "\\" + tour.cost + ".rtf";
                                    tour.country = valueChange;                      // Меняем значение поля страны
                                }
                            }
                            if (oldCountry != null)                                  // Если изменений не было ничего не делаем, иначе задаем новое значение
                                DirectoryCopy(appDir + dataFileText + "\\" + oldCountry, appDir + dataFileText + "\\" + valueChange, true);
                            ComboBoxLoadCountry(0);
                            TreeViewLoad();
                        }
                        else MessageBox.Show("Ошибка совпадения туристических маршрутов");
                    }
                }
                valueChange = null;
                DataSerialize();                                                // Сохраняем в файл данные по ТМ
            }
        }

        //-----------------Диалоговое окно для редактирования Страны либо региона-----------------------//
        private void DialodChangeLoad(string str, bool num, bool text, TreeNode nod)
        {
            DialogMod dialogMod = new DialogMod();
            dialogMod.Owner = this;
            dialogMod.labelDialogMod.Text = str;
            dialogMod.textBoxDialogMod.Visible = text;
            dialogMod.numericUpDownDialogMod.Visible = num;
            if(text)
                dialogMod.textBoxDialogMod.Text = nod.Text;
            else
                dialogMod.numericUpDownDialogMod.Value = Convert.ToInt32(nod.Text);
            dialogMod.ShowInTaskbar = false;
            dialogMod.StartPosition = FormStartPosition.CenterParent;
            dialogMod.ShowDialog(this);
        }
        #endregion Редактирование ТМ

        #region Поиск ТМ по списку ТМ
        //----------------Поиск выбранного ТМ из Списка ТМ--------------------------//
        private int FindTour(TreeNode Node)
        {
            string namet = Node.Text;
            string region = Node.Parent.Text;
            string coun = Node.Parent.Parent.Text;
            int i = 0;
            foreach (TourData tour in tourList)
            {
                i++;
                if (tour.name == namet)
                    if (tour.region == region)
                            if (tour.country == coun)
                            {
                                selectTreeTour = tour;  // Нашли наш тур в Листе туров
                                break;
                            }
            }
            return --i;                                 // Возвращаем индекс ТМ в списке
        }
        #endregion Поиск ТМ по списку ТМ

        #region Удаление ТМ, региона, страны
        //----------------Удаляем файлы данных соответствующие удаляемому ТМ--------------------------//
        private void DelFile(int i)
        {
            if (tourList[i].descriptionTourFile != null)
                if(File.Exists(appDir + dataFileText + tourList[i].descriptionTourFile))
                    File.Delete(appDir + dataFileText + tourList[i].descriptionTourFile);
            if (tourList[i].pictureFile != null)
                if (File.Exists(appDir + dataFileText + tourList[i].pictureFile))
                {
                    pictureBoxTour.Image = ResourceImage.earth;
                    File.Delete(appDir + dataFileText + tourList[i].pictureFile);
                }
            //selectPrevTour.country = null;              // Не сохраняем данные о прошлом ТМ
        }

        //----------------Удаление элементов--------------------------//
        void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            TreeNode nod;
            if (treeViewTour.SelectedNode != null)
            {
                nod = treeViewTour.SelectedNode;
                if (nod.Level == 2)
                {
                    int i = FindTour(nod);                              // Находим индекс в списке ТМ
                    DelFile(i);                                         // Удаляем файлы
                    tourList.RemoveAt(i);                               // Удаляем из спмска
                    ComboBoxLoadCountry(0);                             // Обновляем списки
                    ComboBoxLoadRegion(0);
                    TreeViewLoad();                                     // Перезагружаем дерево
                }
                else if (nod.Level == 1)                                // Удаляем ТМ с заданным регионом
                {
                    int n = 0;                                          // Подсчитываем к-во удаляемых ТМ по региону
                    foreach (TourData tour in tourList)
                        if (tour.country == nod.Parent.Text)
                            if (tour.region == nod.Text)
                                n++;
                    for (int j = n - 1; j >= 0; j--)                    
                    {
                        int i = 0;
                        foreach (TourData tour in tourList)             // Ищем индекс тура для его удаления из списка
                        {
                            i++;                                        
                            if (tour.region == nod.Text)
                                break;
                        }                                               
                        DelFile(i - 1);
                        tourList.RemoveAt(i - 1);
                    }
                    ComboBoxLoadCountry(0);
                    ComboBoxLoadRegion(0);
                    TreeViewLoad();
                }
                else if (nod.Level == 0)                                // Удаляем ТМ с заданной страной
                {
                    int n = 0;
                    foreach (TourData tour in tourList)
                        if (tour.country == nod.Text)
                            n++;
                    for (int j = n - 1; j >= 0; j--)
                    {
                        int i = 0;
                        foreach (TourData tour in tourList)             // Ищем индекс тура для его удаления из списка
                        {
                            i++;
                            if (tour.country == nod.Text)
                                break;
                        }
                        DelFile(i - 1);
                        tourList.RemoveAt(i - 1);
                    }
                    ComboBoxLoadCountry(0);
                    ComboBoxLoadRegion(0);
                    TreeViewLoad();
                }
                DataSerialize();                                        // Обновляем БД
            }
        }
        #endregion Удаление ТМ, региона, страны

        #region Текстовый редактор
        private void buttonTextClear_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

        private void ButtonTextFormat_Click(object sender, EventArgs e)
        {
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog.Font;
            }
        }

        private void ButtonFontColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox.SelectionColor = colorDialog.Color;
            }
        }

        private void ButtonFontLeft_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void ButtonFontCenter_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void ButtonFontRight_Click(object sender, EventArgs e)
        {
            richTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void ButtonSaveDescription_Click(object sender, EventArgs e)
        {
            TreeNode nod;
            if (treeViewTour.SelectedNode != null)
            {
                nod = treeViewTour.SelectedNode;
                if (nod.Level == 2)
                {
                    if (!Directory.Exists(appDir + dataFileText))                //Папка существует
                        System.IO.Directory.CreateDirectory(appDir + dataFileText);
                    if (!Directory.Exists(appDir + dataFileText + nod.FullPath))                //Папка существует
                        System.IO.Directory.CreateDirectory(appDir + dataFileText + nod.FullPath);
                    int i = FindTour(nod);
                    MessageBox.Show(nod.FullPath);
                    richTextBox.SaveFile(appDir + dataFileText + nod.FullPath + "\\" + tourList[i].cost + ".rtf");
                    tourList[i].descriptionTourFile = nod.FullPath + "\\" + tourList[i].cost + ".rtf";
                }
                else
                {
                    MessageBox.Show("Выберите тур для сохранения описания.");
                }
            }
        }
        #endregion Текстовый редактор

        #region Отправка заявки на ТМ
        private void buttonOrderSend_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(maskedTextBoxMail.Text))
                labelOrderMailErr.Visible = true;                               // Некорректно введена электронная почта
            else 
            {
                labelOrderMailErr.Visible = false;                              // Корректно введена электронная почта
                if (textBoxOrderCountry.Text == "")                             // Не выбран туристический маршрут
                    labelOrderErr.Visible = true;   
                else
                {
                    labelOrderErr.Visible = false;                              // Информации достаточно => заявка отправлена
                    if (DateTime.Today < dateTimePicker1.Value.Date)            // Дата не может быть меньше текущей
                    {
                        labelOrderDateErr.Visible = false;
                        MessageBox.Show("Заявка отпрвлена.\nВ ближайшее время с Вами свяжется менеджер.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        labelOrderDateErr.Visible = true;
                }
            }
        }
        #endregion Отправка заявки на ТМ     

        #region Загрузка БД туристических маршрутов
        private void LoadFileTRZ()
        {
            tourList.Clear();
            Directory.Delete(appDir + @"\AppData", true);
            Directory.CreateDirectory(appDir + @"\AppData");
            ZipFile.ExtractToDirectory(openFileDialogLoadTour.FileName, appDir + @"\AppData");
            using (Stream input = File.OpenRead(appDir + dataFile))
            {
                BinaryFormatter bf = new BinaryFormatter();
                if (bf != null)
                    tourList = (List<TourData>)bf.Deserialize(input);
            }
            pictureBoxTour.Image = ResourceImage.earth;
            TreeViewLoad();
        }
        private void toolStripMenuLoad_Click(object sender, EventArgs e)
        {
            openFileDialogLoadTour.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\"; // Открываем Рисунки пользователя
            DialogResult result = openFileDialogLoadTour.ShowDialog();
            if (result == DialogResult.OK)
            {
                try // безопасная попытка 
                {
                    FileInfo file = new FileInfo(appDir + dataFile);
                    long size = file.Length;
                    if (size != 0)
                    {
                        DialogResult res = MessageBox.Show("Для загрузки требуется удалить существующий каталог туров. Вы согласны?", "Remover old tour list?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (res == DialogResult.OK)
                            LoadFileTRZ();
                    }
                    else LoadFileTRZ();
                }
                catch (Exception ex) // если попытка загрузки не удалась 
                {
                    // выводим сообщение с причиной ошибки 
                    MessageBox.Show("Не удалось загрузить файл: " + ex.Message);
                }
            }
        }
        #endregion Загрузка БД туристических маршрутов

        #region Сохранение БД туристических маршрутов в файл для передачи
        private void toolStripMenuSave_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(appDir + dataFile);
            long size = file.Length;
            if (size != 0)
            {
                DataSerialize();
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\"; // Открываем Рисунки пользователя
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string directoryPath = appDir + @"\AppData";
                    ZipFile.CreateFromDirectory(directoryPath, saveFileDialog.FileName + @".trz");
                }
            }
        }
        #endregion Сохранение БД туристических маршрутов в файл для передачи

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var binWriter = new System.IO.BinaryWriter(File.Create("help.chm"));
            if (Authorized.ForeColor == Color.Green)
                binWriter.Write(ResourceImage.User_guide_Admin);
            else binWriter.Write(ResourceImage.User_guide);
            binWriter.Close();
            string commandText = appDir + @"\\help.chm";

            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }

        private void TabMenu_Click(object sender, EventArgs e)
        {
            if (Authorized.ForeColor == Color.Green)
            {
                if (TabMenu.SelectedTab.Text == "Map" || TabMenu.SelectedTab.Text == "Order")
                {
                    buttonTextClear.Enabled = false;
                    ButtonTextFormat.Enabled = false;
                    ButtonFontColor.Enabled = false;
                    ButtonFontCenter.Enabled = false;
                    ButtonFontRight.Enabled = false;
                    ButtonFontLeft.Enabled = false;
                    ButtonSaveDescription.Enabled = false;
                    if(TabMenu.SelectedTab.Text == "Order")
                        toolStripButton4.Enabled = false;
                    else toolStripButton4.Enabled = true;
                }
                else if(TabMenu.SelectedTab.Text == "Description")
                {
                    buttonTextClear.Enabled = true;
                    ButtonTextFormat.Enabled = true;
                    ButtonFontColor.Enabled = true;
                    ButtonFontCenter.Enabled = true;
                    ButtonFontRight.Enabled = true;
                    ButtonFontLeft.Enabled = true;
                    ButtonSaveDescription.Enabled = true;
                    toolStripButton4.Enabled = true;
                }
            }
        }

        private void richTextBox_Leave(object sender, EventArgs e)
        {
            if (Authorized.ForeColor == Color.Green)
            {
                if (selectPrevTour.descriptionTourFile == null)                             // Если еще ни разу не сохранялся текстовый файл, то создаем соответствующие папки
                {
                    if (!Directory.Exists(appDir + dataFileText))                           //Папка существует
                        System.IO.Directory.CreateDirectory(appDir + dataFileText);
                    if (!Directory.Exists(appDir + dataFileText + selectPrevFullPath))       //Папка существует
                        System.IO.Directory.CreateDirectory(appDir + dataFileText + selectPrevFullPath);
                    richTextBox.SaveFile(appDir + dataFileText + selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf");
                    tourList[selectPrevi].descriptionTourFile = selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf";
                }
                else
                    richTextBox.SaveFile(appDir + dataFileText + selectPrevFullPath + "\\" + selectPrevTour.cost + ".rtf"); // Сохраняем текстовый файл
            }
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            PasswordForm PassForm = new PasswordForm();                                     // Диалог вызова ввода пароля
            PassForm.Name = "Change password";
            PassForm.labelChangePas.Visible = true;
            PassForm.ShowInTaskbar = false;
            PassForm.StartPosition = FormStartPosition.CenterParent;
            PassForm.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }
    }
}
