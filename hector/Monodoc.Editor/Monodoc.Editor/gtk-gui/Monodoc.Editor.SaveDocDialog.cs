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
    
    
    public partial class SaveDocDialog {
        
        private Gtk.FileChooserWidget saveFileDialog;
        
        private Gtk.Button buttonCancel;
        
        private Gtk.Button buttonOk;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget Monodoc.Editor.SaveDocDialog
            this.Name = "Monodoc.Editor.SaveDocDialog";
            this.Title = Mono.Unix.Catalog.GetString("Save as...");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.HasSeparator = false;
            // Internal child Monodoc.Editor.SaveDocDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog1_VBox";
            w1.BorderWidth = ((uint)(2));
            // Container child dialog1_VBox.Gtk.Box+BoxChild
            this.saveFileDialog = new Gtk.FileChooserWidget(((Gtk.FileChooserAction)(1)));
            this.saveFileDialog.Name = "saveFileDialog";
            w1.Add(this.saveFileDialog);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(w1[this.saveFileDialog]));
            w2.Position = 0;
            // Internal child Monodoc.Editor.SaveDocDialog.ActionArea
            Gtk.HButtonBox w3 = this.ActionArea;
            w3.Name = "dialog1_ActionArea";
            w3.Spacing = 6;
            w3.BorderWidth = ((uint)(5));
            w3.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonCancel = new Gtk.Button();
            this.buttonCancel.CanDefault = true;
            this.buttonCancel.CanFocus = true;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseStock = true;
            this.buttonCancel.UseUnderline = true;
            this.buttonCancel.Label = "gtk-cancel";
            this.AddActionWidget(this.buttonCancel, -6);
            Gtk.ButtonBox.ButtonBoxChild w4 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.buttonCancel]));
            w4.Expand = false;
            w4.Fill = false;
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonOk = new Gtk.Button();
            this.buttonOk.CanDefault = true;
            this.buttonOk.CanFocus = true;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseStock = true;
            this.buttonOk.UseUnderline = true;
            this.buttonOk.Label = "gtk-save";
            w3.Add(this.buttonOk);
            Gtk.ButtonBox.ButtonBoxChild w5 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.buttonOk]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 703;
            this.DefaultHeight = 178;
            this.Show();
            this.saveFileDialog.FileActivated += new System.EventHandler(this.OnSaveFileDialogFileActivated);
            this.buttonCancel.Clicked += new System.EventHandler(this.OnButtonCancelClicked);
            this.buttonOk.Clicked += new System.EventHandler(this.OnButtonOkClicked);
        }
    }
}
