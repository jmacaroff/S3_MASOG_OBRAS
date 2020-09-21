using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
}
