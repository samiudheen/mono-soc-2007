// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Monodoc.Editor {
    
    
    public partial class EditorWindow {
        
        private Gtk.Action File;
        
        private Gtk.Action Quit;
        
        private Gtk.Action Open;
        
        private Gtk.Action Save;
        
        private Gtk.Action SaveAs;
        
        private Gtk.VBox vbox1;
        
        private Gtk.MenuBar menubar1;
        
        private Gtk.Toolbar toolbar1;
        
        private Gtk.VBox edit_container;
        
        private Gtk.Statusbar status_bar;
        
        private Gtk.ProgressBar progress_bar;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget Monodoc.Editor.EditorWindow
            Gtk.UIManager w1 = new Gtk.UIManager();
            Gtk.ActionGroup w2 = new Gtk.ActionGroup("Default");
            this.File = new Gtk.Action("File", Mono.Unix.Catalog.GetString("_File"), null, null);
            this.File.ShortLabel = Mono.Unix.Catalog.GetString("_File");
            w2.Add(this.File, null);
            this.Quit = new Gtk.Action("Quit", Mono.Unix.Catalog.GetString("_Quit"), null, "gtk-quit");
            this.Quit.ShortLabel = Mono.Unix.Catalog.GetString("_Quit");
            w2.Add(this.Quit, null);
            this.Open = new Gtk.Action("Open", Mono.Unix.Catalog.GetString("_Open"), null, "gtk-open");
            this.Open.ShortLabel = Mono.Unix.Catalog.GetString("_Open");
            w2.Add(this.Open, null);
            this.Save = new Gtk.Action("Save", Mono.Unix.Catalog.GetString("_Save"), null, "gtk-save");
            this.Save.Sensitive = false;
            this.Save.ShortLabel = Mono.Unix.Catalog.GetString("_Save");
            w2.Add(this.Save, null);
            this.SaveAs = new Gtk.Action("SaveAs", Mono.Unix.Catalog.GetString("Save _As"), null, "gtk-save-as");
            this.SaveAs.Sensitive = false;
            this.SaveAs.ShortLabel = Mono.Unix.Catalog.GetString("Save _As");
            w2.Add(this.SaveAs, null);
            w1.InsertActionGroup(w2, 0);
            this.AddAccelGroup(w1.AccelGroup);
            this.Name = "Monodoc.Editor.EditorWindow";
            this.Title = Mono.Unix.Catalog.GetString("Monodoc Documentation Editor");
            this.Icon = Gdk.Pixbuf.LoadFromResource("monodoc.png");
            this.WindowPosition = ((Gtk.WindowPosition)(1));
            // Container child Monodoc.Editor.EditorWindow.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 3;
            // Container child vbox1.Gtk.Box+BoxChild
            w1.AddUiFromString("<ui><menubar name='menubar1'><menu action='File'><menuitem action='Open'/><separator/><menuitem action='Save'/><menuitem action='SaveAs'/><separator/><menuitem action='Quit'/></menu></menubar></ui>");
            this.menubar1 = ((Gtk.MenuBar)(w1.GetWidget("/menubar1")));
            this.menubar1.Name = "menubar1";
            this.vbox1.Add(this.menubar1);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            w1.AddUiFromString("<ui><toolbar name='toolbar1'><toolitem action='Open'/><toolitem action='Save'/></toolbar></ui>");
            this.toolbar1 = ((Gtk.Toolbar)(w1.GetWidget("/toolbar1")));
            this.toolbar1.Name = "toolbar1";
            this.toolbar1.ShowArrow = false;
            this.toolbar1.ToolbarStyle = ((Gtk.ToolbarStyle)(0));
            this.vbox1.Add(this.toolbar1);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox1[this.toolbar1]));
            w4.Position = 1;
            w4.Expand = false;
            w4.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.edit_container = new Gtk.VBox();
            this.edit_container.Name = "edit_container";
            this.edit_container.Spacing = 6;
            this.vbox1.Add(this.edit_container);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox1[this.edit_container]));
            w5.Position = 2;
            // Container child vbox1.Gtk.Box+BoxChild
            this.status_bar = new Gtk.Statusbar();
            this.status_bar.Name = "status_bar";
            this.status_bar.Spacing = 6;
            this.status_bar.HasResizeGrip = false;
            // Container child status_bar.Gtk.Box+BoxChild
            this.progress_bar = new Gtk.ProgressBar();
            this.progress_bar.Name = "progress_bar";
            this.status_bar.Add(this.progress_bar);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.status_bar[this.progress_bar]));
            w6.Position = 1;
            this.vbox1.Add(this.status_bar);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox1[this.status_bar]));
            w7.Position = 3;
            w7.Expand = false;
            w7.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 740;
            this.DefaultHeight = 537;
            this.progress_bar.Hide();
            this.Show();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
            this.Quit.Activated += new System.EventHandler(this.OnQuitActivated);
            this.Open.Activated += new System.EventHandler(this.OnOpenActivated);
            this.Save.Activated += new System.EventHandler(this.OnSaveActivated);
            this.SaveAs.Activated += new System.EventHandler(this.OnSaveAsActivated);
        }
    }
}
