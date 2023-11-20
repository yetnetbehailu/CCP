using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CCP.Helpers
{
    public class EnumHelper
    {
        // radio list/buttons of enum properties
        public List<SelectListItem> ConvertEnumToRadioList<TEnum>() where TEnum : Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum));
            List<SelectListItem> radioList = new List<SelectListItem>();

            foreach (var enumValue in enumValues)
            {
                string str = enumValue.ToString();
                var selectListItem = new SelectListItem
                {
                    Text = str,
                    Value = str,
                };
                radioList.Add(selectListItem);
            }

            return radioList;
        }
    }
}

