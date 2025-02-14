using System.Text.Json;
using DT191G_Moment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DT191G_Moment2.Controllers;
public class HomeController : Controller
{
    private const string SessionKey = "NotesList";

    [Route("/")]
    [Route("/index")]
    //Hämta alla anteckningar
    public IActionResult Index()
    {
        var notesJson = HttpContext.Session.GetString(SessionKey);
        var notes = string.IsNullOrEmpty(notesJson)
            ? new List<NoteModel>()
            : JsonSerializer.Deserialize<List<NoteModel>>(notesJson) ?? new List<NoteModel>();

        return View(notes);
    }
}