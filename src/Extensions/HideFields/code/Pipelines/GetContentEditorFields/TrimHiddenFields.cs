using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell;
using Sitecore.Shell.Applications.ContentEditor.Pipelines.GetContentEditorFields;
using Sitecore.Shell.Applications.ContentManager;
using static Helixbase.Extensions.HideFields.Enums;

namespace Helixbase.Extensions.HideFields.Pipelines
{
    public class TrimHiddenFields
    {
        private GetContentEditorFieldsArgs args;

        public void Process(GetContentEditorFieldsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (args.Sections == null || args.Sections.Count == 0)
                return;

            this.args = args;

            for (int sectionIndex = 0; sectionIndex < args.Sections.Count; sectionIndex++)
            {
                Editor.Section section = args.Sections[sectionIndex];
                TrimFields(section);

                if (Settings.HidingEmptySections && section.Fields.Count == 0)
                {
                    args.Sections.RemoveAt(sectionIndex);
                    sectionIndex--;
                }
            }
        }

        private void TrimFields(Editor.Section section)
        {
            for (int fieldIndex = 0; fieldIndex < section.Fields.Count; fieldIndex++)
            {
                if (!FieldIsVisible(section.Fields[fieldIndex]))
                {
                    section.Fields.RemoveAt(fieldIndex);
                    fieldIndex--;
                }
            }
        }

        private bool FieldIsVisible(Editor.Field field)
        {
            try
            {
                Item templateFieldItem = GetTemplateFieldItem(field, args.Item.Database);

                if (templateFieldItem == null)
                    return true;

                ShowField showField = ShowField.Always;

                try
                {
                    showField = ParseShowField(new ID(templateFieldItem.Fields[Items.ShowField].Value));
                }
                catch { }

                if (showField.Equals(ShowField.Always)) { return true; }
                if (showField.Equals(ShowField.Never)) { return false; }
                if (showField.Equals(ShowField.WhenShowingStandardFields) && (!UserOptions.ContentEditor.ShowSystemFields)) { return false; }
            }
            catch { }

            return true;
        }


        public static ShowField ParseShowField(ID OptionID)
        {
            if (OptionID.Equals(Items.ShowFieldOptions.Always)) { return ShowField.Always; }
            if (OptionID.Equals(Items.ShowFieldOptions.Never)) { return ShowField.Never; }
            if (OptionID.Equals(Items.ShowFieldOptions.WhenShowingStandardFields)) { return ShowField.WhenShowingStandardFields; }
            return ShowField.Always;
        }

        private Item GetTemplateFieldItem(Editor.Field field, Database database)
        {
            try
            {
                return database.GetItem(field.TemplateField.ID);
            }
            catch
            {
                return null;
            }
        }
    }
}
