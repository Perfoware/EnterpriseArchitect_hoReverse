﻿using System.Collections.Generic;

namespace EaServices.Auto.Analyze
{
    /// <summary>
    /// GUI Helper
    /// </summary>
    public static class GuiHelper
    {
        /// <summary>
        /// Aggregates the filter list an aggregated filter or to null (no filter)
        /// https://documentation.devexpress.com/WindowsForms/2567/Controls-and-Libraries/Data-Grid/Filter-and-Search/Filtering-in-Code
        /// </summary>
        /// <param name="lFilters"></param>
        /// <returns></returns>
        public static string AggregateFilter(List<string> lFilters)
        {

            string delimiter = "";
            string filters = "";
            foreach (var filter in lFilters)
            {
                filters = $@"{filters} {delimiter} {filter}";
                delimiter = " And ";
            }
            if (delimiter == "") return null;
            return filters;
        }

        /// <summary>
        /// Add a filter from the passed string to the filter list
        /// Arbitrary string     = '(........)'    ==> (........)
        /// LIKE                 =  '*AAA*'        ==> nameFilterValue LIKE '*AAA*'
        /// NOT LIKE             =  'NOT *AAAA*'   ==> nameFilterValue NOT LIKE '*AAA*'
        /// </summary>
        /// <param name="lFilters"></param>
        /// <param name="firstWildCard"></param>
        /// <param name="nameFilerValue"></param>
        /// <param name="filterValue"></param>
        public static void AddSubFilter(List<string> lFilters, string firstWildCard, string nameFilerValue, string filterValue)
        {
            string compareValue = filterValue.Trim();
            if (compareValue != "" && ! compareValue.ToLower().StartsWith("<filter"))
            {
                if (compareValue.StartsWith("(") || compareValue.Split(' ').Length > 2)
                {
                    lFilters.Add($" {filterValue} ");
                    return;
                }
                if (compareValue.ToLower().StartsWith("not "))
                {
                    string s = compareValue.Split(' ')[1];
                    lFilters.Add($"{nameFilerValue} NOT LIKE '{firstWildCard}{s}%'");
                    return;
                }
                else
                {
                    lFilters.Add($"{nameFilerValue} LIKE '{firstWildCard}{compareValue}%'");
                    return;
                }
            }
        }
       

    }
}
