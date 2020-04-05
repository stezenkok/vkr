namespace TimetableEditor
{
    partial class Week
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemReferences = new System.Windows.Forms.MenuItem();
            this.menuItemTeachers = new System.Windows.Forms.MenuItem();
            this.menuItemRooms = new System.Windows.Forms.MenuItem();
            this.menuItemGroups = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemReferences});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemExit});
            this.menuItemFile.Text = "Файл";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 0;
            this.menuItemExit.Text = "Выход";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemReferences
            // 
            this.menuItemReferences.Index = 1;
            this.menuItemReferences.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemTeachers,
            this.menuItemRooms,
            this.menuItemGroups});
            this.menuItemReferences.Text = "Справочники";
            // 
            // menuItemTeachers
            // 
            this.menuItemTeachers.Index = 0;
            this.menuItemTeachers.Text = "Преподаватели";
            this.menuItemTeachers.Click += new System.EventHandler(this.menuItemTeachers_Click);
            // 
            // menuItemRooms
            // 
            this.menuItemRooms.Index = 1;
            this.menuItemRooms.Text = "Аудитории";
            this.menuItemRooms.Click += new System.EventHandler(this.menuItemRooms_Click);
            // 
            // menuItemGroups
            // 
            this.menuItemGroups.Index = 2;
            this.menuItemGroups.Text = "Учебные группы";
            this.menuItemGroups.Click += new System.EventHandler(this.menuItemGroups_Click);
            // 
            // Week
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Menu = this.mainMenu;
            this.Name = "Week";
            this.Text = "Неделя";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemReferences;
        private System.Windows.Forms.MenuItem menuItemTeachers;
        private System.Windows.Forms.MenuItem menuItemRooms;
        private System.Windows.Forms.MenuItem menuItemGroups;
    }
}