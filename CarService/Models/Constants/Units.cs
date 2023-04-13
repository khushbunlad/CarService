using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.Models.Constants
{
    public class Units
    {
        public static readonly string NotApplicable = "N/A";
        public static readonly string unit = "unit";
        public static readonly string kg = "kg";
        public static readonly string gram = "gram";
        public static readonly string liter = "liter";
        public static readonly string ml = "ml";
        public static readonly string meter = "meter";

        public static List<SelectListItem> UnitList { get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Text = Units.NotApplicable,Value=Units.NotApplicable},
                    new SelectListItem{Text = Units.unit,Value=Units.unit},
                    new SelectListItem{Text = Units.kg,Value=Units.kg},
                    new SelectListItem{Text = Units.gram,Value=Units.gram},
                    new SelectListItem{Text = Units.liter,Value=Units.liter},
                    new SelectListItem{Text = Units.ml,Value=Units.ml},
                    new SelectListItem{Text = Units.meter,Value=Units.meter},
                };
            } }

    }
}
