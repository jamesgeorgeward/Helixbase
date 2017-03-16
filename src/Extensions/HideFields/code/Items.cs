using Sitecore.Data;

namespace Helixbase.Extensions.HideFields
{
    public class Items
    {
        public static ID ShowField = new ID("{390110DA-7573-4114-BF78-59E783516472}");
        public static ID ShowFieldOptionsFolder = new ID("{C166C472-1E1D-4C61-8A2C-2B50BFCB6FBC}");

        public static class ShowFieldOptions
        {
            public static ID Always = new ID("{A1E5A53C-A5B0-49DA-9C74-50FA51834381}");
            public static ID Never = new ID("{AC8479F5-D45A-4250-8917-ED361C90579B}");
            public static ID WhenShowingStandardFields = new ID("{A3E1F2BF-4469-4C1A-9CF4-42230A4C0C81}");
        }
    }
}
