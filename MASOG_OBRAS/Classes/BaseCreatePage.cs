﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASOG_OBRAS.Classes
{

    public abstract class BaseCreatePage : PageModel
    {
        [BindProperty]
        public string MessageError { get; set; }

        public async Task<IActionResult> AddNewValue(DbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
                return Redirect("./Index");
            }
            catch (DbUpdateException e)
            {
                SqlException sqlException = e.GetBaseException() as SqlException;
                Console.WriteLine(sqlException.ErrorCode);
                Console.WriteLine(sqlException.Number);
                Console.WriteLine(sqlException.Message);
                MessageError = sqlException.Message;
                return Page();
            }
        }
    }
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
