using halak.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace halak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HorgaszokController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult getAll()
        {
            using (var cx = new HalakContext())
            {
                try
                {
                    return Ok(cx.Horgaszoks.ToList());
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("ById")]
        public IActionResult GetById(int id)
        {
            using (var cx = new HalakContext())
            {
                try
                {
                    if (cx.Horgaszoks.Select(x => x.Id).Contains(id))
                    {
                        var result = cx.Horgaszoks.FirstOrDefault(x => x.Id == id);
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Uj")]
        public async Task<IActionResult> PostUj(Horgaszok horgasz)
        {
            using (var cx =new HalakContext())
            {
                try
                {
                   await cx.Horgaszoks.AddAsync(horgasz);
                   await cx.SaveChangesAsync();
                    return Ok("Sikeres rögzítés");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("Modositas")]
        public IActionResult Put(Horgaszok horgasz)
        {
            using (var cx = new HalakContext())
            {
                try
                {
                    if (cx.Horgaszoks.Select(x => x.Id).Contains(horgasz.Id))
                    {
                        cx.Horgaszoks.Update(horgasz);
                        cx.SaveChanges();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("ById/{azon}")]
        public IActionResult Delete(int id, string azon)
        {
            if (azon==Program.azonosito)
            {
                using (var cx = new HalakContext())
                {
                    try
                    {
                        if (cx.Horgaszoks.Select(x => x.Id).Contains(id))
                        {
                            Horgaszok torlendo = new Horgaszok() { Id = id };
                            cx.Horgaszoks.Remove(torlendo);
                            cx.SaveChanges();
                            return Ok("Sikeres törlés");
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception ex)
                    {

                        return BadRequest(ex.Message);
                    }
                }
            }
            else
            {
                return Unauthorized("Nem engedélyezett művelet.");
            }
           
        }


    }
}
