// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.Database.Designer {
    
    
    public partial class UserEditorDialog {
        
        private Gtk.VBox vboxContent;
        
        private Gtk.HBox hboxName;
        
        private Gtk.Label label7;
        
        private Gtk.Entry entryName;
        
        private Gtk.Button buttonCancel;
        
        private Gtk.Button buttonOk;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.Database.Designer.UserEditorDialog
            this.Name = "MonoDevelop.Database.Designer.UserEditorDialog";
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.HasSeparator = false;
            // Internal child MonoDevelop.Database.Designer.UserEditorDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog1_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog1_VBox.Gtk.Box+BoxChild
            this.vboxContent = new Gtk.VBox();
            this.vboxContent.Name = "vboxContent";
            this.vboxContent.Spacing = 6;
            this.vboxContent.BorderWidth = ((uint)(6));
            // Container child vboxContent.Gtk.Box+BoxChild
            this.hboxName = new Gtk.HBox();
            this.hboxName.Name = "hboxName";
            this.hboxName.Spacing = 6;
            // Container child hboxName.Gtk.Box+BoxChild
            this.label7 = new Gtk.Label();
            this.label7.Name = "label7";
            this.label7.Xalign = 0F;
            this.label7.LabelProp = Mono.Unix.Catalog.GetString("Name");
            this.hboxName.Add(this.label7);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hboxName[this.label7]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child hboxName.Gtk.Box+BoxChild
            this.entryName = new Gtk.Entry();
            this.entryName.CanFocus = true;
            this.entryName.Name = "entryName";
            this.entryName.IsEditable = true;
            this.entryName.InvisibleChar = '●';
            this.hboxName.Add(this.entryName);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hboxName[this.entryName]));
            w3.Position = 1;
            this.vboxContent.Add(this.hboxName);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vboxContent[this.hboxName]));
            w4.Position = 0;
            w4.Expand = false;
            w4.Fill = false;
            w1.Add(this.vboxContent);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(w1[this.vboxContent]));
            w5.Position = 0;
            // Internal child MonoDevelop.Database.Designer.UserEditorDialog.ActionArea
            Gtk.HButtonBox w6 = this.ActionArea;
            w6.Name = "dialog1_ActionArea";
            w6.Spacing = 6;
            w6.BorderWidth = ((uint)(5));
            w6.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonCancel = new Gtk.Button();
            this.buttonCancel.CanDefault = true;
            this.buttonCancel.CanFocus = true;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseStock = true;
            this.buttonCancel.UseUnderline = true;
            this.buttonCancel.Label = "gtk-cancel";
            this.AddActionWidget(this.buttonCancel, -6);
            Gtk.ButtonBox.ButtonBoxChild w7 = ((Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonCancel]));
            w7.Expand = false;
            w7.Fill = false;
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonOk = new Gtk.Button();
            this.buttonOk.Sensitive = false;
            this.buttonOk.CanDefault = true;
            this.buttonOk.CanFocus = true;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseStock = true;
            this.buttonOk.UseUnderline = true;
            this.buttonOk.Label = "gtk-ok";
            this.AddActionWidget(this.buttonOk, -5);
            Gtk.ButtonBox.ButtonBoxChild w8 = ((Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonOk]));
            w8.Position = 1;
            w8.Expand = false;
            w8.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 640;
            this.DefaultHeight = 480;
            this.Show();
            this.entryName.Changed += new System.EventHandler(this.NameChanged);
            this.buttonCancel.Clicked += new System.EventHandler(this.CancelClicked);
            this.buttonOk.Clicked += new System.EventHandler(this.OkClicked);
        }
    }
}
