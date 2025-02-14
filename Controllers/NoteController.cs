using DT191G_Moment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace DT191G_Moment2.Controllers;
public class NoteController : Controller
{
    private const string SessionKey = "NotesList";

    //Metod för kategorilistan
    private static List<SelectListItem> GetCategoryList() => [
        new SelectListItem { Value = "Arbete", Text = "Arbete" },
        new SelectListItem { Value = "Personligt", Text = "Personligt" },
        new SelectListItem { Value = "Studier", Text = "Studier" }
    ];

    //Formulär för att skapa ny anteckning
    [HttpGet]
    [Route("/newnote")]
    public IActionResult NewNote()
    {
        ViewBag.Categories = GetCategoryList();
        return View();
    }

    //Skapa ny anteckning
    [HttpPost]
    [Route("/newnote")]
    public IActionResult NewNote(NoteModel newNote)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = GetCategoryList();
            return View(newNote); //Om valideringen misslyckas, returnera vyn med felmeddelanden
        }

        var notesJson = HttpContext.Session.GetString(SessionKey);
        var notes = !string.IsNullOrEmpty(notesJson) ? JsonSerializer.Deserialize<List<NoteModel>>(notesJson) : [];

        //Sätt ett unikt ID för anteckningen
        newNote.Id = notes!.Count != 0 ? notes.Max(n => n.Id) + 1 : 1;

        notes.Add(newNote);

        //Spara uppdaterad lista i session
        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(notes));

        return RedirectToAction("Index", "Home");
    }

    //Formulär för att redigera en anteckning
    [HttpGet]
    [Route("/editnote/{id}")]
    public IActionResult EditNote(int id)
    {
        var notesJson = HttpContext.Session.GetString(SessionKey);
        var notes = !string.IsNullOrEmpty(notesJson) ? JsonSerializer.Deserialize<List<NoteModel>>(notesJson) : [];

        var note = notes!.Find(n => n.Id == id);

        ViewBag.Categories = GetCategoryList();

        return View(note);
    }

    //Uppdatera anteckning
    [HttpPost]
    [Route("/editnote/{id}")]
    public IActionResult EditNote(int id, NoteModel updatedNote)
    {
        var notesJson = HttpContext.Session.GetString(SessionKey);
        var notes = !string.IsNullOrEmpty(notesJson) ? JsonSerializer.Deserialize<List<NoteModel>>(notesJson) : [];

        var note = notes!.Find(n => n.Id == id);
        if (note != null)
        {
            note.Title = updatedNote.Title;
            note.Notes = updatedNote.Notes;
            note.Category = updatedNote.Category;
            note.IsImportant = updatedNote.IsImportant;

            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(notes));
        }

        return RedirectToAction("Index", "Home");
    }

    //Ta bort en anteckning
    [HttpPost]
    [Route("/deletenote")]
    public IActionResult DeleteNote(int id)
    {
        var notesJson = HttpContext.Session.GetString(SessionKey);
        var notes = !string.IsNullOrEmpty(notesJson) ? JsonSerializer.Deserialize<List<NoteModel>>(notesJson) : [];

        var noteToRemove = notes!.Find(n => n.Id == id);
        if (noteToRemove != null)
        {
            notes.Remove(noteToRemove);
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(notes));

            TempData["DeleteSuccess"] = $"Anteckningen '{noteToRemove.Title}' har raderats!";
        }

        return RedirectToAction("Index", "Home");
    }
}