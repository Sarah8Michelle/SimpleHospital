using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHospital.Data;
using SimpleHospital.Models;

namespace SimpleHospital.Controllers
{
    public class FileUploadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _host;

        public FileUploadsController (ApplicationDbContext context, IHostingEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: FileUploads
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileUploads.ToListAsync());
        }

        // GET: FileUploads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileUploads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, FileUpload fileUpload)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_host.WebRootPath, "file", fileName);

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fileUpload.FileName = fileName;
            }

            _context.FileUploads.Add(fileUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult GetPdf(string fileName)
        {
            string filePath = "~/file/" + fileName;
            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(filePath, "application/pdf");
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads.FindAsync(id);

            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: FileUploads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FileName")] FileUpload fileUpload)
        {
            if (id != fileUpload.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(fileUpload.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fileUpload);
        }

        // GET: FileUploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads.FirstOrDefaultAsync(m => m.Id == id);

            if(fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: FileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var fileUpload = await _context.FileUploads.FindAsync(id);

                _context.FileUploads.Remove(fileUpload);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool FileExists(int id)
        {
            return _context.FileUploads.Any(e => e.Id == id);
        }
    }
}

//MIME Types:
/*
 * audio/aac
 * audio/midi
 * audio/ogg
 * audio/x-wav
 * audio/webm
 * audio/3gpp
 * audio/3gpp2
 * application/x-abiword
 * application/octet-stream
 * application/vnd.amazon.ebook
 * application/octet-stream
 * application/x-bzip
 * application/x-bzip2
 * application/x-csh
 * application/msword
 * application/epub+zip
 * application/java-archive
 * application/javascript
 * application/json
 * application/vnd.apple.installer+xml
 * application/vnd.oasis.opendocument.presentation
 * application/vnd.oasis.opendocument.spreadsheet
 * application/vnd.oasis.opendocument.text
 * application/ogg
 * application/pdf
 * application/vnd.ms-powerpoint
 * application/x-rar-compressed
 * application/rtf
 * application/x-sh
 * application/x-shockwave-flash
 * application/x-tar
 * application/vnd.visio
 * application/xhtml+xml
 * application/vnd.ms-excel
 * application/xml
 * application/vnd.mozilla.xul+xml
 * application/zip
 * application/x-7z-compressed
 * video/x-msvideo
 * video/mpeg
 * video/ogg
 * video/webm
 * video/3gpp
 * video/3gpp2
 * text/css
 * text/csv
 * text/html
 * text/calendar
 * image/gif
 * image/x-icon
 * image/jpeg
 * image/svg+xml
 * image/tiff
 * image/webp
 * font/ttf
 * font/woff
 * font/woff2
 */
