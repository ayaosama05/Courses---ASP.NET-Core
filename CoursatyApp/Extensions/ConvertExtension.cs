﻿using CoursatyApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursatyApp.Extensions
{
    public static class ConvertExtension
    {
        public static List<SelectListItem> ConvertToSelectList<T>(this IEnumerable<T> collection,int selectedVal) where T:IPrimaryProperties
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedVal)
                    }).ToList();
        }
    }
}
